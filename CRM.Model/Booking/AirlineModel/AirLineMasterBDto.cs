using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.Security;


namespace CRM.Model.Booking.AirlineModel 
{
    public class AirLineMasterBDto : UserProfileBDto
    {
        public int AirLineId { get; set; }
        public string AirLineCode { get; set; }
        public string AirLineName { get; set; }
        

    }
}
