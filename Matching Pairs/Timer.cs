using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_Pairs
{
    public partial class Form1
    {
        const int easyTime = 60;
        const int mediumTime = 40;
        const int hardTime = 5;
        List<int> scoreList = new List<int>();
        int minutes, seconds, scoreSeconds;
        int timerSeconds;
        bool isActive;
        string msg = "Do you want to play again?";


        private void gameTimer_Tick(object sender, EventArgs e)
        {
            if (isActive)
            {
                if (rdoScore.Checked)
                {
                    scoreSeconds++;
                    seconds++;
                    if (seconds >= 60)
                    {
                        minutes++;
                        seconds = 0;
                    }
                }
                if (rdoAttack.Checked)
                {
                    timerSeconds--;
                    if (timerSeconds == -1)
                    {
                        SystemSounds.Exclamation.Play();
                        gameTimer.Stop();
                        isActive = false;
                        DialogResult pressed =
                            MessageBox.Show(msg, "Better luck next time", MessageBoxButtons.YesNo);
                        if (pressed == DialogResult.Yes)
                        {
                            SetTimerSeconds();
                            ReadyGame();
                        }
                        else
                        {
                            lblTimer.Visible = false;
                            panelMenu.Visible = true;
                        }
                    }
                }
            }
            PrintStopwatch();
        }        

        private void PrintStopwatch()
        {
            if (rdoScore.Checked)
            {
                lblTimer.Text = String.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                lblTimer.Text = String.Format("{0:00}:{1:00}", 0, timerSeconds);
            }
        }

        private void SetTimerSeconds()
        {
            timerSeconds =
                rdoEasy.Checked ? easyTime : rdoMedium.Checked ? mediumTime : hardTime;
        }
    }
}
