using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalProject_FallSemester;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FinalProject_FallSemester.Tests
{
    [TestClass()]
    public class Form1Tests
    {
        [TestMethod()]
        public void clearBoxTest()
        {
            //TODO Finish test
        }

        [TestMethod()]
        public void LoadCustomerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void VerifyCustomerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void LoadDrinkTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CustomerIDTest()
        {
        }
        [TestMethod()]
        public void MealsTest()
        {
            // Arrange
            string meals = "Salad";

            // Act
            bool actual = meals.VegDetails();

            // Assert
            const bool expected = true;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LoadStatesTest()

        {
            // Arrange
            African_Restaurent appObject = new African_Restaurent();
           

            string[] actualStringArray = new string[] {  "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY", "AS", "DC", "FM", "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME","MN", "MD", "MA", "RI", "SC", "SD" };


            string expectedResult = " TN TX UT VT VA WA WV WI WY AS DC FM AL AK AZ AR CA CO CT DE FL GA HI ID IL IN IA KS KY LA ME MN MD MA RI SC SD ";


            // Act
            string actualResult = appObject.LoadStates(actualStringArray);

            // Assert
            Assert.AreEqual<string>(expectedResult, actualResult);

        }

        [TestMethod()]
        public void LoadCustomerTest1( )
        {

           

        }

        [TestMethod()]
        public void FinalProject_FallSemesterIsSaladMealTest()
        {
            // Arrange
            string meals = "Salad";

            // Act
            bool actual = meals.VegDetails();

            // Assert
            const bool expected = true;
            Assert.AreEqual(expected, actual);
        }
    }
}