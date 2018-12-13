using System;
using System.Collections.Generic;

namespace MyApi.Models
{
    public partial class UserTask
    {
        public long Id { get; set; }
        public string AppUsersId { get; set; }
        public long? TasksId { get; set; }

        public AspNetUsers AppUsers { get; set; }
        public Tasks Tasks { get; set; }
    }
}
