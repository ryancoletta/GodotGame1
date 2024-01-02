using Godot;
using System;

public partial class Player : Piece
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        foreach (Node2D child in GetChildren())
        {
            if (child is AnimatedSprite2D)
            {
                ((AnimatedSprite2D)child).Play("idle");
            }
        }
        base._Ready();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("undo"))
        {
            CommandInvoker.UndoCommands();
            return;
        }

        if (@event.IsActionPressed("reset"))
        {
            CommandInvoker.ResetCommands(); 
            return;
        }

        if (@event.IsActionPressed("up"))
        {
            //AttemptMove(player, new Vector3i(0, 1, 0));
            CommandInvoker.ExecuteCommands();
            return;
        }
        else if (@event.IsActionPressed("right"))
        {
            CommandInvoker.AddCommand(new IMove(this, new Vector2I(1,0)));
            //AttemptMove(player, new Vector3i(1, 0, 0));
            CommandInvoker.ExecuteCommands();
            return;
        }
        else if (@event.IsActionPressed("down"))
        {
            //AttemptMove(player, new Vector3i(0, -1, 0));
            CommandInvoker.ExecuteCommands();
            return;
        }
        else if (@event.IsActionPressed("left"))
        {
            CommandInvoker.AddCommand(new IMove(this, new Vector2I(-1, 0)));
            //AttemptMove(player, new Vector3i(-1, 0, 0));
            CommandInvoker.ExecuteCommands();
            return;
        }
    }
}
