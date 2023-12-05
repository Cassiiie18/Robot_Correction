using RobotApp.Enums;

namespace RobotApp.Events;

public class RobotEventArgs : EventArgs
{
    public RobotEventArgs(MessageType type, string message)
    {
        Type = type;
        Message = message;
    }
    
    public MessageType Type { get; set; }

    public string Message { get; set; }
}