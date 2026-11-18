using System;
using System.Collections.Generic;
using System.Text;
using HorizonAdventurePark.Enums;
using HorizonAdventurePark.Models;

namespace HorizonAdventurePark.Services
{
    public class EmployeeService
    {

        private Employee[] employees;
        private Assignment[] assignments;

        private int employeeCount = 0;
        private int assignmentCount = 0;


        public EmployeeService(Employee[] employees, Assignment[] assignments)
        {
            this.employees = employees;
            this.assignments = assignments;
        }


        public void AddEmployee(string id,string name, EmployeeRole role)
        {
            if (employeeCount >= employees.Length)
            {
                Console.WriteLine(
                    "Employee creation failed: employee storage is full.");
                return;
            }

            if (FindEmployeeById(id) != null)
            {
                Console.WriteLine(
                    "Employee creation failed:an employee with this ID already exists.");
                return;
            }

            Employee employee = new Employee( id, name,role );

            employees[employeeCount] = employee;
            employeeCount++;

            Console.WriteLine( "Employee added successfully.");
        }


        public void AssignEmployee( string assignmentId,string employeeId ,string locationId,DateTime startTime, DateTime endTime)
        {
            if (assignmentCount >= assignments.Length)
            {
                Console.WriteLine("Assignment failed: Assignment storage is full.");
                return;
            }

            Employee employee = FindEmployeeById(employeeId);

            if (employee == null)
            {
                Console.WriteLine( "Assignment failed: Employee does not exist.");
                return;
            }

            if (startTime >= endTime)
            {
                Console.WriteLine("Assignment failed: Start time must be before end time.");
                return;
            }

            if (HasAssignmentConflict( employeeId, startTime, endTime))
            {
                Console.WriteLine("Assignment failed: Employee is already assigned to another location during this period.");
                return;
            }

            Assignment assignment = new Assignment( assignmentId, employeeId, locationId, startTime, endTime );

            assignments[assignmentCount] = assignment;
            assignmentCount++;

            Console.WriteLine( "Employee assigned successfully.");
        }


        // helper methods 
        //
        //

        private Employee FindEmployeeById(string employeeId)
        {
            for (int i = 0; i < employeeCount; i++)
            {
                if (employees[i].Id == employeeId)
                {
                    return employees[i];
                }
            }

            return null;
        }

        private bool HasAssignmentConflict(string employeeId,DateTime startTime,DateTime endTime)
        {
            for (int i = 0; i < assignmentCount; i++)
            {
                Assignment assignment = assignments[i];

                if (assignment.EmployeeId == employeeId &&  assignment.Status == AssignmentStatus.Active)
                {
                    bool overlaps = startTime < assignment.EndTime &&  endTime > assignment.StartTime;

                    if (overlaps)
                    {
                        return true;
                    }
                }
            }

            return false;
        }





    }
}
