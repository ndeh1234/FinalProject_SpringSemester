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
     * Author: Ndeh Khan
     * Project: Final Project
     * Class: C# / Capstone
     * Title: African Restaurant
     * Presentation date: 12/19/2019
     * */
    public partial class African_Restaurent : Form
    {  
        // Declaring  SQL Server objects or instances
        public SqlConnection connection;
        public SqlCommand command;
        public SqlDataAdapter ad;
        public SqlDataReader read;
        public DataSet dataset;
        string item = string.Empty;

        // Declaring and innitializing variables
        string Price = "0";
        int min, max, a;
        public Random rand = new Random();
        public African_Restaurent()
        {
            InitializeComponent();
        }
            // Loads Form
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {   //  Extablishes an SQL connection to the RESTAURANT_DB database
                connection = new SqlConnection("Server= LAPTOP-S3ATB7HT\\SQLEXPRESS; Database=RESTAURANT_DB; Integrated Security=True;");
                rbtNew.Checked = false;
                rbtRegister.Checked = false;
                this.LoadCustomer();
                this.LoadStates();
                this.VerifyCustomer();
                this.Drinks(); this.Meals(); this.Vegetables();
            }
            catch (Exception ex) // Catches any general exception thrown
            {
                MessageBox.Show(ex.Message);
            }
        }

        //  Clears the text boxes on the register group box and sets the index specifying the current selected item
        public void clearBox()
        {
            txtFName.Clear(); txtLName.Clear();
            txtAddress.Clear(); txtEmail.Clear();

            cmbState.SelectedIndex = -1;
            cmbDrink.SelectedIndex = -1;
            cmbMeal.SelectedIndex = -1;
            cmbVeg.SelectedIndex = -1;
            txtFName.Focus();

        }

        // creates a SQL command object that uses a connection object to connect to the database and select rows from Customers
        public void LoadCustomer()
        {
            //connection.Open(); // Opens connection
            command = new SqlCommand("Select CustomerID AS ID, LName+ ' '+ FName AS Customer From Customers", connection);
            ad = new SqlDataAdapter(command);
            dataset = new DataSet();
            dataset.Clear();
            
            ad.Fill(dataset, "Customers");
            dataGridView1.DataSource = dataset.Tables["Customers"];
            //connection.Close();
        }

        // creates a SQL command object that uses a connection object to connect to the database and verifies customer id
        public void VerifyCustomer()
        {
            connection.Open(); // Opens connection
            command = new SqlCommand("Select CustomerID From Customers", connection);
            read = command.ExecuteReader();
            cmbVerify.Items.Clear();
            while(read.Read())
            {
                cmbVerify.Items.Add(read.GetInt32(0));
            }
            cmbVerify.SelectedIndex = -1;
            connection.Close();
        }

        public void LoadDrink()
        {
            connection.Open();
            command = new SqlCommand("Select Description From Drinks", connection);

            dataset = new DataSet();
            dataset.Clear();
            ad = new SqlDataAdapter(command);
            ad.Fill(dataset);
            //cmbDrink.DataSource = dataset.Tables[0];

            //cmbDrink.ValueMember = "Description";
            //cmbDrink.DisplayMember = "Description";

            //cmbDrink.SelectedIndex = -1;

            connection.Close();
        }

        // Event handler for the register radio button
        private void rbtRegister_CheckedChanged(object sender, EventArgs e)
        {
                // Enables the register radio button when checked and disables all others controls on form
                if (rbtRegister.Checked.Equals(true))
                {
                this.clearBox();
                panelOld.Enabled = true;
                grbRegister.Enabled = false;
                grbMenu.Enabled = false;
                }
        }  

        // Event handler for the new custmers rabio button
        private void rbtNew_CheckedChanged(object sender, EventArgs e)
        {
        // Enables the the new custmers rabio button when checked and disables all others controls on form
            if (rbtNew.Checked.Equals(true))
            {
                this.clearBox();
                panelOld.Enabled = false;
                grbMenu.Enabled = false;
                grbRegister.Enabled = true;
            }
        }


        // Event handler for the exit button click event
        private void button3_Click(object sender, EventArgs e)
        {

            // Displays a dialog box and gets the user response
            string msg = "Thank You for doing business with us!\n Did you enjoy our services?";
            DialogResult button =
            MessageBox.Show(msg, "Dear Customer",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Information);


            // Statements that check user responsees
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


        //Event handler for the drink combo boxes
        private void cmbDrink_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.DrinKDetails();
                listMenu1.Items.Add(cmbDrink.SelectedItem);
                listMenu2.Items.Add("Drk");
                listMenu3.Items.Add(Price);
                panelDrink.Visible = true;
                panelMeal.Visible = false;
                panelVeg.Visible = false;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().ToString() + " " + ex.Message);
            }
        }

        // Event handler for the meal combo box
        private void cmbMeal_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.MealDetails();
                listMenu1.Items.Add(cmbMeal.SelectedItem);
                listMenu2.Items.Add("Ml");
                listMenu3.Items.Add(Price);

                panelDrink.Visible = false;
                panelMeal.Visible = true;
                panelVeg.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().ToString() + " " + ex.Message);
            }
        }

        // EVent handler for the vegetables combo box
        private void cmbVeg_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.VegDetails();
                
                listMenu1.Items.Add(cmbVeg.SelectedItem);
                listMenu2.Items.Add("Veg");
                listMenu3.Items.Add(Price);

                panelDrink.Visible = false;
                panelMeal.Visible = false;
                panelVeg.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetType().ToString() + " " + ex.Message);
            }
        }

        // Event Handler for the register button click event
        private void button1_Click(object sender, EventArgs e)
        {
             // creates a SQL command object that uses a connection to connect to the database and insert data into Customers

            // Try block
            try
            {
                connection.Open(); // opens connection
                command = new SqlCommand("INSERT INTO CUSTOMERS(FNAME, LNAME, EMAIL, [ADDRESS], [STATE], ZIPCODE) VALUES('" + txtFName.Text + "', '" + txtLName.Text + "', '" + txtEmail.Text + "','" + txtAddress.Text + "', '" + cmbState.SelectedItem + "', '" + txtZip.Text + "')", connection);

                command.ExecuteNonQuery();
                this.LoadCustomer();
                grbMenu.Enabled = true;
                grbRegister.Enabled = false;
                rbtRegister.Checked = false;
                
            }
            catch(SqlException ex) // Catches any SQL exception thrown
            {
                MessageBox.Show(ex.GetType().ToString() + " " + ex.Message);
            }
            catch(Exception ex) // Catches any general exception thrown
            {
                MessageBox.Show(ex.GetType().ToString() + " " + ex.Message);
            }
            finally
            {
                connection.Close(); // Closes connection
                
            }
            this.VerifyCustomer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmbVerify.SelectedIndex == -1)
            {
                MessageBox.Show("Select the Customer ID from the Dropdown combobox", "INVALID OPERATION!");
            }
            else
            {
                lblNewID.Text = this.CustomerID().ToString();
                grbMenu.Enabled = true;   // Enables the menu group box
            }
        }


        //Event handler for reset button click event
        private void button5_Click(object sender, EventArgs e)
        {
            this.ResetControls();
           //this.clearBox();


        }

        private void grbRegister_Enter(object sender, EventArgs e)
        {

        }


        // Event handler for the remove button
        private void button6_Click(object sender, EventArgs e)
        {
           // Deletes any selected rows from the list boxes at a specific index
            int i = listMenu1.SelectedIndex;
            listMenu1.Items.RemoveAt(i);
            listMenu2.Items.RemoveAt(i);
            listMenu3.Items.RemoveAt(i);
        }


        // Method that defines range for the customer Id
        public int CustomerID()
        {
            min = 1001; max = 10000;
            int customerID = rand.Next(min, max);
            return customerID;
        }

       // Method that associates drinks to prices, and innitializes price for a drink if it's checked 

        public void DrinKDetails()
        {
            if (cmbDrink.SelectedItem.Equals("FANTA")) 
            {
                Price = "2";

            }
            else if (cmbDrink.SelectedItem.Equals("COCA COLA"))
            {
                Price = "2";
            }
            else if (cmbDrink.SelectedItem.Equals("COFFEE"))
            {
                Price = "2.5";
            }
        }

        // Method that associates Vegetables to prices, and innitializes price for a vegetable if it's checked
        public void VegDetails()
        {
            if (cmbVeg.SelectedItem.Equals("CREAMED SPINACH"))
            {
                Price = "20";

            }
            else if (cmbVeg.SelectedItem.Equals("CABBAGE"))
            {
                Price = "20";
            }
            else if (cmbVeg.SelectedItem.Equals("SALAD"))
            {
                Price = "15";
            }
        }

        // Event Handler for the registered member button click event
        private void rbtRegister_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbtRegister.Checked.Equals(true))
            {
                this.clearBox();
                panelOld.Enabled = true;
                grbRegister.Enabled = false;
                grbMenu.Enabled = false;
            }
        }

        // Event Handler for the New Member click button event
        private void rbtNew_CheckedChanged_1(object sender, EventArgs e)
        {
            if (rbtNew.Checked.Equals(true))
            {
                this.clearBox();
                panelOld.Enabled = true;
                grbMenu.Enabled = false;
                grbRegister.Enabled = true;
            }
        }

        // Method that associates meals to prices, and innitializes price for a meal if it's checked
        public void MealDetails()
        {
            if (cmbMeal.SelectedItem.Equals("GOAT BEEF"))
            {
                Price = "45";

            }
            else if (cmbMeal.SelectedItem.Equals("COCONUT RICE"))
            {
                Price = "25";
            }
            else if (cmbMeal.SelectedItem.Equals("PLANTAINS"))
            {
                Price = "30";
            }
        }

        private void cmbVerify_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        // Method that  loads two-letter state abbreviation into the state combo box
        public void LoadStates()
        {
            cmbState.Items.Clear();
            string[] state = { "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY", "AS", "DC", "FM", "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME","MN", "MD", "MA", "RI", "SC", "SD" };

            foreach(string st in state) //loops over a state string arrray and adds items to the state combo box
            {
               
                cmbState.Items.Add(st);
            }
            cmbState.SelectedIndex = -1;
        }

        public void Drinks()
        {
            connection.Open(); // Opens connection
            command = new SqlCommand("Select Description From Drinks", connection);
            read = command.ExecuteReader();
            cmbDrink.Items.Clear();
            while (read.Read())
            {
                cmbDrink.Items.Add(read.GetString(0));
            }
            read.Close();
            cmbDrink.SelectedIndex = -1;
            connection.Close();
        }

        public void Meals()
        {
            connection.Open(); // Opens connection
            command = new SqlCommand("Select Description From Meals", connection);
            read = command.ExecuteReader();
            cmbMeal.Items.Clear();
            while (read.Read())
            {
                cmbMeal.Items.Add(read.GetString(0));
            }
            read.Close();
            cmbMeal.SelectedIndex = -1;
            connection.Close();
        }

        // Event handler for the place order button click event
        private void button4_Click(object sender, EventArgs e)
        {
            
            connection.Open(); 
            try
            {
                for (int i = 0; i < listMenu1.Items.Count; i++)
                {
                    command = new SqlCommand("INSERT INTO [TRANSACTIONS1](TRANID, ItemOrder, Description, Price) VALUES('" + Convert.ToInt32(lblNewID.Text) + "', '" + listMenu1.Items[i].ToString() + "','" + listMenu2.Items[i].ToString() + "', '" + listMenu3.Items[i].ToString() + "')", connection);
                    a = command.ExecuteNonQuery();
                }

                if (a > 0)  // Displays a transaction successful message if there is an insert
                {
                    command = new SqlCommand("INSERT INTO TRACKID1 VALUES('" + Convert.ToInt32(lblNewID.Text) + "', '" + Convert.ToInt32(cmbVerify.SelectedItem) + "')", connection);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Transaction Successful", "ORDER PLACED");
                }
                else
                {          // Displays a transaction fail message if nothing is insert
                    MessageBox.Show("Transaction Failed", "ORDER NOT PLACED");
                }

            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message, "ORDER NOT PLACED");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ORDER NOT PLACED");
            }
            finally
            {
                connection.Close();
            }
            
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            frmOrderTotal order = new frmOrderTotal();
            order.Show();
        }

        //  resets the  list boxe controls, cmbMeal, cmbVeg, and cmbDrink
        public void ResetControls()
        {
            listMenu1.Items.Clear();
            listMenu2.Items.Clear();
            listMenu3.Items.Clear();
           

            cmbMeal.SelectedIndex = 0;
            cmbVeg.SelectedIndex = 0;
            cmbDrink.SelectedIndex = 0;

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }



        // Enable text boxes
        private void EnableTextBoxes()
        {

            //Action function to enable text boxes  
            Action<Control.ControlCollection> func = null;
            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Enabled = false;
                    else
                        func(control.Controls);

                func(controls);
            };
            }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void LoadStatesTest(string[] actualStringArray)
        {
            throw new NotImplementedException();
        }

        public void Vegetables()
        {
            connection.Open(); // Opens connection
            command = new SqlCommand("Select Description From Vegetables", connection);
            read = command.ExecuteReader();
            cmbVeg.Items.Clear();
            while (read.Read())
            {
                cmbVeg.Items.Add(read.GetString(0));
            }
            read.Close();
                
            cmbVeg.SelectedIndex = -1;
            connection.Close();
        }


    }
}
