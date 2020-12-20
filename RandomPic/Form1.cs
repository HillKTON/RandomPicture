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
using System.Threading;

namespace RandomPic
{
    public partial class Form1 : Form
    {
        int[] stats = new int[2] { 0, 0 };
        string temp_url;
        bool proc = true;
        Image picture;
        static string url()
        {
            Random rand = new Random();
            string output = "";
            string seed = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM0123456789";
            string[] file_format = { "png", "jpg" };
            int len = 7;
            for (int i = 0; i < len; i++)
            {
                output += seed[rand.Next(seed.Length)];
            }
            return $"https://i.imgur.com/{output}.{file_format[rand.Next(2)]}";
            //return "https://i.imgur.com/FttQoPk.jpg";
        }

        private void SearchPicture()
        {
            HttpRequest request = new HttpRequest();
            var ui = new Action(() => UpdateGUI());
            while (proc)
            {
                temp_url = url();
                HttpResponse resp = request.Get(temp_url);
                if (resp.Address.AbsoluteUri != "https://i.imgur.com/removed.png")
                {
                    picture = Image.FromStream(resp.ToMemoryStream());
                    stats[0] += 1;
                    proc = false;
                }
                else
                {
                    stats[1] += 1;
                }
                this.Invoke(ui);
            }
        }

        private void UpdateGUI()
        {
            if (proc)
            {
                label1.Text = $"Успех к провалу: {stats[0]} | {stats[1]}";
            } 
            else
            {
                label1.Text = $"Успех к провалу: {stats[0]} | {stats[1]}";
                pictureBox1.Image = picture;
                textBox1.Text = temp_url;
                button1.Enabled = true;
                textBox1.Enabled = true;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            textBox1.Enabled = false;
            proc = true;
            for (int i = 0; i < 11; i++)
            {
                new Thread(new ThreadStart(SearchPicture)).Start();
            }
        }
    }
}
