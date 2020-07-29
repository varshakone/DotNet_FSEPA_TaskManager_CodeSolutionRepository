using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.BusinessLayer.Services;
using TaskManager.DataLayer;
using TaskManager.Entities;
using TaskManager.Test.Utility;
using Xunit;
using TaskStatus = TaskManager.Entities.TaskStatus;

namespace TaskManager.Test.TestCases
{
    [CollectionDefinition("parallel", DisableParallelization = false)]
    public  class BoundaryTest
    {
        IConfigurationRoot config;
        private Mock<IMongoCollection<TaskItem>> _mockCollection;
        private Mock<IMongoDBContext> _mockContext;
        private Mock<IOptions<MongoSettings>> _mockOptions;
        private MongoDBContext context;
        private Mock<IMongoCollection<TaskGroup>> _mockCollectionGroup;

        private TaskItem taskItem;
        private TaskGroup taskGroup;

        private String testResult;

       static FileUtility fileUtility;
        static BoundaryTest()
        {
             fileUtility = new FileUtility();
            fileUtility.FilePath = "../../../../output_boundary_revised.txt";
            fileUtility.CreateTextFile();
        }
        public BoundaryTest()
        {
            taskGroup = new TaskGroup
            {
                GroupName = "Office Task",
                Active ="Yes",
                Color ="purple"

            };
            taskItem = new TaskItem
            {
                Name = "Development",
                Priority = TaskPriority.High,
                TaskStatus = TaskStatus.Yet_To_Start,
                TaskStartDate = DateTime.Now,
                TaskEndDate = DateTime.Now.AddDays(5),
                TaskGroup ="Office Task",
                TaskColorCode = "purple"
           };

            MongoDBUtility mongoDBUtility = new MongoDBUtility();
            _mockContext = mongoDBUtility.MockContext;
            _mockCollection = mongoDBUtility.MockCollection;
            _mockOptions = mongoDBUtility.MockOptions;
            context = mongoDBUtility.MongoDBContext;
          

            config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
        }
      
       // validate task start date is provided or not
       [Fact]
        public async Task BoundaryTestFor_TaskStartDateRequiredAsync()
        {
            try
            {
                if(taskItem.TaskStartDate != null)
                {
                    _mockCollection.Setup(op => op.InsertOneAsync(taskItem, null,
                    default(CancellationToken))).Returns(Task.CompletedTask);
                    _mockContext.Setup(c => c.GetCollection<TaskItem>(typeof(TaskItem).Name)).Returns(_mockCollection.Object);
                    var taskService = new TaskService(context);

                    //Action
                    var result = taskService.NewTask(taskItem);
                 
                    if(result == "New Task Added")
                    {
                    
                        testResult = "BoundaryTestFor_TaskStartDate_Required="+ "True";

                        // Write test case result in text file
                        fileUtility.WriteTestCaseResuItInText(testResult);

                        // Write test case result in xml file
                        if (config["env"] == "development")
                        {
                            cases newcase = new cases
                            {
                                TestCaseType = "Boundary",
                                Name = "BoundaryTestFor_TaskStartDate_Required",
                                expectedOutput = "True",
                                weight = 2,
                                mandatory = "True",
                                desc = "na"
                            };
                            await new FileUtility().WriteTestCaseResuItInXML(newcase);
                        }
                    }
                }
                else
                {
                    // Assert 
                    Assert.NotEmpty(taskItem.TaskStartDate.ToShortDateString());
                }

            }
            catch(Exception exception)
            {
                var res = exception;
                testResult = "BoundaryTestFor_TaskStartDate_Required=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_TaskStartDate_Required",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }

            }
        }

        [Fact]
        public async Task BoundaryTestFor_TaskName_Required()
        {
            try
            {
                if (taskItem.Name != null)
                {
                    _mockCollection.Setup(op => op.InsertOneAsync(taskItem, null,
                    default(CancellationToken))).Returns(Task.CompletedTask);
                    _mockContext.Setup(c => c.GetCollection<TaskItem>(typeof(TaskItem).Name)).Returns(_mockCollection.Object);
                    var taskService = new TaskService(context);

                    //Action
                    var result = taskService.NewTask(taskItem);
                 
                    if (result == "New Task Added")
                    {
                     
                        testResult = "BoundaryTestFor_TaskName_Required=" + "True";

                        // Write test case result in text file
                        fileUtility.WriteTestCaseResuItInText(testResult);

                        // Write test case result in xml file
                        if (config["env"] == "development")
                        {
                            cases newcase = new cases
                            {
                                TestCaseType = "Boundary",
                                Name = "BoundaryTestFor_TaskName_Required",
                                expectedOutput = "True",
                                weight = 2,
                                mandatory = "True",
                                desc = "na"
                            };
                            await new FileUtility().WriteTestCaseResuItInXML(newcase);
                        }
                    }
                }
                else
                {
                    // Assert 
                    Assert.NotNull(taskItem.Name);
                }

            }
            catch (Exception exception)
            {
                var res = exception;
                testResult = "BoundaryTestFor_TaskName_Required=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_TaskName_Required=",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }

            }
        }

        [Fact]
        public async Task BoundaryTestFor_TaskEndDate_Required()
        {
            try
            {
                if (taskItem.TaskEndDate != null)
                {
                    _mockCollection.Setup(op => op.InsertOneAsync(taskItem, null,
                    default(CancellationToken))).Returns(Task.CompletedTask);
                    _mockContext.Setup(c => c.GetCollection<TaskItem>(typeof(TaskItem).Name)).Returns(_mockCollection.Object);
                    var taskService = new TaskService(context);

                    //Action
                    var result = taskService.NewTask(taskItem);
                 
                    if (result == "New Task Added")
                    {
                     
                        testResult = "BoundaryTestFor_TaskEndDate_Required=" + "True";

                        // Write test case result in text file
                        fileUtility.WriteTestCaseResuItInText(testResult);

                        // Write test case result in xml file
                        if (config["env"] == "development")
                        {
                            cases newcase = new cases
                            {
                                TestCaseType = "Boundary",
                                Name = "BoundaryTestFor_TaskEndDate_Required",
                                expectedOutput = "True",
                                weight = 2,
                                mandatory = "True",
                                desc = "na"
                            };
                            await new FileUtility().WriteTestCaseResuItInXML(newcase);
                        }
                    }
                }
                else
                {
                    // Assert 
                    Assert.NotEmpty(taskItem.TaskEndDate.ToShortDateString());
                }

            }
            catch (Exception exception)
            {
                var res = exception;
                testResult = "BoundaryTestFor_TaskEndDate_Required=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_TaskEndDate_Required",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }

            }
        }

        [Fact]
        public async Task BoundaryTestFor_TaskEndDateGreaterThanTaskStartDate()
        {
            try
            {
                if (taskItem.TaskStartDate < taskItem.TaskEndDate)
                {
                    _mockCollection.Setup(op => op.InsertOneAsync(taskItem, null,
                    default(CancellationToken))).Returns(Task.CompletedTask);
                    _mockContext.Setup(c => c.GetCollection<TaskItem>(typeof(TaskItem).Name)).Returns(_mockCollection.Object);
                    var taskService = new TaskService(context);

                    //Action
                    var result = taskService.NewTask(taskItem);
                 
                    if (result == "New Task Added")
                    {
                   
                        testResult = "BoundaryTestFor_TaskEndDateGreaterThanTaskStartDate=" + "True";

                        // Write test case result in text file
                        fileUtility.WriteTestCaseResuItInText(testResult);

                        // Write test case result in xml file
                        if (config["env"] == "development")
                        {
                            cases newcase = new cases
                            {
                                TestCaseType = "Boundary",
                                Name = "BoundaryTestFor_TaskEndDateGreaterThanTaskStartDate",
                                expectedOutput = "True",
                                weight = 2,
                                mandatory = "True",
                                desc = "na"
                            };
                            await new FileUtility().WriteTestCaseResuItInXML(newcase);
                        }
                    }
                }
                else
                {
                    // Assert 
                    throw new Exception();
                }

            }
            catch (Exception exception)
            {
                var res = exception;
                testResult = "BoundaryTestFor_TaskEndDateGreaterThanTaskStartDate=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_TaskEndDateGreaterThanTaskStartDate",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }

            }
        }

        [Fact]
        public async Task BoundaryTestFor_TaskColorCode_Require()
        {
            try
            {
                if (taskItem.TaskColorCode !=null)
                {
                    _mockCollection.Setup(op => op.InsertOneAsync(taskItem, null,
                    default(CancellationToken))).Returns(Task.CompletedTask);
                    _mockContext.Setup(c => c.GetCollection<TaskItem>(typeof(TaskItem).Name)).Returns(_mockCollection.Object);
                    var taskService = new TaskService(context);

                    //Action
                    var result = taskService.NewTask(taskItem);
                 
                    if (result == "New Task Added")
                    {
                      
                        testResult = "BoundaryTestFor_TaskColorCode_Require=" + "True";

                        // Write test case result in text file
                        fileUtility.WriteTestCaseResuItInText(testResult);

                        // Write test case result in xml file
                        if (config["env"] == "development")
                        {
                            cases newcase = new cases
                            {
                                TestCaseType = "Boundary",
                                Name = "BoundaryTestFor_TaskColorCode_Require",
                                expectedOutput = "True",
                                weight = 2,
                                mandatory = "True",
                                desc = "na"
                            };
                            await new FileUtility().WriteTestCaseResuItInXML(newcase);
                        }
                    }
                }
                else
                {
                    // Assert 
                    Assert.NotNull(taskItem.TaskColorCode);
                }

            }
            catch (Exception exception)
            {
                var res = exception;
                testResult = "BoundaryTestFor_TaskColorCode_Require=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_TaskColorCode_Require",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }

            }
        }

        [Fact]
        public async Task BoundaryTestFor_TaskGroup_Require()
        {
            try
            {
                if (taskItem.TaskGroup!= null)
                {
                    _mockCollection.Setup(op => op.InsertOneAsync(taskItem, null,
                    default(CancellationToken))).Returns(Task.CompletedTask);
                    _mockContext.Setup(c => c.GetCollection<TaskItem>(typeof(TaskItem).Name)).Returns(_mockCollection.Object);
                    var taskService = new TaskService(context);

                    //Action
                    var result = taskService.NewTask(taskItem);
                    
                    if (result == "New Task Added")
                    {
                      
                        testResult = "BoundaryTestFor_TaskGroup_Require=" + "True";

                        // Write test case result in text file
                        fileUtility.WriteTestCaseResuItInText(testResult);

                        // Write test case result in xml file
                        if (config["env"] == "development")
                        {
                            cases newcase = new cases
                            {
                                TestCaseType = "Boundary",
                                Name = "BoundaryTestFor_TaskGroup_Require",
                                expectedOutput = "True",
                                weight = 2,
                                mandatory = "True",
                                desc = "na"
                            };
                            await new FileUtility().WriteTestCaseResuItInXML(newcase);
                        }
                    }
                }
                else
                {
                    // Assert 
                    Assert.NotNull(taskItem.TaskGroup);
                }

            }
            catch (Exception exception)
            {
                var res = exception;
                testResult = "BoundaryTestFor_TaskGroup_Require=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_TaskGroup_Require",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }

            }
        }

        [Fact]
        public async Task BoundaryTestFor_GroupName_Require()
        {
            try
            {
                if (taskGroup.GroupName != null)
                {
                    MongoDBUtility mongoDBUtility = new MongoDBUtility(new TaskGroup());
                    _mockContext = mongoDBUtility.MockContext;
                    _mockCollectionGroup = mongoDBUtility.MockCollectionGroup;
                    _mockOptions = mongoDBUtility.MockOptions;
                    context = mongoDBUtility.MongoDBContext;


                    _mockCollectionGroup.Setup(op => op.InsertOneAsync(taskGroup, null,
                    default(CancellationToken))).Returns(Task.CompletedTask);
                    _mockContext.Setup(c => c.GetCollection<TaskGroup>(typeof(TaskGroup).Name)).Returns(_mockCollectionGroup.Object);
                    var taskService = new TaskService(context);

                    //Action
                    var result = taskService.NewTaskGroup(taskGroup);
                    
                    if (result == "New Group Added")
                    {
                      
                        testResult = "BoundaryTestFor_GroupName_Require=" + "True";

                        // Write test case result in text file
                        fileUtility.WriteTestCaseResuItInText(testResult);

                        // Write test case result in xml file
                        if (config["env"] == "development")
                        {
                            cases newcase = new cases
                            {
                                TestCaseType = "Boundary",
                                Name = "BoundaryTestFor_GroupName_Require",
                                expectedOutput = "True",
                                weight = 2,
                                mandatory = "True",
                                desc = "na"
                            };
                            await new FileUtility().WriteTestCaseResuItInXML(newcase);
                        }
                    }
                }
                else
                {
                    // Assert 
                    Assert.NotNull(taskGroup.GroupName);
                }

            }
            catch (Exception exception)
            {
                var res = exception;
                testResult = "BoundaryTestFor_GroupName_Require=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_GroupName_Require",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }

            }
        }

        [Fact]
        public async Task BoundaryTestFor_GroupIsActive_Require()
        {
            try
            {
                if (taskGroup.Active != null)
                {
                    MongoDBUtility mongoDBUtility = new MongoDBUtility(new TaskGroup());
                    _mockContext = mongoDBUtility.MockContext;
                    _mockCollectionGroup = mongoDBUtility.MockCollectionGroup;
                    _mockOptions = mongoDBUtility.MockOptions;
                    context = mongoDBUtility.MongoDBContext;


                    _mockCollectionGroup.Setup(op => op.InsertOneAsync(taskGroup, null,
                    default(CancellationToken))).Returns(Task.CompletedTask);
                    _mockContext.Setup(c => c.GetCollection<TaskGroup>(typeof(TaskGroup).Name)).Returns(_mockCollectionGroup.Object);
                    var taskService = new TaskService(context);

                    //Action
                    var result = taskService.NewTaskGroup(taskGroup);
                   
                    if (result == "New Group Added")
                    {
                      
                        testResult = "BoundaryTestFor_GroupIsActive_Require=" +"True";

                        // Write test case result in text file
                        fileUtility.WriteTestCaseResuItInText(testResult);

                        // Write test case result in xml file
                        if (config["env"] == "development")
                        {
                            cases newcase = new cases
                            {
                                TestCaseType = "Boundary",
                                Name = "BoundaryTestFor_GroupIsActive_Require",
                                expectedOutput = "True",
                                weight = 2,
                                mandatory = "True",
                                desc = "na"
                            };
                            await new FileUtility().WriteTestCaseResuItInXML(newcase);
                        }
                    }
                }
                else
                {
                    // Assert 
                    Assert.NotNull(taskGroup.Active);
                }

            }
            catch (Exception exception)
            {
                var res = exception;
                testResult = "BoundaryTestFor_GroupIsActive_Require=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_GroupIsActive_Require",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }

            }
        }

        [Fact]
        public async Task BoundaryTestFor_GroupColor_Require()
        {
            try
            {
                if (taskGroup.Active != null)
                {
                    MongoDBUtility mongoDBUtility = new MongoDBUtility(new TaskGroup());
                    _mockContext = mongoDBUtility.MockContext;
                    _mockCollectionGroup = mongoDBUtility.MockCollectionGroup;
                    _mockOptions = mongoDBUtility.MockOptions;
                    context = mongoDBUtility.MongoDBContext;


                    _mockCollectionGroup.Setup(op => op.InsertOneAsync(taskGroup, null,
                    default(CancellationToken))).Returns(Task.CompletedTask);
                    _mockContext.Setup(c => c.GetCollection<TaskGroup>(typeof(TaskGroup).Name)).Returns(_mockCollectionGroup.Object);
                    var taskService = new TaskService(context);

                    //Action
                    var result = taskService.NewTaskGroup(taskGroup);
                   
                    if (result == "New Group Added")
                    {
                        
                        testResult = "BoundaryTestFor_GroupColor_Require=" + "True";

                        // Write test case result in text file
                        fileUtility.WriteTestCaseResuItInText(testResult);

                        // Write test case result in xml file
                        if (config["env"] == "development")
                        {
                            cases newcase = new cases
                            {
                                TestCaseType = "Boundary",
                                Name = "BoundaryTestFor_GroupColor_Require",
                                expectedOutput = "True",
                                weight = 2,
                                mandatory = "True",
                                desc = "na"
                            };
                            await new FileUtility().WriteTestCaseResuItInXML(newcase);
                        }
                    }
                }
                else
                {
                    // Assert 
                    Assert.NotNull(taskGroup.Color);
                }

            }
            catch (Exception exception)
            {
                var res = exception;
                testResult = "BoundaryTestFor_GroupColor_Require=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Boundary",
                        Name = "BoundaryTestFor_GroupColor_Require",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }

            }
        }
    }
}
