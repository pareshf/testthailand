using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.FaresModel.TourModel;
using CRM.Model.Security;


namespace CRM.Model.FaresModel.TourModel
{
    public  class TourWebBDto
    {
       public int tourId { get; set; }
       public string Hightlights { get; set; }
       public string Cost { get; set; }
       public string Itinerary{ get; set; }
       public string ImportantNote { get; set; }
       public string Terms { get; set; }
    }
}
