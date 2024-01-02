using System;

// https://www.youtube.com/watch?v=I1BocNFIkwI&ab_channel=BoardToBitsGames
public interface ICommand
{
    void Execute() { }
    void Undo() { }
}
