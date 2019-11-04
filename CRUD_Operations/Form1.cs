using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CRUD_Operations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        SqlConnection con = new SqlConnection(@"Data Source=FPQD0I;Initial Catalog=SolutionsTest;Integrated Security=True");
        public int StudentID;

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStudentsRecord();
        }

        private void GetStudentsRecord()
        {
            var cmd = new SqlCommand("Select * from StudentTb", con);
            var dt = new DataTable();

            con.Open();

            var sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            StudentRecordDataGridView.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsValid())
            {
                var cmd = new SqlCommand("INSERT INTO StudentTb VALUES (@name, @Surname, @Roll, @Address, @Mobile)",
                    con) {CommandType = CommandType.Text};
                cmd.Parameters.AddWithValue("@name", txtStudentsName.Text);
                cmd.Parameters.AddWithValue("@Surname", txtSurname.Text);
                cmd.Parameters.AddWithValue("@Roll", txtStudentNumber.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("New student is successfully saved in the database","Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsRecord();

                ResetFormControls();
            }
        }

        private bool IsValid()
        {
            if (txtStudentsName.Text == string.Empty)
            {
                MessageBox.Show("Student Name is required", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ResetFormControls();
        }

        private void ResetFormControls()
        {
            StudentID = 0;
            txtStudentsName.Clear();
            txtSurname.Clear();
            txtStudentNumber.Clear();
            txtMobile.Clear();
            txtAddress.Clear();

            txtStudentsName.Focus();
        }

        private void StudentRecordDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            StudentID = Convert.ToInt32(StudentRecordDataGridView.SelectedRows[0].Cells[0].Value);
            txtStudentsName.Text = StudentRecordDataGridView.SelectedRows[0].Cells[1].Value.ToString();
            txtSurname.Text = StudentRecordDataGridView.SelectedRows[0].Cells[2].Value.ToString();
            txtStudentNumber.Text = StudentRecordDataGridView.SelectedRows[0].Cells[3].Value.ToString();
            txtAddress.Text = StudentRecordDataGridView.SelectedRows[0].Cells[4].Value.ToString();
            txtMobile.Text = StudentRecordDataGridView.SelectedRows[0].Cells[5].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (StudentID > 0)
            {
                var cmd = new SqlCommand(
                    "UPDATE StudentTb SET Name = @Name, Surname = @Surname, RollNumber =  @Roll, Address = @Address, Mobile = @Mobile WHERE StudentID = @ID",
                    con) {CommandType = CommandType.Text};
                cmd.Parameters.AddWithValue("@name", txtStudentsName.Text);
                cmd.Parameters.AddWithValue("@Surname", txtSurname.Text);
                cmd.Parameters.AddWithValue("@Roll", txtStudentNumber.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@ID", this.StudentID);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Information is updated successfully","Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsRecord();
                ResetFormControls();
            }

            else
            {
                MessageBox.Show("Please select a student to update their information ","Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(StudentID > 0)
            {
                var cmd = new SqlCommand("DELETE FROM StudentTb WHERE StudentID = @ID", con)
                {
                    CommandType = CommandType.Text
                };

                cmd.Parameters.AddWithValue("@ID", this.StudentID);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student is deleted from the system","Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetStudentsRecord();
                ResetFormControls();
            }
            else
            {
                MessageBox.Show("Please select a student to delete ","Select?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

    }
}
