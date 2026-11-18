using HorizonAdventurePark.Enums;

namespace HorizonAdventurePark.Models
{
      public class Visitor
    {
        public String Id {  get; private set; }
        public String Name { get; set; }
        public int Age { get; set; }
        public double Height { get; set; }
        public VisitorCategory Category { get; set; }



        public Visitor(String id , String name ,int age ,  double height ,VisitorCategory category)
        {
            Id = id ;
            Name = name;
            Age = age;
            Height = height;
            Category = category;

        }



    }
}
