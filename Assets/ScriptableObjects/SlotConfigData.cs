using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "SlotConfigData", menuName = "Scriptable Objects/SlotConfigData")]
public class SlotConfigData : ScriptableObject
{
    [Header("Grid")]
    public int numberOfReels = 5;
    public int numberOfRows = 3;

    [Header("Paylines")]
    public int numberOfPaylines = 9;

    [Header("Reel")]
    public int reelStripLength = 100;

    [Header("Bet")]
    public int minBetPerLine = 1;
    public int maxBetPerLine = 50;
    public int defaultBetPerLine = 10;

    [Header("ReelHeight")]
    public int numberOfInPlayElements = 3;
    public int numberOfHiddenElements = 1;
}
