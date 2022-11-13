using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DatabaseopgaveHotel
{
    public class HotelFacility
    {
        public int Facility_No { get; set; }

        public int Hotel_No { get; set; }

        public override string ToString()
        {
            return $"ID: {Facility_No}, ID: {Hotel_No}";
        }
    }
}
