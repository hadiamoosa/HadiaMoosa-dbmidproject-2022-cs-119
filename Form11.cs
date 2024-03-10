using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mid_Project
{
    public partial class Form11 : Form
    {
        public Form11()
        {
            InitializeComponent();
            LoadDataIntoDataGridView();
        }
        private void LoadDataIntoDataGridView()
        {
            String ConnectionStr = @"Data Source=(local);Initial Catalog=ProjectA;Integrated Security=True";
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionStr))
                {
                    connection.Open();

                    // Replace "YourQuery" with your actual SQL query
                    string query = "SELECT P.Title AS ProjectTitle, S.RegistrationNo AS StudentRegistrationNo, PeS.FirstName + ' ' + PeS.LastName AS StudentName, E.Name AS EvaluationName, GE.ObtainedMarks, GE.EvaluationDate FROM Project AS P JOIN GroupProject AS GP ON P.Id = GP.ProjectId JOIN GroupStudent AS GS ON GP.GroupId = GS.GroupId JOIN Student AS S ON GS.StudentId = S.Id JOIN Person AS PeS ON S.Id = PeS.Id JOIN GroupEvaluation AS GE ON GS.GroupId = GE.GroupId JOIN Evaluation AS E ON GE.EvaluationId = E.Id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }

    }
}
