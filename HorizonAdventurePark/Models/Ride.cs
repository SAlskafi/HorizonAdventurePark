using HorizonAdventurePark.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonAdventurePark.Models
{
    public class Ride
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

         public RideType Type { get; private set; }

        
        public int  MinimumAge { get; private set; }

        public double MinimumHeight { get; private set; }

        
        public bool  RequiresAdult { get; private set; }

        public int MaximumCapacity { get; private set; }

       public int CurrentOccupancy { get; private set; }

        
        public RideStatus  Status { get; private set; }



        public Ride(string id , string name,  RideType type,int minimumAge,double minimumHeight, bool requiresAdult , int maximumCapacity)
        {
            Id = id;
            Name = name;
            Type = type;
            MinimumAge = minimumAge;
            MinimumHeight = minimumHeight;
            RequiresAdult = requiresAdult;
            MaximumCapacity = maximumCapacity;
            CurrentOccupancy = 0;
            Status = RideStatus.Open;
        }


        public bool AdmitVisitor()
        {
            if (CurrentOccupancy >= MaximumCapacity)
            {
                return false;
            }

            CurrentOccupancy++;
            return true;
        }

        public void UpdateStatus(RideStatus status)
        {
            Status = status;
        }

    }
}
