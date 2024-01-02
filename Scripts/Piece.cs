using Godot;
using System;

public partial class Piece : Node2D
{
    protected Vector2I coordinates;

    public override void _Ready()
	{
        Grid.Register(this);
	}

    public void Initialize(Vector2I coordinates)
    {
        this.coordinates = coordinates;
    }

    public virtual void Move(Vector2I direction)
    {
        coordinates += direction;
        Position = Grid.CoordinatesToPosition(coordinates);
    }

    public void Destroy()
    {
        QueueFree();
    }
}
