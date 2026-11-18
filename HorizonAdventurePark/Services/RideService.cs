using HorizonAdventurePark.Enums;
using HorizonAdventurePark.Models;

namespace HorizonAdventurePark.Services
{
    public class RideService
    {
        private Ride[] rides;
        private int rideCount = 0;


        public RideService(Ride[] rides)
        {
            this.rides = rides;
        }


        
        public void AddRide(string id,string name, RideType type, int minimumAge, double minimumHeight,bool requiresAdult,int maximumCapacity)
        {
            if (rideCount >= rides.Length)
            {
                Console.WriteLine( "Ride creation failed: Ride storage is full.");
                return;
            }

            if (FindRideById(id) != null)
            {
                Console.WriteLine( "Ride creation failed: A ride with this ID already exists.");
                return;
            }

            if (maximumCapacity <= 0)
            {
                Console.WriteLine( "Ride creation failed: Maximum capacity must be greater than zero.");
                return;
            }

            if (minimumAge < 0)
            {
                Console.WriteLine( "Ride creation failed: Minimum age cannot be negative.");
                return;
            }

            if (minimumHeight < 0)
            {
                Console.WriteLine( "Ride creation failed: Minimum height cannot be negative.");
                return;
            }

            Ride ride = new Ride( id, name , type, minimumAge,  minimumHeight,requiresAdult, maximumCapacity);

            rides[rideCount] = ride;
            rideCount++;

            Console.WriteLine( "Ride added successfully.");
        }


     

        public void UpdateRideStatus(string rideId, RideStatus newStatus)
        {
            Ride ride = FindRideById(rideId);

            if (ride == null)
            {
                Console.WriteLine("Status update failed: Ride does not exist.");
                return;
            }

            ride.UpdateStatus(newStatus);

            Console.WriteLine( "Ride status updated successfully.");
        }

           public void ViewRideOccupancy(string rideId)
        {
            Ride ride = FindRideById(rideId);

            if (ride == null)
            {
                Console.WriteLine("Cannot view occupancy: Ride does not exist.");
                return;
            }

            Console.WriteLine( $"Ride: {ride.Name}");

            Console.WriteLine( $"Current Occupancy: {ride.CurrentOccupancy}/{ride.MaximumCapacity}");

            Console.WriteLine(  $"Status: {ride.Status}");
        }


        // helper method 
        //
        //
        public Ride FindRideById(string id)
        {
            for (int i = 0; i < rideCount; i++)
            {
                if (rides[i].Id == id)
                {
                    return rides[i];
                }
            }

            return null;
        }
    }
}
