using HorizonAdventurePark.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HorizonAdventurePark.Services
{
    public class VisitorService
    {

        private Visitor[] visitors;
        private int visitorCount = 0;



        public VisitorService(Visitor[] visitors)
        {
            this.visitors = visitors;
        }

        public void RegisterVisitor(Visitor visitor)
        {
            if (visitorCount >= visitors.Length)
            {
                Console.WriteLine("Registration failed: Visitor storage is full.");
                return;
            }

            if (IsDuplicate(visitor.Id))
            {
                Console.WriteLine("Registration failed: a visitor with this ID already exists.");
                return;
            }

            visitors[visitorCount] = visitor;
            visitorCount++;

            Console.WriteLine();
            Console.WriteLine("Visitor registered successfully.");
        }

        //helper method
        private bool IsDuplicate(string visitorId)
        {
            for (int i = 0; i < visitorCount; i++)
            {
                if (visitors[i].Id == visitorId)
                {
                    return true;
                }
            }

            return false;
        }


        public Visitor FindVisitorById(string id)
        {
            for (int i = 0; i < visitorCount; i++)
            {
                if (visitors[i].Id == id)
                {
                    return visitors[i];
                }
            }

            return null;
        }

    }
}
