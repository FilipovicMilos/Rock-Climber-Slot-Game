using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaylinesController : MonoBehaviour
{
    [SerializeField] private SlotGameController gameController;

    [SerializeField] private SlotConfigData configData;
    

    [SerializeField] private List<PaylineData> currentPaylines;

    private List<PaylineData> allPaylines;
    


    private void Awake()
    {
        FillList();
    }

    private void FillList()
    {
        allPaylines = new List<PaylineData>();

        for (int i = 0; i < currentPaylines.Count; i++)
        {
            allPaylines.Add(currentPaylines[i]);
        }
    }

    public bool Remove()
    {
        if (RemoveLine())
        {
            gameController.UpdatePaylines(currentPaylines);

            return true;
        }

        return false;
    }

    public bool Add()
    {
        if (AddLine())
        {
            gameController.UpdatePaylines(currentPaylines);

            return true;
        }

        return false;
    }

    public bool AddLine()
    {

        

        if (currentPaylines != null && currentPaylines.Count < configData.numberOfPaylines) {
            int count = currentPaylines.Count;

            PaylineData payline = allPaylines[count];

            currentPaylines.Add(payline);

            Debug.Log("Linija dodata, broj linija koje igrate je: " + currentPaylines.Count);

            return true;
        }
        else
        {
            Debug.Log("Linija ne moze biti dodata, trenutno koristite sve linije");

            return false;
        }
        
    }

    public bool RemoveLine()
    {
        if (currentPaylines != null && currentPaylines.Count > 1)
        {
            currentPaylines.Remove(currentPaylines[currentPaylines.Count - 1]);

            Debug.Log("Linija oduzeta, broj linija koje igrate je: " + currentPaylines.Count);

            return true;
        }
        else
        {
            Debug.Log("Linija ne moze biti oduzeta, trenutno koristite samo 1 liniju");

            return false;
        }
    }

    internal int GetCount()
    {
        return currentPaylines.Count;
    }

}
