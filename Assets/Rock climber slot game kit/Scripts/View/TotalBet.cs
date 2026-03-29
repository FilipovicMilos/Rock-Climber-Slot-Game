using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TotalBet : MonoBehaviour
{
    [SerializeField] private LinesController lineController;
    [SerializeField] private BetsController betController;

    [SerializeField] private TMP_Text totalBetText;

    public Action OnTotalBetChanged;
    private double x;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        lineController.OnNumberOfLinesChanged += OnNumberOfLinesChanged;
        betController.OnBetChanged += OnBetPerLineChanged;

        x = lineController.numberOfLines * betController.bet;
        totalBetText.text = $"TOTAL BET: {x}";
    }

    private void OnDestroy()
    {
        lineController.OnNumberOfLinesChanged -= OnNumberOfLinesChanged;
    }

    private void OnNumberOfLinesChanged(int newNumberOfLines)
    {
        UpdateBet();
    }

    private void OnBetPerLineChanged(double newBetPerLine)
    {
        UpdateBet();
    }

    private void UpdateBet()
    {
        x = lineController.numberOfLines * betController.bet;

        totalBetText.text = $"TOTAL BET: {x}";
    }
}
