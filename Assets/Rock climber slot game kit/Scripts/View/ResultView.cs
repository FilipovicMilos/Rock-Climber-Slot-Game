using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class ResultView : MonoBehaviour
{
    
    [SerializeField] private TotalWinController totalWinController;

    public Transform resultGrid;

    [SerializeField] private GameObject leftLinesNumbers;
    [SerializeField] private GameObject rightLinesNumbers;

    private GameResult gameResult;

    private Transform symbolInReel;
    private Transform scatterSymbolInReel;

    private Vector3 positionVector;

    [SerializeField] private ScatterData scatterData;

    private void Awake()
    {
        gameResult = null;
        symbolInReel = null;
        scatterSymbolInReel = null;
        positionVector = new Vector3(-1, 2, 0);

        int reels = resultGrid.childCount;
        int rows = resultGrid.GetChild(0).childCount;

    }

    public void AnimateResult(GameResult gameResult)
    {
        this.gameResult = gameResult;

        totalWinController.UpdateWin(gameResult.totalWin);

        AnimateLines(gameResult.lineWins);

        AnimateWinFramesAndWinEffects(gameResult);

        
        AnimateScatterWin(gameResult);
        
    }

    private void AnimateLines(List<LineWin> lineWins)
    {

        if (lineWins.Count == 0)
            return;

        foreach (LineWin winLine in lineWins)
        {
            var childLeft = leftLinesNumbers.transform.GetChild(winLine.line.lineID - 1);
            var childRight = rightLinesNumbers.transform.GetChild(winLine.line.lineID - 1);

            Image activeLineNumberLeft = childLeft.GetComponent<Image>();
            Image activeLineNumberRight = childRight.GetComponent<Image>();

            activeLineNumberLeft.sprite = childLeft.GetComponent<WinLineNumberSwitcher>().winSprite;
            activeLineNumberRight.sprite = childRight.GetComponent<WinLineNumberSwitcher>().winSprite;

            childLeft.GetChild(0).GetComponent<Image>().enabled = true;
        }

    }

    private void AnimateWinFramesAndWinEffects(GameResult gameResult)
    {

            for (int i = 0; i < gameResult.lineWins.Count; i++)
            {
                LineWin lineWin = gameResult.lineWins[i];
                int matchCount = lineWin.matchCount;

                for (int j = 0; j < matchCount; j++)
                {
                    int indexInReel = lineWin.line.rowIndices[j];

                    Transform reel = transform.GetChild(j);

                    symbolInReel = reel.GetChild(indexInReel + 1);

                    symbolInReel.GetChild(0).GetComponent<RectTransform>().anchoredPosition = positionVector;
                    symbolInReel.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

                    symbolInReel.GetChild(1).GetComponent<RectTransform>().anchoredPosition = positionVector;
                    symbolInReel.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
                }
            }
    }

    private void AnimateScatterWin(GameResult gameResult)
    {

        if (gameResult.scatterWin.payout == 0)
            return;

        for (int reelIndex = 0; reelIndex < gameResult.grid.GetLength(0); reelIndex++)
        {

            for (int rowIndex = 0; rowIndex < gameResult.grid.GetLength(1); rowIndex++)
            {
                if (gameResult.grid[reelIndex, rowIndex].symbolID == scatterData.scatterSymbol.symbolID)
                {
                    Transform reel = transform.GetChild(reelIndex);

                    scatterSymbolInReel = reel.GetChild(rowIndex + 1);

                    scatterSymbolInReel.GetChild(0).GetComponent<RectTransform>().anchoredPosition = positionVector;
                    scatterSymbolInReel.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

                    scatterSymbolInReel.GetChild(1).GetComponent<RectTransform>().anchoredPosition = positionVector;
                    scatterSymbolInReel.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
    }

    internal void EndWinAnimations()
    {
        if (symbolInReel == null && scatterSymbolInReel == null)
        {
            gameResult = null;
            return;
        }
        else if(symbolInReel == null && scatterSymbolInReel != null)
        {
            EndScatterWinAnimation();
        }
        else if (symbolInReel != null && scatterSymbolInReel == null)
        {
            EndWinEffectAndFrame();

            EndLinesAnimation();
        }

        gameResult = null;
    }

    private void EndLinesAnimation()
    {

        for (int i = 0; i < leftLinesNumbers.transform.childCount; i++) {
            var childLeft = leftLinesNumbers.transform.GetChild(i);

            childLeft.GetComponent<Image>().sprite = childLeft.GetComponent<WinLineNumberSwitcher>().defaultSprite;

            childLeft.GetChild(0).GetComponent<Image>().enabled=false;
        }

        for (int i = 0; i < rightLinesNumbers.transform.childCount; i++)
        {
            var childRight = rightLinesNumbers.transform.GetChild(i);

            childRight.GetComponent<Image>().sprite = childRight.GetComponent<WinLineNumberSwitcher>().defaultSprite;
        }
    }


    private void EndWinEffectAndFrame()
    {
        if (gameResult == null)
        {
            Debug.Log("prazan result");

            return;
        }
        else if (gameResult.lineWins == null || gameResult.lineWins.Count == 0)
        {
            Debug.Log("prazna lista");

            return;

        }

        for (int i = 0; i < gameResult.lineWins.Count; i++)
        {

            LineWin lineWin = gameResult.lineWins[i];
            int matchCount = lineWin.matchCount;

            for (int j = 0; j < matchCount; j++)
            {
                int indexInReel = lineWin.line.rowIndices[j];

                Transform reel = transform.GetChild(j);

                symbolInReel = reel.GetChild(indexInReel + 1);

                symbolInReel.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

                symbolInReel.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    private void EndScatterWinAnimation()
    {
        if (gameResult.scatterWin.payout == 0)
            return;

        for (int reelIndex = 0; reelIndex < gameResult.grid.GetLength(0); reelIndex++)
        {

            for (int rowIndex = 0; rowIndex < gameResult.grid.GetLength(1); rowIndex++)
            {
                if (gameResult.grid[reelIndex, rowIndex].symbolID == scatterData.scatterSymbol.symbolID)
                {
                    Transform reel = transform.GetChild(reelIndex);

                    scatterSymbolInReel = reel.GetChild(rowIndex + 1);

                    scatterSymbolInReel.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;

                    scatterSymbolInReel.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }
}
