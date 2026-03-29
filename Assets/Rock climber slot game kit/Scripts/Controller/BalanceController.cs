using System;
using UnityEngine;

public class BalanceController
{
    public static double currentBalance { get; private set; }

    public BalanceController()
    {
        currentBalance = 10000;

    }

    public bool SubstractSpinAmount(double amount)
    {
        if (currentBalance < amount) {
            return false;
        }

        currentBalance -= amount;

        return true;
    }

    public void AddSpinWin(double winAmount)
    {
        currentBalance += winAmount;
    }
}
