using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonAdventurePark.Models
{
    public class StaffAssignment
    {
        public string Id { get; private set; }
        public string EmployeeId { get; private set; }
        public string RideId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public StaffAssignment( string id, string employeeId, string rideId,DateTime startTime, DateTime endTime)
        {
            Id = id;
            EmployeeId = employeeId;
            RideId = rideId;
            StartTime =startTime;
            EndTime = endTime;
        }
    }
}
