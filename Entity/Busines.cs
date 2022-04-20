using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Entity
{
    public class Busines
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Director { get; set; }
        public DateTime CreationTime { get;set; }
        public virtual City City { get; set; }
        public virtual Activity Activity { get; set; }
        public List<Workload> Workloads { get; set; } = new List<Workload>();
        public int WorkersQuantity { get; set; }
        public string ToString() {
            return Name;
        }
    }
}
