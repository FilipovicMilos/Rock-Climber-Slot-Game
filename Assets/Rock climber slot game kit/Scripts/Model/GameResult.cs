using System;
using System.Collections.Generic;

public class GameResult
{
    public SymbolData[,] grid { get; }

    public List<LineWin> lineWins { get; }

    public ScatterWin scatterWin { get; }
    
    public double totalWin;

    public int[] finalIndexes;

    public GameResult(SymbolData[,] grid, List<LineWin> lineWins, ScatterWin scatterWin, double totalWin, int[] finalIndexes)
    {
        this.grid = grid;
        this.lineWins = lineWins;
        this.scatterWin = scatterWin;
        this.totalWin = totalWin;
        this.finalIndexes = finalIndexes;
    }
}