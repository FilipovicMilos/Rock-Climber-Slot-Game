using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AllReelsController : MonoBehaviour
{
    [SerializeField] private SlotConfigData slotConfigData;

    [SerializeField] private SpinButtonController spinButtonController;

    [SerializeField] private SlotGameController slotGameController;
    private GameResult gameResult;

    
    [SerializeField] private ResultView resultView;
    
    [SerializeField] private ReelController reel1;
    [SerializeField] private ReelController reel2;
    [SerializeField] private ReelController reel3;
    [SerializeField] private ReelController reel4;
    [SerializeField] private ReelController reel5;

    private ReelController[] reels;

    private AbstractStateInterface currentState;
    private int reelsStopped;

    private void Awake()
    {

        ChangeState(new IdleState(this));

        reels = new ReelController[5];
        reels[0] = reel1;
        reels[1] = reel2;
        reels[2] = reel3;
        reels[3] = reel4;
        reels[4] = reel5;
    }

    private void Start()
    {
        reelsStopped = 0;

        for (int i = 0; i < 5; i++)
        {
            reels[i].OnReelStopped += CheckIfAllReelsStopped;
        }

        PopulateReels();
    }

    private void PopulateReels()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            int totalVisible = slotConfigData.numberOfInPlayElements + slotConfigData.numberOfHiddenElements;

            reels[i].SetInitialSymbols(slotConfigData.numberOfHiddenElements, slotConfigData.numberOfInPlayElements);
        }
    }

    

    private void OnDestroy()
    {
        for (int i = 0; i < 5; i++)
        {
            reels[i].OnReelStopped -= CheckIfAllReelsStopped;
        }
    
    }

    internal void ChangeState(AbstractStateInterface newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    

    public void OnSpinButtonPressed()
    {
        resultView.EndLinesAnimation();

        currentState?.OnSpinPressed();
    }

    public void OnReelsStopped()
    {
        currentState?.OnSpinFinished();
    }

    public void OnResultShown()
    {
        currentState?.OnResultShown();
    }
    /// <summary>
    /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>
    internal void CallStartSpin()
    {
     
        gameResult = slotGameController.RequestSpin();

        if (gameResult == null) {

            ChangeState(new IdleState(this));

            return;
        }

        CallAnimateResultView(gameResult);
    }

    internal void CallAnimateResultView(GameResult gameResult)
    {
        for (int i = 0; i < 5; i++)
        {
            reels[i].StartSpin();
        }

        for (int i = 0; i < 5; i++)
        {
            reels[i].SetReelSpinResult(gameResult.finalIndexes[i]);
        }

    }

    private void CheckIfAllReelsStopped()
    {
        reelsStopped++;
        if (reelsStopped == 5) { 
            OnReelsStopped();
            
        }
    }

    internal void ResetToIdle()
    {
        reelsStopped = 0;

        gameResult = null;

        spinButtonController.EnableButton();
    }

    internal void ShowResult()
    {
        resultView.AnimateResult(gameResult);

        slotGameController.CallAddWinToBalance();

        spinButtonController.EnableButton();

        OnResultShown();
    }
}
