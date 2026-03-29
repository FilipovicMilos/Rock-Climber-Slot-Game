using TMPro;
using UnityEngine;

public class BalanceTracker : MonoBehaviour
{
    [SerializeField] private TMP_Text totalBalanceText;

    private double totalBalance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalBalance = BalanceController.currentBalance;

        totalBalanceText.text = $"BALANCE: {totalBalance}";
    }

    // Update is called once per frame
    void Update()
    {
        if (totalBalance != BalanceController.currentBalance) { 
            totalBalance = BalanceController.currentBalance;
            totalBalanceText.text = $"BALANCE: {totalBalance}";
        }
    }
}
