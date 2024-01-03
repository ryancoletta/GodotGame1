using Godot;
using System;

public partial class Tile : Node2D
{
    public Vector2I coordinate;
    Piece occupant;

    public Piece Occupant { get { return occupant; } }

    public Tile(int x, int y)
    {
        coordinate = new Vector2I(x, y);
    }

    public void Occupy(Piece occupant) 
    {
        this.occupant = occupant;
    }

    public void Vacate ()
    {
        occupant = null;
    }
}
