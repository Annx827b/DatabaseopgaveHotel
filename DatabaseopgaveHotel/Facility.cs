using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseopgaveHotel
{
    public class Facility
    {
        public int Facility_No { get; set; }
        public string Name { get; set; }
      
        public override string ToString()
        {
            return $"ID: {Facility_No}, Name: {Name}";
        }
    }
}
