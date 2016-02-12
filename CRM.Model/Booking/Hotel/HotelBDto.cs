using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRM.Model.General;
using CRM.Model.Security;

namespace CRM.Model.Booking.Hotel
{
    public class HotelBDto : UserProfileBDto
    {

        #region HotelDetails
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public int Rating { get; set; }
        public AddressBDto Address { get; set; }
        public ContactBDto Contact { get; set; }
        public byte[] HotelPhoto { get; set; }
        public string HotelPhotoType { get; set; }
        public string HotelWebsite { get; set; }
        #endregion

        #region RoomDetails
        public int SrNo { get; set; }
        public string RoomNo { get; set; }
        
        public string RoomDesc { get; set; }
        
        public Decimal Discount { get; set; }
        public Decimal Rate { get; set; }
        public Decimal Tax { get; set; }
        public Decimal GST { get; set; }

        public byte[] RoomPhoto { get; set; }
        public string RoomPhotoType { get; set; }
        public bool IsRoomPhotoUpdate { get; set; }

        public int CurrancyPriceId { get; set; }
        
        public int RoomTypeId { get; set; }
        public int Currancy { get; set; }


        #endregion
    }
}
