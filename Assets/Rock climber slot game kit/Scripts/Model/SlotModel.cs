using System;
using System.Collections.Generic;
using System.Diagnostics;


public class SlotModel
{
    private readonly SlotConfigData _configData;    //konfig objekat sa osnovnim info o igri
    private readonly List<ReelStripData> _reelStrips;   //lista reelova, ima 5 elemenata za 5 reelova
    private List<PaylineData> _paylineData;             //lista od 10 (po defaultu) linija
    private readonly PaytableData _payTableData;     //preko ovog atributa znacemo koliki multiplier treba da uzmemo
    private readonly ScatterData _scatterData;      //preko ovog atributa hvatamo scatter pravila


    private readonly Random _random;
    private int? seed;
    private int[] finalIndexes;

    public SlotModel(SlotConfigData config, List<ReelStripData> reelStrips, List<PaylineData> paylines, 
        PaytableData payTable, ScatterData scatter, int? seed = null)
    {
        _configData = config;
        _reelStrips = reelStrips;
        _paylineData = paylines;
        _payTableData = payTable;
        _scatterData = scatter;

        _random = seed.HasValue ? new Random(seed.Value) : new Random();
        finalIndexes = new int[5];

        ValidateConfiguration();
    }

    private void ValidateConfiguration()
    {
        if (_reelStrips == null)
            throw new ArgumentNullException(nameof(_reelStrips));

        if (_reelStrips.Count != _configData.numberOfReels)
            throw new Exception("Broj reelova tj velicina liste nije ista kao zadata vrednost za broj reelova u SlotConfigData!!!");

        foreach (ReelStripData reel in _reelStrips)
        {
            if (reel.symbols.Count != _configData.reelStripLength)
                throw new Exception("Duzina reela tj broj pozicija reela nije ista kao zadatak vrednost u SlotConfigData!!!");
        }

        if (_paylineData.Count != _configData.numberOfPaylines)
            throw new Exception("Broj paylines u listi nije kao zadata vrednost u SlotConfigData!!!");

        if (_scatterData.scatterSymbol.symbolID != 6)
            throw new Exception("Ne poklapa se id scatter simbola!!!");
    }

    public GameResult Spin(double betPerLine, int activeLines, List<PaylineData> paylines)
    {
        _paylineData = paylines;

        ValidateSpinInput(betPerLine, activeLines);

        SymbolData[,] grid = GenerateGrid();

        List<LineWin> lineWins = CalculateLineWins(grid, betPerLine);

        ScatterWin scatterWin = CalculateScatterWin(grid, betPerLine*activeLines);

        double totalWin = CalculateTotalWin(lineWins, scatterWin);

        GameResult gameResult = new GameResult(grid, lineWins, scatterWin, totalWin, finalIndexes);

        

        return gameResult;
    }

    

    private void ValidateSpinInput(double betPerLine, int activeLines)
    {
        if (betPerLine < _configData.minBetPerLine || betPerLine > _configData.maxBetPerLine)
            throw new Exception("Bet po liniji nije u range-u kao sto je definisano u SlotConfigData!!!");

        if (activeLines <= 0 || activeLines > _configData.numberOfPaylines)
            throw new Exception("Broj paylines nije u range-u kao sto je definisano u SlotConfigData!!!");
    }

    private SymbolData[,] GenerateGrid()
    {
        int reels = _configData.numberOfReels;
        int rows = _configData.numberOfRows;

        var grid = new SymbolData[reels, rows];

        for (int reelIndex = 0; reelIndex < reels; reelIndex++)
        {
            var strip = _reelStrips[reelIndex].symbols;
            int stripLength = strip.Count;

            int startIndex = _random.Next(0, stripLength);
            finalIndexes[reelIndex] = startIndex;

            for (int rowIndex = 0; rowIndex < rows; rowIndex++)
            {
                int stripIndex = (startIndex + rowIndex) % stripLength;
                
                grid[reelIndex, rowIndex] = strip[stripIndex];
            }
        }

        return grid;
    }

    private List<LineWin> CalculateLineWins(SymbolData[,] grid, double betPerLine)
    {
        List<PaylineData> paylines = _paylineData;
        double allPaylinesWin = 0;
        List<LineWin> winLines = new List<LineWin>();

        foreach (PaylineData payline in paylines)
        {

            LineWin lineWin = CalculateLineWin(payline, grid, betPerLine);

            if (lineWin.payout != 0) { 
                winLines.Add(lineWin);
                allPaylinesWin += lineWin.payout;
            }

            
        }

        return winLines;
    }

    private LineWin CalculateLineWin(PaylineData payline, SymbolData[,] grid, double betPerLine)
    {
        int firstIndice = payline.rowIndices[0];
        SymbolData firstElement = grid[0, firstIndice], element;

        LineWin lineWin = new LineWin();
        int reels = _configData.numberOfReels;
        int counter = 1;

        for (int reelIndex = 1; reelIndex < reels; reelIndex++) {
            int indice = payline.rowIndices[reelIndex];
            element = grid[reelIndex, indice];

            if (firstElement.symbolID == element.symbolID)
            {
                counter++;
            }
            else {
                break;
            }
        }

        lineWin.line = payline;
        lineWin.symbol = firstElement;
        lineWin.matchCount = counter;
        lineWin.payout = betPerLine * _payTableData.GetPayout(firstElement, counter);

        return lineWin;
    }

    private ScatterWin CalculateScatterWin(SymbolData[,] grid, double totalBet)
    {
        int counter = 0;
        int reels = _configData.numberOfReels;
        int rows = _configData.numberOfRows;

        for (int reelIndex = 0; reelIndex < reels; reelIndex++) {
            for (int rowIndex = 0; rowIndex < rows; rowIndex++) {
                if (grid[reelIndex,rowIndex].symbolID == _scatterData.scatterSymbol.symbolID)
                {
                    counter++;
                    break;
                }
            }
        }

        ScatterWin scatterWin = new ScatterWin();
        scatterWin.scatterData = _scatterData;
        scatterWin.matchCount = counter;
        scatterWin.payout = totalBet * _payTableData.ScatterPayout( _scatterData, counter);

        return scatterWin;
    }

    private double CalculateTotalWin(List<LineWin> lineWins, ScatterWin scatterWin)
    {
        double totalLineWins = 0;

        foreach(LineWin lineWin in lineWins) {
            totalLineWins += lineWin.payout;
        }

        return totalLineWins + scatterWin.payout;
    }
}