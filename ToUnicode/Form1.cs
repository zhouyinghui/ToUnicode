using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.CheckedListBox;

namespace ToUnicode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }



        public int get_char_code(char character)
        {
            UTF32Encoding encoding = new UTF32Encoding();
            byte[] bytes = encoding.GetBytes(character.ToString().ToCharArray());
            return BitConverter.ToInt32(bytes, 0);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            listBox1.MouseDoubleClick += ListBox1_MouseDoubleClick;
            button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            byte[] data = System.Text.Encoding.Unicode.GetBytes(textBox1.Text);

            char[] chars = Encoding.Unicode.GetChars(data);
            List<double> list = new List<double>();
            foreach (var item in chars)
            {
                double numericValue = get_char_code(item);
                list.Add(numericValue);
                string str = string.Format("{0}-{1}", item, numericValue);
                listBox1.Items.Add(str);
            }
            if (list.Count > 0)
            {
                toolStripStatusLabel1.Text = "生成成功";

            }
        }

        private void ListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            //MessageBox.Show(listBox.Text);
            if (listBox.Text.Contains("-"))
            {
                string[] array = listBox.Text.Split('-');
                Clipboard.SetDataObject(array[1]);
                toolStripStatusLabel1.Text = "复制成功[" + listBox.Text + "]";
            }
        }

        private void BtnOpenFileDIalog_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                TxtFolder.Text = fbd.SelectedPath;

                List<string> listImages = new List<string>();

                string[] files = Directory.GetFiles(fbd.SelectedPath, "*.png", SearchOption.AllDirectories);
                ImageList imageList = new ImageList();
                foreach (var item in files)
                {
                    listBox1.Items.Add(item);
                    imageList.Images.Add(Path.GetFileNameWithoutExtension(item), Bitmap.FromFile(item));
                }
                listView1.SmallImageList = imageList;
                listView1.LargeImageList = imageList;
                foreach (var item in imageList.Images.Keys)
                {
                    listView1.Items.Add(new ListViewItem()
                    {
                        ImageKey = item,
                        Name = item,
                    });
                }

            }
        }

        private void btnGetBmfc_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "BmFont配置文件(*.bmfc)|*.bmfc";
            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = ofd.FileName;
                txtBmfc.Text = filePath;
                string[] lines = File.ReadAllLines(filePath);

                List<Entity.Bmfc> bmfcs = new List<Entity.Bmfc>();

                Entity.Bmfc bmfc = null;
                foreach (var item in lines)
                {
                    if (item.StartsWith("#"))
                    {
                        bmfc = new Entity.Bmfc();
                        bmfc.GroupName = item;
                        bmfcs.Add(bmfc);
                    }
                    else if (string.IsNullOrEmpty(item))
                    {

                    }
                    else
                    {
                        if (bmfc != null)
                        {
                             
                            bmfc.Propertys.Add(item);
                        }
                    }
                }
                
            }
        }
    }
}
