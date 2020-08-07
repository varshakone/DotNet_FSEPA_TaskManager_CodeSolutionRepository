using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManager.Entities
{
   public class TaskItem
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String TaskId { get; set; }

        [BsonElement("Task Name")]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public String   Name { get; set; }

        [BsonElement("Task Priority")]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public TaskPriority Priority  { get; set; }

        [BsonElement("Task Status")]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public TaskStatus TaskStatus { get; set; }

        [BsonElement("Task Start Date")]
        [BsonRequired]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime TaskStartDate  { get; set; }

        [BsonElement("Task End Date")]
        [BsonRequired]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime TaskEndDate  { get; set; }

        [BsonElement("Task Group")]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public String TaskGroup { get; set; }

        [BsonElement("Task Color Code")]
        [BsonRequired]
        [BsonRepresentation(BsonType.String)]
        public String TaskColorCode  { get; set; }

    }
}
