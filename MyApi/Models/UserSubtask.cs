using System;
using System.Collections.Generic;

namespace MyApi.Models
{
    public partial class UserSubtask
    {
        public long Id { get; set; }
        public string AppUsersId { get; set; }
        public long? SubtasksId { get; set; }
        public DateTime PauseTime { get; set; }
        public DateTime PlayTime { get; set; }
        public DateTime RecordTime { get; set; }
        public DateTime StopTime { get; set; }

        public AspNetUsers AppUsers { get; set; }
        public Subtask Subtasks { get; set; }
    }
}
