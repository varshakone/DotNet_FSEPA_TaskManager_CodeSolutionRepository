using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.BusinessLayer.Interface;
using TaskManager.BusinessLayer.Services;
using TaskManager.BusinessLayer.Services.Repository;
using TaskManager.DataLayer;
using TaskManager.Entities;
using TaskManager.Test.Utility;
using Xunit;
using TaskStatus = TaskManager.Entities.TaskStatus;

namespace TaskManager.Test.TestCases
{
    [Collection("parallel")]
    public class BusinessLogicTest
    {

        // private refernce declaration
        IConfigurationRoot config;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskService _taskService;
        private MongoDBContext context;
        

        private TaskItem taskItem;
        private TaskGroup taskGroup;
        private String testResult;
        static FileUtility fileUtility;


        // Static constructor to create text file
        static BusinessLogicTest()
        {
            fileUtility = new FileUtility();
            fileUtility.FilePath = "../../../../output_business_revised.txt";
            fileUtility.CreateTextFile();
        }

        //Instantiate objects.
        public BusinessLogicTest()
        {
            taskGroup = new TaskGroup
            {
                GroupName = "Office Task",
                Active = "Yes",
                Color = "purple"

            };
            taskItem = new TaskItem
            {
                Name = "Development",
                Priority = TaskPriority.High,
                TaskStatus = TaskStatus.Yet_To_Start,
                TaskStartDate = DateTime.Now,
                TaskEndDate = DateTime.Now.AddDays(5),
                TaskGroup = "Office Task",
                TaskColorCode = "purple"
            };

            MongoDBUtility mongoDBUtility = new MongoDBUtility();
            context = mongoDBUtility.MongoDBContext;
            _taskRepository = new TaskRepository(context);
            _taskService = new TaskService(_taskRepository);

            config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();

        }


        
        /// <summary>
        /// test method to check new task added into database or not
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BusinessTestFor_NewTask_Successed()
        {
            try
            {
                    //_mockCollection.Setup(op => op.InsertOneAsync(taskItem, null,
                    //default(CancellationToken))).Returns(Task.CompletedTask);
                    //_mockContext.Setup(c => c.GetCollection<TaskItem>(typeof(TaskItem).Name)).Returns(_mockCollection.Object);
                    //var taskService = new TaskService(context);

                    //Action
                    var result =await _taskService.NewTask(taskItem);
                    if (result == "New Task Added")
                    {
                      testResult = "BusinessTestFor_NewTask_Successed=" + "True";

                        // Write test case result in text file
                        fileUtility.WriteTestCaseResuItInText(testResult);

                        // Write test case result in xml file
                        if (config["env"] == "development")
                        {
                            cases newcase = new cases
                            {
                                TestCaseType = "Business",
                                Name = "BusinessTestFor_NewTask_Successed",
                                expectedOutput = "True",
                                weight = 2,
                                mandatory = "True",
                                desc = "na"
                            };
                            await new FileUtility().WriteTestCaseResuItInXML(newcase);
                        }
                    }
              else
                {
                    // Assert 
                    Assert.NotNull(result);
                }

            }
            catch (Exception exception)
            {
                var error = exception;
                testResult = "BusinessTestFor_NewTask_Successed=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Business",
                        Name = "BusinessTestFor_NewTask_Successed",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }

            }

        }


        /// <summary>
        /// check whether NewTaskGroup() method in TaskService able to add new group in db
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BusinessTestFor_NewTaskGroup_Successed()
        {
            var result = string.Empty;
            try
            {
                if (taskGroup.GroupName != null)
                {
                    //MongoDBUtility mongoDBUtility = new MongoDBUtility(new TaskGroup());
                    //_mockContext = mongoDBUtility.MockContext;
                    //_mockCollectionGroup = mongoDBUtility.MockCollectionGroup;
                    //_mockOptions = mongoDBUtility.MockOptions;
                    //context = mongoDBUtility.MongoDBContext;


                    //_mockCollectionGroup.Setup(op => op.InsertOneAsync(taskGroup, null,
                    //default(CancellationToken))).Returns(Task.CompletedTask);
                    //_mockContext.Setup(c => c.GetCollection<TaskGroup>(typeof(TaskGroup).Name)).Returns(_mockCollectionGroup.Object);
                    //var taskService = new TaskService(context);

                    //Action
                     result =await _taskService.NewTaskGroup(taskGroup);
                    
                    if (result == "New Group Added")
                    {
                       
                        testResult = "BusinessTestFor_NewTaskGroup_Successed=" +"True";

                        // Write test case result in text file
                        fileUtility.WriteTestCaseResuItInText(testResult);

                        // Write test case result in xml file
                        if (config["env"] == "development")
                        {
                            cases newcase = new cases
                            {
                                TestCaseType = "Business",
                                Name = "BusinessTestFor_NewTaskGroup_Successed",
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
                    Assert.Equal("New Group Added",result);
                }

            }
            catch (Exception exception)
            {
                var error = exception;
                testResult = "BusinessTestFor_NewTaskGroup_Successed=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Business",
                        Name = "BusinessTestFor_NewTaskGroup_Successed",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }

            }

        }

        /// <summary>
        /// check whether EditTask() method in TaskService update task present in database
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BusinessTestFor_EditTask_Successed()
        {
            try
            {
                
                taskItem.Priority = TaskPriority.Medium;
                taskItem.TaskStatus = TaskStatus.Progress;
                taskItem.TaskEndDate = taskItem.TaskStartDate.AddDays(10);
                taskItem.TaskColorCode = "Orange";
                //Action
                var result =await _taskService.EditTask(taskItem);
                
                if (result == 1)
                {
                    
                    testResult = "BusinessTestFor_EditTask_Successed=" + "True";

                    // Write test case result in text file
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Business",
                            Name = "BusinessTestFor_EditTask_Successed",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    // Assert 
                    Assert.Equal(1,result);
                }

            }
            catch (Exception exception)
            {
                var error = exception;
                testResult = "BusinessTestFor_EditTask_Successed=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Business",
                        Name = "BusinessTestFor_EditTask_Successed",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }

            }


        }


        /// <summary>
        /// check whether GetAllTask() method in TaskService returns list of all task present 
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BusinessTestFor_GetAllTask_Successed()
        {
            List<TaskItem> result =null;
            try
            {
               
             
                //Action
                 result =await _taskService.GetAllTask();
               
                if (result.Count !=0)
                {                    
                    testResult = "BusinessTestFor_GetAllTask_Successed=" + "True";

                    // Write test case result in text file
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Business",
                            Name = "BusinessTestFor_GetAllTask_Successed",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    // Assert 
                    Assert.InRange(result.Count,1,30);
                }

            }
            catch (Exception exception)
            {
                var error = exception;
                testResult = "BusinessTestFor_GetAllTask_Successed=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Business",
                        Name = "BusinessTestFor_GetAllTask_Successed",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }

            }

        }



        /// <summary>
        ///   check whether GetDashboard() method in TaskService returns dashboard with minimum total groups
        ///   total task , pending task and completed task
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BusinessTestFor_GetTaskDashboard_Successed()
        {
            TaskDashboard result = null;
            try
            {
             
                //Action
                result =await _taskService.GetDashboard();
                
                if (result.TotalGroups != 0 || result.TotalTask!= 0 || result.PendingTask!= 0 || result.CompletedTask != 0)
                {
                    
                    testResult = "BusinessTestFor_GetTaskDashboard_Successed=" +"True";

                    // Write test case result in text file
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Business",
                            Name = "BusinessTestFor_GetTaskDashboard_Successed",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    // Assert 
                    Assert.InRange(result.TotalGroups, 1, 30);
                }

            }
            catch (Exception exception)
            {
                var error = exception;
                testResult = "BusinessTestFor_GetTaskDashboard_Successed=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Business",
                        Name = "BusinessTestFor_GetTaskDashboard_Successed",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }

            }

        }

       
        /// <summary>
        /// test method verifies that returns all groups present in db
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BusinessTestFor_GetAllTaskGroups_Successed()
        {
            List<TaskGroup> result = null;
            try
            {
               
                //Action
                result =await _taskService.GetAllTaskGroup();
               
                if (result != null)
                {
                    
                    testResult = "BusinessTestFor_GetAllTaskGroups_Successed=" + "True";

                    // Write test case result in text file
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Business",
                            Name = "BusinessTestFor_GetAllTaskGroups_Successed",
                            expectedOutput = "True",
                            weight = 2,
                            mandatory = "True",
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    // Assert 
                    Assert.NotNull(result);
                }

            }
            catch (Exception exception)
            {
                var error = exception;
                testResult = "BusinessTestFor_GetAllTaskGroups_Successed=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Business",
                        Name = "BusinessTestFor_GetAllTaskGroups_Successed",
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
