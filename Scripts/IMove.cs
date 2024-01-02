using Godot;
using System;

public partial class IMove : ICommand
{
    Piece piece;
    Vector2I direction;

    public IMove(Piece piece, Vector2I direction)
    {
        this.piece = piece;
        this.direction = direction;
    }

    public void Execute()
    {
        piece.Move(direction);
    }

    public void Undo()
    {
        piece.Move(-direction);
    }
}
