using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace JSON
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void button1_DragEnter(object sender, DragEventArgs e)
        {

        }

        // 제이슨 처리
        void ReadJson(string s)
        {
            JObject j;

            using (StreamReader f = File.OpenText(s))
            using (JsonTextReader r=new JsonTextReader(f))
            {
                j = (JObject)JToken.ReadFrom(r);
                //Debug.WriteLine(j.ToString()); 
            }

            Debug.WriteLine(j["name"].ToString());
            j["name"] = "lilly";
            Debug.WriteLine(j["name"].ToString());
            File.WriteAllText(s, j.ToString());

            using (StreamWriter f = File.CreateText(s))
            using (JsonTextWriter w = new JsonTextWriter(f))
            {
                j.WriteTo(w);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            // 드랍된 파일들 처리
            textBox1.Text = "";
            var nms = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var nm in nms)
            {
                //Debug.WriteLine("nm:" + nm);
                try
                {
                    ReadJson(nm);
                    textBox1.AppendText(nm + " :정상\r\n");
                }
                catch (Exception)
                {
                    //Debug.WriteLine("파일 에러:" + nm);
                    textBox1.AppendText(nm+" :에러\r\n");
                    //throw;
                }
            }
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            // 드랍 커서 효과
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
    }
}
