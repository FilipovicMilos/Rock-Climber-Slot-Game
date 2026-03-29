using UnityEngine;

[CreateAssetMenu(fileName = "ScatterData", menuName = "Scriptable Objects/ScatterData")]
public class ScatterData : ScriptableObject
{
    [Header("Scatter Symbol")]
    [Tooltip("Referenca na SymbolData koji sluzi kao scatter (Rock Climber Logo)")]
    public SymbolData scatterSymbol;

    [Header("Minimum win")]
    [Tooltip("Minimalan broj scatter simbola na ekranu da bi doslo do isplate")]
    public int minimumCount = 3;

    [Header("Payouts")]
    [Tooltip("Isplata kada padnu tacno 3 scatter simbola bilo gde na ekranu")]
    public int payout3x = 5;
    [Tooltip("Isplata kada padnu tacno 4 scatter simbola bilo gde na ekranu")]
    public int payout4x = 20;
    [Tooltip("Isplata kada padnu tacno 5 scatter simbola bilo gde na ekranu")]
    public int payout5x = 100;
}
