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
}
