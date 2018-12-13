using System;
using System.Collections.Generic;

namespace MyApi.Models
{
    public partial class Subtask
    {
        public Subtask()
        {
            UserSubtask = new HashSet<UserSubtask>();
        }

        public long Id { get; set; }
        public string SubtaskId { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public double EstimatedHours { get; set; }
        public double ElapsedHours { get; set; }
        public double PendingHours { get; set; }
        public string Observations { get; set; }
        public string Status { get; set; }
        public long? TaskId { get; set; }

        public Tasks Task { get; set; }
        public ICollection<UserSubtask> UserSubtask { get; set; }
    }
}
