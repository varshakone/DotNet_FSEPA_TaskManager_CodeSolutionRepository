using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Entities
{
 public   class TaskGroup
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String GroupID { get; set; }

        [BsonElement("Group Name")]
        [BsonRequired]
        public String GroupName { get; set; }

        [BsonElement("Is Group Active")]
        [BsonRequired]
        public String Active { get; set; }

        [BsonElement("Group Color")]
        [BsonRequired]
        public string Color { get; set; }
    }
}
