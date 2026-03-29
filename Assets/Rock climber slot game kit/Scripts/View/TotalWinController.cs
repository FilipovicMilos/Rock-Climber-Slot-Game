using TMPro;
using UnityEngine;

public class TotalWinController : MonoBehaviour
{

    [SerializeField] private TMP_Text totalWinText;

    private double totalWin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        totalWin = 0;
        totalWinText.text = $"LAST SPIN WIN: {totalWin}";

    }

    public void UpdateWin(double win) { 
        totalWin = win;

        totalWinText.text = $"LAST SPIN WIN: {totalWin}";
    }

}
