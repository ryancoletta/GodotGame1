using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Grid : TileMap
{
    const int PIECE_LAYER = 0;
    const int PLAYER_SOURCE_ID = 0;
    Rect2I rect;
    Tile[,] tiles;
    List<Piece> pieces = new List<Piece>();
    Player player;

    public override void _Ready()
    {
        rect = GetUsedRect();
        tiles = new Tile[rect.Size.X, rect.Size.Y];
        for (int y = 0; y < rect.Size.Y; y++)
        {
            for (int x = 0; x < rect.Size.X; x++)
            {
                tiles[x, y] = new Tile(x, y);
                Vector2I mapCoordinate = GridToMap(new Vector2I(x, y));
                Vector2 localPosition = MapToLocal(mapCoordinate);
                Vector2 globalPosition = ToGlobal(localPosition);
                tiles[x, y].Position = globalPosition;

                // TODO: add switch statement that modifies tile class based on id?
            }
        }
    }

    public override void _Process(double delta)
    {
        // HACK: tilemap scene collections are not instantiated by _Ready()
        if (pieces.Count == 0)
        {
            foreach (Node2D child in GetChildren())
            {
                Vector2I mapCoordinate = LocalToMap(child.Position);
                Vector2I gridCoordinate = MapToGrid(mapCoordinate);
                ((Piece)child).Initialize(tiles[gridCoordinate.X, gridCoordinate.Y]);
                pieces.Add((Piece)child);
            }

            // find the player
            Array<Vector2I> playerInstances = GetUsedCellsById(PIECE_LAYER, PLAYER_SOURCE_ID);
            if (playerInstances.Count == 0)
            {
                GD.PrintErr("no player found in tilemap");
            }
            else if (playerInstances.Count > 1)
            {
                GD.PrintErr(playerInstances.Count + " players found in tilemap, there should only be 1");
            }
            else
            {
                Vector2I playerMapCoordinate = playerInstances[0];
                Vector2I playerGridCoordinate = MapToGrid(playerMapCoordinate);
                player = (Player)tiles[playerGridCoordinate.X, playerGridCoordinate.Y].Occupant;
                GD.Print(player);
            }
        }
    }

    // returns the grid coordinates (lower left origin) from the given map coordinates  
    private Vector2I MapToGrid(Vector2I mapCoordinate)
    {
        return new Vector2I(Mathf.RoundToInt(rect.Size.X / 2f) - mapCoordinate.X, Mathf.RoundToInt(rect.Size.Y / 2f) - 1 + mapCoordinate.Y);
    }

    // returns the map coordinates (center origin) from the given grid coordinates  
    private Vector2I GridToMap(Vector2I gridCoordinate)
    {
        return new Vector2I(gridCoordinate.X - Mathf.RoundToInt(rect.Size.X / 2f), gridCoordinate.Y - Mathf.RoundToInt(rect.Size.Y / 2f));
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
            Vector2I destinationCoordinate = player.Coordinate + Vector2I.Up;
            CommandInvoker.AddCommand(new IMoveTo(player, tiles[destinationCoordinate.X, destinationCoordinate.Y]));
            CommandInvoker.ExecuteCommands();
            return;
        }
        else if (@event.IsActionPressed("right"))
        {
            Vector2I destinationCoordinate = player.Coordinate + Vector2I.Right;
            CommandInvoker.AddCommand(new IMoveTo(player, tiles[destinationCoordinate.X, destinationCoordinate.Y]));
            CommandInvoker.ExecuteCommands();
            return;
        }
        else if (@event.IsActionPressed("down"))
        {
            Vector2I destinationCoordinate = player.Coordinate + Vector2I.Down;
            CommandInvoker.AddCommand(new IMoveTo(player, tiles[destinationCoordinate.X, destinationCoordinate.Y]));
            CommandInvoker.ExecuteCommands();
            return;
        }
        else if (@event.IsActionPressed("left"))
        {
            Vector2I destinationCoordinate = player.Coordinate + Vector2I.Left;
            CommandInvoker.AddCommand(new IMoveTo(player, tiles[destinationCoordinate.X, destinationCoordinate.Y]));
            CommandInvoker.ExecuteCommands();
            return;
        }
    }
}
