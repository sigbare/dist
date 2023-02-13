using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace dist
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";         
        }
         private async void YanDis()
        {

            try
            {
               var api = new DiskHttpApi(GetToken());
               var rootFolderData = await api.MetaInfo.GetInfoAsync(new ResourceRequest
                {
                    Path = "/"

                });

               foreach (var item in rootFolderData.Embedded.Items)

                {
                    textBox2.Text += (($"{item.Name}\t{item.Type}\t{item.MimeType}")) + Environment.NewLine;

                }

            }
            catch 
            {
                MessageBox.Show("whats wrong");
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        private async void SaveToken()
        {
            string path = (Directory.GetCurrentDirectory() + @"\Feel\config.JSON");
            var token = svToken.Text;
            if (token != "")
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    byte[] buffer = Encoding.Default.GetBytes(svToken.Text);
                    await fs.WriteAsync(buffer, 0, buffer.Length);
                }
            else
                MessageBox.Show("Введите свой ID-токен");

        }
        private  void CreatConfig()
        {
            string path = (Directory.GetCurrentDirectory() + @"\Feel");
            if(!Directory.Exists(path))
            Directory.CreateDirectory(path); 
        }

        private string GetToken()
        {
            string path = (Directory.GetCurrentDirectory() + @"\Feel\config.JSON");
            if (File.Exists(path))
                using (FileStream fs = File.OpenRead(path))
                {
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    string token = Encoding.Default.GetString(buffer);
                    return token;
                }
            else 
            {
                MessageBox.Show("Save your ID-Token");
                return null;
            }
                    
                

                
        }
        private  void button3_Click(object sender, EventArgs e)
        {
            YanDis();
             
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreatConfig();
            SaveToken();
        }
    }
}
