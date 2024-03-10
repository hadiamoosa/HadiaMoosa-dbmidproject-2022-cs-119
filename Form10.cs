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
    public partial class Form10 : Form
    {
        public Form10()
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
                    string query = "SELECT P.Id AS ProjectId, P.Title, A.Id AS AdvisorId, LAdvisor.Value AS AdvisorDesignation, Pe.FirstName + ' ' + Pe.LastName AS AdvisorName, S.Id AS StudentId FROM Project AS P INNER JOIN ProjectAdvisor AS PA ON P.Id = PA.ProjectId INNER JOIN Advisor AS A ON PA.AdvisorId = A.Id INNER JOIN Person AS Pe ON A.Id = Pe.Id LEFT JOIN GroupStudent AS GS ON P.Id = GS.GroupId LEFT JOIN Student AS S ON GS.StudentId = S.Id LEFT JOIN Person AS PeS ON S.Id = PeS.Id LEFT JOIN Lookup AS LAdvisor ON A.Designation = LAdvisor.Id;";

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
