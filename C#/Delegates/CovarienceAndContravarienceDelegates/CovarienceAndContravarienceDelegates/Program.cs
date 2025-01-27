﻿using System;

namespace ConvarienceAndContravarienceDelegates
{
    class Program
    {
        delegate Car CarFactoryDel(int id, string name);

        delegate void LogICECarDetailsDel(ICECar car);
        delegate void LogEVCarDetailsDel(EVCar car);
        static void Main(string[] args)
        {
            CarFactoryDel carFactoryDel1 = CarFactory.ReturnICECar;

            Car iceCar = carFactoryDel1(1, "Audi R8");

            Console.WriteLine($"Object Type: {iceCar.GetType()}");
            Console.WriteLine($"Car Details - {iceCar.GetCarDetails()}");

            CarFactoryDel carFactoryDel2 = CarFactory.ReturnEVCar;
            Car evCar = carFactoryDel2(2, "S-Class");

            Console.WriteLine($"Object Type: {evCar.GetType()}");
            Console.WriteLine($"Car Details - {evCar.GetCarDetails()}");

            LogICECarDetailsDel logICECarDetails = CarFactory.LogCarDetails;
            logICECarDetails(iceCar as ICECar);
            LogEVCarDetailsDel logEVCarDetails = CarFactory.LogCarDetails;
            logEVCarDetails(evCar as EVCar);
        }
    }

    public static class CarFactory
    {
        public static ICECar ReturnICECar(int id, string name)
        {
            return new ICECar { Id = id, Name = name };
        }

        public static EVCar ReturnEVCar(int id, string name)
        {
            return new EVCar { Id = id, Name = name };
        }

        public static void LogCarDetails(Car car)
        {
            if(car is ICECar)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ICECarLogDetails.txt")))
                {
                    sw.WriteLine($"Object Type: {car.GetType()}");
                    sw.WriteLine($"Car Details - {car.GetCarDetails()}");
                }
            }
            else if(car is EVCar)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EVCarLogDetails.txt")))
                {
                    sw.WriteLine($"Object Type: {car.GetType()}");
                    sw.WriteLine($"Car Details - {car.GetCarDetails()}");
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }

    public abstract class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual string GetCarDetails()
        {
            return $"{Id} - {Name}";
        }
    }

    public class ICECar : Car
    {
        public override string GetCarDetails()
        {
            return $"{base.GetCarDetails()} - Internal Combustion Engine";
        }
    }

    public class EVCar : Car
    {
        public override string GetCarDetails()
        {
            return $"{base.GetCarDetails()} - Electric";
        }
    }
}