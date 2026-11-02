using System;

namespace MovieBookingManagement.Models
{
    public class MovieModel
    {
        public int Movie_ID { get; set; }

        public string Movie_name { get; set; }

        public DateTime Release_Date { get; set; }

        public int Cat_ID { get; set; }

        public decimal rate { get; set; }

        public string Cat_Type { get; set; }
    }
}