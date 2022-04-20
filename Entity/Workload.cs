using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    public class Workload
    {
        public Workload() { }
        public long Id { get; set; }
        public int Year { get; set; }
        public Busines Busines { get; set; }
        public long WorkloadValue { get; set; }
    }
}
