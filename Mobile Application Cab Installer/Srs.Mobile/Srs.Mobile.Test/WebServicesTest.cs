using Srs.Mobile.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Srs.Mobile.WebReference;

namespace Srs.Mobile.Test
{
    
    
    /// <summary>
    ///This is a test class for WebServicesTest and is intended
    ///to contain all WebServicesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WebServicesTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for LoginSucess
        ///</summary>
        [TestMethod()]
        public void LoginSucessTest()
        {
            WebServices target = new WebServices(); // Initialize the web service
            string username = "admin"; // username
            string password = "password"; // password
            int expected = 3; // method should return the user type which is 3
            int actual;
            actual = target.LoginSucess(username, password);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetAisleList
        ///</summary>
        [TestMethod()]
        public void GetAisleListTest()
        {
            WebServices target = new WebServices(); //Initialize the web service
            int expected = 6; // There are 6 Aisles defined in the database
            Aisles[] actual;
            actual = target.GetAisleList();
            Assert.AreEqual(expected, actual.Length);
        }
    }
}
