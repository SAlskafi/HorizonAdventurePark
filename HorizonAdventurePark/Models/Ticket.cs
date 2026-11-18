using HorizonAdventurePark.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonAdventurePark.Models
{
    public class Ticket
    {
        
        public string Id { get; private set; }

        public string VisitorId { get; private set; }

         public TicketType  Type { get; private set; }

        public double Price { get; private set; }

        public DateTime IssueDate { get; private set; }

         public DateTime ExpiryDate { get; private set; }

        public TicketStatus Status { get; private set; }

        public Ride[] IncludedRides { get; private set; }

        public Ticket( string id, string visitorId, TicketType type,double price, int validityDays , Ride[] includedRides )  
        {
            Id = id;
            VisitorId = visitorId;
            Type = type;
            Price = price;
            IssueDate = DateTime.Now;
            ExpiryDate = IssueDate.AddDays(validityDays); 
            Status = TicketStatus.Active;
            IncludedRides = includedRides;
        }


        public void Deactivate()
        {
            Status = TicketStatus.Cancelled;
        }


        
      

    }
}
