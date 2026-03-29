using System;
using UnityEngine;



public class ReelController : MonoBehaviour
{
    
    [SerializeField] private int reelIndex;
   
    private RectTransform reelRect;

    private float move = 0;

    public float speed = 4000f;

    public float symbolHeight = 220f;
    [SerializeField] private Transform symbolPool;

    [SerializeField] private ReelStripData reelStripData;

    bool isSpinning = false;
    private int currentIndex;

    private int spinCounter = 0;
    [SerializeField] private int spinDuration;

    [SerializeField] private Vector2 startPosition;

    
    private int finalIndex;

    public Action OnReelStopped;
    public Action OnReelSpinning;

    private void Awake()
    {
        reelRect = GetComponent<RectTransform>();

        currentIndex = UnityEngine.Random.Range(0, 100);
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpinning)
            return;

        float delta = speed * Time.deltaTime;

        move += delta;
        reelRect.anchoredPosition -= new Vector2(0,delta);

        if(move >= symbolHeight)
        {
            move -= symbolHeight;
            reelRect.anchoredPosition += new Vector2(0, symbolHeight);


            for (int i = 0; i < transform.childCount; i++)
            {
                RectTransform childRect = transform.GetChild(i).GetComponent<RectTransform>();
                childRect.anchoredPosition = new Vector2(50, -50 - ((i + 1) * symbolHeight));
            }

            if (spinCounter == spinDuration - 4)
            {
                
                currentIndex = (finalIndex + 2) % reelStripData.symbols.Count;

            }
            else 
            {
                currentIndex = (currentIndex + reelStripData.symbols.Count - 1) % reelStripData.symbols.Count;
            }

            spinCounter++;

            ReplaceLastSymbol();

            if (spinCounter == spinDuration)
                StopSpinWithResult();

        }

    }

    private void ReplaceLastSymbol()
    {
        // Return old to pool
        Symbol oldSymbol = transform.GetChild(transform.childCount - 1).GetComponent<Symbol>();
        oldSymbol.OffReel();
        oldSymbol.transform.SetParent(symbolPool, false);

        // Get from pool
        SymbolData elementFromReelStrip = reelStripData.symbols[currentIndex];
        Symbol s = GetSymbolFromPool(elementFromReelStrip);
        if (s)
        {
            PlaceSymbolOnReel(s, -1);
            s.transform.SetAsFirstSibling();
        }
    }

    private Symbol GetSymbolFromPool(SymbolData data)
    {
        foreach (Transform child in symbolPool)
        {
            Symbol s = child.GetComponent<Symbol>();

            if (s.symbol == data)
            {
                return s;
            }
        }
        return null;
    }

    private void PlaceSymbolOnReel(Symbol s, int position = 0)
    {
        s.transform.SetParent(transform, false);
        s.GetComponent<RectTransform>().anchoredPosition = new Vector2(50, -50 - (position + 1) * symbolHeight);
        s.OnReel(this);
    }

    public void StartSpin()
    {
        isSpinning = true;
        OnReelSpinning?.Invoke();
    }

    internal void StopSpinWithResult()
    {
        isSpinning = false;
        spinCounter = 0;

        reelRect.anchoredPosition = startPosition;

        OnReelStopped?.Invoke();
    }

    internal void SetReelSpinResult(int finalIndex)
    {
        this.finalIndex = finalIndex;

    }

    internal void SetInitialSymbols(int hidden, int inPlay)
    {

        for (int i = 0; i < hidden; ++i)
        {
            SymbolData elementFromReelStrip = reelStripData.symbols[(currentIndex - hidden + reelStripData.symbols.Count + i) % reelStripData.symbols.Count];
            Symbol s = GetSymbolFromPool(elementFromReelStrip);
            PlaceSymbolOnReel(s, -hidden + i);

        }

        for (int i = 0; i < inPlay; ++i)
        {
            SymbolData elementFromReelStrip = reelStripData.symbols[(currentIndex + reelStripData.symbols.Count + i) % reelStripData.symbols.Count];
            Symbol s = GetSymbolFromPool(elementFromReelStrip);
            PlaceSymbolOnReel(s, i);
        }
    }
}
