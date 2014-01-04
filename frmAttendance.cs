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
    public partial class frmAttendance : Form
    {

        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        string cs = "Data Source=.\\SqlExpress; Integrated Security=True; AttachDbFilename=|DataDirectory|\\CMS_DB.mdf; User Instance=true;";
        public frmAttendance()
        {
            InitializeComponent();
        }
        private void Student_Attendance_Load(object sender, EventArgs e)
        {
            AutocompleEmployeeID();
            AutocompleCourse();
        }
        public void AutocompleCourse()
        {
            try
            {
                con = new SqlConnection(cs);
                con.Open();
                string ct = "select distinct RTRIM(course) from Student ";
                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cmbCourse.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AutocompleEmployeeID()
        {
            try
            {

                con = new SqlConnection(cs);
                con.Open();


                string ct = "select distinct RTRIM(StaffID) from Employee ";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbStaffID.Items.Add(rdr[0]);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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


                string ct = "select distinct RTRIM(branch) from Student where course = '" + cmbCourse.Text + "'";

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


                string ct = "select distinct RTRIM(Semester) from Student where branch = '" + cmbBranch.Text + "' and course= '" + cmbCourse.Text + "'";

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
            cmbSubjectCode.Items.Clear();
            cmbSubjectCode.Text = "";
            cmbSubjectCode.Enabled = true;
            try
            {

                con = new SqlConnection(cs);
                con.Open();


                string ct = "select distinct RTRIM(Session) from Student where Course = '" + cmbCourse.Text + "' and Branch= '" + cmbBranch.Text + "'";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cmbSession.Items.Add(rdr[0]);
                }
                con.Close();
                con = new SqlConnection(cs);
                con.Open();


                string ct1 = "select distinct RTRIM(SubjectCode) from SubjectInfo where CourseName = '" + cmbCourse.Text + "' and Branch= '" + cmbBranch.Text + "' and semester= '" + cmbSemester.Text + "'";

                cmd = new SqlCommand(ct1);
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
      

        private void btnSubmit_Click(object sender, EventArgs e)
        {
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
                var _with1 = listView1;
                _with1.Clear();
                _with1.Columns.Add("Scholar No.", 120,HorizontalAlignment.Left);
                _with1.Columns.Add("Student Name", 250, HorizontalAlignment.Center);

                con = new SqlConnection(cs);
                con.Open();

                cmd = new SqlCommand("select ScholarNo,student_name from Student where Course = '" + cmbCourse.Text + "' and Branch= '" + cmbBranch.Text + "' and semester= '" + cmbSemester.Text + "' and Session= '" + cmbSession.Text + "' and section='" + cmbSection.Text + "' order by student_name,ScholarNo", con);
              
              
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var item = new ListViewItem();
                    item.Text = rdr[0].ToString().Trim();        
                    item.SubItems.Add(rdr[1].ToString().Trim());  
                    listView1.Items.Add(item);
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

                cmd.CommandText = "SELECT SubjectName FROM SubjectInfo WHERE SubjectCode = '" + cmbSubjectCode.Text + "'";
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

        private void cmbEmployeeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT StaffName FROM Employee WHERE StaffID = '" + cmbStaffID.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {

                  txtStaffName.Text = rdr.GetString(0).Trim();
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

        private void cmbSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSection.Items.Clear();
            cmbSection.Text = "";
            cmbSection.Enabled = true;

            try
            {

                con = new SqlConnection(cs);
                con.Open();


                string ct = "select distinct RTRIM(Section) from Student where Course = '" + cmbCourse.Text + "' and branch = '" + cmbBranch.Text + "' and Semester = '" + cmbSemester.Text + "' and session = '" +cmbSession.Text + "'" ;

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

        private void frmAttendance_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMainMenu frm = new frmMainMenu();
            this.Hide();
            frm.UserType.Text = label11.Text;
            frm.User.Text = label12.Text;
            frm.Show();
        }
        private void Reset()
        {
            cmbBranch.Text = "";
            cmbCourse.Text = "";
            cmbStaffID.Text = "";
            cmbSection.Text = "";
            cmbSemester.Text = "";
            cmbSession.Text = "";
            cmbSubjectCode.Text = "";
            txtStaffName.Text = "";
            txtSubjectName.Text = "";
            dateTimePicker1.Text = System.DateTime.Today.ToString();
            listView1.Items.Clear();
            btnSave.Enabled = true;
            Delete.Enabled = false;
            Update_record.Enabled = false;
            cmbBranch.Enabled = false;
            cmbSemester.Enabled = false;
            cmbSession.Enabled = false;
            cmbSection.Enabled = false;
            cmbSubjectCode.Enabled = false;
        }
        private void NewRecord_Click(object sender, EventArgs e)
        {
            Reset();
          
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
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
                if (cmbSubjectCode.Text == "")
                {
                    MessageBox.Show("Please select subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSubjectCode.Focus();
                    return;
                }
                if (cmbStaffID.Text == "")
                {
                    MessageBox.Show("Please select staff id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbStaffID.Focus();
                    return;
                }
               

            try 
            {
       
            for (int i = listView1.Items.Count - 1; i >= 0; i--)
            {
               
			    con = new SqlConnection(cs);
                if (listView1.Items[i].Checked == true)
                {
                    txtStatus.Text = "Yes";
                }
                else
                {
                    txtStatus.Text = "No";
                }
                string cd = "insert into Attendance(course,branch,semester,session,section,subjectcode,subjectname,staffid,staffname,attendancedate,scholarno,student_name,Status) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13)";

                cmd = new SqlCommand(cd);

                cmd.Connection = con;
                cmd.Parameters.AddWithValue("d1", cmbCourse.Text);
                cmd.Parameters.AddWithValue("d2", cmbBranch.Text);
                cmd.Parameters.AddWithValue("d3", cmbSemester.Text);
                cmd.Parameters.AddWithValue("d4", cmbSession.Text);
                cmd.Parameters.AddWithValue("d5", cmbSection.Text);
                cmd.Parameters.AddWithValue("d6", cmbSubjectCode.Text);
                cmd.Parameters.AddWithValue("d7", txtSubjectName.Text);
                cmd.Parameters.AddWithValue("d8", cmbStaffID.Text);
                cmd.Parameters.AddWithValue("d9", txtStaffName.Text);
                cmd.Parameters.AddWithValue("d10", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("d11", listView1.Items[i].SubItems[0].Text);
                cmd.Parameters.AddWithValue("d12", listView1.Items[i].SubItems[1].Text);
                cmd.Parameters.AddWithValue("d13", txtStatus.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


		
	}
            
            
            MessageBox.Show("Successfully saved", "Student Attendance", MessageBoxButtons.OK, MessageBoxIcon.Information);
        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Delete_Click(object sender, EventArgs e)
        {
          
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

                  if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();


            }
        }

           private void delete_records()
        {

            try
            {


                int RowsAffected = 0;

                con = new SqlConnection(cs);

                con.Open();


                string cq = "delete from attendance where Course = '" + cmbCourse.Text + "' and Branch= '" + cmbBranch.Text + "' and semester= '" + cmbSemester.Text + "' and Session= '" + cmbSession.Text + "' and section='" + cmbSection.Text + "'";


                cmd = new SqlCommand(cq);

                cmd.Connection = con;

                
                RowsAffected = cmd.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 
                    Reset();
                  
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);

                  Reset();
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

           private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
           {
               if (label11.Text == "Admin")
               {
                   Delete.Enabled = true;
               }
               else
               {
                   Delete.Enabled = false;
               }
           }

           private void button1_Click(object sender, EventArgs e)
           {
               if (label11.Text == "Admin")
               {
                   Update_record.Enabled = true;
               }
               else if (label11.Text == "Employee")
               {
                   Update_record.Enabled = true;
               }
               else
               {
                   Update_record.Enabled = false;
               }
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
               if (cmbSubjectCode.Text == "")
               {
                   MessageBox.Show("Please select subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   cmbSubjectCode.Focus();
                   return;
               }
               if (cmbStaffID.Text == "")
               {
                   MessageBox.Show("Please select staff id", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   cmbStaffID.Focus();
                   return;
               }
               try
               {
                   var _with1 = listView1;
                   _with1.Clear();
                   _with1.Columns.Add("Scholar No.", 120, HorizontalAlignment.Left);
                   _with1.Columns.Add("Student Name", 250, HorizontalAlignment.Center);
                   _with1.Columns.Add("Status", 0, HorizontalAlignment.Center);

                   con = new SqlConnection(cs);
                   con.Open();

                   cmd = new SqlCommand("select ScholarNo,student_name,Status from Attendance where Course = '" + cmbCourse.Text + "' and Branch= '" + cmbBranch.Text + "' and semester= '" + cmbSemester.Text + "' and Session= '" + cmbSession.Text + "' and section='" + cmbSection.Text + "' and StaffID ='" + cmbStaffID.Text + "' and SubjectCode = '" + cmbSubjectCode.Text + "' and Attendancedate = '" + dateTimePicker1.Text + "' order by student_name,ScholarNo", con);

                   rdr = cmd.ExecuteReader();

                   while (rdr.Read())
                   {
                     
                       var item = new ListViewItem();
                       item.Text = rdr[0].ToString().Trim();
                       item.SubItems.Add(rdr[1].ToString().Trim());
                       item.SubItems.Add(rdr[2].ToString().Trim());
                       listView1.Items.Add(item);
                       for (int i = listView1.Items.Count - 1; i >= 0; i--)
                       {
                          
                               if ( listView1.Items[i].SubItems[2].Text== "Yes")
                               {
                                   listView1.Items[i].Checked = true;
                               }
                               else
                               {
                                   listView1.Items[i].Checked = false;
                               }
                           }
                       }
                   
                   }

               
               catch (Exception ex)
               {
                   MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }

           }

           private void Update_record_Click(object sender, EventArgs e)
           {


               try
               {

                   for (int i = listView1.Items.Count - 1; i >= 0; i--)
                   {

                       con = new SqlConnection(cs);
                       if (listView1.Items[i].Checked == true)
                       {
                           txtStatus.Text = "Yes";
                       }
                       else
                       {
                           txtStatus.Text = "No";
                       }
                       string cd = "update Attendance set Status=@d13 where  course=@d1 and branch=@d2 and semester=@d3 and session=@d4 and section=@d5 and subjectcode=@d6 and subjectname=@d7 and staffid=@d8 and staffname=@d9 and attendancedate=@d10 and scholarNo=@d11 and Student_name=@d12 ";

                       cmd = new SqlCommand(cd);

                       cmd.Connection = con;
                       cmd.Parameters.AddWithValue("d1", cmbCourse.Text);
                       cmd.Parameters.AddWithValue("d2", cmbBranch.Text);
                       cmd.Parameters.AddWithValue("d3", cmbSemester.Text);
                       cmd.Parameters.AddWithValue("d4", cmbSession.Text);
                       cmd.Parameters.AddWithValue("d5", cmbSection.Text);
                       cmd.Parameters.AddWithValue("d6", cmbSubjectCode.Text);
                       cmd.Parameters.AddWithValue("d7", txtSubjectName.Text);
                       cmd.Parameters.AddWithValue("d8", cmbStaffID.Text);
                       cmd.Parameters.AddWithValue("d9", txtStaffName.Text);
                       cmd.Parameters.AddWithValue("d10", dateTimePicker1.Text);
                       cmd.Parameters.AddWithValue("d11", listView1.Items[i].SubItems[0].Text);
                       cmd.Parameters.AddWithValue("d12", listView1.Items[i].SubItems[1].Text);
                       cmd.Parameters.AddWithValue("d13", txtStatus.Text);
                       con.Open();
                       cmd.ExecuteNonQuery();
                       con.Close();
                  }


                   MessageBox.Show("Successfully updated", "Student Attendance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   Update_record.Enabled = false;
               }
               catch (Exception ex)
               {
                   MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }

           }

     

    }
}
