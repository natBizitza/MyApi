using System;
using System.Collections.Generic;

namespace MyApi.Models
{
    public partial class Tasks
    {
        public Tasks()
        {
            Subtask = new HashSet<Subtask>();
            UserTask = new HashSet<UserTask>();
        }

        public long Id { get; set; }
        public string TaskId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Criticality { get; set; }
        public string Priority { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EstimatedStartDate { get; set; }
        public DateTime EstimatedEndDate { get; set; }
        public DateTime RealStartDate { get; set; }
        public DateTime RealEndDate { get; set; }
        public double EstimatedHours { get; set; }
        public double ElapsedHours { get; set; }
        public double PendingHours { get; set; }
        public string Status { get; set; }
        public long? SubmoduleId { get; set; }

        public Submodule Submodule { get; set; }
        public ICollection<Subtask> Subtask { get; set; }
        public ICollection<UserTask> UserTask { get; set; }
    }
}
