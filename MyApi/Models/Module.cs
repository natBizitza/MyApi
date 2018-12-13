using System;
using System.Collections.Generic;

namespace MyApi.Models
{
    public partial class Module
    {
        public Module()
        {
            Submodule = new HashSet<Submodule>();
        }

        public long Id { get; set; }
        public string ModuleId { get; set; }
        public string Description { get; set; }
        public double EstimatedHours { get; set; }
        public double ElapsedHours { get; set; }
        public double PendingHours { get; set; }
        public string Status { get; set; }
        public long? ProjectId { get; set; }

        public Project Project { get; set; }
        public ICollection<Submodule> Submodule { get; set; }
    }
}
