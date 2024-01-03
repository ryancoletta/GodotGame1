using Godot;
using System;

public partial class IMoveTo : ICommand
{
    Piece piece;
    Tile origin;
    Tile destination;

    public IMoveTo(Piece piece, Tile tile)
    {
        this.piece = piece;
        origin = piece.Tile;
        destination = tile;
    }

    public void Execute()
    {
        piece.MoveTo(destination);
    }

    public void Undo()
    {
        piece.MoveTo(origin);
    }
}
