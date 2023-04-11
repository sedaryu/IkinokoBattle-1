using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private ItemsDialog itemDialog;

    [SerializeField] private Button pauseButton;
    [SerializeField] private Button itemsButton;
    [SerializeField] private Button recipeButton;

    // Start is called before the first frame update
    void Start()
    {
        pauseButton.onClick.AddListener(Pause);
        itemsButton.onClick.AddListener(ToggleItemsDialog);
        recipeButton.onClick.AddListener(ToggleRecipeDialog);
    }

    private void Pause()
    { 
        
    }

    private void ToggleItemsDialog()
    {
        itemDialog.Toggle();
    }

    private void ToggleRecipeDialog()
    {

    }
}
