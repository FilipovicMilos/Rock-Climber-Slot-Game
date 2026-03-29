using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PaytableData", menuName = "Scriptable Objects/PaytableData")]
public class PaytableData : ScriptableObject
{
    [Tooltip("Lista svih line simbola u igri sa njihovim isplatama. " +
             "Scatter simbol ne treba dodavati ovde - njime upravlja ScatterData.")]
    public List<SymbolData> symbols;

    public int GetPayout(SymbolData symbol, int matchCount)
    {
        if (!symbols.Contains(symbol)) return 0;

        switch (matchCount)
        {
            case 3: return symbol.payout3x;
            case 4: return symbol.payout4x;
            case 5: return symbol.payout5x;
            default: return 0;
        }
    }

    public int ScatterPayout(ScatterData scatter, int count)
    {
        

        switch (count)
        {
            case 3: return scatter.payout3x;
            case 4: return scatter.payout4x;
            case 5: return scatter.payout5x;
            default: return 0;
        }

    }
}
