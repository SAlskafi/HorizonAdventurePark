using HorizonAdventurePark.Enums;
using HorizonAdventurePark.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonAdventurePark.Services
{
    public class ReservationService
    {
        private Reservation[]  reservations;
        private int reservationCount = 0;


        private Ride[] rides;
        private Visitor[] visitors;
        private Ticket[] tickets;

        public ReservationService(Reservation[] reservations, Ride[] rides,Visitor[] visitors,Ticket[] tickets)
        {
            this.reservations = reservations;
            this.rides = rides;
            this.visitors = visitors;
            this.tickets = tickets;
        }



        public void  CreateReservation( string reservationId, string visitorId,string rideId,DateTime timeSlot)
        {


            if (reservationCount >= reservations.Length)
            {
                Console.WriteLine("Reservation failed: Reservation storage is full.");
                return;
            }


            Visitor visitor = FindVisitorById(visitorId);

            if (visitor == null)
            {
                Console.WriteLine("Reservation failed: visitor does not exist.");
                return;
            }


            Ride ride = FindRideById(rideId);

            if (ride == null)
            {
                Console.WriteLine( "Reservation failed: Ride does not exist.");
                return;
            }

            Ticket ticket = FindActiveTicketByVisitorId(visitorId);

            if (ticket == null)
            {
                Console.WriteLine("Reservation failed: Visitor does not have a valid active ticket.");
                return;
            }

            if (DateTime.Now > ticket.ExpiryDate)
            {
                Console.WriteLine( "Reservation failed: Visitor's ticket has expired.");
                return;
            }
            if (!IsRideIncluded(ticket, ride))
            {
                Console.WriteLine( "Reservation failed: This ride is not included in the visitor's ticket.");
                return;
            }

            if (ride.Status == RideStatus.Closed)
            {
                Console.WriteLine( "Reservation failed: Ride is currently closed.");
                return;
            }

            if (ride.Status == RideStatus.UnderMaintenance)
            {
                Console.WriteLine("Reservation failed: Ride is currently under maintenance.");
                return;
            }

            if (IsDuplicateReservation(visitorId, rideId, timeSlot))
            {
                Console.WriteLine("Reservation failed: Visitor already has a reservation for this ride and time slot.");
                return;
            }

            int activeReservations = GetReservationCount(rideId, timeSlot);

            int totalOccupancy = ride.CurrentOccupancy + activeReservations; // from direct visitor + who has reservation

            if (totalOccupancy >= ride.MaximumCapacity)
            {
                Console.WriteLine( "Reservation failed: Ride has reached maximum capacity for the selected time slot.");
                return;
            }

            Reservation reservation = new Reservation(reservationId,visitorId,rideId, timeSlot);

            reservations[reservationCount] = reservation;
            reservationCount++;

            Console.WriteLine( "Reservation created successfully.");


        }


        //

        //
        //helper methods 

        private Visitor FindVisitorById(string visitorId)
        {
            for (int i = 0; i < visitors.Length; i++)
            {
                if (visitors[i] != null && visitors[i].Id == visitorId)
                {
                    return visitors[i];
                }
            }

            return null;
        }
        private Ride FindRideById(string rideId)
        {
            for (int i = 0; i < rides.Length; i++)
            {
                if (rides[i] != null && rides[i].Id == rideId)
                {
                    return rides[i];
                }
            }

            return null;
        }
        private Ticket FindActiveTicketByVisitorId(string visitorId)
        {
            for (int i = 0; i < tickets.Length; i++)
            {
                if (tickets[i] != null &&  tickets[i].VisitorId == visitorId)
                {
                    if (tickets[i].Status == TicketStatus.Active)
                    {
                        return tickets[i];
                    }
                }
            }

            return null;
        }
        private bool IsRideIncluded(Ticket ticket, Ride ride)
        {
            for (int i = 0; i < ticket.IncludedRides.Length; i++)
            {
                if (ticket.IncludedRides[i] != null && ticket.IncludedRides[i].Id == ride.Id)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsDuplicateReservation(string visitorId,string rideId, DateTime timeSlot)
        {
            for (int i = 0; i < reservationCount; i++)
            {
                if (reservations[i].VisitorId == visitorId &&
                    reservations[i].RideId == rideId &&
                    reservations[i].TimeSlot == timeSlot &&
                    reservations[i].Status == ReservationStatus.Active)
                {
                    return true;
                }
            }

            return false;
        }


        private int GetReservationCount(  string rideId, DateTime timeSlot)
        {
            int count = 0;

            for (int i = 0; i < reservationCount; i++)
            {
                if (reservations[i].RideId == rideId &&
                    reservations[i].TimeSlot == timeSlot &&
                    reservations[i].Status == ReservationStatus.Active)
                {
                    count++;
                }
            }

            return count;
        }

        private Reservation FindReservationById(string reservationId)
        {
            for (int i = 0; i < reservationCount; i++)
            {
                if (reservations[i] != null &&  reservations[i].Id == reservationId)
                {
                    return reservations[i];
                }
            }

            return null;
        }

        public void CancelReservation(string reservationId)
        {
            Reservation reservation = FindReservationById(reservationId);

            if (reservation == null)
            {
                Console.WriteLine( "Reservation cancellation failed: Reservation does not exist.");
                return;
            }

            if (reservation.Status == ReservationStatus.Cancelled)
            {
                Console.WriteLine( "Reservation cancellation failed: Reservation is already cancelled.");
                return;
            }

            reservation.Cancel();

            Console.WriteLine( "Reservation cancelled successfully.");
        }
    }
}
