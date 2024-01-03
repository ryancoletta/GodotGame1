using Godot;
using System;

public partial class Piece : Node2D
{
    protected Tile tile;

    public Tile Tile { get { return tile; } }
    public Vector2I Coordinate { get { return tile.coordinate; } }

    public void Initialize(Tile tile)
    {
        this.tile = tile;
        tile.Occupy(this);
        GD.Print("Spawned at " + Coordinate);
    }

    public virtual void MoveTo(Tile destination)
    {
        tile.Vacate();
        tile = destination;
        tile.Occupy(this);
        Position = tile.Position;

        GD.Print("Move to " + Coordinate);

        /*        if (true)
                {
                    //Position = position;
                }
                else
                {
                    Tween newTween = CreateTween();
                    newTween.SetTrans(Tween.TransitionType.Linear);
                    //newTween.TweenProperty(this, "global_position", position, 0.1f);
                    newTween.TweenCallback(new Callable(this, "FinishedMovement"));
                    newTween.Play();
                }*/
    }

    public void FinishedMovement() 
    {
        GD.Print("I've arrived");
    }

    public void Destroy()
    {
        QueueFree();
    }
}
