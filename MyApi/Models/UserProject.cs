using System;
using System.Collections.Generic;

namespace MyApi.Models
{
    public partial class UserProject
    {
        public long Id { get; set; }
        public string AppUsersId { get; set; }
        public long? ProjectsId { get; set; }

        public AspNetUsers AppUsers { get; set; }
        public Project Projects { get; set; }
    }
}
