using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResultView : MonoBehaviour
{
    
    [SerializeField] private TotalWinController totalWinController;

    public Transform resultGrid;

    [SerializeField] private GameObject leftLinesNumbers;
    [SerializeField] private GameObject rightLinesNumbers;

    GameResult gameResult;

    private void Awake()
    {

        int reels = resultGrid.childCount;
        int rows = resultGrid.GetChild(0).childCount;

    }

    public void AnimateResult(GameResult gameResult)
    {
        this.gameResult = gameResult;

        totalWinController.UpdateWin(gameResult.totalWin);

        AnimateLines(gameResult.lineWins);


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

    internal void EndLinesAnimation()
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

}
