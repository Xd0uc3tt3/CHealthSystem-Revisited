using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSystem_Revisited
{
    public class Health
    {
        public int CurrentHealth;
        public int MaxHealth;

        public Health(int maxHealth)
        {
            if (maxHealth <= 0)
            {
                maxHealth = 1;
            }

            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(int ChangeAmount)
        {
            CurrentHealth -= ChangeAmount;

            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
        }

        public void Heal(int ChangeAmount)
        {
            CurrentHealth += ChangeAmount;

            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }
    }

    public class Player
    {
        //
    }

    internal class Program
    {
        static bool IsAlive = true;
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name: ");
            string playerName = Console.ReadLine();

            Random random = new Random();


            while (IsAlive == true)
            {
                Console.Clear();
                ShowHUD();

                Console.WriteLine();
                Console.WriteLine("Press D to take damage or H to heal.");

                ConsoleKey key = Console.ReadKey(true).Key;

                int ChangeAmount = random.Next(1, 21);

                if (key == ConsoleKey.D)
                {
                    Console.WriteLine($"You took damage.");
                }
                else if (key == ConsoleKey.H)
                {
                    Console.WriteLine($"You healed health.");
                }
            }

            Console.Clear();
        }

        static void ShowHUD()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Player:");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Health: ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Shield:");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Status:");

            Console.ResetColor();
        }
    }
}
