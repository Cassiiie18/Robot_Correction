using RobotApp.Enums;
using RobotApp.Events;

namespace RobotApp.Models;



public class Robot
{
    public Robot(Grille grille)
    {
        Grille = grille;
    }
    
    public Action<object, RobotEventArgs> RobotEvent = null;
    
    public Action RobotActions = null;
    
    public int PosX { get; private set; } = 0;

    public int PosY { get; private set; } = 0;

    public Direction Direction { get; private set; } = Direction.North;

    public Grille Grille { get; private set; }
    
    public void Forward()
    {
        switch (Direction)
        {
            case Direction.North:
                if ((PosY + 1) <= Grille.Height)
                {
                    PosY++;
                    RobotEvent.Invoke(this, new RobotEventArgs(MessageType.Info, "Le robot avance vers le nord"));
                }
                else
                {
                    RobotEvent.Invoke(this, new RobotEventArgs(MessageType.Error, "Le robot ne peut pas avancer plus loin vers le nord"));
                }
                break;
            case Direction.South:
                if (PosY - 1 >= 0)
                {
                    PosY--;
                    RobotEvent.Invoke(this, new RobotEventArgs(MessageType.Info, "Le robot avance vers le sud"));
                }
                else
                {
                    RobotEvent.Invoke(this, new RobotEventArgs(MessageType.Error, "Le robot ne peut pas avancer plus loin vers le sud"));
                }
                break;
            case Direction.East:
                if (PosX + 1 <= Grille.Width)
                {
                    PosX++;
                    RobotEvent.Invoke(this, new RobotEventArgs(MessageType.Info, "Le robot avance vers l'est"));
                }
                else
                {
                    RobotEvent.Invoke(this, new RobotEventArgs(MessageType.Error, "Le robot ne peut pas avancer plus loin vers l'est"));
                }
                break;
            case Direction.West:
                if (PosX - 1 >= 0)
                {
                    PosX--;
                    RobotEvent.Invoke(this, new RobotEventArgs(MessageType.Info, "Le robot avance vers l'ouest"));
                }
                else
                {
                    RobotEvent.Invoke(this, new RobotEventArgs(MessageType.Error, "Le robot ne peut pas avancer plus loin vers l'ouest"));
                }
                break;
        }
    }
    
    public void TurnLeft()
    {
        Direction = (int)Direction == 0 ? Direction.West : (Direction)((int)Direction - 1);
    }
    
    public void TurnRight()
    {
        Direction = (int)Direction == 3 ? Direction.North : (Direction)((int)Direction + 1);
    }

    public void Register(Order Order)
    {
        switch (Order)
        {
            case Order.Forward:
                RobotActions += Forward;
                break;
            case Order.TurnLeft:
                RobotActions += TurnLeft;
                break;
            case Order.TurnRight:
                RobotActions += TurnRight;
                break;
            case Order.Execute:
                Execute();
                break;
        }
    }
    
    public void Execute()
    {
        if (RobotActions == null)
        {
            RobotEvent(this, new RobotEventArgs(MessageType.Error, "Hé ho hein bon... "));
        }
        else
        {
            RobotActions();
            if (PosX == Grille.FinalX && PosY == Grille.FinalY)
            {
                RobotEvent(this, new RobotEventArgs(MessageType.Victory, "Le robot a atteint la position finale ! POUPOUPIDOOOOOOU !"));
            }
            else
            {
                RobotEvent(this, new RobotEventArgs(MessageType.Error, "☠️ Le robot n'a pas atteint la position finale ☠️"));
            }
        }
        
    }
    
}