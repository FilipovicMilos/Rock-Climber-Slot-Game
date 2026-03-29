using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UI;

public class SpinButtonController : MonoBehaviour
{

    [SerializeField] private Button spinButton;
    [SerializeField] private SlotGameController gameController;
    [SerializeField] private AllReelsController allReelsController;

    private void Awake()
    {
        spinButton.onClick.AddListener(OnSpinClicked);
    }

    // Update is called once per frame
    void Update()
    {


        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            OnSpinClicked();
        }
    }

    private void OnSpinClicked()
    {
        DisableButton();
        allReelsController.OnSpinButtonPressed();
    }

   

    public void EnableButton()
    {
        spinButton.interactable = true;
    }

    public void DisableButton()
    {
        spinButton.interactable = false;
    }
}
