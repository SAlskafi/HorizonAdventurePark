using HorizonAdventurePark.Enums;
using HorizonAdventurePark.Models;
using HorizonAdventurePark.Services;

namespace HorizonAdventurePark
{
    internal class Program
    {
        static void Main(string[] args)
        {


            // Shared arrays that store the system data during the current run.
            Visitor[] visitors = new Visitor[100];
            Ticket[] tickets = new Ticket[100];
            Ride[] rides = new Ride[100];
            Reservation[] reservations = new Reservation[100];
            Employee[] employees = new Employee[100];
            Assignment[] assignments = new Assignment[100];





            // Services use the shared arrays created by the main program.
            VisitorService visitorService = new VisitorService(visitors);

            TicketService ticketService = new TicketService(tickets, rides);

            RideService rideService = new RideService(rides);

            ReservationService reservationService = new ReservationService(reservations, rides, visitors, tickets);

            EmployeeService employeeService = new EmployeeService(employees, assignments);

            RideAccessService rideAccessService = new RideAccessService(reservations);






            // Initial employees used to test the staff assignment operation.
            employeeService.AddEmployee(
                "E001",
                "Ahmad",
                EmployeeRole.RideOperator);

            employeeService.AddEmployee(
                "E002",
                "Sara",
                EmployeeRole.RideOperator);

            employeeService.AddEmployee(
                "E003",
                "Omar",
                EmployeeRole.OperationsManager);








            // the menu:


            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine("=== Horizon Adventure Park — Operations System ===");
                Console.WriteLine("1. Register Visitor");
                Console.WriteLine("2. Issue Ticket");
                Console.WriteLine("3. Deactivate Ticket");
                Console.WriteLine("4. Validate Ride Access");
                Console.WriteLine("5. Create Reservation");
                Console.WriteLine("6. Cancel Reservation");
                Console.WriteLine("7. View Ride Occupancy");
                Console.WriteLine("8. Add Ride");
                Console.WriteLine("9. Update Ride Status");
                Console.WriteLine("10. Assign Employee");
                Console.WriteLine("11. Exit");

                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        {

                            // useing helper methods  to handle if a user enters non-numeric, empty input

                            string visitorId = ReadNonEmptyString("Enter Visitor ID: ");


                            string visitorName = ReadNonEmptyString("Enter Visitor Name: ");


                            int visitorAge = ReadInt("Enter age: ");

                            double visitorHeight = ReadDouble("Enter Visitor Height (cm): ");


                            if (visitorAge < 0)
                            {
                                Console.WriteLine(
                                    "Registration failed: Age cannot be negative.");
                                break;
                            }


                            if (visitorHeight <= 0)
                            {
                                Console.WriteLine(
                                    "Registration failed: Height must be greater than zero.");
                                break;
                            }

                            VisitorCategory category = ReadVisitorCategory();

                            Visitor visitor = new Visitor(visitorId, visitorName, visitorAge, visitorHeight, category);
                            visitorService.RegisterVisitor(visitor);

                            break;

                        }

                    case "2":
                        {

                            Console.WriteLine();
                            Console.WriteLine("=== Issue Ticket ===");

                            string visitorId = ReadNonEmptyString("Enter Visitor ID: ");

                            Visitor visitor = visitorService.FindVisitorById(visitorId);

                            if (visitor == null)
                            {
                                Console.WriteLine(
                                    "Ticket issuing failed: Visitor does not exist.");
                                break;
                            }

                            string ticketId = ReadNonEmptyString("Enter Ticket ID: ");

                            TicketType ticketType = ReadTicketType();

                            int validityDays = ReadInt("Enter ticket validity in days: ");

                            if (validityDays <= 0)
                            {
                                Console.WriteLine(
                                    "Ticket issuing failed: Validity days must be greater than zero.");
                                break;
                            }

                            ticketService.IssueTicket(visitor, ticketType, ticketId, validityDays);

                            break;
                        }

                    case "3":
                        {
                            Console.WriteLine();
                            Console.WriteLine("=== Deactivate Ticket ===");

                            string ticketId = ReadNonEmptyString("Enter Ticket ID: ");

                            ticketService.DeactivateTicket(ticketId);

                            break;
                        }

                    case "4":
                        {
                            Console.WriteLine();
                            Console.WriteLine("=== Validate Ride Access ===");

                            string visitorId = ReadNonEmptyString("Enter Visitor ID: ");

                            Visitor visitor = visitorService.FindVisitorById(visitorId);

                            if (visitor == null)
                            {
                                Console.WriteLine(
                                    "Access validation failed: Visitor does not exist.");
                                break;
                            }

                            string ticketId = ReadNonEmptyString("Enter Ticket ID: ");

                            Ticket ticket = ticketService.FindTicketById(ticketId);

                            if (ticket == null)
                            {
                                Console.WriteLine(
                                    "Access validation failed: Ticket does not exist.");
                                break;
                            }

                            string rideId = ReadNonEmptyString("Enter Ride ID: ");

                            Ride ride = rideService.FindRideById(rideId);

                            if (ride == null)
                            {
                                Console.WriteLine(
                                    "Access validation failed: Ride does not exist.");
                                break;
                            }

                            rideAccessService.ValidateAccess(visitor, ticket, ride);

                            break;
                        }

                    case "5":
                        {
                            Console.WriteLine();
                            Console.WriteLine("=== Create Reservation ===");

                            string reservationId = ReadNonEmptyString("Enter Reservation ID: ");

                            string visitorId = ReadNonEmptyString("Enter Visitor ID: ");

                            string rideId = ReadNonEmptyString("Enter Ride ID: ");

                            DateTime timeSlot = ReadDateTime("Enter Time Slot (yyyy-MM-dd HH:mm): ");

                            reservationService.CreateReservation(reservationId, visitorId, rideId, timeSlot);

                            break;
                        }


                    case "6":
                        {
                            Console.WriteLine();
                            Console.WriteLine("=== Cancel Reservation ===");

                            string reservationId = ReadNonEmptyString("Enter Reservation ID: ");

                            reservationService.CancelReservation(reservationId);

                            break;
                        }


                    case "7":

                        {
                            Console.WriteLine();
                            Console.WriteLine("=== View Ride Occupancy ===");

                            string rideId = ReadNonEmptyString("Enter Ride ID: ");

                            rideService.ViewRideOccupancy(rideId);

                            break;
                        }


                    case "8":
                        {
                            Console.WriteLine();
                            Console.WriteLine("=== Add Ride ===");

                            string rideId = ReadNonEmptyString("Enter Ride ID: ");

                            string rideName = ReadNonEmptyString("Enter Ride Name: ");

                            RideType rideType = ReadRideType();

                            int minimumAge = ReadInt("Enter Minimum Age: ");

                            double minimumHeight = ReadDouble("Enter Minimum Height (cm): ");

                            bool requiresAdult = ReadYesNo("Does this ride require an accompanying adult? (y/n): ");

                            int maximumCapacity = ReadInt("Enter Maximum Capacity: ");

                            rideService.AddRide(rideId, rideName, rideType, minimumAge, minimumHeight, requiresAdult, maximumCapacity);

                            break;
                        }

                    case "9":
                        {
                            Console.WriteLine();
                            Console.WriteLine("=== Update Ride Status ===");

                            string rideId = ReadNonEmptyString("Enter Ride ID: ");

                            RideStatus newStatus = ReadRideStatus();

                            rideService.UpdateRideStatus(rideId, newStatus);

                            break;
                        }

                    case "10":
                        {
                            Console.WriteLine();
                            Console.WriteLine("=== Assign Employee ===");

                            string assignmentId = ReadNonEmptyString("Enter Assignment ID: ");

                            string employeeId = ReadNonEmptyString("Enter Employee ID: ");

                            string locationId = ReadNonEmptyString("Enter Ride/Facility ID: ");

                            DateTime startTime = ReadDateTime("Enter Start Time (yyyy-MM-dd HH:mm): ");

                            DateTime endTime = ReadDateTime("Enter End Time (yyyy-MM-dd HH:mm): ");

                            employeeService.AssignEmployee(assignmentId, employeeId, locationId, startTime, endTime);

                            break;
                        }

                    case "11":
                        isRunning = false;
                        Console.WriteLine("System closed.");
                        break;

                    default:
                        Console.WriteLine(
                            "Invalid option. Please select a number from 1 to 11.");
                        break;
                }
            }
        }






            //
            //
            // helper methods 


            private static int ReadInt(string message)
            {
                while (true)
                {
                    Console.Write(message);

                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int value))
                    {
                        return value;
                    }

                    Console.WriteLine();
                    Console.WriteLine( "Invalid input. please enter a valid whole number.");
                }
            }


             private static double ReadDouble(string message)
            {
                while (true)
                {
                    Console.Write(message);

                    string input = Console.ReadLine();

                    if (double.TryParse(input, out double value))
                    {
                        return value;
                    }

                    Console.WriteLine();
                    Console.WriteLine( "Invalid input. please enter a valid number.");
                }
            }


             private static string ReadNonEmptyString(string message)
            {
                while (true)
                {
                    Console.Write(message);

                    string input = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        return input;
                    }
                    Console.WriteLine();
                    Console.WriteLine(  "Input cant be empty. Please enter a value.");
                }
            }


            private static VisitorCategory ReadVisitorCategory()
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Visitor Categories:");
                    Console.WriteLine("1. General");
                    Console.WriteLine("2. VIP");
                    Console.WriteLine("3. Child");
                    Console.WriteLine("4. Senior");
                    Console.WriteLine("5. Staff Accompanied Minor");

                    int choice = ReadInt("Select visitor category: ");

                    switch (choice)
                    {
                        case 1:
                            return VisitorCategory.General;

                        case 2:
                            return VisitorCategory.VIP;

                        case 3:
                            return VisitorCategory.Child;

                        case 4:
                            return VisitorCategory.Senior;

                        case 5:
                            return VisitorCategory.StaffAccompaniedMinor;

                        default:
                            Console.WriteLine();
                            Console.WriteLine( "Invalid category.please select a number from 1 to 5.");
                            break;
                    }
                }
            }



            private static TicketType ReadTicketType()
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ticket Types:");
                    Console.WriteLine("1. Regular");
                    Console.WriteLine("2. VIP");

                    int choice = ReadInt("Select ticket type: ");

                    switch (choice)
                    {
                        case 1:
                            return TicketType.Regular;

                        case 2:
                            return TicketType.VIP;

                        default:
                            Console.WriteLine( "Invalid ticket type. please select 1 or 2.");
                            break;
                    }
                }
            }



            private static DateTime ReadDateTime(string message)
            {
                while (true)
                {
                    Console.Write(message);

                    string input = Console.ReadLine();

                    if (DateTime.TryParse(input, out DateTime value))
                    {
                        return value;
                    }

                    Console.WriteLine();
                    Console.WriteLine( "Invalid input. Please enter a valid date and time.");
                }
            }


             private static RideType ReadRideType()
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ride Types:");
                    Console.WriteLine("1. Thrill");
                    Console.WriteLine("2. Family");
                    Console.WriteLine("3. Water");

                    int choice = ReadInt("Select ride type: ");

                    switch (choice)
                    {
                        case 1:
                            return RideType.Thrill;

                        case 2:
                            return RideType.Family;

                        case 3:
                            return RideType.Water;

                        default:
                            Console.WriteLine( "Invalid ride type. Please select a number from 1 to 3.");
                            break;
                    }
                }
            }

             private static bool ReadYesNo(string message)
            {
                while (true)
                {
                    Console.Write(message);

                    string input = Console.ReadLine();

                    if (input.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }

                    if (input.Equals("n", StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }

                    Console.WriteLine( "Invalid input. Please enter y or n.");
                }
            }


             private static RideStatus ReadRideStatus()
            {
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ride Statuses:");
                    Console.WriteLine("1. Open");
                    Console.WriteLine("2. Closed");
                    Console.WriteLine("3. Under Maintenance");

                    int choice = ReadInt("Select ride status: ");

                    switch (choice)
                    {
                        case 1:
                            return RideStatus.Open;

                        case 2:
                            return RideStatus.Closed;

                        case 3:
                            return RideStatus.UnderMaintenance;

                        default:
                            Console.WriteLine( "Invalid ride status. Please select a number from 1 to 3.");
                            break;
                    }
                }
            }
        }
    }

