using HorizonAdventurePark.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonAdventurePark.Models
{
    public class Reservation
    {
        public string Id { get; private set; }

        public string VisitorId { get; private set; }

        public string RideId { get; private set; }

       public DateTime TimeSlot { get; private set; }
       public ReservationStatus Status { get; private set; }
       
        public Reservation(string id ,string visitorId, string rideId, DateTime timeSlot)
        {
            Id = id;
            VisitorId= visitorId;
            RideId = rideId;
            TimeSlot = timeSlot;
            Status =ReservationStatus.Active;
           
        }

        
        public void Cancel()
        
        {
            Status = ReservationStatus.Cancelled;
        }
   

        
    }
}
