using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
namespace College_Management_System
{
    public partial class frmSalarySlipRpt : Form
    {
        DataTable dtable = new DataTable();
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        public frmSalarySlipRpt()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = null;
            cmbPaymentID.Text = "";
        }

        private void cmbPaymentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptSalarySlip rpt = new rptSalarySlip();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                EmployeePayment_DataSet myDS = new EmployeePayment_DataSet();
                //The DataSet you created.


                myConnection = new SqlConnection("Data Source=.\\SqlExpress; Integrated Security=True; AttachDbFilename=|DataDirectory|\\CMS_DB.mdf; User Instance=true;");
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * from Employeepayment where PaymentID= '" + cmbPaymentID.Text + "'";

                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "EmployeePayment");
                rpt.SetDataSource(myDS);
                crystalReportViewer1.ReportSource = rpt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }
        public void Autocomplete()
        {

            try
            {
                string cs = "Data Source=.\\SqlExpress; Integrated Security=True; AttachDbFilename=|DataDirectory|\\CMS_DB.mdf; User Instance=true;";

                SqlConnection CN = new SqlConnection(cs);

                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(PaymentID) FROM EmployeePayment", CN);
                ds = new DataSet("ds");

                adp.Fill(ds);
                dtable = ds.Tables[0];
                cmbPaymentID.Items.Clear();

                foreach (DataRow drow in dtable.Rows)
                {
                    cmbPaymentID.Items.Add(drow[0].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void frmSalarySlipRpt_Load(object sender, EventArgs e)
        {
            Autocomplete();
        }
    }
}
