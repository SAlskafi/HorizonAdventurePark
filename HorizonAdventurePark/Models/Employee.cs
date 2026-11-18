using HorizonAdventurePark.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonAdventurePark.Models
{
    public class Employee
    {
        public string Id { get; private set; }


        public string Name { get; private set; }


        public EmployeeRole Role { get; private set; }


        public Employee(string id, string name, EmployeeRole role)
        {
            Id = id;
            Name =name;
            Role = role;
        }


    }
}
