using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Collections;

namespace NASA_Daily_Image_Feed
{
    public partial class Form1 : Form
    {
        public string url = "https://www.nasa.gov/rss/dyn/lg_image_of_the_day.rss";
        private List<NASAPost> nps = new List<NASAPost>();
        private int currentPost = 0;
        public Form1()
        {
            InitializeComponent();

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int nextpost = currentPost + 1;

            if (nextpost > nps.Count - 1)
            {
                MessageBox.Show("No more in list");
            }
            else
            {

                lblImage.Text = nps[nextpost].Title.ToString();
                rtbInfo.Text = nps[nextpost].Description.ToString();
                string imageToLoad = nps[nextpost].Enclosure.ToString();
                pbx.Load(imageToLoad);
                lblDate.Text = nps[nextpost].PublishedDate.ToString();
                currentPost = nextpost;
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            int nextpost = currentPost - 1;
            if (nextpost < 0)
            {
                MessageBox.Show("Start of List Reached");
            }
            else
            {

                lblImage.Text = nps[nextpost].Title.ToString();
                rtbInfo.Text = nps[nextpost].Description.ToString();
                string imageToLoad = nps[nextpost].Enclosure.ToString();
                pbx.Load(imageToLoad);
                lblDate.Text = nps[nextpost].PublishedDate.ToString();
                currentPost = nextpost;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load_Feed();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

            load_Feed();
        }

        private void load_Feed()
        {
            try
            {
                var posts = new NASARSSReader().ReadFeed(url);

                if (posts != null)
                {
                    nps = new List<NASAPost>();

                    foreach (NASAPost np in posts)
                    {
                        nps.Add(np);
                    }

                    lblImage.Text = nps[0].Title.ToString();
                    rtbInfo.Text = nps[0].Description.ToString();
                    string imageToLoad = nps[0].Enclosure.ToString();
                    pbx.Load(imageToLoad);
                    lblDate.Text = nps[0].PublishedDate.ToString();
                    currentPost = 0;
                }
            }
            catch (Exception ex)
            {
                rtbInfo.AppendText(ex.ToString());
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pbx_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(nps[currentPost].Link.ToString());
        }
    }
}

