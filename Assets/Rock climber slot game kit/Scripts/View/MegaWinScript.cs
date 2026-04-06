using System;
using TMPro;
using UnityEngine;

public class MegaWinScript : MonoBehaviour
{
    private double winCounter = 0;
    private bool flag = false;
    private double totalWin = 0;

    [SerializeField] private TMP_Text totalBetText;
    private int i = 0;

    // Update is called once per frame
    void Update()
    {
        if (!flag)
        {
            return;
        }

        i++;
        if(i % 2 == 0)
        {
            winCounter += 5;

            if (winCounter <= totalWin)
            {
                totalBetText.text = $"{winCounter}";
            }
        }

        if(winCounter >= totalWin + 300)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;

            transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = false;
            transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;

            flag = false;
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


        flag = true;
        this.totalWin = totalWin;
    }
}
