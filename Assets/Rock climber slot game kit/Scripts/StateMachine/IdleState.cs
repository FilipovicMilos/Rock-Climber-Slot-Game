using UnityEngine;

public class IdleState : AbstractStateInterface
{
    private AllReelsController allReelsController;

    public IdleState(AllReelsController allReelsController)
    {
        this.allReelsController = allReelsController;
    }

    public void Enter()
    {
        Debug.Log("Enter Idle state");
        
        allReelsController.ResetToIdle();
    }

    public void Exit()
    {
        Debug.Log("Exit Idle State");
    }

    

    public void OnResultShown()
    {
        throw new System.NotImplementedException();
    }

    public void OnSpinFinished()
    {
        throw new System.NotImplementedException();
    }

    public void OnSpinPressed()
    {
        Debug.Log("IdleState: Spin pressed");

        allReelsController.ChangeState(new SpinningState(allReelsController));
    }

    
}
