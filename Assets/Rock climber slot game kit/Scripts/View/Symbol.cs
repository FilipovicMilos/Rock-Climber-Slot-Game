using UnityEngine;
using UnityEngine.UI;

public class Symbol : MonoBehaviour
{
    
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite spinningSprite;

    private ReelController parent;

    public SymbolData symbol;

    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    public void OnReel(ReelController controller)
    {
        parent = controller;
        parent.OnReelStopped += ResetSymbolSpriteOnReelStopped;
        parent.OnReelSpinning += SetSymbolSpriteOnReelSpinning;

    }

    public void OffReel()
    {
        parent.OnReelSpinning -= SetSymbolSpriteOnReelSpinning;
        parent.OnReelStopped -= ResetSymbolSpriteOnReelStopped;
        
        parent = null;

        img.sprite = idleSprite;
    }

    private void SetSymbolSpriteOnReelSpinning()
    {
        img.sprite = spinningSprite;
    }

    private void ResetSymbolSpriteOnReelStopped()
    {
        img.sprite = idleSprite;
    }
}
