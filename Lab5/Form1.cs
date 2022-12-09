using System;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /* Name: Rodrigo Nerys
         * Date: 12/06/2022
         * This is a program for lab#5 */

        //declare const by my name
        const string PROGRAMMER = "Rodrigo";
        //declare counter for login
        int count = 1;
        

        /* Name: GetRandom
         * Sent: int
         * Returned: int
         * Return random number*/
        private int GetRandom(int min, int max)
        {
            Random rand = new Random();
            int ranNum = rand.Next(min, max + 1);
            return ranNum;
        }

        /* Form Load function
         * Add my name, Hide all groups except login,
         * Put cursor un the code textbox, call random function */
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text += PROGRAMMER;
            grpChoose.Hide();
            grpText.Hide();
            grpStats.Hide();
            txtCode.Focus();
            const int MIN = 100000, MAX = 200000;
            int code = GetRandom(MIN, MAX);
            lblCode.Text = Convert.ToString(code);
        }
        /* Botton Login function
         * Compare authentication code
         * show the Choose group and disable the Login group
         * count attempts until 3 and close the program */
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string msg1 = " incorrect code(s) entered\nTry again - only 3 attempts allowed",
                   msg2 = " attempts to login\nAccount locked - Closing program";

            if (txtCode.Text == lblCode.Text)
            {
                grpChoose.Show();
                grpLogin.Enabled = false;                
            }
            else
            {
                count++;
                switch (count)
                {
                    case 1:
                        MessageBox.Show(count + msg1, PROGRAMMER);
                        txtCode.SelectAll();
                        break;
                    case 2:
                        MessageBox.Show(count + msg1, PROGRAMMER);
                        txtCode.SelectAll();
                        break;
                    case 3:
                        MessageBox.Show(count + msg2, PROGRAMMER);
                        Close();
                        break;
                }
            }
        }
        /* Reset Text group function
         * send none, return none */
        private void ResetTextGrp()
        {
            txtString1.Text = "";
            txtString2.Text = "";
            chkSwap.Checked = false;
            lblResults.ResetText();
            txtString1.Focus();
            AcceptButton = btnJoin;
            CancelButton = btnReset;
        }
        /* Reset Stats group function
         * send none, return none */
        private void ResetStatsGrp()
        {
            nudHowMany.Value = 10;
            lstNumbers.Items.Clear();
            lblSum.ResetText();
            lblMean.ResetText();
            lblOdd.ResetText();
            nudHowMany.Focus();
            AcceptButton = btnGenerate;
            CancelButton = btnClear;
        }
        /* setup options function
         * show and hide base on selection */
        private void SetupOption()
        {
            if (radText.Checked)
            {
                grpText.Show();
                grpStats.Hide();
                ResetTextGrp();
            }
            else
            {
                grpStats.Show();
                grpText.Hide();
                ResetStatsGrp();
            }
        }
        //radText check changed function
        private void radText_CheckedChanged(object sender, EventArgs e)
        {
            ResetTextGrp();
        }
        //radStats check changed function
        private void radStats_CheckedChanged(object sender, EventArgs e)
        {
            SetupOption();
        }
        // button Reset calls Reset Text group
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetTextGrp();
        }
        // button clear calls Reset Stats
        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetStatsGrp();
        }
        /* Swap function
         * Send: 2 strings
         * return: none
         * swap the RAM memory location of the two strings*/
        private void Swap(ref string str1, ref string str2)
        {
            string temp = str1;
            str1 = str2;
            str2 = temp;
        }
        /* CheckInput function
         * send: none
         * return: boolean
         * tests if textboxes have data */
        private bool CheckInput()
        {
            bool testInput = true;

            if (txtString1.Text == "" || txtString2.Text == "")
                testInput = false;

            return testInput;
        }
        /* check Swap changes
         * redisplay data
         * show message on the label*/
        private void chkSwap_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckInput() && chkSwap.Checked)
            {
                string str1 = txtString1.Text, str2 = txtString2.Text, msg = "Strings have been swapped!";
                Swap(ref str1, ref str2);
                txtString1.Text = str1;
                txtString2.Text = str2;
                lblResults.Text = msg;
            }            
        }
        /* button Join function
         * join both strings and display in label */
        private void btnJoin_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                string msg1 = "First String = " + txtString1.Text, msg2 = "\nSecond String = " + txtString2.Text,
                    msg3 = "\nJoined = " + txtString1.Text + "-->" + txtString2.Text;
                lblResults.Text = msg1 + msg2 + msg3;

            }
        }
        /* button Analyse function
         * returns the length of characters in each string */
        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (CheckInput()) 
            {
                string msg1 = "First String = " + txtString1.Text, msg2 = "\nSecond String = " + txtString2.Text,
                    msg3 = "\n Characters = ";
                lblResults.Text = msg1 + msg3 + txtString1.TextLength + msg2 + msg3 + txtString2.TextLength;

            }
        }
        /* button generate function
         * create a randon number and add it to a listbox
         * call functions addlist and countodd */
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            lstNumbers.Items.Clear();
            Random rand = new Random(733);
            const int MIN = 1000, MAX = 5000;
            int i, num, sum, howMany = (int)nudHowMany.Value;

            for (i = 0; i < howMany; i++)
            {
                
                num = rand.Next(MIN, MAX+1);
                lstNumbers.Items.Add(num);                
            }
            sum = AddList();
            lblSum.Text = sum.ToString("n0");
            double mean = Convert.ToDouble(sum) / howMany;
            lblMean.Text = mean.ToString("n");

            lblOdd.Text = Convert.ToString(OddCount());
        }
        /* addList function
         * send: none
         * return: int
         * return the sum of the numbers within the listbox */
        private int AddList()
        {
            int i = 0, sum = 0, howMany = (int)nudHowMany.Value;
            while (i < howMany)
            {
                sum += Convert.ToInt32(lstNumbers.Items[i]);                
                i++;                
            }

            return sum;
        }
        /* countodd function
         * send: none
         * return: int
         * return the numbers of odd items */
        private int CountOdd()
        {
            int i = 0, count = 0, value, howMany = (int)(nudHowMany.Value);
            const int DIV = 2, MOD = 1;
            do
            {
                value = Convert.ToInt32(lstNumbers.Items[i]);
                i++;
                if (value % DIV == MOD)
                    count ++;

            } while (i < howMany);

            return count;
        }
    }
}
