using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Entities
{
   public class TaskDashboard
    {
        public int TotalGroups { get; set; }
        public int TotalTask { get; set; }
        public int CompletedTask { get; set; }
        public int PendingTask { get; set; }
    }
}
