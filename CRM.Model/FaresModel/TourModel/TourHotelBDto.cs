using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.FaresModel.TourModel;
using CRM.Model.Security;

namespace CRM.Model.FaresModel.TourModel
{
    public class TourHotelBDto
    {
        public int tourId { get; set; }
        public int SrNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public int HotelId { get; set; }
        public int NoOfRooms { get; set; }
        public decimal Amount { get; set; }


        public int Currency { get; set; }
        public int RoomTypeId { get; set; }

        public decimal Tax { get; set; }
        public decimal Gst { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public string Remarks { get; set; }

    }
}
