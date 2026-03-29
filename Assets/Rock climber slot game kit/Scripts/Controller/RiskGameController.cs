using System;
using UnityEngine;
using UnityEngine.UI;

public class RiskGameController : MonoBehaviour
{

    [SerializeField] private Button riskButton;

    [SerializeField] private Button redButton;
    [SerializeField] private Button blackButton;

    [SerializeField] private SlotGameController gameController;

    //red = 0   black = 1
    private int color;

    private void Awake()
    {
        riskButton.onClick.AddListener(RequestRiskGame);

        redButton.onClick.AddListener(OnClickRed);
        blackButton.onClick.AddListener(OnClickBlack);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        riskButton.interactable = false;
        redButton.interactable = false;
        blackButton.interactable = false;

    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    internal void RequestRiskGame()
    {
        EnableButtons();
    }

    private void OnClickRed()
    {
        color = 0;
        gameController.RiskGame(color);
    }

    private void OnClickBlack()
    {
        color = 1;
        gameController.RiskGame(color);

    }

    public void EnableButtons()
    {
        redButton.interactable = true;
        blackButton.interactable = true;
    }

    public void DisableButtons()
    {
        redButton.interactable = false;
        blackButton.interactable = false;
    }

    internal void EnableRiskButton()
    {
        riskButton.interactable = true;
    }
}
