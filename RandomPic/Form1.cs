using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Leaf.xNet;

namespace RandomPic
{
    public partial class Form1 : Form
    {
        static string url()
        {
            Random rand = new Random();
            string output = "";
            string seed = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789";
            int len = 7;
            for (int i = 0; i < len; i++)
            {
                output += seed[rand.Next(seed.Length)];
            }
            return $"https://i.imgur.com/{output}.png";
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool proc = true;
            HttpRequest request = new HttpRequest();
            do
            {
                string temp_url = url();
                HttpResponse resp = request.Get(temp_url);
                if (resp.Address.AbsoluteUri != "https://i.imgur.com/removed.png")
                {
                    pictureBox1.Image = Image.FromStream(resp.ToMemoryStream());
                    proc = false;
                } else
                {
                    MessageBox.Show(temp_url);
                }
            } 
            while (proc);
            
        }
    }
}
