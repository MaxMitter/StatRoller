using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace StatsSimulator
{
    class StatSet
    {
        public int[] Stats { get; set; }

        public StatSet()
        {
            Stats = new int[6];
        }

        public override string ToString()
        {
            return $"{Stats[0]}, {Stats[1]}, {Stats[2]}, {Stats[3]}, {Stats[4]}, {Stats[5]}" +
                   $" | Sum: {Stats.Sum()}";
        }

        public int Sum()
        {
            return Stats.Sum();
        }

        public double Avg()
        {
            return Stats.Average();
        }
    }


    class Program
    {
        delegate int RollStat();
        static List<StatSet> Simulate(int amount, RollStat statRoller)
        {
            var list = new List<StatSet>();
            for (int i = 0; i < amount; i++)
                list.Add(Roll(statRoller));
            return list;
        }
        static StatSet Roll(RollStat statRoller)
        {
            StatSet set = new StatSet();
            set.Stats[0] = statRoller();
            set.Stats[1] = statRoller();
            set.Stats[2] = statRoller();
            set.Stats[3] = statRoller();
            set.Stats[4] = statRoller();
            set.Stats[5] = statRoller();

            return set;
        }

        static List<StatSet> Simulate20(int amount)
        {
            return Simulate(amount, () => new Random().Next(1, 20));
        }

        static List<StatSet> Simulate20WithAdv(int amount)
        {
            return Simulate(amount, () =>
            {
                Random r = new Random();
                var r1 = r.Next(1, 20);
                var r2 = r.Next(1, 20);
                return r1 > r2 ? r1 : r2;
            });
        }

        static List<StatSet> Simulate3d6(int amount)
        {
            return Simulate(amount, () =>
            {
                Random r = new Random();
                return r.Next(1, 6) + r.Next(1, 6) + r.Next(1, 6);
            });
        }

        static List<StatSet> Simulate4d6DropLow(int amount)
        {
            return Simulate(amount, () =>
            {
                var list = new List<int>();
                var r = new Random();
                for (int i = 0; i < 4; i++)
                    list.Add(r.Next(1, 6));

                list.Remove(list.Min());
                return list.Sum();
            });
        }

        static void SimulateAll(int amount)
        {
            var list1 = Simulate20(amount);
            var list2 = Simulate20WithAdv(amount);
            var list3 = Simulate3d6(amount);
            var list4 = Simulate4d6DropLow(amount);
            
            Console.WriteLine("#################### D20 ######################");
            Console.WriteLine("Best: " + list1.OrderBy(s => s.Sum()).Last());
            Console.WriteLine("Worst: " + list1.OrderBy(s => s.Sum()).First());
            Console.WriteLine("Average: " + list1.Average(s => s.Avg()) + " in " + list1.Count + " rolls.");
            
            Console.WriteLine("#################### D20 with Advantage ######################");
            Console.WriteLine("Best: " + list2.OrderBy(s => s.Sum()).Last());
            Console.WriteLine("Worst: " + list2.OrderBy(s => s.Sum()).First());
            Console.WriteLine("Average: " + list2.Average(s => s.Avg()) + " in " + list2.Count + " rolls.");
            
            Console.WriteLine("#################### 3D6 ######################");
            Console.WriteLine("Best: " + list3.OrderBy(s => s.Sum()).Last());
            Console.WriteLine("Worst: " + list3.OrderBy(s => s.Sum()).First());
            Console.WriteLine("Average: " + list3.Average(s => s.Avg()) + " in " + list3.Count + " rolls.");
            
            Console.WriteLine("#################### 4d6 drop lowest ######################");
            Console.WriteLine("Best: " + list4.OrderBy(s => s.Sum()).Last());
            Console.WriteLine("Worst: " + list4.OrderBy(s => s.Sum()).First());
            Console.WriteLine("Average: " + list4.Average(s => s.Avg()) + " in " + list4.Count + " rolls.");
        }
        
        static void Main(string[] args)
        {
            SimulateAll(100);
        }
    }
}