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
    public partial class FrmSalarySlip : Form
    {
        public FrmSalarySlip()
        {
            InitializeComponent();
        }

        private void FrmSalarySlip_Load(object sender, EventArgs e)
        {
            try
            {
                rptSalarySlip rpt = new rptSalarySlip();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                EmployeePayment_DataSet myDS = new EmployeePayment_DataSet();
                //The DataSet you created.
                frmSalaryPayment frm = new frmSalaryPayment();

                myConnection = new SqlConnection("Data Source=.\\SqlExpress; Integrated Security=True; AttachDbFilename=|DataDirectory|\\CMS_DB.mdf; User Instance=true;");
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * from EmployeePayment";

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
    }
}
