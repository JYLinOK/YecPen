using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ecPen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

        }

        private int DongY =  300;   // 获取或设置  y  方向平移距离，+ 下 - 上  830 dao -100

        private int DongX = 1000;   // 获取或设置  x  方向平移距离，+ 右 - 左  1000 dao this.pictureBox1.Width > 1000

        private int pWidth;       // 获取或设置  pictureBox1_Paint 的宽度

        private int xZhouQiXiangShu = 200;  // 获取或设置 x 方向周期放大像素

        private int yZhouQiXiangShu = 100;  // 获取或设置 y 方向周期放大像素


        private float xXiangShuBi;

        private bool huaTu = false;
        private bool geMiao;
        private bool chuShi = false;

        private int huaTuX;
        private int huaTuY;
        private int X1;
        private int X2;
        private int Y1;
        private int Y2;
        private int shuDu_HuaBi;
        private int shuDu_HuaBiBan;

        Pen HuiTu_mypen = new Pen(Color.Black, 2);  //定义绘图画笔
        Pen HanShu_mypen = new Pen(Color.Red, 2);  //定义函数画笔


        Bitmap bmp;  // 定义 pictureBox2 中的图片


        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text =
                " timer1.Interval: " + timer1.Interval + Environment.NewLine
             + " pictureBox1.Size: " + pictureBox1.Size + Environment.NewLine
             + " this.pictureBox1.Width: " + this.pictureBox1.Width + Environment.NewLine
             + " this.pictureBox1.Location: " + this.pictureBox1.Location + Environment.NewLine
             ;

            //*************************************************************
            //各个标签信息

            label2.Text = "画笔速度： " + trackBar1.Value    ;
            label4.Text = "画板速度： " + trackBar2.Value;
            label5.Text = "水平伸缩： " + trackBar4.Value;
            label6.Text = "竖直伸缩： " + trackBar3.Value;
            label9.Text = "竖直移动： " + trackBar6.Value;
            label10.Text = "水平移动： " + trackBar7.Value;
            label11.Text = "画笔磅值： " + trackBar8.Value;
            label3.Text = "画笔磅值： " + trackBar5.Value;
            label7.Text = "画笔颜色： " + colorDialog2.Color;
            label8.Text = "背景图片： " + openFileDialog1.FileName;

            //*************************************************************

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 10;
            timer1.Start();

            timer2.Interval = 10;
            timer2.Start();

            timer3.Interval = 1;
            timer3.Start();

            timer4.Interval = 10;
            timer4.Start();

            pWidth = this.pictureBox1.Width;

            bmp = new Bitmap(pictureBox2.Image, pictureBox2.Width, pictureBox2.Height);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //*************************************************
            //函数图绘制
            pictureBox1.Width = pWidth;

            pWidth += shuDu_HuaBi;

            int xx = this.pictureBox1.Location.X;
            
            xx -= shuDu_HuaBiBan;

            this.pictureBox1.Location = new Point (xx,3);

            //*************************************************
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //***************************************************************************************************
            // Graphics 设置  函数绘图
            //参数设置
            double x1 = DongX;
            double y1 = 0;
            double x2;
            double y2;

            for ( x2 = 1; x2 < this.pictureBox1.Width; x2++)         
            {
                y2 = Math.Sin(x2 / 180 * Math.PI) * yZhouQiXiangShu;   // * yZhouQiXiangShu 表示y方向的 放大拉伸 比例

                xXiangShuBi = 360 / xZhouQiXiangShu;   

                //  2PI 角度 (360度) / 周期 T = 频率；

                g.DrawLine(HanShu_mypen, (float)x1, (float)y1 + DongY, (float)(x2 / xXiangShuBi) + DongX, (float)y2 + DongY);
                x1 = x2 / xXiangShuBi + DongX;
                y1 = y2;
            }

            //***************************************************************************************************
            
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
         //********************************************************************************
         // 实时画图板画图
            if(  true )
            {
                if (geMiao == true)
                {
                    X1 = huaTuX;
                    Y1 = huaTuY;

                    geMiao = false;
                }
                else
                {
                    X2 = huaTuX;
                    Y2 = huaTuY;

                    geMiao = true;
                }
               
               
                if( huaTu == true )
                {
                    this.pictureBox2.CreateGraphics().DrawLine( HuiTu_mypen, X1, Y1, X2, Y2);
                    
                    Graphics g = Graphics.FromImage(bmp);
                    g.DrawLine(HuiTu_mypen, X1, Y1, X2, Y2);
                }
            }

            //********************************************************************************

        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            huaTu = true;
            chuShi = true;
            // 设定初始位置
            huaTuX = e.X;
            huaTuY = e.Y;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            huaTu = false;
            chuShi = false;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            chuShi = false;
            huaTuX = e.X;
            huaTuY = e.Y;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if(colorDialog2.ShowDialog() == DialogResult.OK )
            {
                HuiTu_mypen.Color = this.colorDialog2.Color;
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            //**************************************************
            //参数设置
            shuDu_HuaBi = trackBar1.Value;
            shuDu_HuaBiBan = trackBar2.Value;
            xZhouQiXiangShu = trackBar4.Value;
            yZhouQiXiangShu = trackBar3.Value;
            DongY = trackBar6.Value ;
            DongX = trackBar7.Value;
            trackBar7.Maximum = this.pictureBox1.Width;
            HanShu_mypen.Width = trackBar8.Value;
            HuiTu_mypen.Width = trackBar5.Value;

            //**************************************************

        }

        private void button14_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer2.Stop();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if( colorDialog1.ShowDialog() == DialogResult.OK )
            {
                HanShu_mypen.Color = this.colorDialog1.Color;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //************************************
            //回收项目
            button14.Visible = false;
            button15.Visible = false;
            button3.Visible = false;
            button2.Visible = false;
            button13.Visible = false;

            trackBar1.Visible = false;
            trackBar2.Visible = false;
            trackBar3.Visible = false;
            trackBar4.Visible = false;
            trackBar7.Visible = false;
            trackBar6.Visible = false;
            trackBar8.Visible = false;

            label2.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;

            //************************************

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //************************************
            //展示项目
            button14.Visible = true;
            button15.Visible = true;
            button3.Visible = true;
            button2.Visible = true;
            button13.Visible = true;

            trackBar1.Visible = true;
            trackBar2.Visible = true;
            trackBar3.Visible = true;
            trackBar4.Visible = true;
            trackBar7.Visible = true;
            trackBar6.Visible = true;
            trackBar8.Visible = true;

            label2.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;

            //************************************
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if ( colorDialog3.ShowDialog() == DialogResult.OK )
            {
                pictureBox2.BackColor = this.colorDialog3.Color;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            timer2.Dispose();

           DongY = 300;   // 获取或设置  y  方向平移距离，+ 下 - 上  830 dao -100
           DongX = 1000;   // 获取或设置  x  方向平移距离，+ 右 - 左  1000 dao this.pictureBox1.Width > 1000   
           xZhouQiXiangShu = 200;  // 获取或设置 x 方向周期放大像素
           yZhouQiXiangShu = 100;
           huaTu = false;
           chuShi = false;
            //pictureBox1 复位
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Size = new Size(1458, 917);


        }

        private void button4_Click(object sender, EventArgs e)
        {
            string str =
                "   ecPen © JY.Lin" + Environment.NewLine +
                "   简易笔 © 林进威" + Environment.NewLine +
                "   版权所有，盗版必究" + Environment.NewLine +
                "   本软件秉持简单愉悦的学习精神设计" + Environment.NewLine +
                "   感谢老师的辛勤教导！" + Environment.NewLine +
                "   2017/12/28" + Environment.NewLine 
                ;
            MessageBox.Show( str);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bmp = new Bitmap(pictureBox2.Image, pictureBox2.Width, pictureBox2.Height);
            pictureBox2.Image = bmp;

            saveFileDialog1.Filter = "  .Jpeg| *.Jpeg| .Bmp|*.Bmp| .Gif|*.Gif ";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = openFileDialog1.FileName;
            saveFileDialog1.ShowDialog();
            this.DialogResult = DialogResult.OK;
            pictureBox2.Image.Save(saveFileDialog1.FileName);
           



            //if (saveFileDialog1.FileName != null && saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    pictureBox2.Image.Save(saveFileDialog1.FileName);
            //}
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox2.Image.Save(saveFileDialog1.FileName);
        
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "  *.|*.*|.Jpeg| *.Jpeg| .Bmp|*.Bmp| .Gif|*.Gif ";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.ImageLocation = openFileDialog1.FileName;
                
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string str =
                "   ecPen © JY.Lin" + Environment.NewLine +
                "   先选好背景图片再画图，选项卡变换后画图板内容将清空。" + Environment.NewLine +
                "   本软件原理是在图片上画图。" + Environment.NewLine +
                "   本软件为轻松愉悦使用型软件，画图时请注意保存。" + Environment.NewLine +
                "   默认背景图片为白纸背景图。" + Environment.NewLine +              
                "   2017/12/28" + Environment.NewLine
                ;
            MessageBox.Show(str);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            label7.Visible = false;
            label8.Visible = false;

            trackBar5.Visible = false;

            button10.Visible = false;
            button11.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            label3.Visible = true;
            label7.Visible = true;
            label8.Visible = true;

            trackBar5.Visible = true;

            button10.Visible = true;
            button11.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
        }

      



        private void pictureBox2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            saveFileDialog1.Filter = "  .Jpeg| *.Jpeg| .Bmp|*.Bmp| .Gif|*.Gif ";
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.FileName = openFileDialog1.FileName;
            saveFileDialog1.ShowDialog();
            this.DialogResult = DialogResult.OK;
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {

        }
    }
}
