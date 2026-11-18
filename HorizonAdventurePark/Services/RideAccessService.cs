using HorizonAdventurePark.Enums;
using HorizonAdventurePark.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonAdventurePark.Services
{
    public  class RideAccessService
    {

        private Reservation[] reservations;

        public RideAccessService(Reservation[] reservations)
        {
            this.reservations = reservations;
        }


        public void ValidateAccess( Visitor visitor,Ticket ticket, Ride ride)
        {
            if (visitor == null)
            {
                Console.WriteLine( "Access denied: Visitor does not exist.");
                return;
            }

            if (ticket == null)
            {
                Console.WriteLine("Access denied: Visitor does not have a ticket.");
                return;
            }

            if (ride == null)
            {
                Console.WriteLine( "Access denied: Ride does not exist.");
                return;
            }

            if (ticket.VisitorId != visitor.Id)
            {
                Console.WriteLine("Access denied: This ticket does not belong to this visitor.");
                return;
            }

            if (ticket.Status != TicketStatus.Active)
            {
                Console.WriteLine( "Access denied: Ticket is not active.");
                return;
            }

            if (DateTime.Now > ticket.ExpiryDate)
            {
                Console.WriteLine( "Access denied: Ticket has expired.");
                return;
            }


            if (ride.Status != RideStatus.Open)
            {
                Console.WriteLine( $"Access denied: Ride is currently {ride.Status}.");
                return;
            }

            if (!IsRideIncluded(ticket, ride))
            {
                Console.WriteLine( "Access denied: This ride is not included in the visitor's ticket.");
                return;
            }

            if (visitor.Age < ride.MinimumAge)
            {
                Console.WriteLine( $"Access denied: Visitor does not meet the minimum age requirement ({ride.MinimumAge} years) for this ride.");
                return;
            }

            if (visitor.Height < ride.MinimumHeight)
            {
                Console.WriteLine( $"Access denied: Visitor does not meet the minimum height requirement ({ride.MinimumHeight} cm) for this ride.");
                return;
            }

            if (ride.RequiresAdult && visitor.Category != VisitorCategory.StaffAccompaniedMinor)
            {
                Console.WriteLine( "Access denied: This ride requires an accompanying adult.");
                return;
            }

            DateTime currentTimeSlot = GetCurrentTimeSlot();

            int activeReservations = GetActiveReservationCount(ride.Id, currentTimeSlot);

            int totalOccupancy = ride.CurrentOccupancy + activeReservations;

            if (totalOccupancy >= ride.MaximumCapacity)
            {
                Console.WriteLine(  "Access denied: Ride has reached its maximum capacity.");
                return;
            }

            ride.AdmitVisitor();

            Console.WriteLine("ACCESS GRANTED.");
        }

        //
        //
        //helper methods 

        private bool IsRideIncluded( Ticket ticket, Ride ride)
        {
            for (int i = 0; i < ticket.IncludedRides.Length; i++)
            {
                if (ticket.IncludedRides[i].Id == ride.Id)
                {
                    return true;
                }
            }

            return false;
        }

        private DateTime GetCurrentTimeSlot()
        {
            DateTime now = DateTime.Now;

            return new DateTime( now.Year, now.Month,now.Day, now.Hour, 0, 0);
        }


        private int GetActiveReservationCount( string rideId,DateTime timeSlot)
        {
            int count = 0;

            for (int i = 0; i < reservations.Length; i++)
            {
                if (reservations[i] != null &&
                    reservations[i].RideId == rideId &&
                    reservations[i].TimeSlot == timeSlot &&
                    reservations[i].Status == ReservationStatus.Active)
                {
                    count++;
                }
            }

            return count;
        }

    }
}
