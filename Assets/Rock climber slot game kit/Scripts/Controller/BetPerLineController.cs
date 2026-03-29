using System;
using UnityEngine;
using UnityEngine.UI;

public class BetPerLineController : MonoBehaviour
{

    [SerializeField] private SlotConfigData configData;


    public double currentBet {  get; private set; }

    private void Awake()
    {
        currentBet = configData.defaultBetPerLine;

        
    }

    public bool Increase()
    {
        if (currentBet < configData.maxBetPerLine)
        {
            if(currentBet < 5)
            {
                currentBet++;

            }else if(currentBet ==5)
            {
                currentBet += 5;

            }else if(currentBet >=10)
            {
                currentBet += 10;
            }
            
            
            Debug.Log("Povecali ste bet po liniji, sada je " +  currentBet);

            return true;
        }
        else
        {
            Debug.Log("Ne mozete povecati bet po liniji, vec je maksimalan");

            return false;
        }
            
    }

    public bool Decrease()
    {
        if (currentBet > configData.minBetPerLine)
        {
            if (currentBet <= 5)
            {
                currentBet--;

            }
            else if (currentBet == 10)
            {
                currentBet -= 5;

            }
            else if (currentBet > 10)
            {
                currentBet -= 10;
            }

            
            Debug.Log("Smanjili ste bet po liniji, sada je " + currentBet);

            return true;
        }
        else
        {
            Debug.Log("Ne mozete smanjiti bet po liniji, vec je minimalan");

            return false;
        }
            
    }

}
