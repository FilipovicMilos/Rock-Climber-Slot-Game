using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ReelStripData", menuName = "Scriptable Objects/ReelStripData")]
public class ReelStripData : ScriptableObject
{
    [Tooltip("Lista simbola na reelu poredjana po tezinama.\n" +
        "Svaki simbol se dodaje onoliko puta kolika mu je tezina.\n" +
        "Ukupan broj unosa mora biti jednaka reelStripLength iz SlotConfigData.")]
    public List<SymbolData> symbols;
}
