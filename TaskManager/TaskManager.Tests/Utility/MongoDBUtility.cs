using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DataLayer;
using TaskManager.Entities;

namespace TaskManager.Test.Utility
{
  public   class MongoDBUtility
    {
        private Mock<IMongoCollection<TaskItem>> _mockCollection;
        private Mock<IMongoCollection<TaskGroup>> _mockCollectionGroup;
        private Mock<IMongoDBContext> _mockContext;
        private Mock<IOptions<MongoSettings>> _mockOptions;
        MongoSettings settings;
        MongoDBContext mongoDBcontext;
        public MongoDBUtility()
        {
            _mockContext = new Mock<IMongoDBContext>();
            _mockCollection = new Mock<IMongoCollection<TaskItem>>();
            _mockOptions = new Mock<IOptions<MongoSettings>>();
            settings = new MongoSettings()
            {
                Connection = "mongodb://localhost:27017",

                DatabaseName = "TaskManagerDB"
            };
            _mockOptions.Setup(s => s.Value).Returns(settings);
            mongoDBcontext = new MongoDBContext(_mockOptions.Object);
        }

        public MongoDBUtility(TaskGroup group)
        {
            
            _mockContext = new Mock<IMongoDBContext>();
            _mockCollectionGroup = new Mock<IMongoCollection<TaskGroup>>();
            _mockOptions = new Mock<IOptions<MongoSettings>>();
            settings = new MongoSettings()
            {
                Connection = "mongodb://localhost:27017",

                DatabaseName = "TaskManagerDB"
            };
            _mockOptions.Setup(s => s.Value).Returns(settings);
            mongoDBcontext = new MongoDBContext(_mockOptions.Object);
        }

        public Mock<IMongoCollection<TaskItem>> MockCollection { get => _mockCollection;  }
        public Mock<IMongoDBContext> MockContext { get => _mockContext; }
        public Mock<IOptions<MongoSettings>> MockOptions { get => _mockOptions; }
        public MongoSettings Settings { get => settings; }
        public MongoDBContext MongoDBContext { get => mongoDBcontext;  }
        public Mock<IMongoCollection<TaskGroup>> MockCollectionGroup { get => _mockCollectionGroup;  }
    }
}
