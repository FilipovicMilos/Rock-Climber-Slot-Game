using UnityEngine;

public class ShowingResultState : AbstractStateInterface
{
    private AllReelsController allReelsController;

    public ShowingResultState(AllReelsController allReelsController)
    {
        this.allReelsController = allReelsController;
    }

    public void Enter()
    {
        Debug.Log("Enter ShowingResult State");

        allReelsController.ShowResult();
    }

    public void Exit()
    {
        Debug.Log("Exit ShowingResult State");
    }

    

    public void OnResultShown()
    {
        Debug.Log("ShowingResultState: Result finished");

        allReelsController.ChangeState(new IdleState(allReelsController));
    }

    public void OnSpinFinished()
    {
        throw new System.NotImplementedException();
    }

    public void OnSpinPressed()
    {
        throw new System.NotImplementedException();
    }

    
}
