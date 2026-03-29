using UnityEngine;

public enum SymbolType
{
    Low,
    Mid,
    High,
    Scatter
}

[CreateAssetMenu(fileName = "SymbolData", menuName = "Scriptable Objects/SymbolData")]
public class SymbolData : ScriptableObject
{
    [Header("Identity")]
    public int symbolID;
    public string symbolName;
    public Sprite symbolSprite;
    public SymbolType symbolType;

    [Header("Reel weight")]
    [Tooltip("Koliko cesto se ovaj simbol pojavljuje na reelu od 100 mesta")]
    public int reelWeight;

    [Header("Line payouts (multiplier * bet per line)")]
    [Tooltip("Isplata za 3 ista simbola na liniji")]
    public int payout3x;
    [Tooltip("Isplata za 4 ista simbola na liniji")]
    public int payout4x;
    [Tooltip("Isplata za 5 istih simbola na liniji")]
    public int payout5x;
}
