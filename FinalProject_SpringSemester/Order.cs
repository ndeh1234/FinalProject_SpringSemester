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

namespace FinalProject_FallSemester
{
    /*
    * Author: Ndeh Jhan
    * Project: Final Project
    * Class: C#
    * Title: African Restaurant
    * Presentation date: 5/14/2019
    * */

    public partial class frmOrderTotal : Form

    {
        // Declaring objects or instances
        public SqlConnection connection;
        public SqlCommand command;
        public SqlDataAdapter adapter;
        public DataSet dataset;

        public frmOrderTotal()
        {
            InitializeComponent();
        }

        // Event handler for the view all button
        private void button1_Click(object sender, EventArgs e)
        {
            this.LoadGridview();
        }

        // Event handler for the form order total
        private void frmOrderTotal_Load(object sender, EventArgs e)
        {
            try
            {    // creates a SQL connection object that uses a connection to connect to the database
                connection = new SqlConnection("Server= LAPTOP-S3ATB7HT\\SQLEXPRESS; Database= RESTAURANT_DB; Integrated Security=True;");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Database Error! " + ex.Message, "SQL EXEPTION");
            }
        }

        public void LoadGridview()
        { // creates a SQL command object that uses a connection object to connect to the database and retrieve specific columns and caclculates  group totals

            connection.Open(); 
            command = new SqlCommand("Select TRANID, ItemOrder, sum(Price) AS PRICE From TRANSACTIONS GROUP BY TRANID, ROLLUP(ItemOrder)", connection);
            dataset = new DataSet();
            dataset.Clear();
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dataset, "TRANSACTIONS");
            dataGridView1.DataSource = dataset.Tables[0];
            connection.Close();
        }

        // Event handler for transaction button
        private void btnTransaction_Click(object sender, EventArgs e)
        {

            try
            {
                connection.Open();
                command = new SqlCommand("Select TRANID, ItemOrder, sum(Price) AS PRICE From TRANSACTIONS WHERE TRANID='" + Convert.ToInt32(txtTranID.Text) + "' GROUP BY TRANID, ROLLUP(ItemOrder)", connection);
                dataset = new DataSet();
                dataset.Clear();
                adapter = new SqlDataAdapter(command);
                adapter.Fill(dataset, "TRANSACTIONS");
                dataGridView1.DataSource = dataset.Tables[0];
            }
            catch (FormatException ex)
            { } 
              
            

            connection.Close();
        }

        // Event handler for the exit button
        private void button2_Click(object sender, EventArgs e)
        {

            // Displays a dialog box and gets the user response
            string msg = "Thank You for doing business with us!\n Did you enjoy our services?";
            DialogResult button =
            MessageBox.Show(msg, "Dear Customer",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Information);


            // Statements that check user response
            if (button == DialogResult.No)
            {
                MessageBox.Show("We're sorry ! \n Our priority is customer satisfaction.We'll take care of that next time. ?", "African Restaurant");
                //this.Close();
            }
            if (button == DialogResult.Yes)
            {
                MessageBox.Show("Thank You! \n We Hope you will come Soon!", "African Restaurent");
                this.Close();
            }
        }
    }
}
