using System;
using System.Collections.Generic;
using System.Text;
using HorizonAdventurePark.Enums;

namespace HorizonAdventurePark.Models
{
    public class Assignment
    {
        public string Id { get; private set; }

        public string EmployeeId { get; private set; }

        // Identifies the ride or facility where the employee is assigned.
        public string LocationId { get; private set; }

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }

        public AssignmentStatus Status { get; private set; }


        public Assignment(string id,string employeeId,string locationId,DateTime startTime, DateTime endTime)
        {

            Id = id;
            EmployeeId = employeeId;
            LocationId = locationId;
            StartTime = startTime;
            EndTime = endTime;
            Status = AssignmentStatus.Active;
        }


        public void Cancel()
        {
            Status = AssignmentStatus.Cancelled;
        }
    }
}
