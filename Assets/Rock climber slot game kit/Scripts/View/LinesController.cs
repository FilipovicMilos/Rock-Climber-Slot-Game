using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LinesController : MonoBehaviour
{
    [SerializeField] private PaylinesController paylinesController;

    [SerializeField] private Button removeLineButton;
    [SerializeField] private Button addLineButton;
    [SerializeField] private TMP_Text numberOfLinesText;

    [SerializeField] private Color disabledColor;
    private Color enabledColor;

    public int numberOfLines { get; private set; }

    public Action<int> OnNumberOfLinesChanged;

    private void OnClickAdd()
    {
        if (paylinesController.Add())
        {
            numberOfLines++;
            if(numberOfLines == 9)
            {
                addLineButton.interactable = false;
                addLineButton.GetComponent<Image>().color = disabledColor;
            }
            else
            {
                addLineButton.interactable = true;
            }

            removeLineButton.interactable = true;
            removeLineButton.GetComponent<Image>().color = enabledColor;
            numberOfLinesText.text = $"{numberOfLines} LINES";

            OnNumberOfLinesChanged?.Invoke(numberOfLines);
        }
    }

    private void OnClickRemove()
    {
        if (paylinesController.Remove())
        {
            numberOfLines--;
            if(numberOfLines == 1)
            {
                removeLineButton.interactable = false;
                removeLineButton.GetComponent<Image>().color = disabledColor;

            }
            else
            {
                removeLineButton.interactable = true;
            }

            addLineButton.interactable = true;
            addLineButton.GetComponent<Image>().color = enabledColor;
            numberOfLinesText.text = $"{numberOfLines} LINES";

            OnNumberOfLinesChanged?.Invoke(numberOfLines);

        }


    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numberOfLines = 9;
        addLineButton.interactable = false;
        removeLineButton.interactable = true;

        removeLineButton.onClick.AddListener(OnClickRemove);
        addLineButton.onClick.AddListener(OnClickAdd);

        enabledColor = addLineButton.GetComponent<Image>().color;
        removeLineButton.GetComponent <Image>().color = enabledColor;
        addLineButton.GetComponent<Image>().color = disabledColor;

        numberOfLinesText.text = $"{numberOfLines} LINES";

        OnNumberOfLinesChanged?.Invoke(numberOfLines);

    }

    
}