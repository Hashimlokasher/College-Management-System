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
    public partial class frmAttendanceReport : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;

        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        string cs = "Data Source=.\\SqlExpress; Integrated Security=True; AttachDbFilename=|DataDirectory|\\CMS_DB.mdf; User Instance=true;";

        public frmAttendanceReport()
        {
            InitializeComponent();
        }
        public void AutocompleCourse()
        {
            try
            {

                con = new SqlConnection(cs);
                con.Open();


                string ct = "select distinct RTRIM(course) from attendance ";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbCourse.Items.Add(rdr[0]);
                    cmbCourse1.Items.Add(rdr[0]);

                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void frmAttendanceReport_Load(object sender, EventArgs e)
        {
            AutocompleCourse();
        }

        private void cmbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbBranch.Items.Clear();
            cmbBranch.Text = "";
            cmbBranch.Enabled = true;

            try
            {

                con = new SqlConnection(cs);
                con.Open();


                string ct = "select distinct RTRIM(branch) from Attendance where course = '" + cmbCourse.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbBranch.Items.Add(rdr[0]);

                }
                con.Close();


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSemester.Items.Clear();
            cmbSemester.Text = "";
            cmbSemester.Enabled = true;

            try
            {

                con = new SqlConnection(cs);
                con.Open();


                string ct = "select distinct RTRIM(Semester) from attendance where branch = '" + cmbBranch.Text + "' and course= '" + cmbCourse.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbSemester.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSession.Items.Clear();
            cmbSession.Text = "";
            cmbSession.Enabled = true;

            try
            {

                con = new SqlConnection(cs);
                con.Open();


                string ct = "select distinct RTRIM(Session) from Attendance where Course = '" + cmbCourse.Text + "' and Branch= '" + cmbBranch.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbSession.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSection.Items.Clear();
            cmbSection.Text = "";
            cmbSection.Enabled = true;

            try
            {

                con = new SqlConnection(cs);
                con.Open();


                string ct = "select distinct RTRIM(Section) from Attendance where Course = '" + cmbCourse.Text + "' and branch = '" + cmbBranch.Text + "' and Semester = '" + cmbSemester.Text + "' and session = '" + cmbSession.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbSection.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cmbCourse.Text = "";
            cmbBranch.Text = "";
            cmbBranch.Enabled = false;
            cmbSemester.Text = "";
            cmbSemester.Enabled = false;
            cmbSession.Text = "";
            cmbSession.Enabled = false;
            cmbSection.Text = "";
            cmbSection.Enabled = false;
            dateTimePicker1.Text = DateTime.Today.ToString();
            dateTimePicker2.Text = DateTime.Today.ToString();
            crystalReportViewer1.ReportSource = null;
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = null;

            if (cmbCourse.Text == "")
            {
                MessageBox.Show("Please select course", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCourse.Focus();
                return;
            }
            if (cmbBranch.Text == "")
            {
                MessageBox.Show("Please select branch", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbBranch.Focus();
                return;
            }
            if (cmbSemester.Text == "")
            {
                MessageBox.Show("Please select semester", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSemester.Focus();
                return;
            }
            if (cmbSession.Text == "")
            {
                MessageBox.Show("Please select session", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSession.Focus();
                return;
            }
            if (cmbSection.Text == "")
            {
                MessageBox.Show("Please select section", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSection.Focus();
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptAttendance rpt = new rptAttendance();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                CMS_DBDataSet myDS = new CMS_DBDataSet();
                //The DataSet you created.
            

                myConnection = new SqlConnection("Data Source=.\\SqlExpress; Integrated Security=True; AttachDbFilename=|DataDirectory|\\CMS_DB.mdf; User Instance=true;");
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * from Attendance where Course= '" + cmbCourse.Text + "'and branch='" + cmbBranch.Text + "'and Session='" + cmbSession.Text + "' and Semester = '" + cmbSemester.Text + "' and Section = '" + cmbSection.Text + "' and Status = 'Yes' and AttendanceDate between @date1 and @date2 order by Student_name ";
                MyCommand.Parameters.Add("@date1", SqlDbType.DateTime, 30, " AttendanceDate").Value = dateTimePicker1.Value.Date;
                MyCommand.Parameters.Add("@date2", SqlDbType.DateTime, 30, " AttendanceDate").Value = dateTimePicker2.Value.Date;

                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "Attendance");
              
                con = new SqlConnection(cs);

                con.Open();
                cmd = new SqlCommand("select Count(ScholarNo) from Attendance where  Course= '" + cmbCourse.Text + "'and branch='" + cmbBranch.Text + "'and Session='" + cmbSession.Text + "' and Semester = '" + cmbSemester.Text + "' and Section = '" + cmbSection.Text + "'  and AttendanceDate between @date1 and @date2 group by ScholarNo ", con);
                cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, " AttendanceDate").Value = dateTimePicker1.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, " AttendanceDate").Value = dateTimePicker2.Value.Date;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    label21.Text = rdr.GetInt32(0).ToString();
                }
                else
                {
                    label21.Text = "";
                }
                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
              
                rpt.SetDataSource(myDS);
                rpt.SetParameterValue("variable", dateTimePicker1.Value);
                rpt.SetParameterValue("variable1", dateTimePicker2.Value);
                rpt.SetParameterValue("variable2", label21.Text);
                
             
                crystalReportViewer1.ReportSource = rpt;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmAttendanceReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            frmMainMenu frm = new frmMainMenu();
            frm.User.Text = label19.Text;
            frm.UserType.Text = label23.Text;
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmbCourse1.Text = "";
            cmbBranch1.Text = "";
            cmbBranch1.Enabled = false;
            cmbSemester1.Text = "";
            cmbSemester1.Enabled = false;
            cmbSession1.Text = "";
            cmbSession1.Enabled = false;
            cmbSection1.Text = "";
            cmbSection1.Enabled = false;
            cmbSubjectCode.Text = "";
            cmbSubjectCode.Enabled = false;
            txtSubjectName.Text = "";
            txtSubjectName.ReadOnly = true;
            dateTimePicker4.Text = DateTime.Today.ToString();
            dateTimePicker3.Text = DateTime.Today.ToString();
            crystalReportViewer2.ReportSource = null;
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            cmbCourse.Text = "";
            cmbBranch.Text = "";
            cmbBranch.Enabled = false;
            cmbSemester.Text = "";
            cmbSemester.Enabled = false;
            cmbSession.Text = "";
            cmbSession.Enabled = false;
            cmbSection.Text = "";
            cmbSection.Enabled = false;
            dateTimePicker1.Text = DateTime.Today.ToString();
            dateTimePicker2.Text = DateTime.Today.ToString();
            crystalReportViewer1.ReportSource = null;
            cmbCourse1.Text = "";
            cmbBranch1.Text = "";
            cmbBranch1.Enabled = false;
            cmbSemester1.Text = "";
            cmbSemester1.Enabled = false;
            cmbSession1.Text = "";
            cmbSession1.Enabled = false;
            cmbSection1.Text = "";
            cmbSection1.Enabled = false;
            cmbSubjectCode.Text = "";
            cmbSubjectCode.Enabled = false;
            txtSubjectName.Text = "";
            txtSubjectName.ReadOnly = true;
            dateTimePicker4.Text = DateTime.Today.ToString();
            dateTimePicker3.Text = DateTime.Today.ToString();
            crystalReportViewer2.ReportSource = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (cmbCourse1.Text == "")
            {
                MessageBox.Show("Please select course", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCourse1.Focus();
                return;
            }
            if (cmbBranch1.Text == "")
            {
                MessageBox.Show("Please select branch", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbBranch1.Focus();
                return;
            }
            if (cmbSemester1.Text == "")
            {
                MessageBox.Show("Please select semester", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSemester1.Focus();
                return;
            }
            if (cmbSession1.Text == "")
            {
                MessageBox.Show("Please select session", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSession1.Focus();
                return;
            }
            if (cmbSection1.Text == "")
            {
                MessageBox.Show("Please select section", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSection1.Focus();
                return;
            }
            if (cmbSubjectCode.Text == "")
            {
                MessageBox.Show("Please select subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSubjectCode.Focus();
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                rptAttendance rpt = new rptAttendance();
                //The report you created.
                SqlConnection myConnection = default(SqlConnection);
                SqlCommand MyCommand = new SqlCommand();
                SqlDataAdapter myDA = new SqlDataAdapter();
                CMS_DBDataSet myDS = new CMS_DBDataSet();
                //The DataSet you created.
                

                myConnection = new SqlConnection("Data Source=.\\SqlExpress; Integrated Security=True; AttachDbFilename=|DataDirectory|\\CMS_DB.mdf; User Instance=true;");
                MyCommand.Connection = myConnection;
                MyCommand.CommandText = "select * from Attendance where Course= '" + cmbCourse1.Text + "' and branch='" + cmbBranch1.Text + "' and Session='" + cmbSession1.Text + "' and Semester = '" + cmbSemester1.Text + "' and Section = '" + cmbSection1.Text + "' and SubjectCode = '" + cmbSubjectCode.Text + "' and AttendanceDate between @date1 and @date2 order by Student_name ";
                MyCommand.Parameters.Add("@date1", SqlDbType.DateTime, 30, " AttendanceDate").Value = dateTimePicker4.Value.Date;
                MyCommand.Parameters.Add("@date2", SqlDbType.DateTime, 30, " AttendanceDate").Value = dateTimePicker3.Value.Date;

                MyCommand.CommandType = CommandType.Text;
                myDA.SelectCommand = MyCommand;
                myDA.Fill(myDS, "Attendance");
             
                con.Open();
                cmd = new SqlCommand("select Count(ScholarNo) from Attendance where Course= '" + cmbCourse1.Text + "' and branch='" + cmbBranch1.Text + "' and Session='" + cmbSession1.Text + "' and Semester = '" + cmbSemester1.Text + "' and Section = '" + cmbSection1.Text + "' and SubjectCode = '" + cmbSubjectCode.Text + "' and AttendanceDate between @date1 and @date2 group by ScholarNo", con);
                cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, " AttendanceDate").Value = dateTimePicker4.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, " AttendanceDate").Value = dateTimePicker3.Value.Date;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    label20.Text = rdr.GetInt32(0).ToString();
                }
                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                rpt.SetDataSource(myDS);
                rpt.SetParameterValue("variable", dateTimePicker4.Value);
                rpt.SetParameterValue("variable1", dateTimePicker3.Value);
                rpt.SetParameterValue("variable2", label20.Text);
                crystalReportViewer2.ReportSource = rpt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void cmbCourse1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbBranch1.Items.Clear();
            cmbBranch1.Text = "";
            cmbBranch1.Enabled = true;

            try
            {

                con = new SqlConnection(cs);
                con.Open();


                string ct = "select distinct RTRIM(branch) from Attendance where course = '" + cmbCourse1.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbBranch1.Items.Add(rdr[0]);

                }
                con.Close();


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbBranch1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSemester1.Items.Clear();
            cmbSemester1.Text = "";
            cmbSemester1.Enabled = true;

            try
            {

                con = new SqlConnection(cs);
                con.Open();


                string ct = "select distinct RTRIM(Semester) from attendance where branch = '" + cmbBranch1.Text + "' and course= '" + cmbCourse1.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbSemester1.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSemester1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSession1.Items.Clear();
            cmbSession1.Text = "";
            cmbSession1.Enabled = true;

            try
            {

                con = new SqlConnection(cs);
                con.Open();


                string ct = "select distinct RTRIM(Session) from Attendance where Course = '" + cmbCourse1.Text + "' and Branch= '" + cmbBranch1.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbSession1.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSession1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSection1.Items.Clear();
            cmbSection1.Text = "";
            cmbSection1.Enabled = true;

            try
            {

                con = new SqlConnection(cs);
                con.Open();


                string ct = "select distinct RTRIM(Section) from Attendance where Course = '" + cmbCourse1.Text + "' and branch = '" + cmbBranch1.Text + "' and Semester = '" + cmbSemester1.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbSection1.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSection1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbSubjectCode.Items.Clear();
                cmbSubjectCode.Text = "";
                cmbSubjectCode.Enabled = true;
                con = new SqlConnection(cs);
                con.Open();


                string ct = "select distinct RTRIM(SubjectCode) from Attendance where Course = '" + cmbCourse1.Text + "' and Branch= '" + cmbBranch1.Text + "' and semester= '" + cmbSemester1.Text + "' and session= '" + cmbSession1.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbSubjectCode.Items.Add(rdr[0]);
                }
                con.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbSubjectCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT SubjectName FROM Attendance WHERE SubjectCode = '" + cmbSubjectCode.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {

                    txtSubjectName.Text = rdr.GetString(0).Trim();
                }


                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }


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