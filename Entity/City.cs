using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class City
    {
        public long Id { get; set; }
        public string name { get; set; }

        public List<Busines> Busines { get; set; }
        public override string ToString()
        {
            return this.name;
        }
    }
}
