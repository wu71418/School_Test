using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Random rd = new Random();
        int[] v=new int[4];  
        PictureBox[] bombs = new PictureBox[4];  
        
        int[] hit = new int[4]; 
        int score, miss;
        string name;
        bool fgGameOver;
        bool fgGamestop=false;

        void initial()
        {
            bombs[0] = pictureBox1;
            bombs[1] = pictureBox3;
            bombs[2] = pictureBox4;
            bombs[3] = pictureBox5;

            pictureBox2.Image = Image.FromFile("pic\\bg.jfif"); 

            for(int i=0;i<bombs.Length;i++)  
                pictureBox2.Controls.Add(bombs[i]);

            pictureBox2.Controls.Add(label1);  
            pictureBox2.Controls.Add(label2);  
            pictureBox2.Controls.Add(label3);
            pictureBox2.Controls.Add(label4);

            for (int i = 0; i < hit.Length; i++)
                hit[i] = 1;

            score = 0;
            miss = 0;
            fgGameOver = true;
        }

        void generate()
        {
            for (int i = 0; i < v.Length; i++)
            {
                if (hit[i] == 1 || hit[i] == 2)
                {
                    v[i] = rd.Next(33, 127);  
                    bombs[i].Top = rd.Next(50, 100);
                    bombs[i].Image = Image.FromFile("pic\\star" + v[i].ToString() + ".png");  
                    hit[i] = 0;
                }
            }
        }

        void isGameOver()
        {
            if(score<0 || miss>=5)
            {
                fgGameOver = true;
                timer1.Enabled = false;
                timer2.Enabled = false;
                timer3.Enabled = false;
                timer4.Enabled = false;
                name=Interaction.InputBox("儲存成績", "輸入姓名", "無名氏");
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           bombs[0].Top += 10;  

            if(bombs[0].Top>ClientSize.Height - 150)
            {
                miss++;
                hit[0] = 2;
                label4.Text = miss.ToString();
                isGameOver();
                if(fgGameOver==false)
                    generate();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            bombs[1].Top += 10; 

            if (bombs[1].Top > ClientSize.Height - 150)
            {
                miss++;
                hit[1] = 2;
                label4.Text = miss.ToString();
                isGameOver();
                if (fgGameOver == false)
                    generate();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initial();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int key;
            bool getHit;

            key = e.KeyChar;

            if (fgGameOver == true)
                return;

            getHit = false;
            for (int i = 0; i < v.Length; i++)
            {
                if (key == v[i])  
                {
                    score += 5;
                    hit[i] = 1;
                    getHit = true;
                    label3.Text = score.ToString();
                    generate();
                    break;
                }
            }

            if(getHit == false)
            {
                score -= 5;
                label3.Text = score.ToString();
            }

            isGameOver();
        }

        private void 新遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            initial();
            generate();

            timer1.Enabled = true;
            timer2.Enabled = true;
            timer3.Enabled = true;
            timer4.Enabled = true;


            fgGameOver = false;
        }

        private void 暫停遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fgGamestop==false)
            {
            fgGamestop = true;
            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            timer4.Enabled = false;
                暫停遊戲ToolStripMenuItem.Text = "繼續遊戲";
            }
            else
            {
                fgGamestop = false;
                timer1.Enabled = true;
                timer2.Enabled = true;
                timer3.Enabled = true;
                timer4.Enabled = true;
                暫停遊戲ToolStripMenuItem.Text = "暫停遊戲";
            }
            
        }

        private void 結束遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result;

            timer1.Enabled = false;
            timer2.Enabled = false;
            timer3.Enabled = false;
            timer4.Enabled = false;

            result = MessageBox.Show("是否要結束遊戲?", "結束遊戲", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if(result==DialogResult.Yes)
            {
                Close();
            }
            else
            {
                timer1.Enabled = true;
                timer2.Enabled = true;
                timer3.Enabled = true;
                timer4.Enabled = true;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            bombs[2].Top += 10; 

            if (bombs[2].Top > ClientSize.Height - 150)
            {
                miss++;
                hit[2] = 2;
                label4.Text = miss.ToString();
                isGameOver();
                if (fgGameOver == false)
                    generate();
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {

            bombs[3].Top += 10;  

            if (bombs[3].Top > ClientSize.Height - 150)
            {
                miss++;
                hit[3] = 2;
                label4.Text = miss.ToString();
                isGameOver();
                if (fgGameOver == false)
                    generate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strfilename = @"score.txt";
            string[] strwrite = { name, score.ToString() };
            string[] strRead;

            try
            {
                strRead = File.ReadAllLines(strfilename);
                foreach (var item in strRead)
                textBox1.AppendText(item+ "\r\n");
            }
            catch(Exception ex)
            {
                MessageBox.Show("讀取資料失敗");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strfilename = @"score.txt";
            string[] strwrite = {name,score.ToString() };

            try
            {
                File.AppendAllLines(strfilename, strwrite);
                MessageBox.Show("完成");
            }
            catch(Exception ex)
            {
                MessageBox.Show("寫入錯誤");
            }
        }
    }
}
