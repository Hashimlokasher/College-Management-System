using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace College_Management_System
{
    public partial class frmAttendanceRecord : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;

        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        string cs = "Data Source=.\\SqlExpress; Integrated Security=True; AttachDbFilename=|DataDirectory|\\CMS_DB.mdf; User Instance=true;";

        public frmAttendanceRecord()
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
        private void frmAttendanceRecord_Load(object sender, EventArgs e)
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
            listView1.Items.Clear();
            label19.Visible = false;
            label20.Visible = false;
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
           
            
            try
            {
                label19.Visible = false;
                label20.Visible = false;
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


                var _with1 = listView1;
                    _with1.Clear();
                    _with1.Columns.Add("Scholar No.", 120, HorizontalAlignment.Left);
                    _with1.Columns.Add("Student Name", 250, HorizontalAlignment.Center);
                    _with1.Columns.Add("Total Attendance", 120, HorizontalAlignment.Center);

                    con = new SqlConnection(cs);

                    con.Open();
                    cmd = new SqlCommand("select RTrim(Student.ScholarNo)[Scholar No.], RTRIM(Student.student_name)[Student Name],count(Status)[Total Attendance] from Student inner join Attendance on Student.ScholarNo=Attendance.ScholarNo and Student.Course= '" + cmbCourse.Text + "'and Student.branch='" + cmbBranch.Text + "'and Student.Session='" + cmbSession.Text + "' and Student.Semester = '" + cmbSemester.Text + "' and Student.Section = '" + cmbSection.Text + "' and status= 'Yes' and Attendance.AttendanceDate between @date1 and @date2 group by Student.ScholarNo,Student.student_name order by Student.student_name", con);
                    cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, " AttendanceDate").Value = dateTimePicker1.Value.Date;
                    cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, " AttendanceDate").Value = dateTimePicker2.Value.Date;

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        var item = new ListViewItem();
                        item.Text = rdr[0].ToString();
                        item.SubItems.Add(rdr[1].ToString());
                        item.SubItems.Add(rdr[2].ToString());
                        listView1.Items.Add(item);
                       
                    }

                 
                    con.Close();
                
             
                con = new SqlConnection(cs);

                con.Open();
                cmd = new SqlCommand("select Count(ScholarNo) from Attendance where  Course= '" + cmbCourse.Text + "'and branch='" + cmbBranch.Text + "'and Session='" + cmbSession.Text + "' and Semester = '" + cmbSemester.Text + "' and Section = '" + cmbSection.Text + "'  and AttendanceDate between @date1 and @date2 group by ScholarNo ", con);
                cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, " AttendanceDate").Value = dateTimePicker1.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, " AttendanceDate").Value = dateTimePicker2.Value.Date;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    label19.Text = rdr.GetInt32(0).ToString();
                    label19.Visible = true;
                    label20.Visible = true;
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
            listView2.Items.Clear();
            label21.Visible = false;
            label22.Visible = false;
        }

        private void tabControl1_Click(object sender, EventArgs e)
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
            listView2.Items.Clear();
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
            listView1.Items.Clear();
            label21.Visible = false;
            label22.Visible = false;
            label19.Visible = false;
            label20.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            try
            {
                label21.Visible = false;
                label22.Visible = false;
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
              
                var _with1 = listView2;
                _with1.Clear();
                _with1.Columns.Add("Scholar No.", 120, HorizontalAlignment.Left);
                _with1.Columns.Add("Student Name", 250, HorizontalAlignment.Center);
                _with1.Columns.Add("Total Attendance", 120, HorizontalAlignment.Center);

                con = new SqlConnection(cs);

                con.Open();
                cmd = new SqlCommand("select RTrim(Student.ScholarNo)[Scholar No.], RTRIM(Student.student_name)[Student Name],count(Status)[Total Attendance] from Student inner join Attendance on Student.ScholarNo=Attendance.ScholarNo and Student.Course= '" + cmbCourse1.Text + "' and Student.branch='" + cmbBranch1.Text + "' and Student.Session='" + cmbSession1.Text + "' and Student.Semester = '" + cmbSemester1.Text + "' and Student.Section = '" + cmbSection1.Text + "' and SubjectCode = '" + cmbSubjectCode.Text + "' and status = 'Yes' and AttendanceDate between @date1 and @date2 group by Student.ScholarNo,Student.student_name order by Student.student_name", con);
                 cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, " AttendanceDate").Value = dateTimePicker4.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, " AttendanceDate").Value = dateTimePicker3.Value.Date;


                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var item = new ListViewItem();
                    item.Text = rdr[0].ToString();
                    item.SubItems.Add(rdr[1].ToString());
                    item.SubItems.Add(rdr[2].ToString());
                    listView2.Items.Add(item);
                }
                con.Close();
                
              
                con = new SqlConnection(cs);

                con.Open();
                cmd = new SqlCommand("select Count(ScholarNo) from Attendance where Course= '" + cmbCourse1.Text + "' and branch='" + cmbBranch1.Text + "' and Session='" + cmbSession1.Text + "' and Semester = '" + cmbSemester1.Text + "' and Section = '" + cmbSection1.Text + "' and SubjectCode = '" + cmbSubjectCode.Text + "' and AttendanceDate between @date1 and @date2 group by ScholarNo", con);
                cmd.Parameters.Add("@date1", SqlDbType.DateTime, 30, " AttendanceDate").Value = dateTimePicker4.Value.Date;
                cmd.Parameters.Add("@date2", SqlDbType.DateTime, 30, " AttendanceDate").Value = dateTimePicker3.Value.Date;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    label22.Text = rdr.GetInt32(0).ToString();
                    label21.Visible = true;
                    label22.Visible = true;
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

        private void frmAttendanceRecord_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMainMenu frm = new frmMainMenu();
            this.Hide();
            frm.UserType.Text = label24.Text;
            frm.User.Text = label25.Text;
            frm.Show();
        }

      
      
     


    }
}
