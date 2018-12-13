using System;
using System.Collections.Generic;

namespace MyApi.Models
{
    public partial class Project
    {
        public Project()
        {
            Module = new HashSet<Module>();
            UserProject = new HashSet<UserProject>();
        }

        public long Id { get; set; }
        public string ProjectId { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double EstimatedHours { get; set; }
        public double ElapsedHours { get; set; }
        public double PendingHours { get; set; }
        public string Status { get; set; }

        public ICollection<Module> Module { get; set; }
        public ICollection<UserProject> UserProject { get; set; }
    }
}
