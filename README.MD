This project is a windows form application for a restaurant business that can allow users (customers) to place orders online and enables the restaurant management to keep track of every transaction.  The  GUI is designed, using a combination of Text Boxes, Labels, Combo Boxes, Check Boxes, Picture Boxe, Buttons, Panels, DataGridView and more. The application uses two forms. On the first form, the customer would be identified either as a regular or new customer. New customers would be required to enter the following personal information: First Name, Last Name, Email, Address, State, and Zip Code.
Customer information would be used to generate a customerID and TransactionID for the customer. The customer would now have access to the Menu comprising of Meals and Drinks. A customer can remove any menu or item from the list selected by mistake. A customer can view their order total before placing order. To view their total order, customers would be required to enter their customerID and TransactionID. This takes us to the second form (second form enabled).
     On the second form, the DataGridView would be enabled to allow the customers to view their order. If the customer is satisfied with their order, they would go ahead and hit a place order button. A message would pop up telling the customer whether their transaction was successful or not. A dialog box would now be opened giving the customer the options to Exit or continue placing order. If the customer chooses to exit, another dialog box would open that requires the customer to say whether they enjoyed the services or not?  If the customer chooses NO, a message would pop up telling the customer “Sorry, our priority is customer satisfaction! We will take care of that next time”. If the customer chooses YES, a message would pop up telling the customer “Thanks for doing business with us! We hope you come again soon!”


An SQL connection is established to the Restaurant DB created in Microsoft SQL Server Management Studio, then you could create a SQL command object that uses a connection object to connect to the database (Communicate with the database from the user interface).


    In Microsoft SQL Server Management Studio,  a relational database with the following entities (tables or relations) is designed and implemented:

-	Customers
-	Drinks
-	Meals
-	Transactions
-	TrackID

The customer table contains the following attributes (columns):
-	First Name,
-	Last Name, 
-	Email
-	Address
-	State
-	Zip Code
 Records are read from the database to a datareader
 then assign the datareader results to the gridview
