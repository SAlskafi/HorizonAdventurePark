using HorizonAdventurePark.Enums;
using HorizonAdventurePark.Models;
using System.Net.NetworkInformation;

namespace HorizonAdventurePark.Services
{
    public class TicketService
    {
        private Ticket[] tickets;
        private int ticketCount = 0; 


        private Ride[] rides;


        public TicketService( Ticket[] tickets,Ride[] rides)
        {
            this.tickets = tickets;
            this.rides = rides;
           
        }


        public void IssueTicket( Visitor visitor, TicketType ticketType,string ticketId, int validityDays)
        {
            if (visitor == null)
            {
                Console.WriteLine( "Ticket issuing failed: Visitor does not exist.");
                return;
            }

            if (!IsTicketTypeAllowed(visitor.Category, ticketType))
            {
                Console.WriteLine("Ticket issuing failed: The selected ticket type is not allowed for this visitor category.");
                return;
            }

            if (ticketCount >= tickets.Length)
            {
                Console.WriteLine( "Ticket issuing failed: Ticket storage is full.");
                return;
            }

            if (IsDuplicate(ticketId))
            {
                Console.WriteLine( "Ticket issuing failed: A ticket with this ID already exists.");
                return;
            }

            double price = GetTicketPrice(ticketType);

            Ride[] includedRides;

            if (ticketType == TicketType.VIP)
            {
                includedRides = rides; // all rides
            }
            else
            {
                includedRides = GetRegularRides();
            }

            Ticket ticket = new Ticket (ticketId, visitor.Id, ticketType, price, validityDays, includedRides);

            tickets[ticketCount] = ticket;
            ticketCount++;

            Console.WriteLine("Ticket issued successfully.");
        }

        //
        //helper methods :
          private bool IsTicketTypeAllowed(VisitorCategory category,TicketType ticketType)
        {
            if (category == VisitorCategory.VIP && ticketType == TicketType.VIP)
            {
                return true;
            }

            if (category != VisitorCategory.VIP &&  ticketType == TicketType.Regular)
            {
                return true;
            }

            return false;
        }


        private double GetTicketPrice(TicketType ticketType)
        {
            switch (ticketType)
            {
                case TicketType.VIP:
                    return 100;

                case TicketType.Regular:
                    return 50;

                default:
                    return 0;
            }
        }


        private int GetRegularRideCount()
        {
            int count = 0;

            for (int i = 0; i < rides.Length; i++)
            {
                if (rides[i] != null &&
                   (rides[i].Type == RideType.Family ||
                    rides[i].Type == RideType.Water))
                {
                    count++;
                }
            }

            return count;
        }

        // the rigular ticket allowed you to ride all ride that has type (family,water)
         private Ride[] GetRegularRides()
        {
            int regularRideCount = GetRegularRideCount();

            Ride[] regularRides = new Ride[regularRideCount];

            int index = 0;

            for (int i = 0; i < rides.Length; i++)
            {
                if (rides[i] != null &&
                    (rides[i].Type == RideType.Family ||
                     rides[i].Type == RideType.Water))
                {
                    regularRides[index] = rides[i];
                    index++;
                }
            }

            return regularRides;
        }

      private bool IsDuplicate(string ticketId)
        {
            for (int i = 0; i < ticketCount; i++)
            {
                if (tickets[i].Id == ticketId)
                {
                    return true;
                }
            }

            return false;
        }

    public Ticket FindTicketById(string ticketId)
        {
            for (int i = 0; i < ticketCount; i++)
            {
                if (tickets[i].Id == ticketId)
                {
                    return tickets[i];
                }
            }

            return null;
        }



        public void DeactivateTicket(string ticketId)
        {
            Ticket ticket = FindTicketById(ticketId);

            if (ticket == null)
            {
                Console.WriteLine("Ticket deactivation failed: Ticket does not exist.");
                return;
            }

            ticket.Deactivate();

            Console.WriteLine("Ticket deactivated successfully.");
        }

       

    }
}