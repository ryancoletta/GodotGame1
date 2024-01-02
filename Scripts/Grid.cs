using Godot;
using Godot.Collections;
using System;
using System.Linq;

public static class Grid
{
    const float TILE_SIZE = 32f;
    static Dictionary<Vector2I, Piece> pieces = new Dictionary<Vector2I, Piece>();

    public static void Register(Piece piece)
    {
        Vector2I coordinates = (Vector2I)((piece.Position + Vector2.One * TILE_SIZE / 2f) / TILE_SIZE);
        pieces.Add(coordinates, piece);
        piece.Initialize(coordinates);
        GD.Print(coordinates);
        GD.Print(piece);
    }


    public static Vector2 CoordinatesToPosition(Vector2I coordinates)
    {
        return (Vector2)coordinates * TILE_SIZE - Vector2.One * TILE_SIZE / 2f;
    }
}
