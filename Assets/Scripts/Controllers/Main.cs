using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Main : MonoBehaviour
{
    public Text loginText;
    public InputField loginInputField;
    
    public Text passwordText;
    public InputField passwordInputField;

    public Dropdown countriesDropdown;
    public Text countryText;

    public void enterAccount(string sceneName)
    {
        Debug.Log("LOGIN START");
        if (!validationData())
            return;
        
        String login = loginText.text;
        String password = passwordInputField.text;
        Int32 countryId = Storage.countries
            .Find(x => x.title == countriesDropdown.options[countriesDropdown.value].text).id;
        
        StartCoroutine(corutineLogin(sceneName, login, password, countryId));
    }

    public void registrationAccount(string sceneName)
    {
        if (!validationData())
            return;
        
        String login = loginText.text;
        String password = passwordInputField.text;
        Int32 countryId = Storage.countries
            .Find(x => x.title == countriesDropdown.options[countriesDropdown.value].text).id;

        StartCoroutine(corutineRegistration(sceneName, login, password, countryId));
    }
    
    public Boolean validationData() {
        if (String.IsNullOrEmpty(loginInputField.text))
            return false;

        if (String.IsNullOrEmpty(passwordInputField.text))
            return false;

        return true;
    }
    
    public IEnumerator corutineLogin(string sceneName, String login, String password, Int32 countryId)
    {
        yield return RequestController.login(login, password, countryId);
        Debug.Log($"CORUTINE END");
        
        UserProfile userProfile = Storage.userProfile;
        Debug.Log($"PROFILE IS NULL = {userProfile == null}");
        if (userProfile != null)
            SceneManager.LoadScene(sceneName);
    }

    public IEnumerator corutineRegistration(string sceneName, String login, String password, Int32 countryId)
    {
        yield return RequestController.registration(login, password, countryId);
        Debug.Log($"CORUTINE END");
        
        UserProfile userProfile = Storage.userProfile;
        Debug.Log($"PROFILE IS NULL = {userProfile == null}");
        if (userProfile != null)
            SceneManager.LoadScene(sceneName);
    }
}
