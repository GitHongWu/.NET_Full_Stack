using System;

namespace Exercise5
{
    class Box
    {
        double length = 2;
        double breadth = 21;
        double height = 5;

        public double getVolume()
        {
            return length * breadth * height;
        }
        public void setLength(double len)
        {
            length = len;
        }

        public void setBreadth(double bre)
        {
            breadth = bre;
        }

        public void setHeight(double hei)
        {
            height = hei;
        }
        public static Box operator +(Box b, Box c)
        {
            Box box = new Box();
            box.length = b.length + c.length;
            box.breadth = b.breadth + c.breadth;
            box.height = b.height + c.height;
            return box;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Box Box1 = new Box();
            Box1.setLength(6.0);
            Box1.setBreadth(7.0);
            Box1.setHeight(5.0);

            Box Box2 = new Box();
            Box2.setLength(12.0);
            Box2.setBreadth(13.0);
            Box2.setHeight(10.0);
            
            double volume = Box1.getVolume();
            Console.WriteLine("Volume of Box1 : {0}", volume);

            volume = Box2.getVolume();
            Console.WriteLine("Volume of Box2 : {0}", volume);

            Box Box3 = new Box();
            Box3 = Box1 + Box2;

            volume = Box3.getVolume();
            Console.WriteLine("Volume of Box3 : {0}", volume);
        }
    }
}
