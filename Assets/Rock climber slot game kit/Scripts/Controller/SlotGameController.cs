using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotGameController : MonoBehaviour
{
    [Header("ViewScripts")]
    [SerializeField] private SpinButtonController spinButtonController;
    [SerializeField] private ResultView resultView;
    [SerializeField] private AllReelsController allReelsController;

    [Header("Controllers")]
    [SerializeField] private BetPerLineController betPerLineController;
    [SerializeField] private PaylinesController paylinesController;
    private BalanceController balanceController;
    [SerializeField] private RiskGameController riskGameController;



    [Header("Game Config")]
    [SerializeField] private SlotConfigData slotConfigData;
    [SerializeField] private List<ReelStripData> reelStripsData;
    [SerializeField] private List<PaylineData> paylinesData;
    [SerializeField] private PaytableData paytableData;
    [SerializeField] private ScatterData scatterData;

    private SlotModel slotModel;
    private GameResult gameResult;

    private void Awake()
    {
        balanceController = new BalanceController();

        slotModel = new SlotModel(slotConfigData, reelStripsData, paylinesData, paytableData, scatterData);
    }

    public GameResult RequestSpin() {

        Debug.Log("Trenutni balans kredita je " + BalanceController.currentBalance);

        double betPerLine = betPerLineController.currentBet;
        int paylinesNumber = paylinesController.GetCount(); ;

        double totalBet = betPerLine * paylinesNumber;

        bool success = balanceController.SubstractSpinAmount(totalBet);



        if (!success) {
            Debug.Log("Nemate dovoljno kredita za spin!!!");
            return null;
        }

        
        Debug.Log("Pozvana igra! Ulog je " + totalBet + " kredita, za svaku od " + paylinesNumber + " linija po " + betPerLine + " kredita");

        GameResult result = CallSpin(betPerLine, paylinesNumber);

        return result;
    }

    private GameResult CallSpin(double betPerLine, int paylinesNumber)
    {
        
        gameResult = slotModel.Spin(betPerLine, paylinesNumber, paylinesData);

        PrintResult(gameResult);

        Debug.Log("Novi balans je " + BalanceController.currentBalance);

        return gameResult;
    }

    private void PrintResult(GameResult gameResult)
    {
        int reels = gameResult.grid.GetLength(0);
        int rows = gameResult.grid.GetLength(1);

        SymbolData[,] result = new SymbolData[rows, reels];

        for (int reel = 0; reel < reels; reel++)
        {
            string line = "Reel " + (reel + 1) + ": ";

            for (int row = 0; row < rows; row++)
            {
                line += gameResult.grid[reel, row].symbolName + "    ";
            }

            Debug.Log(line);
        }

        string paylines = "";
        for (int i = 0; i < paylinesData.Count; i++)
        {
            paylines += paylinesData[i].paylineName + "    ";
        }

        Debug.Log("Linije koje se proveravaju: " + paylines);
        

        string winLines = "";
        for (int i = 0; i < gameResult.lineWins.Count; i++)
        {
            winLines += gameResult.lineWins[i].line.paylineName + " = " + gameResult.lineWins[i].payout + "    ";
        }

        Debug.Log("Dobitne linije: " + winLines);

        Debug.Log("Spin zavrsen! Total win: " + gameResult.totalWin + " kredita.");

    }

    internal void CallAddWinToBalance()
    {
        balanceController.AddSpinWin(gameResult.totalWin);
    }

    internal void UpdatePaylines(List<PaylineData> currentPaylines)
    {
        paylinesData = currentPaylines;
    }

    internal void RiskGame(int black)
    {
        throw new NotImplementedException();
    }

    
}
