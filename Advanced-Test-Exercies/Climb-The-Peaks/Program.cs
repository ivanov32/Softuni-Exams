using System;
using System.Collections.Generic;
using System.Linq;

namespace ClimbThePeaks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stack<int> dailyPortions = new Stack<int>(Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
            Queue<int> dailyStamina = new Queue<int>(Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

            Dictionary<string,int> givenMountains = new Dictionary<string,int>()
            {
                {"Vihren",80 },
                {"Kutelo",90 },
                {"Banski Suhodol",100 },
                {"Polezhan",60 },
                {"Kamenitza",70 }
            };
            Queue<string> climbedMountains = new Queue<string>();
            int day = 0;
            while (true)
            {
                if (day == 7)
                {
                    break;
                }
                if (dailyPortions.Count == 0 || dailyStamina.Count == 0)
                {
                    break;
                }
                if (givenMountains.Count == 0)
                {
                    break;
                }
                int food = dailyPortions.Pop();
                int stamina = dailyStamina.Dequeue();
                int sum = food + stamina;
                if (sum >= givenMountains.First().Value)
                {
                    climbedMountains.Enqueue(givenMountains.First().Key);
                    givenMountains.Remove(givenMountains.First().Key);
                }
                else
                {
                    day++;
                }
                
            }
            if (givenMountains.Count() == 0)
            {
                Console.WriteLine("Alex did it! He climbed all top five Pirin peaks in one week -> @FIVEinAWEEK");
            }
            else
            {
                Console.WriteLine("Alex failed! He has to organize his journey better next time -> @PIRINWINS");
            }
            if (climbedMountains.Count > 0)
            {
                Console.WriteLine($"Conquered peaks:");
                foreach (var mountains in climbedMountains)
                {
                    Console.WriteLine($"{mountains}");
                }
            }
            
        }
    }
}
