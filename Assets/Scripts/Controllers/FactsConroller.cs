using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class FactsConroller : MonoBehaviour
{
    public Dropdown typesDropdown;
    public InputField inputNumberField;
    public Text descriptionField;
    public Button randomFactButton;
    public Button factButton;

    void Start()
    {
        typesDropdown.ClearOptions();
        StartCoroutine(RequestController.getTypes(typesDropdown));
    }

    public void getFact()
    {
        if (!dataGetFactIsValid())
            return;

        Int32 number = Int32.Parse(inputNumberField.text);
        String typeName = Storage.types.Find(x => x.title == typesDropdown.options[typesDropdown.value].text).en_title;
        
        StartCoroutine(RequestController.getFactByNumber(Storage.userProfile.id, number, typeName, descriptionField));
    }

    public void getFactRandom()
    {
        String typeName = Storage.types.Find(x => x.title == typesDropdown.options[typesDropdown.value].text).en_title;
        
        StartCoroutine(RequestController.getFactRandom(Storage.userProfile.id, typeName, descriptionField, inputNumberField));
    }

    public void openHistoryScene(String sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public Boolean dataGetFactIsValid()
    {
        if (String.IsNullOrEmpty(inputNumberField.text))
            return false;
        return true;
    }
}
