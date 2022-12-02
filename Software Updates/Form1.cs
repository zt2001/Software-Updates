using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Software_Updates
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Init();
        }
        /// <summary>
        /// 已有的文件的列表
        /// </summary>
        List<string> current = default;
        /// <summary>
        /// 更新包的列表
        /// </summary>
        List<string> Updates = default;
        /// <summary>
        ///匹配到的已有文件
        /// </summary>
        List<string> To_be_replaced = new List<string>();
        /// <summary>
        /// 匹配到的更新文件
        /// </summary>
        List<string> To_be_updated = new List<string>();
        /// <summary>
        /// 控件恢复初始
        /// </summary>
        public void Init()
        {
            label1.Text = "选择更新包文件夹路径";
            label4.Text = "0";
            button2.Enabled = false;
            button3.Enabled = false;
        }

        /// <summary>
        /// 窗口初始化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            current = GetFilePathList(System.Windows.Forms.Application.StartupPath);
        }
        /// <summary>
        /// 选择按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "选择更新包所在目录";  //提示的文字
            if (folder.ShowDialog() == DialogResult.OK)
            {
                label1.Text = Intercept_file_name(folder.SelectedPath);
                Updates = GetFilePathList(folder.SelectedPath);
                File_parsing();
            }
            else
            {
                label1.Text = "选择更新包文件夹路径";
                label2.BackColor = Color.LightSlateGray;
                label2.Text = "待解析";

            }
        }
        /// <summary>
        /// 替换更新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            Upadd();
            Init();
        }
        /// <summary>
        /// 新增更新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var L in current)
            {
                // richTextBox1.AppendText(L+ "\n");
                //richTextBox1.AppendText(Intercept_file_name(L));
            }
        }
        /// <summary>
        /// 替换更新
        /// </summary>
        public void Upadd()
        {
            for (int i = 0; To_be_updated.Count>i;i++)
            {
                SaveFile(To_be_updated[i], To_be_replaced[i].Substring(0, To_be_replaced[i].LastIndexOf("\\") + 1));
                //MessageBox.Show(To_be_replaced[i].Substring(0, To_be_replaced[i].LastIndexOf("\\") + 1));
                //    public string Intercept_file_name(string modify)
                //{
                //    return modify.Substring(modify.LastIndexOf("\\") + 1);
                //}));
            }
            MessageBox.Show("更新成功");
        }
        /// <summary>
        /// 获取目录下所有文件名
        /// </summary>
        /// <param name="rootPath">根路径</param>
        /// <returns></returns>
        private static List<string> GetFilePathList(string rootPath)
        {
            //文件集合
            List<string> flieList = new List<string>();
            //文件夹集合
            List<string> dirList = new List<string>();
            dirList.Add(rootPath);

            //foreach会提示：不能循环已经被修改的集合
            for (int i = 0; i < dirList.Count; i++)
            {
                if (Directory.Exists(dirList[i]))
                {
                    if (dirList[i].IndexOf("ProFile") == -1 && dirList[i].IndexOf("ErrLog") == -1 && dirList[i].IndexOf("Log") == -1 && 
                        dirList[i].IndexOf("VisonConfig") == -1 && dirList[i].IndexOf("ProFile") == -1 && dirList[i].IndexOf("CPK") == -1 &&
                         dirList[i].IndexOf("Calibration") == -1 && dirList[i].IndexOf("Accounts") == -1)
                    {
                        //添加文件夹下的文件夹
                        dirList.AddRange(Directory.GetDirectories(dirList[i]));
                        //添加文件下文件
                        flieList.AddRange(Directory.GetFiles(dirList[i]));
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return flieList;
        }
        /// <summary>
        /// 字符串截取，取文件名
        /// </summary>
        /// <param name="modify"></param>
        /// <returns></returns>
        public string Intercept_file_name(string modify)
        {
            string[] arr = modify.Split('\\');

            return arr[arr.Length-1];
        }
        /// <summary>
        /// 字符串截取，取文件所在目录
        /// </summary>
        /// <param name="modify"></param>
        /// <returns></returns>
        public string Intercept_file_name2(string modify)
        {
            string[] arr = modify.Split('\\');
            MessageBox.Show(arr[arr.Length - 2]);
            return arr[arr.Length - 2]
                ;
        }
        /// <summary>
        /// 匹配文件
        /// </summary>
        public void File_parsing()
        {
            To_be_replaced.Clear();
            To_be_updated.Clear();
            foreach (var i in current)
            {
                foreach(var j in Updates)
                {
                    if(Intercept_file_name(i) == Intercept_file_name(j))
                    {
                        To_be_replaced.Add(i);
                        To_be_updated.Add(j);
                    }
                    else
                    {
                        continue;
                    }
                    
                }
            }
            if(To_be_replaced.Count > 0 && To_be_updated.Count > 0)
            {
                label2.BackColor = Color.LightGreen;
                label2.Text = "解析成功";
                button2.Enabled = true;
                //button3.Enabled = true;
                label4.Text = To_be_updated.Count.ToString();
            }
            else
            {
                label2.BackColor = Color.Crimson;
                label2.Text = "解析失败";
                button2.Enabled = false;
                //button3.Enabled = false;
                label4.Text = "0";
            }
        }
        /// <summary>
        /// 复制文件到路径
        /// </summary>
        /// <param name="filePathName">文件</param>
        /// <param name="toFilesPath">路径</param>
        public void SaveFile(string filePathName, string toFilesPath)
        {
            FileInfo file = new FileInfo(filePathName);
            string newFileName = file.Name;
            file.CopyTo(toFilesPath + @"\" + newFileName, true);
        }


    }
}
