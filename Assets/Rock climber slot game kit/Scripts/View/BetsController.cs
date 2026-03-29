using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BetsController : MonoBehaviour
{
    [SerializeField] private BetPerLineController betPerLineController;

    [SerializeField] private Button decreaseBet;
    [SerializeField] private Button increaseBet;
    [SerializeField] private TMP_Text betText;

    [SerializeField] private Color disabledColor;
    private Color enabledColor;

    public Action<double> OnBetChanged;


    public double bet
    {
        private set;
        get;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        decreaseBet.onClick.AddListener(OnClickDecrease);
        increaseBet.onClick.AddListener(OnClickIncrease);

        bet = 10;
        increaseBet.interactable = true;
        decreaseBet.interactable = true;

        enabledColor = increaseBet.GetComponent<Image>().color;
        decreaseBet.GetComponent<Image>().color = enabledColor;
        increaseBet.GetComponent<Image>().color = enabledColor;

        betText.text = $"{bet} BET";

        OnBetChanged?.Invoke(bet);

    }

    private void OnClickIncrease()
    {
        if (betPerLineController.Increase())
        {
            bet = betPerLineController.currentBet;
            if (bet == 50)
            {
                increaseBet.interactable = false;
                increaseBet.GetComponent<Image>().color = disabledColor;
            }
            else
            {
                increaseBet.interactable = true;
            }

            decreaseBet.interactable = true;
            decreaseBet.GetComponent<Image>().color = enabledColor;
            betText.text = $"{bet} BET";

            OnBetChanged?.Invoke(bet);

        }
    }

    private void OnClickDecrease()
    {
        if (betPerLineController.Decrease())
        {
            bet = betPerLineController.currentBet;
            if (bet == 1)
            {
                decreaseBet.interactable = false;
                decreaseBet.GetComponent<Image>().color = disabledColor;
            }
            else
            {
                decreaseBet.interactable = true;
            }

            increaseBet.interactable = true;
            increaseBet.GetComponent<Image>().color = enabledColor;
            betText.text = $"{bet} BET";

            OnBetChanged?.Invoke(bet);

        }
    }

    
}
