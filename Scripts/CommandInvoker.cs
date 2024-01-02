using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public static class CommandInvoker
{
    static Queue<ICommand> commandBuffer = new Queue<ICommand>();
    static Stack<Stack<ICommand>> commandHistory = new Stack<Stack<ICommand>>();
    static Stack<Stack<Stack<ICommand>>> resetHistories = new Stack<Stack<Stack<ICommand>>>();

    public static void AddCommand(ICommand command)
    {
        commandBuffer.Enqueue(command);
    }

    public static void ExecuteCommands()
    {
        if (commandBuffer.Count > 0)
        {
            Stack<ICommand> commandBufferThisTurn = new Stack<ICommand>();
            while (commandBuffer.Count > 0)
            {
                ICommand command = commandBuffer.Dequeue();
                command.Execute();

                commandBufferThisTurn.Push(command);
                GD.Print("Execute " + command);
            }
            commandHistory.Push(commandBufferThisTurn);
        }
    }

    public static void UndoCommands()
    {
        if (commandHistory.Count > 0 && commandBuffer.Count == 0)
        {
            Stack<ICommand> commandsToUndo = commandHistory.Pop();
            while (commandsToUndo.Count > 0)
            {
                ICommand commandToUndo = commandsToUndo.Pop();
                commandToUndo.Undo();
                GD.Print("Undo " + commandToUndo);
            }
        }
        // if resets exist, undo the reset by re-executing all commands up to the reset point
        else if (resetHistories.Count > 0)
        {
            while (resetHistories.Peek().Count > 0)
            {
                Stack<ICommand> reccordedBuffer = resetHistories.Peek().Pop();
                while (reccordedBuffer.Count > 0)
                {
                    AddCommand(reccordedBuffer.Pop());
                }
                ExecuteCommands();
            }
            resetHistories.Pop();
        }
    }

    public static void ResetCommands()
    {
        if (commandHistory.Count > 0 && commandBuffer.Count == 0)
        {
            // get a copy of the existing command history
            Stack<Stack<ICommand>> tempCommandHistory = new Stack<Stack<ICommand>>();
            foreach (Stack<ICommand> commandBuffer in commandHistory)
            {
                tempCommandHistory.Push(new Stack<ICommand>(commandBuffer));
            }
            tempCommandHistory = new Stack<Stack<ICommand>>(tempCommandHistory.Reverse());

            while (commandHistory.Count > 0 && commandBuffer.Count == 0)
            {
                UndoCommands();
            }

            resetHistories.Push(tempCommandHistory);
        }
    }

    public static void ClearCommands()
    {
        commandBuffer.Clear();
        commandHistory.Clear();
        resetHistories.Clear();
    }
}
