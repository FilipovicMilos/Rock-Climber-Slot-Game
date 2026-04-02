using UnityEngine;

public class SpinningState : AbstractStateInterface
{
    private AllReelsController allReelsController;

    public SpinningState(AllReelsController allReelsController)
    {
        this.allReelsController = allReelsController;
    }

    public void Enter()
    {
        Debug.Log("Enter Spinning state");

        allReelsController.CallStartSpin();
    }

    public void Exit()
    {
        Debug.Log("Exit Spinning State");
    }

    

    public void OnResultShown()
    {
        throw new System.NotImplementedException();
    }

    public void OnSpinFinished()
    {
        Debug.Log("SpinningState: Spin finished");

        allReelsController.ChangeState(new ShowingResultState(allReelsController));
    }

    public void OnSpinPressed()
    {
        Debug.Log("pritisnut spin dugme dok je masina u spinning state pa pozivam fast spin");

        allReelsController.CallFastSpin();
    }

    
}
