using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Media;
using System.Windows.Forms;

namespace Matching_Pairs
{
    public partial class Form1 : Form
    {
        Label first, second = null;
        Random random = new Random();
        List<char> templateList = new List<char>(16);
        SortedList<int, Color> CanvasColour = new SortedList<int, Color>()
        {
            [0] = Color.Orange,
            [1] = Color.SteelBlue,
            [2] = Color.LightSeaGreen
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {            
            btnPlay.Focus();           
        }

        private void rdoAttack_CheckedChanged(object sender, EventArgs e)
        {
            panelDiffuculty.Enabled = true;
        }

        private void rdoScore_CheckedChanged(object sender, EventArgs e)
        {
            panelDiffuculty.Enabled = false;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            panelMenu.Visible = false;
            ReadyGame();
            if (rdoAttack.Checked)
            {
                SetTimerSeconds();
            }
        }        

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblImage_Click(object sender, EventArgs e)
        {
            Label clicked = sender as Label;
            //Label clicked = (Label)sender;

            if (timer1.Enabled)
                return;
            if (clicked == null || clicked.ForeColor == Color.Black)
                return;

            if (first == null)
            {
                first = clicked;
                first.ForeColor = Color.Black;
                return;
            }
            else
            {
                second = clicked;
                second.ForeColor = Color.Black;
            }
            
            if (Uncovered() == 0) //if you won
            {
                gameTimer.Stop();
                isActive = false;
                
                string msg = "Your time was " + lblTimer.Text + "\n Do you want to play again?";
                DialogResult pressed = MessageBox.Show(msg, "You have won", MessageBoxButtons.YesNo);
                if (pressed == DialogResult.Yes)
                {                    
                    ResetStopWatch();
                    ReadyGame();
                    if (rdoScore.Checked) UpdateScoreBox();
                    return;
                }
                else
                {
                    lblTimer.Visible = false;
                    panelMenu.Visible = true;
                    if (rdoScore.Checked)
                        UpdateScoreBox();
                }
            }

            if (first.Text == second.Text)
            {
                first = second = null;
            }
            else
            {
                timer1.Start();
            }                
        }

        private void UpdateScoreBox()
        {
            scoreList.Add(scoreSeconds);
            lstScore.Items.Clear();
            if (scoreList.Capacity > 1)
                scoreList.Sort();
            foreach (var item in scoreList)
            {
                lstScore.Items.Add(item.ToString());
            }
            //for (int i = 1; i < scoreList.Count; i++)
            //{
            //    lstScore.Items.Add(scoreList[i].ToString());
            //}
        }

        private void ResetStopWatch()
        {
            minutes = seconds = 0;
            PrintStopwatch();
        }

        private void ReadyGame()
        {
            first = second = null;
            FillTemplateList();
            SetImages();
            gameTimer.Start();
            isActive = true;
            lblTimer.Visible = true;
        }

        private int Uncovered()
        {
            int count = tblCanvas.Controls.Count;
            foreach (Label img in tblCanvas.Controls)
            {
                if (img.ForeColor == Color.Black)
                {
                    count--;
                }
            }
            return count;
        }       

        private void FillTemplateList()
        {
            if (templateList.Count > 0)
                templateList = new List<char>(16);
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            string temp = chars;
            int n = tblCanvas.Controls.Count;
            int randomNumber;
            for (int i = 0; i < n/2; i++)
            {
                randomNumber = random.Next(temp.Length);
                templateList.Add(temp[randomNumber]);
                templateList.Add(temp[randomNumber]);
                temp = temp.Remove(randomNumber, 1);
            }            
        }          

        private void SetImages()
        {
            Label image;
            int randomNumber;
            int randomColour = random.Next(CanvasColour.Count);
            tblCanvas.BackColor = CanvasColour[randomColour];
            lblTimer.BackColor = CanvasColour[randomColour];
            foreach (Control control in tblCanvas.Controls)
            {
                image = control as Label;
                if (image.Name == "lblTimer") continue;
                randomNumber = random.Next(templateList.Count);                
                image.Text = templateList[randomNumber].ToString();
                templateList.RemoveAt(randomNumber);
                image.ForeColor = image.BackColor;
            }            
        }        

        //tick covers uneqal images. It takes just one tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            first.ForeColor = first.BackColor;
            second.ForeColor = second.BackColor;
            first = null;
            second = null;
        }        
    }
    //DONE: winner message, reset afetr win, after win menu, random images, random colours, timer, game modes
    //TODO: Sounds, Delegates (Mosh, Tim), Lambda (Mosh), Dice Game, Events (Mosh, Tim)
}
