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
    public partial class frmInternalMarksEntry : Form
    {
        SqlDataReader rdr = null;
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlDataAdapter adp;
        DataSet ds = new DataSet();
        SqlCommand cmd = null;
        DataTable dt = new DataTable();

        string cs = "Data Source=.\\SqlExpress; Integrated Security=True; AttachDbFilename=|DataDirectory|\\CMS_DB.mdf; User Instance=true;";

        public frmInternalMarksEntry()
        {
            InitializeComponent();
        }

        private void frmInternalMarksEntry_Load(object sender, EventArgs e)
        {
            AutocompleteScholarNo();
            AutocompleteSubjectCode();
        }
        private void AutocompleteScholarNo()
        {

            try
            {
               
                SqlConnection CN = new SqlConnection(cs);

                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(ScholarNo) FROM Student", CN);
                ds = new DataSet("ds");

                adp.Fill(ds);
                dtable = ds.Tables[0];
                cmbScholarNo.Items.Clear();

                foreach (DataRow drow in dtable.Rows)
                {
                    cmbScholarNo.Items.Add(drow[0].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void AutocompleteSubjectCode()
        {

            try
            {
               
                SqlConnection CN = new SqlConnection(cs);

                CN.Open();
                adp = new SqlDataAdapter();
                adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(SubjectCode) FROM Subjectinfo", CN);
                ds = new DataSet("ds");

                adp.Fill(ds);
                dtable = ds.Tables[0];
                cmbSubjectCode.Items.Clear();

                foreach (DataRow drow in dtable.Rows)
                {
                    cmbSubjectCode.Items.Add(drow[0].ToString());

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ScholarNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(cs);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT Student_Name,Course,Branch,Roll_No FROM student WHERE ScholarNo = '" + cmbScholarNo.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {


                    txtStudentName.Text = rdr.GetString(0).Trim();


                    txtCourse.Text = rdr.GetString(1).Trim();

                    txtBranch.Text = rdr.GetString(2).Trim();
                    txtRollNo.Text = rdr.GetString(3).Trim();
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

        private void SubjectCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbSubjectCode.Text = cmbSubjectCode.Text.TrimEnd();
                con = new SqlConnection(cs);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT SubjectName,Semester FROM subjectinfo WHERE SubjectCode = '" + cmbSubjectCode.Text.Trim() + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    txtSubjectName.Text = (rdr.GetString(0).Trim());
            
                    txtSemester.Text = (rdr.GetString(1).Trim());

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
        private void clear()
        {
            cmbScholarNo.Text = "";
            cmbSubjectCode.Text = "";
            txtBranch.Text = "";
            txtCourse.Text = "";
            cmbExam.Text = "";
            txtMinMarks.Text = "";
            txtMaxMarks.Text = "";
            txtObtainedMarks.Text = "";
            txtRollNo.Text = "";
            txtSemester.Text = "";
            txtStudentName.Text = "";
            txtSubjectName.Text = "";
            dtpExamDate.Text = DateTime.Today.ToString();
        
        
        
        }
        private void delete_records()
        {

            try
            {


                int RowsAffected = 0;
         
                con = new SqlConnection(cs);

                con.Open();


                string cq = "delete from InternalMarksEntry where ScholarNo=@DELETE1;";


                cmd = new SqlCommand(cq);

                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@DELETE1", System.Data.SqlDbType.NChar, 15, "ScholarNo"));


                cmd.Parameters["@DELETE1"].Value = cmbScholarNo.Text;
                RowsAffected = cmd.ExecuteNonQuery();

                if (RowsAffected > 0)
                {
                    MessageBox.Show("Successfully deleted", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnDelete.Enabled = false;
                    btnUpdate_record.Enabled = false;
                    clear();
                }
                else
                {
                    MessageBox.Show("No Record found", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    btnDelete.Enabled = false;
                    btnUpdate_record.Enabled = false;
                    clear();

                }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    con.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
      


        private void NewRecord_Click(object sender, EventArgs e)
        {
            clear();
            btnDelete.Enabled = false;
            btnUpdate_record.Enabled = false;
        }

        private void frmInternalMarksEntry_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMainMenu frm = new frmMainMenu();
            this.Hide();
            frm.UserType.Text = label13.Text;
            frm.User.Text = label14.Text;
            frm.Show();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             if (cmbScholarNo.Text == "")
            {
                MessageBox.Show("Please select scholar no.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbScholarNo.Focus();
                return;
            }
            if (cmbSubjectCode.Text == "")
            {
                MessageBox.Show("Please select subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSubjectCode.Focus();
                return;
            }
            if (cmbExam.Text == "")
            {
                MessageBox.Show("Please select exam", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbExam.Focus();
                return;
            }
            if (txtMinMarks.Text == "")
            {
                MessageBox.Show("Please enter min marks", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMinMarks.Focus();
                return;
            }
            if (txtMaxMarks.Text == "")
            {
                MessageBox.Show("Please enter max marks", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaxMarks.Focus();
                return;
            }
            if (txtObtainedMarks.Text == "")
            {
                MessageBox.Show("Please enter obtained marks", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtObtainedMarks.Focus();
                return;
            }
            try
            {
                con = new SqlConnection(cs);
                con.Open();
                string ct = "select ScholarNo,SubjectCode,Exam from InternalMarksEntry where ScholarNo=@find and SubjectCode=@find1 and Exam=@find2";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NChar, 15, "ScholarNo"));
                cmd.Parameters["@find"].Value = cmbScholarNo.Text;
                cmd.Parameters.Add(new SqlParameter("@find1", System.Data.SqlDbType.NChar, 20, "Subject Code"));
                cmd.Parameters["@find1"].Value = cmbSubjectCode.Text;
                cmd.Parameters.Add(new SqlParameter("@find2", System.Data.SqlDbType.NChar, 20, "Exam"));
                cmd.Parameters["@find2"].Value = cmbExam.Text;
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    MessageBox.Show("Record Already Exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              


                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    return;
                }

                con = new SqlConnection(cs);
                con.Open();

                string cb = "insert into InternalMarksEntry(ScholarNo,SubjectCode,Exam,StudentName,Course,Branch,RollNo,SubjectName,Semester,ExamDate,MinMarks,MaxMarks,MarksObtained) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13)";

                cmd = new SqlCommand(cb);

                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, "ScholarNo"));

                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 20, "Exam"));

                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 30, "StudentName"));
                cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Course"));


                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 30, "Branch"));

                cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 15, "RollNo"));

                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.VarChar, 250, "SubjectName"));

                cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 10, "Semester"));
                cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.Int, 10, "MinMarks"));

                cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "MaxMarks"));
                cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.Int, 10, "Obtainedmarks"));
            
              

                cmd.Parameters["@d1"].Value = cmbScholarNo.Text;
                cmd.Parameters["@d2"].Value = cmbSubjectCode.Text;
                cmd.Parameters["@d3"].Value = cmbExam.Text;
                cmd.Parameters["@d4"].Value = txtStudentName.Text;
                cmd.Parameters["@d5"].Value = txtCourse.Text;
                cmd.Parameters["@d6"].Value = txtBranch.Text;
                cmd.Parameters["@d7"].Value = txtRollNo.Text;
                cmd.Parameters["@d8"].Value = txtSubjectName.Text;
                cmd.Parameters["@d9"].Value = txtSemester.Text;
                cmd.Parameters["@d10"].Value = dtpExamDate.Text;
                cmd.Parameters["@d11"].Value = Convert.ToInt32(txtMinMarks.Text);
                cmd.Parameters["@d12"].Value = Convert.ToInt32(txtMaxMarks.Text);
                cmd.Parameters["@d13"].Value = Convert.ToInt32(txtObtainedMarks.Text);
                cmd.ExecuteNonQuery();


                con.Close();
                MessageBox.Show("Successfully Saved", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        private void btnUpdate_record_Click(object sender, EventArgs e)
        {
            try{
              con = new SqlConnection(cs);
                con.Open();

                string cb = "update InternalMarksEntry set StudentName=@d4,Course=@d5,Branch=@d6,RollNo=@d7,SubjectName=@d8,Semester=@d9,ExamDate=@d10,MinMarks=@d11,MaxMarks=@d12,MarksObtained=@d13 where ScholarNo=@d1 and SubjectCode=@d2 and Exam=@d3";

                cmd = new SqlCommand(cb);

                cmd.Connection = con;

                cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.NChar, 15, "ScholarNo"));

                cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.NChar, 20, "SubjectCode"));
                cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.NChar, 20, "Exam"));

                cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.NChar, 30, "StudentName"));
                cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.NChar, 10, "Course"));


                cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.NChar, 30, "Branch"));

                cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.NChar, 15, "RollNo"));

                cmd.Parameters.Add(new SqlParameter("@d8", System.Data.SqlDbType.VarChar, 250, "SubjectName"));

                cmd.Parameters.Add(new SqlParameter("@d9", System.Data.SqlDbType.NChar, 10, "Semester"));
                cmd.Parameters.Add(new SqlParameter("@d10", System.Data.SqlDbType.NChar, 30, "ExamDate"));
                cmd.Parameters.Add(new SqlParameter("@d11", System.Data.SqlDbType.Int, 10, "MinMarks"));

                cmd.Parameters.Add(new SqlParameter("@d12", System.Data.SqlDbType.Int, 10, "MaxMarks"));
                cmd.Parameters.Add(new SqlParameter("@d13", System.Data.SqlDbType.Int, 10, "Obtainedmarks"));



                cmd.Parameters["@d1"].Value = cmbScholarNo.Text;
                cmd.Parameters["@d2"].Value = cmbSubjectCode.Text;
                cmd.Parameters["@d3"].Value = cmbExam.Text;
                cmd.Parameters["@d4"].Value = txtStudentName.Text;
                cmd.Parameters["@d5"].Value = txtCourse.Text;
                cmd.Parameters["@d6"].Value = txtBranch.Text;
                cmd.Parameters["@d7"].Value = txtRollNo.Text;
                cmd.Parameters["@d8"].Value = txtSubjectName.Text;
                cmd.Parameters["@d9"].Value = txtSemester.Text;
                cmd.Parameters["@d10"].Value = dtpExamDate.Text;
                cmd.Parameters["@d11"].Value = Convert.ToInt32(txtMinMarks.Text);
                cmd.Parameters["@d12"].Value = Convert.ToInt32(txtMaxMarks.Text);
                cmd.Parameters["@d13"].Value = Convert.ToInt32(txtObtainedMarks.Text);
                cmd.ExecuteReader();


                con.Close();
                MessageBox.Show("Successfully Updated", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnUpdate_record.Enabled = false;
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

     
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete this record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();


            }
        }

        private void txtMinMarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtMaxMarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void txtObtainedMarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cmbScholarNo.Text == "")
            {
                MessageBox.Show("Please select scholar no.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbScholarNo.Focus();
                return;
            }
            if (cmbSubjectCode.Text == "")
            {
                MessageBox.Show("Please select subject code", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSubjectCode.Focus();
                return;
            }
            if (cmbExam.Text == "")
            {
                MessageBox.Show("Please select exam", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbExam.Focus();
                return;
            }
            if (label13.Text == "Admin")
            {
                btnDelete.Enabled = true;
                btnUpdate_record.Enabled = true;
            }
            else
            {
                btnDelete.Enabled = false;
                btnUpdate_record.Enabled = false;
            }
            try
            {
                con = new SqlConnection(cs);

                con.Open();
                cmd = con.CreateCommand();

                cmd.CommandText = "SELECT MaxMarks,ExamDate,MinMarks,MarksObtained from InternalMarksEntry where ScholarNo = '" + cmbScholarNo.Text + "' and SubjectCode = '" + cmbSubjectCode.Text + "' and Exam = '" + cmbExam.Text + "'";
                rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    txtMaxMarks.Text = rdr.GetInt32(0).ToString();
                    dtpExamDate.Text = (String)rdr["ExamDate"];
                    txtMinMarks.Text = rdr.GetInt32(2).ToString();
                    txtObtainedMarks.Text = rdr.GetInt32(3).ToString();

                }
                else
                {
                    MessageBox.Show("Sorry..No Record found", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

      
        }
    }

