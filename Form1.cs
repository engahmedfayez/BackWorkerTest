using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;

namespace BackWorkerTest
{
    public partial class Form1 : Form
    {

        SqlConnection con1;
        SqlCommand cmd1;

        BackgroundWorker worker;

        public Form1()
        {
            InitializeComponent();

            con1 = new SqlConnection(@"Data Source=DESKTOP-NFQ9Q5O\SQLEXPRESS;Initial Catalog=inv20;Persist Security Info=True;User ID=sa;Password=y2000");
            con1.Open();
            cmd1 = new SqlCommand("", con1);






        }

        private void button1_Click(object sender, EventArgs e)
        {



            if (!backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }

        }



        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {




            for (int i = 0; i <= 100; i++)
            {

                backgroundWorker1.ReportProgress(i);

                Thread.Sleep(50);

                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    backgroundWorker1.ReportProgress(0);
                    return;

                }



            }
        }


        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            progressBar1.Value = e.ProgressPercentage;
            label1.Text = $"{e.ProgressPercentage} %";



        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Error != null)
            {
                MessageBox.Show("Error ... ");
            }

            if (e.Cancelled)
            {
                MessageBox.Show("Cancelled");

            }
            else
            {
                MessageBox.Show("Done");
            }



        }

        private async void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;

            this.Cursor = Cursors.WaitCursor;

            int ef = await Task.Run(() => saveline("tttttt"));

            this.Cursor = Cursors.Arrow;

            button3.Enabled = true;
            MessageBox.Show(ef.ToString());



        }



        private async Task<int> saveline(string x)
        {


            cmd1.CommandText = $"WAITFOR DELAY '00:00:05'";
            cmd1.ExecuteNonQuery();


            cmd1.CommandText =
             $@" 
              
             UPDATE [inv20].[dbo].[ItmForProg_log]
             set[check_note] = '{x}'  ";
            int eff = cmd1.ExecuteNonQuery();

            return await Task.FromResult(eff);



        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
