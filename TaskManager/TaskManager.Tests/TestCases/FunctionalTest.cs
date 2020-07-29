using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Entities;
using TaskManager.Service;
using TaskManager.Test.Utility;
using Xunit;
using TaskStatus = TaskManager.Entities.TaskStatus;

namespace TaskManager.Test.TestCases
{
    [Collection("parallel")]
    public  class FunctionalTest
    {
        IConfigurationRoot config;
        private TaskItem taskItem;
        private TaskGroup taskGroup;
        private String testResult;
        static FileUtility fileUtility;

        private readonly TestServer _server;
        private readonly HttpClient _client;

        static FunctionalTest()
        {
            fileUtility = new FileUtility();
            fileUtility.FilePath = "../../../../output_revised.txt";
            fileUtility.CreateTextFile();
        }
        public FunctionalTest()
        {
            taskGroup = new TaskGroup
            {
                GroupName = "Social Task",
                Active = "Yes",
                Color = "White"

            };
            taskItem = new TaskItem
            {
                Name = "Eye donation",
                Priority = TaskPriority.High,
                TaskStatus = TaskStatus.Yet_To_Start,
                TaskStartDate = DateTime.Now,
                TaskEndDate = DateTime.Now.AddDays(5),
                TaskGroup = "Social Task",
                TaskColorCode = "White"
            };
            config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();

            _server = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        //test method to check new task added into database or not
        [Fact]
        public async Task FunctionalTestFor_NewTask_Api()
        {
            try
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(taskItem), Encoding.UTF8, "application/json");

                String billResponse = string.Empty;
                HttpResponseMessage response = await _client.PostAsync("	http://localhost:9090/api/Task/NewTask", content);
                var status = response.EnsureSuccessStatusCode();

                String taskResult = String.Empty;

                if (status.IsSuccessStatusCode)
                {
                    billResponse = response.Content.ReadAsStringAsync().Result;
                    taskResult = billResponse;
                }
                if (taskResult == "New Task Added")
                {
                   
                    testResult = "FunctionalTestFor_NewTask_Api=" + "True";

                    // Write test case result in text file
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Functional",
                            Name = "FunctionalTestFor_NewTask_Api",
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
                    Assert.NotNull(taskResult);
                }

            }
            catch (Exception exception)
            {
                var res = exception;
                testResult = "FunctionalTestFor_NewTask_Api=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Functional",
                        Name = "FunctionalTestFor_NewTask_Api",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }


            }

        }
        // check whether NewTaskGroup() method in TaskService able to add new group in db
        [Fact]
        public async Task FunctionalTestFor_NewTaskGroup_Api()
        {
            try
            {
                HttpContent content = new StringContent(JsonConvert.SerializeObject(taskGroup), Encoding.UTF8, "application/json");

                String billResponse = string.Empty;
                HttpResponseMessage response = await _client.PostAsync("	http://localhost:9090/api/Task/newgroup", content);
                var status = response.EnsureSuccessStatusCode();

                String groupResult = String.Empty;

                if (status.IsSuccessStatusCode)
                {
                    billResponse = response.Content.ReadAsStringAsync().Result;
                    groupResult = billResponse;
                }
                if (groupResult == "New Group Added")
                {

                    testResult = "FunctionalTestFor_NewTaskGroup_Api=" + "True";

                    // Write test case result in text file
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Functional",
                            Name = "FunctionalTestFor_NewTaskGroup_Api",
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
                    Assert.NotNull(groupResult);
                }

            }
            catch (Exception exception)
            {
                var res = exception;
                testResult = "FunctionalTestFor_NewTaskGroup_Api=" + "False";
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Functional",
                        Name = "FunctionalTestFor_NewTaskGroup_Api",
                        expectedOutput = "False",
                        weight = 2,
                        mandatory = "False",
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }


            }

        }

        // check whether EditTask() method in TaskService update task present in database
        [Fact]
        public async Task FunctionalTestFor_EditTask_Api()
        {
            try
            {
                taskItem.Priority = TaskPriority.VeryHigh;
                taskItem.TaskStatus = TaskStatus.Finished;
                taskItem.TaskEndDate = taskItem.TaskStartDate.AddDays(10);
                taskItem.TaskColorCode = "Orange";
                HttpContent content = new StringContent(JsonConvert.SerializeObject(taskItem), Encoding.UTF8, "application/json");

                String taskResponse = string.Empty;
                HttpResponseMessage response = await _client.PostAsync("	http://localhost:9090/api/Task/edittask", content);
                var status = response.EnsureSuccessStatusCode();

                long editResult = 0;

                if (status.IsSuccessStatusCode)
                {
                    taskResponse = response.Content.ReadAsStringAsync().Result;
                    editResult = JsonConvert.DeserializeObject<long>(taskResponse);
                }
                if (editResult == 1)
                {

                    testResult = "FunctionalTestFor_EditTask_Api=" + "True".ToString();

                    // Write test case result in text file
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Functional",
                            Name = "FunctionalTestFor_EditTask_Api",
                            expectedOutput = "True".ToString(),
                            weight = 2,
                            mandatory = "True".ToString(),
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    // Assert 
                    Assert.Equal(1,editResult);
                }

            }
            catch (Exception exception)
            {
                var res = exception;
                testResult = "FunctionalTestFor_EditTask_Api=" +"False".ToString();
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Functional",
                        Name = "FunctionalTestFor_EditTask_Api",
                        expectedOutput = "False".ToString(),
                        weight = 2,
                        mandatory = "False".ToString(),
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }


            }

        }

        // check whether GetAllTask() method in TaskService returns list of all task present 
        [Fact]
        public async Task FunctionalTestFor_GetAllTask_Api()
        {
            try
            {
                
                List<TaskItem> taskList = null;
                HttpResponseMessage response = await _client.PostAsync("	http://localhost:9090/api/Task/alltask", null);
                var status = response.EnsureSuccessStatusCode();
                String taskResponse;
                long editResult = 0;

                if (status.IsSuccessStatusCode)
                {
                    taskResponse = response.Content.ReadAsStringAsync().Result;
                    taskList = JsonConvert.DeserializeObject<List<TaskItem>>(taskResponse);
                }
                if (taskList.Count > 0)
                {

                    testResult = "FunctionalTestFor_GetAllTask_Api=" + "True".ToString();

                    // Write test case result in text file
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Functional",
                            Name = "FunctionalTestFor_GetAllTask_Api",
                            expectedOutput = "True".ToString(),
                            weight = 2,
                            mandatory = "True".ToString(),
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    // Assert 
                    Assert.InRange( editResult,1,int.MaxValue);
                }

            }
            catch (Exception exception)
            {
                var res = exception;
                testResult = "FunctionalTestFor_GetAllTask_Api=" + "False".ToString();
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Functional",
                        Name = "FunctionalTestFor_GetAllTask_Api",
                        expectedOutput = "False".ToString(),
                        weight = 2,
                        mandatory = "False".ToString(),
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }


            }


        }

        // test method verifies that returns all groups present in db
        [Fact]
        public async Task FunctionalTestFor_GetAllTaskGroups_Api()
        {
            try
            {

                List<TaskGroup> groupList = null;
                HttpResponseMessage response = await _client.PostAsync("	http://localhost:9090/api/Task/allgroups", null);
                var status = response.EnsureSuccessStatusCode();
                String taskResponse;
            

                if (status.IsSuccessStatusCode)
                {
                    taskResponse = response.Content.ReadAsStringAsync().Result;
                    groupList = JsonConvert.DeserializeObject<List<TaskGroup>>(taskResponse);
                }
                if (groupList.Count > 0)
                {

                    testResult = "FunctionalTestFor_GetAllTaskGroups_Api=" + "True".ToString();

                    // Write test case result in text file
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Functional",
                            Name = "FunctionalTestFor_GetAllTaskGroups_Api",
                            expectedOutput = "True".ToString(),
                            weight = 2,
                            mandatory = "True".ToString(),
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    // Assert 
                    Assert.InRange(groupList.Count, 1, int.MaxValue);
                }

            }
            catch (Exception exception)
            {
                var res = exception;
                testResult = "FunctionalTestFor_GetAllTaskGroups_Api=" + "False".ToString();
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Functional",
                        Name = "FunctionalTestFor_GetAllTaskGroups_Api",
                        expectedOutput = "False".ToString(),
                        weight = 2,
                        mandatory = "False".ToString(),
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }


            }


        }

        // check whether GetDashboard() method in TaskService returns dashboard with minimum total groups
        //total task , pending task and completed task

        [Fact]
        public async Task FunctionalTestFor_GetTaskDashboard_Api()
        {
            try
            {

                TaskDashboard dashboard = null;
                HttpResponseMessage response = await _client.PostAsync("	http://localhost:9090/api/Task/dashboard", null);
                var status = response.EnsureSuccessStatusCode();
                String taskResponse;
                
                if (status.IsSuccessStatusCode)
                {
                    taskResponse = response.Content.ReadAsStringAsync().Result;
                    dashboard = JsonConvert.DeserializeObject<TaskDashboard>(taskResponse);
                }
                if (dashboard.TotalGroups > 0 || dashboard.TotalTask > 0 || dashboard.PendingTask == 0 || dashboard.CompletedTask == 0)
                {

                    testResult = "FunctionalTestFor_GetTaskDashboard_Api=" + "True".ToString();

                    // Write test case result in text file
                    fileUtility.WriteTestCaseResuItInText(testResult);

                    // Write test case result in xml file
                    if (config["env"] == "development")
                    {
                        cases newcase = new cases
                        {
                            TestCaseType = "Functional",
                            Name = "FunctionalTestFor_GetTaskDashboard_Api",
                            expectedOutput = "True".ToString(),
                            weight = 2,
                            mandatory = "True".ToString(),
                            desc = "na"
                        };
                        await new FileUtility().WriteTestCaseResuItInXML(newcase);
                    }
                }
                else
                {
                    // Assert 
                    Assert.NotNull(dashboard);
                }

            }
            catch (Exception exception)
            {
                var res = exception;
                testResult = "FunctionalTestFor_GetTaskDashboard_Api=" + "False".ToString();
                // Write test case result in text file
                fileUtility.WriteTestCaseResuItInText(testResult);

                // Write test case result in xml file
                if (config["env"] == "development")
                {
                    cases newcase = new cases
                    {
                        TestCaseType = "Functional",
                        Name = "FunctionalTestFor_GetTaskDashboard_Api",
                        expectedOutput = "False".ToString(),
                        weight = 2,
                        mandatory = "False".ToString(),
                        desc = "na"
                    };
                    await new FileUtility().WriteTestCaseResuItInXML(newcase);
                }


            }

        }
    }
}
