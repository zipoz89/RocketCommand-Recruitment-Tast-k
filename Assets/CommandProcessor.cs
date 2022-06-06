using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandProcessor
{
    private Queue<Command> commandsList = new Queue<Command>();

    public bool HasCommandScheduled 
    {
        get
        {
            return commandsList.Count > 0;
        } 
    }
    public void ScheduleCommand(Command command) 
    {
        commandsList.Enqueue(command);
    }

    public bool ExecuteCommand() 
    {
        if (commandsList.Count > 0)
        {
            var command = commandsList.Dequeue();
            command.Execute();
            return true;
        }
        else 
        {
            return false;
        }
    }
}
