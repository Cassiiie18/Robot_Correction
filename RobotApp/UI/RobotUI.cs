using RobotApp.Enums;
using RobotApp.Events;
using RobotApp.Models;

namespace RobotApp.UI;

public class RobotUI
{
    public void Display(object sender, RobotEventArgs e)
    {
        switch (e.Type)
        {
            case MessageType.Info:
                Console.ForegroundColor = ConsoleColor.White;
                break;
            case MessageType.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                break;
            case MessageType.Victory:
                Console.ForegroundColor = ConsoleColor.Green;
                break;
        }
        
        Console.WriteLine(e.Message);
        
        Console.ResetColor();
        
        Thread.Sleep(1000);
    }
    
    public void RefreshGrille(object sender, RobotEventArgs e)
    {
        Robot robot = (Robot) sender;
        
        Console.Clear();
        
        for (int y = robot.Grille.Height; y >= 0; y--)
        {
            for (int x = 0; x <= robot.Grille.Width; x++)
            {
                if (x == robot.Grille.FinalX && x == robot.PosX && y == robot.Grille.FinalY && y == robot.PosY)
                {
                    if (x == robot.Grille.Width)
                    {
                        Console.Write("🥩|");
                    }
                    else
                    {
                        Console.Write("|🥩");
                    }
                }
                else if (x == robot.PosX && y == robot.PosY)
                {
                    if (x == robot.Grille.Width)
                    {
                        Console.Write("🦟|");
                    }
                    else
                    {
                         Console.Write("|🦟");
                    }
                   
                }
                else if (x == robot.Grille.FinalX && y == robot.Grille.FinalY)
                {
                    if (x == robot.Grille.Width)
                    {
                        Console.Write("😴|");
                    }
                    else
                    {
                        Console.Write("|😴");
                    }
                }

                
                else
                {
                    if (x == robot.Grille.Width)
                    {
                        Console.Write("|");
                    }
                    else
                    {
                        Console.Write("|  ");
                    }
                }

                
            }
            Console.WriteLine();
        }
    }

    public Order Menu()
    {
        
        Console.WriteLine("0. Avancer");
        Console.WriteLine("1. Tourner à gauche");
        Console.WriteLine("2. Tourner à droite");
        Console.WriteLine("3. Exécuter");
        
        string input = "";

        Order order;
        
        do
        {
            input = Console.ReadLine();
            
        } while (Enum.TryParse(input, out order) == false);

        return order;

    }
}