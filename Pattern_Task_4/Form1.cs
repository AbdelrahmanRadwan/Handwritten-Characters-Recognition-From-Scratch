using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex32;
using System.Collections;

namespace PatternTask3
{
    public   partial class Form1 : Form
    {
            Classifier My_Classifier ;
        public Form1()
        {
            InitializeComponent();
            My_Classifier = new Classifier();
            this.StartPosition = FormStartPosition.CenterScreen;
            button1.Visible = true;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {


        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
      
        private void button1_Click_1(object sender, EventArgs e)
        {

            if (richTextBox5.Text == "" && richTextBox6.Text == "")
            {
                MessageBox.Show("you must enter num of training and testing set First!");
                return;
            }

            int NumOfTrainingSet = int.Parse(richTextBox5.Text);
            int NumOfTestingSet = int.Parse(richTextBox6.Text);
            button1.Enabled = false;
            richTextBox5.Enabled = false;
            richTextBox6.Enabled = false;
           

            My_Classifier.Load_Trainig_Data(NumOfTrainingSet);

            MessageBox.Show("Trainig Data are Loaded");


            My_Classifier.Load_Testing_Data(NumOfTestingSet);

            MessageBox.Show("Testing Data are Loaded");


            long Trainig_Time = 0;
            long Testing_Time = 0;

            long timeBefore = System.Environment.TickCount;
            My_Classifier.MU();
            long timeAfter = System.Environment.TickCount;

            Trainig_Time += (timeAfter - timeBefore);
           
            MessageBox.Show("Mue Done");

            timeBefore = System.Environment.TickCount;
            My_Classifier.Sigma();
            timeAfter = System.Environment.TickCount;

            Trainig_Time += (timeAfter - timeBefore);

            MessageBox.Show("Sigma Done");


            timeBefore = System.Environment.TickCount;
            My_Classifier.calculate_non_zero_dia();

            My_Classifier.Culculate_all_Det();

            My_Classifier.Culculate_all_inv();

            My_Classifier.Calculate_prior();
            timeAfter = System.Environment.TickCount;

            Trainig_Time += (timeAfter - timeBefore);

            MessageBox.Show("Invers and determent Done");



            MessageBox.Show("Start Testing");
            timeBefore = System.Environment.TickCount;
            My_Classifier.Testing();
            timeAfter = System.Environment.TickCount;
            MessageBox.Show("Testing Are Finshed");

            Testing_Time = (timeAfter - timeBefore);

            double acc = My_Classifier.Accuracy(NumOfTestingSet);

            richTextBox1.Text = acc.ToString() + " % ";

            richTextBox4.Text = (Trainig_Time / 1000).ToString() + " s ";

            richTextBox3.Text = (Testing_Time / 60000).ToString() + " M ";

            richTextBox2.Text = ((Testing_Time + Trainig_Time)/ 60000).ToString() + " M ";

            int[,] Conv_to_display = My_Classifier.confusionMatrix; //to display

           //expected result
          

            for (int i = 1; i < 11; i++)
            {
                Label L = new Label();
                L.Text = (i - 1).ToString();
                L.BackColor = Color.Blue;
                tableLayoutPanel2.Controls.Add(L, i, 0);


            }
            for (int i = 1; i < 11; i++)
            {
                Label L = new Label();
                L.BackColor = Color.Blue;
                L.Text = (i - 1).ToString();
                tableLayoutPanel2.Controls.Add(L, 0, i);

            }
            for (int i = 1; i < 11; i++)
                for (int j = 1; j < 11; j++)
                {

                    Label L = new Label();
                    if (i == j)
                    {
                        L.BackColor = Color.Blue;
                        L.Text = ((ArrayList)My_Classifier.seperatedclassesfortesting.All[i-1]).Count.ToString();
                    }
                    else
                        L.Text = "0";

                    
                    tableLayoutPanel2.Controls.Add(L, j, i);

                }

            
            // acual result
            for (int i = 1; i < 11; i++)
            {
                Label L = new Label();
                L.Text = (i - 1).ToString();
                L.BackColor = Color.Blue;
                tableLayoutPanel1.Controls.Add(L, i, 0);


            }
            for (int i = 1; i < 11; i++)
            {
                Label L = new Label();
                L.BackColor = Color.Blue;
                L.Text = (i - 1).ToString();
                tableLayoutPanel1.Controls.Add(L, 0, i);

            }
            for (int i = 1; i < 11; i++)
                for (int j = 1; j < 11; j++)
                {

                    Label L = new Label();
                    if (i == j)
                        L.BackColor = Color.Blue;
                    L.Text = Conv_to_display[i - 1, j - 1].ToString();
                    tableLayoutPanel1.Controls.Add(L, j, i);

                }

           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
