using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HealthSystem_Revisited
{
    public class Health
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }

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
            if (ChangeAmount < 0)
            {
                return;
            }

            CurrentHealth -= ChangeAmount;

            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
            }
        }

        public void Heal(int ChangeAmount)
        {
            if (ChangeAmount < 0)
            {
                return;
            }

            CurrentHealth += ChangeAmount;

            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
        }
    }

    public class Player
    {
        public string Name { get; set; }

        public Health Health { get; private set; }
        public Health Shield { get; private set; }

        public Player(string name, int maxHealth, int maxShield)
        {
            Name = name;
            Health = new Health(maxHealth);
            Shield = new Health(maxShield);
        }

        public void TakeDamage(int ChangeAmount)
        {
            if (ChangeAmount < 0)
            {
                return;
            }

            if (Shield.CurrentHealth > 0)
            {
                int shieldDamage = Math.Min(ChangeAmount, Shield.CurrentHealth);
                Shield.TakeDamage(shieldDamage);
                ChangeAmount -= shieldDamage;
            }

            if (ChangeAmount > 0)
            {
                Health.TakeDamage(ChangeAmount);
            }
        }

        public string GetStatus()
        {
            if (Health.CurrentHealth == 0)
            {
                return "Dead";
            }

            float healthPercent = (float)Health.CurrentHealth / Health.MaxHealth;

            if (healthPercent >= 0.75f)
            {
                return "Healthy";
            }
            else if (healthPercent >= 0.40f)
            {
                return "Hurt";
            }
            else if (healthPercent >= 0.10f)
            {
                return "Critical Condition";
            }
            else
            {
                return "Near Death";
            }

        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your name: ");
            string playerName = Console.ReadLine();

            Player player = new Player(playerName, 100, 100);
            Random random = new Random();


            while (player.GetStatus() != "Dead")
            {
                Console.Clear();
                ShowHUD(player);

                Console.WriteLine();
                Console.WriteLine("Press D to take damage or H to heal.");

                ConsoleKey key = Console.ReadKey(true).Key;

                int ChangeAmount = random.Next(1, 21);

                if (key == ConsoleKey.D)
                {
                    player.TakeDamage(ChangeAmount);
                    Console.Clear();
                    ShowHUD(player);
                    Console.WriteLine();
                    Console.WriteLine($"You took {ChangeAmount} damage.");
                }
                else if (key == ConsoleKey.H)
                {
                    player.TakeDamage(ChangeAmount);
                    Console.Clear();
                    ShowHUD(player);
                    Console.WriteLine();
                    Console.WriteLine($"You healed {ChangeAmount} health.");
                }

                Console.ReadKey(true);

            }

            Console.Clear();
            ShowHUD(player);
            Console.WriteLine();
            Console.WriteLine("You died! Press any key...");
            Console.ReadKey();
        }

        static void ShowHUD(Player player)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Player: {player.Name}");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Health: {player.Health.CurrentHealth}/{player.Health.MaxHealth}");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Shield: {player.Shield.CurrentHealth}/{player.Shield.MaxHealth}");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Status: {player.GetStatus()}");

            Console.ResetColor();
        }
    }
}
