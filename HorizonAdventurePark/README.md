# Horizon Adventure Park — Operations System

## Project Overview

Horizon Adventure Park Operations System is a console-based C# application designed to manage the main operational activities of an adventure park.

---

## Features

- Register and manage visitors.
- Issue and deactivate Regular and VIP tickets.
- Assign rides to tickets according to ticket type.
- Validate ride access based on ticket status, expiration, ride availability, age, height, and adult-accompaniment requirements.
- Manage rides and their statuses.
- Check ride capacity for direct admissions and reservations.
- Create and cancel ride reservations.
- Prevent duplicate reservations for the same visitor, ride, and time slot.
- Manage employees and their assignments.
- Prevent overlapping active employee assignments.
- Validate user input through reusable helper methods.
- Use arrays for in-memory data storage instead of `List<T>`.

## Project Structure

The project is organized into:

- **Models** — Represents the main entities such as Visitor, Ticket, Ride, Reservation, Employee, and Assignment.
               mainly store data and contain behavior that directly belongs to the entity itself.

- **Services** — Contains the main operations and business logic related to each area of the system.
- **Enums** — Contains the enumerations used throughout the system(fixed sets of values instead of using arbitrary strings)

- **Program.cs** — Handles the console menu and user interaction.

---


## Data Storage

The main arrays are created in Program.cs ,These arrays are shared with the corresponding services through constructors.

This allows the services and the main program to work with the same data.


## Visitor Categories

The system supports the following visitor categories: (General,VIP,Child,Senior,Staff Accompanied Minor)

The visitor category affects ticket eligibility and ride access rules.

## Staff Accompanied Minor

StaffAccompaniedMinor represents a minor who is accompanied by a staff member.

This category is used when a ride requires an accompanying adult/staff member.

For rides where:

RequiresAdult == true

the system allows the visitor only when the visitor belongs to:

VisitorCategory.StaffAccompaniedMinor

This rule is part of the business assumptions used in this project.

## Ticket System

The system supports two ticket types:(Regular,VIP)

A Regular ticket includes only rides of the  types: (Family,Water)

A VIP ticket includes all available rides.


## Ticket Eligibility

Ticket types are restricted according to visitor category.

VIP visitors can receive VIP tickets.

Non-VIP visitors receive Regular tickets.

The TicketService validates the selected ticket type before issuing the ticket.


## Ride Capacity

Each ride has a maximum capacity.

The system checks capacity for both: (Direct ride admission ,Reservations)

Both operations use the same capacity limit.

The effective capacity usage is calculated using:

Current Occupancy + Active Reservations

A new reservation or direct admission is allowed only when the resulting occupancy does not exceed the ride's maximum capacity.

Cancelled reservations are not counted toward capacity.


## Reservation Cancellation

Reservations are not physically removed from the array when cancelled.

Instead, their status changes from: Active to Cancelled

This keeps the reservation information in the system while preventing cancelled reservations from affecting capacity calculations.

A cancelled reservation also does not prevent the visitor from creating a new reservation for the same ride and time slot.


## Employee Assignment Conflict

An employee cannot be assigned to two locations during overlapping periods.

The system checks whether the new assignment overlaps with an existing active assignment.

The overlap rule is:

startTime < existingEndTime &&
endTime > existingStartTime

This allows assignments such as:

14:00 - 18:00
18:00 - 22:00

because they only meet at the boundary and do not overlap.

However:

14:00 - 18:00
16:00 - 20:00

is rejected because the time periods overlap.

The system also ensures that:

Start Time < End Time


## Assumptions

The following assumptions were made where the requirements did not explicitly define the behavior:

VIP visitors are eligible for VIP tickets.
Non-VIP visitors are eligible for Regular tickets.
Regular tickets include Family and Water rides.
VIP tickets include all rides.
StaffAccompaniedMinor represents a minor accompanied by a staff member.
A ride with RequiresAdult = true requires the visitor to belong to the StaffAccompaniedMinor category.
Ticket status starts as Active automatically.
Ticket expiration is calculated from the issue date and validity period.
Ride capacity applies to both reservations and direct admissions.
Cancelled reservations do not count toward ride capacity.
Employee assignments that touch exactly at the boundary are allowed because they do not overlap.
The system stores data in memory using arrays during the current program execution.



###How to Run

1. Open the solution in Visual Studio.
2. Build the project to make sure there are no errors.
3. Run the application.
4. Use the numbered menu to interact with the system.

The application stores its data in memory while it is running.