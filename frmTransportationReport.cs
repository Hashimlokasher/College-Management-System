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
    public partial class frmTransportationReport : Form
    {
        public frmTransportationReport()
        {
            InitializeComponent();
        }

        private void frmTransportationReport_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptTransportation rpt = new rptTransportation();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                StudentDataSet myDS = new StudentDataSet();
                //The DataSet you created.
                Transportation_DBDataSet frm = new Transportation_DBDataSet();

                myConnection = new SqlConnection("Data Source=.\\SqlExpress; Integrated Security=True; AttachDbFilename=|DataDirectory|\\CMS_DB.mdf; User Instance=true;");
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * from transportation order by SourceLocation";
              
                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "Transportation");
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
    }
}
