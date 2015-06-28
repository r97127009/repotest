using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lottery_test
{
    public partial class Form1 : Form
    {
        Random rdm4star = new Random();//宣告四星彩用亂數
        int[] star;//宣告存放開獎數字用之陣列

        Random rdmWilly1 = new Random();//宣告威力彩亂數
        Random rdmWilly2 = new Random();//宣告威力彩第二區亂數
        int[] Willy1;//宣告威力彩陣列
        int[] tempWilly1;//宣告排序用暫存威力彩陣列

        Random rdm539 = new Random();//宣告今彩539亂數
        int[] i539;//宣告今彩539陣列
        int[] tempi539;//宣告暫存今彩539陣列

        bool star4=true;//四星彩選擇正彩or組彩之亂數

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            lblTime1.Text = String.Format("{0:F}",DateTime.Now);
            rbtn4star1.Checked = true;
            rbtn4star2.Checked = false;
           
        }

        private void rbtn4star1_CheckedChanged(object sender, EventArgs e)
        {
            star4 = true;//正彩
        }

        private void rbtn4star2_CheckedChanged(object sender, EventArgs e)
        {
            star4 = false;//組彩
        }

        private void btnRandom1_Click(object sender, EventArgs e)
        {
            try
            {
                if (star4)//正彩
                {
                    int i = 0;
                    string str1 = "";
                    star = new int[4];
                    star[0] = rdm4star.Next(0, 10);
                    if (star[0] == 0)
                    { str1 = "0 "; }
                        //避開第一個數字為0時無法將0寫入listbox的bug
                    else
                    { str1 = String.Format("{0} ", star[0]); }
                    for (i = 1; i <= 3; i++)
                    {
                        star[i] = rdm4star.Next(0, 10);
                        str1 = str1 + String.Format("{0} ", star[i]);
                    }
                    //lblNumber.Text = str1;
                    pictureBox1.Image = imageList1.Images[star[0]];
                    pictureBox2.Image = imageList1.Images[star[1]];
                    pictureBox3.Image = imageList1.Images[star[2]];
                    pictureBox4.Image = imageList1.Images[star[3]];
                    str1 = String.Format("第{0}注： ", lbox4star.Items.Count + 1) + str1;
                    lbox4star.Items.Add(str1);
                }
                else//組彩
                {
                    int i = 0;//i為需要產生之亂數次數
                    string str1 = "";//合成用字串，輸入至listbox上
                    star = new int[4];//存放四星彩的四個數字之陣列
                    for (i = 0; i <= 3; i++)
                    {
                        star[i] = rdm4star.Next(0, 10);
                        str1 = str1 + String.Format("{0} ", star[i]);
                        if ((star[0] == star[1]) && (star[0] == star[2]) && (star[0] == star[3])
                            && (star[1] == star[2]) && (star[1] == star[3]) && (star[2] == star[3]))
                        {
                            str1 = "";
                            continue;//若四個數字一樣則重新產生
                        }
                    }
                    //lblNumber.Text = str1;
                    pictureBox1.Image = imageList1.Images[star[0]];
                    pictureBox2.Image = imageList1.Images[star[1]];
                    pictureBox3.Image = imageList1.Images[star[2]];
                    pictureBox4.Image = imageList1.Images[star[3]];
                    str1 = String.Format("第{0}注： ", lbox4star.Items.Count + 1) + str1;
                    lbox4star.Items.Add(str1);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnClear1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drResult1;
                drResult1 = MessageBox.Show("是否確定清空紀錄?", "警告!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (drResult1 == DialogResult.Yes)
                { lbox4star.Items.Clear(); }
                //詢問是否清除過去紀錄，選擇yes則刪除
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnType1_Click(object sender, EventArgs e)
        {
            try
            {
                string stra = "", strb = "", strc = "", strd = "", stre = "", strf = "";
                if ((cbox1.Text.Length > 0) && (cbox2.Text.Length > 0)
                   && (cbox3.Text.Length > 0) && (cbox4.Text.Length > 0))
                {
                    stra = cbox1.SelectedItem.ToString();
                    strb = cbox2.SelectedItem.ToString();
                    strc = cbox3.SelectedItem.ToString();
                    strd = cbox4.SelectedItem.ToString();
                    if (star4)
                    {
                        pictureBox1.Image = imageList1.Images[Int32.Parse(stra)];
                        pictureBox2.Image = imageList1.Images[Int32.Parse(strb)];
                        pictureBox3.Image = imageList1.Images[Int32.Parse(strc)];
                        pictureBox4.Image = imageList1.Images[Int32.Parse(strd)];

                        stre = stra + " " + strb + " " + strc + " " + strd + " ";
                        strf = String.Format("第{0}注： ", lbox4star.Items.Count + 1)
                           + stre;
                        //lblNumber.Text = stre;
                        lbox4star.Items.Add(strf);
                    }
                    else
                    {
                        if ((stra == strb) && (stra == strc) && (stra == strd)
                            && (strb == strc) && (strb == strd) && (strc == strd))
                        {
                            MessageBox.Show("組彩不可投注四位相同數字!\n請重新選擇號碼。",
                                "警告!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            pictureBox1.Image = imageList1.Images[Int32.Parse(stra)];
                            pictureBox2.Image = imageList1.Images[Int32.Parse(strb)];
                            pictureBox3.Image = imageList1.Images[Int32.Parse(strc)];
                            pictureBox4.Image = imageList1.Images[Int32.Parse(strd)];

                            stre = stra + " " + strb + " " + strc + " " + strd + " ";
                            strf = String.Format("第{0}注： ", lbox4star.Items.Count + 1)
                               + stre;
                            //lblNumber.Text = stre;
                            lbox4star.Items.Add(strf);
                        }
                    }
                }
                else
                { MessageBox.Show("請選擇數值！","警告！",
                    MessageBoxButtons.OK,MessageBoxIcon.Warning); }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

       void RanWilly(int[] rWilly,int[] temprWilly,int min,int max,int num)
        {
            try
            {
                int temp = 0;//暫時取得比對用之亂數
                int n = 0;//陣列次數
                bool more;//檢查亂數是否重複

                rWilly = new int[6];
                do
                {
                    more = false;//預設為不重複
                    temp = rdmWilly1.Next(min, max + 1);

                    for (int i = 0; i <= 5; i++)
                    {
                        if (rWilly[i] == temp)
                        {
                            more = true;
                            break;
                        }
                    }
                    if (more == false)
                    {
                        rWilly[n] = temp;
                        n++;
                    }
                }
                while (n < 6);
                Array.Copy(rWilly, temprWilly, rWilly.GetLength(0));
                Array.Sort(temprWilly);

                pboxW1.Image = imageList2.Images[rWilly[0]];
                pboxW2.Image = imageList2.Images[rWilly[1]];
                pboxW3.Image = imageList2.Images[rWilly[2]];
                pboxW4.Image = imageList2.Images[rWilly[3]];
                pboxW5.Image = imageList2.Images[rWilly[4]];
                pboxW6.Image = imageList2.Images[rWilly[5]];
                pboxW7.Image = imageList2.Images[temprWilly[0]];
                pboxW8.Image = imageList2.Images[temprWilly[1]];
                pboxW9.Image = imageList2.Images[temprWilly[2]];
                pboxW10.Image = imageList2.Images[temprWilly[3]];
                pboxW11.Image = imageList2.Images[temprWilly[4]];
                pboxW12.Image = imageList2.Images[temprWilly[5]];

                int spe;//特別區
                spe = rdmWilly2.Next(1, 9);
                string strSpe = "";
                strSpe = String.Format("{0:D2}", spe);
                pboxWs1.Image = imageList3.Images[spe];

                string strMsg = "開出順序：", str1 = "", str2 = "";
                for (int i = 0; i <= 5; i++)
                { strMsg += String.Format("{0:D2} ", rWilly[i]); }
                strMsg += "\n大小順序：";
                for (int i = 0; i <= 5; i++)
                {
                    strMsg += String.Format("{0:D2} ", temprWilly[i]);
                    str1 += String.Format("{0:D2} ", temprWilly[i]);
                }
                strMsg += "\n特別區：";
                //lblNumber2.Text =strMsg+strSpe;
                str2 = String.Format("第{0}注：", lboxWilly.Items.Count + 1) + str1 + " " + strSpe;
                lboxWilly.Items.Add(str2);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnRandom2_Click(object sender, EventArgs e)
        {
            try
            {
                int min = 1, max = 38, num = 6;
                Willy1 = new int[6];
                tempWilly1 = new int[6];
                RanWilly(Willy1, tempWilly1, min, max, num);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnType2_Click(object sender, EventArgs e)
        {
            try
            {
                string str1, str2, str3, str4, str5, str6, strSpe, strMsg, strNum = "";
                if ((cboxW1.Text.Length > 0) && (cboxW2.Text.Length > 0)
                    && (cboxW3.Text.Length > 0) && (cboxW4.Text.Length > 0)
                    && (cboxW5.Text.Length > 0) && (cboxW6.Text.Length > 0)
                    && (cboxW7.Text.Length > 0))
                {
                    str1 = cboxW1.SelectedItem.ToString();
                    str2 = cboxW2.SelectedItem.ToString();
                    str3 = cboxW3.SelectedItem.ToString();
                    str4 = cboxW4.SelectedItem.ToString();
                    str5 = cboxW5.SelectedItem.ToString();
                    str6 = cboxW6.SelectedItem.ToString();
                    strSpe = cboxW7.SelectedItem.ToString();
                    if ((str1 == str2) || (str1 == str3) || (str1 == str4) || (str1 == str5) || (str1 == str6))
                    { doublecount(); }
                    else if ((str2 == str3) || (str2 == str4) || (str2 == str5) || (str2 == str6))
                    { doublecount(); }
                    else if ((str3 == str4) || (str3 == str5) || (str3 == str6))
                    { doublecount(); }
                    else if ((str4 == str5) || (str4 == str6))
                    { doublecount(); }
                    else if ((str5 == str6))
                    { doublecount(); }
                    else
                    {
                        string[] WNumber = new string[6];
                        string[] tempWnumber = new string[6];
                        WNumber[0] = str1;
                        WNumber[1] = str2;
                        WNumber[2] = str3;
                        WNumber[3] = str4;
                        WNumber[4] = str5;
                        WNumber[5] = str6;
                        Array.Copy(WNumber, tempWnumber, WNumber.GetLength(0));
                        Array.Sort(tempWnumber);
                        for (int i = 0; i <= 5; i++)
                        { strNum = strNum + String.Format(tempWnumber[i]) + " "; }
                        strMsg = String.Format("第{0}注：", lboxWilly.Items.Count + 1) + strNum + " " + strSpe;
                        lboxWilly.Items.Add(strMsg);

                        pboxW1.Image = imageList2.Images[Int32.Parse(str1)];
                        pboxW2.Image = imageList2.Images[Int32.Parse(str2)];
                        pboxW3.Image = imageList2.Images[Int32.Parse(str3)];
                        pboxW4.Image = imageList2.Images[Int32.Parse(str4)];
                        pboxW5.Image = imageList2.Images[Int32.Parse(str5)];
                        pboxW6.Image = imageList2.Images[Int32.Parse(str6)];
                        pboxW7.Image = imageList2.Images[Int32.Parse(tempWnumber[0])];
                        pboxW8.Image = imageList2.Images[Int32.Parse(tempWnumber[1])];
                        pboxW9.Image = imageList2.Images[Int32.Parse(tempWnumber[2])];
                        pboxW10.Image = imageList2.Images[Int32.Parse(tempWnumber[3])];
                        pboxW11.Image = imageList2.Images[Int32.Parse(tempWnumber[4])];
                        pboxW12.Image = imageList2.Images[Int32.Parse(tempWnumber[5])];
                        pboxWs1.Image = imageList3.Images[Int32.Parse(strSpe)];
                    }
                }
                else
                {
                    MessageBox.Show("請選擇數值！", "警告！",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        void doublecount()
        { MessageBox.Show("選取數字不可重複!請重新選擇"); }

        private void btnClear2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drResult1;
                drResult1 = MessageBox.Show("是否確定清空紀錄?", "警告!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (drResult1 == DialogResult.Yes)
                { lboxWilly.Items.Clear(); }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        void Ran539(int[] r539,int[] tempr539,int min,int max,int num)
        {
            try
            {
                int temp = 0;//暫存比較值
                int n = 0;//陣列次數
                bool more;//判定是否重複

                r539 = new int[5];
                tempr539 = new int[5];
                do
                {
                    more = false;
                    temp = rdm539.Next(min, max + 1);

                    for (int i = 0; i <= 4; i++)
                    {
                        if (r539[i] == temp)
                        {
                            more = true;
                            break;
                        }
                    }
                    if (more == false)
                    {
                        r539[n] = temp;
                        n++;
                    }
                }
                while (n < 5);
                Array.Copy(r539, tempr539, r539.GetLength(0));
                Array.Sort(tempr539);

                string strMsg = "開出順序：", str1 = "", str2 = "";
                for (int i = 0; i <= 4; i++)
                { strMsg += String.Format("{0:D2} ", r539[i]); }
                strMsg += "\n大小順序：";
                for (int i = 0; i <= 4; i++)
                {
                    strMsg += String.Format("{0:D2} ", tempr539[i]);
                    str1 += String.Format("{0:D2} ", tempr539[i]);
                }
                //lblNumber3.Text = strMsg;
                str2 = String.Format("第{0}注：", lbox539.Items.Count + 1) + str1;
                lbox539.Items.Add(str2);

                pboxI1.Image = imageList2.Images[r539[0]];
                pboxI2.Image = imageList2.Images[r539[1]];
                pboxI3.Image = imageList2.Images[r539[2]];
                pboxI4.Image = imageList2.Images[r539[3]];
                pboxI5.Image = imageList2.Images[r539[4]];
                pboxI6.Image = imageList2.Images[tempr539[0]];
                pboxI7.Image = imageList2.Images[tempr539[1]];
                pboxI8.Image = imageList2.Images[tempr539[2]];
                pboxI9.Image = imageList2.Images[tempr539[3]];
                pboxI10.Image = imageList2.Images[tempr539[4]];
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
        
        private void btnRandom3_Click(object sender, EventArgs e)
        {
            try
            {
                int min = 1, max = 39, num = 5;
                Ran539(i539, tempi539, min, max, num);
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnType3_Click(object sender, EventArgs e)
        {
            try
            {
                string str1, str2, str3, str4, str5, strMsg = "";
                if ((cbox5391.Text.Length > 0) && (cbox5392.Text.Length > 0)
                    && (cbox5393.Text.Length > 0) && (cbox5394.Text.Length > 0)
                    && (cbox5395.Text.Length > 0))
                {
                    str1 = cbox5391.SelectedItem.ToString();
                    str2 = cbox5392.SelectedItem.ToString();
                    str3 = cbox5393.SelectedItem.ToString();
                    str4 = cbox5394.SelectedItem.ToString();
                    str5 = cbox5395.SelectedItem.ToString();

                    if ((str1 == str2) || (str1 == str3) || (str1 == str4) || (str1 == str5))
                    { doublecount(); }
                    else if ((str2 == str3) || (str2 == str4) || (str2 == str5))
                    { doublecount(); }
                    else if ((str3 == str4) || (str3 == str5))
                    { doublecount(); }
                    else if ((str4 == str5))
                    { doublecount(); }
                    else
                    {
                        string[] number539 = new string[5];
                        string[] tempnum539 = new string[5];
                        number539[0] = str1;
                        number539[1] = str2;
                        number539[2] = str3;
                        number539[3] = str4;
                        number539[4] = str5;
                        Array.Copy(number539, tempnum539, number539.GetLength(0));
                        Array.Sort(tempnum539);
                        for (int i = 0; i <= 4; i++)
                        { strMsg += String.Format("{0} ", tempnum539[i]); }
                        strMsg = String.Format("第{0}注：", lbox539.Items.Count + 1) + strMsg;
                        lbox539.Items.Add(strMsg);
                        string str6, str7, str8, str9, str10;
                        str6 = tempnum539[0];
                        str7 = tempnum539[1];
                        str8 = tempnum539[2];
                        str9 = tempnum539[3];
                        str10 = tempnum539[4];

                        pboxI1.Image = imageList2.Images[Int32.Parse(str1)];
                        pboxI2.Image = imageList2.Images[Int32.Parse(str2)];
                        pboxI3.Image = imageList2.Images[Int32.Parse(str3)];
                        pboxI4.Image = imageList2.Images[Int32.Parse(str4)];
                        pboxI5.Image = imageList2.Images[Int32.Parse(str5)];
                        pboxI6.Image = imageList2.Images[Int32.Parse(str6)];
                        pboxI7.Image = imageList2.Images[Int32.Parse(str7)];
                        pboxI8.Image = imageList2.Images[Int32.Parse(str8)];
                        pboxI9.Image = imageList2.Images[Int32.Parse(str9)];
                        pboxI10.Image = imageList2.Images[Int32.Parse(str10)];
                    }
                }
                else
                {
                    MessageBox.Show("請選擇數值！", "警告！",
                      MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void btnClear3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult drResult1;
                drResult1 = MessageBox.Show("是否確定清空紀錄?", "警告!",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (drResult1 == DialogResult.Yes)
                { lbox539.Items.Clear(); }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime1.Text = String.Format("{0:F}", DateTime.Now);
            lblTime2.Text = String.Format("{0:F}", DateTime.Now);
            lblTime3.Text = String.Format("{0:F}", DateTime.Now);
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            DialogResult drResultQuit;
            drResultQuit = MessageBox.Show("是否確定離開程式?","警告!",
                MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if(drResultQuit==DialogResult.No)
            { e.Cancel = true; }//離開程式前詢問是否確定離開
        }

        private void llblTaiwan1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.taiwanlottery.com.tw/index_new.aspx");
        }

        private void llblTaiwan2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.taiwanlottery.com.tw/index_new.aspx");
        }

        private void llblTaiwan3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.taiwanlottery.com.tw/index_new.aspx");
        }

    }
}
