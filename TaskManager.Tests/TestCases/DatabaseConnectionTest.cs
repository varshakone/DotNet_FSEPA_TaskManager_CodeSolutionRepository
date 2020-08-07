using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DataLayer;
using TaskManager.Entities;
using TaskManager.Test.Utility;
using Xunit;

namespace TaskManager.Test.TestCases
{
    [Collection("parallel")]
    public  class DatabaseConnectionTest
    {
        private Mock<IOptions<MongoSettings>> _mockOptions;
        private Mock<IMongoDatabase> _mockDB;
        private Mock<IMongoClient> _mockClient;
        static FileUtility fileUtility;
        IConfigurationRoot config;

        public DatabaseConnectionTest()
        {
            _mockOptions = new Mock<IOptions<MongoSettings>>();
            _mockDB = new Mock<IMongoDatabase>();
            _mockClient = new Mock<IMongoClient>();

            config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
        }

        static DatabaseConnectionTest()
        {
            fileUtility = new FileUtility();
            fileUtility.FilePath = "../../../../output_batabase_revised.txt";
            fileUtility.CreateTextFile();
        }
        [Fact]
        public void MongoBookDBContext_Constructor_Success()
        {
            try
            {
                var settings = new MongoSettings()
                {
                    Connection = "mongodb://user:password@127.0.0.1:27017/guestbook",
                    DatabaseName = "guestbook"
                };
                _mockOptions.Setup(s => s.Value).Returns(settings);
                _mockClient.Setup(c => c
                .GetDatabase(_mockOptions.Object.Value.DatabaseName, null))
                    .Returns(_mockDB.Object);

                //Action
                var context = new MongoDBContext(_mockOptions.Object);
                if(context != null)
                {
                    string testResult = "MongoBookDBContext_Constructor_Success=" + "True";

                    // Write test case result in text file
                    fileUtility.WriteTestCaseResuItInText(testResult);

                                

                    }
                //Assert 
                Assert.NotNull(context);
            }
            catch(Exception ex)
            {
                string testResult = "MongoBookDBContext_Constructor_Success=" + "False";

                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);
            }
        }


        [Fact]
        public void MongoBookDBContext_GetCollection_ValidName_Success()
        {
            try
            {
                //Arrange
                var settings = new MongoSettings()
                {
                    Connection = "mongodb://localhost:27017",
                    DatabaseName = "TaskManagerDB"
                };

                _mockOptions.Setup(s => s.Value).Returns(settings);

                _mockClient.Setup(c => c.GetDatabase(_mockOptions.Object.Value.DatabaseName, null)).Returns(_mockDB.Object);

                // Action
                var context = new MongoDBContext(_mockOptions.Object);
                var myCollection = context.GetCollection<TaskItem>("TaskItems");
                if(myCollection !=null)
                {
                    string testResult = "MongoBookDBContext_GetCollection_ValidName_Success=" + "True";

                    // Write test case result in text file
                    fileUtility.WriteTestCaseResuItInText(testResult);
                }
                //Assert 
                Assert.NotNull(myCollection);
            }
            catch(Exception ex)
            {
                string testResult = "MongoBookDBContext_GetCollection_ValidName_Success=" + "False";

                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);
            }
        }
    }
}
