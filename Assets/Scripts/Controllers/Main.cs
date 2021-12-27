using System;
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
        if (!validationData())
            return;
        
        if (!profileIsFound())
            return;
        
        SceneManager.LoadScene(sceneName);
    }

    public void registrationAccount(string sceneName)
    {
        if (!validationData())
            return;
        
        if (profileIsFound())
            return;
        
        SceneManager.LoadScene(sceneName);
    }
    
    public Boolean validationData() {
        if (String.IsNullOrEmpty(loginInputField.text))
            return false;

        if (String.IsNullOrEmpty(passwordInputField.text))
            return false;

        return true;
    }

    public Boolean profileIsFound()
    {
        String login = loginText.text;
        String password = passwordText.text;
       // Int32 countryId = Storage.countries.countries
       //     .Find(x => x.title == countriesDropdown.options[countriesDropdown.value].text).id;
 
        // отправляется запрос есть ли такой пользователь

        // извлекается информация из ответа о пользователе
        Profile profile = null; // конвертировать его в объект
        if (profile == null)
            return false;
        else
        {
            Storage.profile = profile;
            return true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
