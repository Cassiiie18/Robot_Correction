using RobotApp.Enums;
using RobotApp.Events;
using RobotApp.UI;

namespace RobotApp.Models;

public class Grille
{
    public Grille(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public int Width { get; private set; }
    
    public int Height { get; private set; }

    public int FinalX { get; private set; }
    
    public int FinalY { get; private set; }

    public void InitGame()
    {
        Robot robot = new Robot(this);
        
        Random random = new Random();
        
        FinalX = random.Next(1, Width);
        FinalY = random.Next(1, Height);
        
        RobotUI ui = new RobotUI();
        
        robot.RobotEvent += ui.Display;
        robot.RobotEvent += ui.RefreshGrille;

        Order lastOrder;
        Direction lastDirection = Direction.North;
        
        do
        {
            ui.RefreshGrille(robot, null);
            Console.WriteLine("Veuillez entrer une commande :");
            Console.WriteLine("Votre direction actuelle est " + lastDirection);
            lastOrder = ui.Menu();

            if (lastOrder == Order.TurnLeft)
            {
                lastDirection = (int)lastDirection == 0 ? Direction.West : (Direction)((int)lastDirection - 1);
            }
            else if (lastOrder == Order.TurnRight)
            {
                lastDirection = (int)lastDirection == 3 ? Direction.North : (Direction)((int)lastDirection + 1);
            }
            
            robot.Register(lastOrder);
            robot.RobotEvent.Invoke(robot, new RobotEventArgs(MessageType.Info, "Le robot a reçu l'ordre de " + lastOrder));
        } while (lastOrder != Order.Execute);
    }
}