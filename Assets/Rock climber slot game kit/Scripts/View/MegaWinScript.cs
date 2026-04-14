using System;
using TMPro;
using UnityEngine;

public class MegaWinScript : MonoBehaviour
{
    private double winCounter = 0;
    private bool doUpdate = false;
    private bool doWinCounterLog = false;
    private double totalWin = 0;

    [SerializeField] private TMP_Text totalBetText;
    private int i = 0;

    // Update is called once per frame
    void Update()
    {
        if (!doUpdate)
        {
            return;
        }

        winCounter += (totalWin / 200);

        i++;
        if(i % 2 == 0 && doWinCounterLog)
        {
            if (winCounter >= totalWin)
            {
                winCounter = totalWin;
                doWinCounterLog = false;
            }

            totalBetText.text = $"{(int)winCounter}";
        }

        if(winCounter >= 2 * totalWin)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;

            transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;

            winCounter = 0;
            doUpdate = false;
            totalWin = 0;
            i = 0;
        }
    }

    internal void AnimateMegaWin(double totalWin)
    {
        Transform totalWinCounterChild = transform.GetChild(3);

        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
        
        totalWinCounterChild.GetComponent<SpriteRenderer>().enabled = true;
        totalWinCounterChild.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = true;

        doWinCounterLog = true;
        doUpdate = true;
        this.totalWin = totalWin;
    }
}
