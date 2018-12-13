using System;
using System.Collections.Generic;

namespace MyApi.Models
{
    public partial class Submodule
    {
        public Submodule()
        {
            Task = new HashSet<Tasks>();
        }

        public long Id { get; set; }
        public string SubmoduleId { get; set; }
        public string Description { get; set; }
        public double EstimatedHours { get; set; }
        public double ElapsedHours { get; set; }
        public double PendingHours { get; set; }
        public string Status { get; set; }
        public long? ModuleId { get; set; }

        public Module Module { get; set; }
        public ICollection<Tasks> Task { get; set; }
    }
}
