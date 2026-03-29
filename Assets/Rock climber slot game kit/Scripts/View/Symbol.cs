using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite spinningSprite;

    private ReelController parent;

    public SymbolData symbol;

    public void OnReel(ReelController controller)
    {
        parent = controller;
        parent.OnReelStopped += ResetSymbolSpriteOnReelStopped;
        parent.OnReelSpinning += SetSymbolSpriteOnReelSpinning;

    }

    public void OffReel()
    {
        parent.OnReelStopped -= ResetSymbolSpriteOnReelStopped;
        parent.OnReelSpinning -= SetSymbolSpriteOnReelSpinning;
        parent = null;

        transform.GetComponent<Image>().sprite = idleSprite;
    }

    private void SetSymbolSpriteOnReelSpinning()
    {
        transform.GetComponent<Image>().sprite = spinningSprite;
    }

    private void ResetSymbolSpriteOnReelStopped()
    {
        transform.GetComponent<Image>().sprite = idleSprite;
    }
}
