using UnityEngine;

[CreateAssetMenu(fileName = "PaylineData", menuName = "Scriptable Objects/PaylineData")]
public class PaylineData : ScriptableObject
{
    [Tooltip("ID linije")]
    public int lineID;

    [Tooltip("Naziv payline-a")]
    public string paylineName;

    [Tooltip("Indeksi redova od gore ka dole (0,1,2). Niz ima 5 vrednosti, za svaki reel")]
    public int[] rowIndices = new int[5];
}
