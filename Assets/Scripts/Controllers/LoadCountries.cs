using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadCountries : MonoBehaviour
{
    public Dropdown countriesDropdown;
    // Start is called before the first frame update
    void Start()
    {
        countriesDropdown.ClearOptions();
        // UnityWebRequest request = UnityWebRequest.Get(RequestController.URI + RequestController.API_COUNTRIES);
        // var f = request.SendWebRequest();
        // Debug.Log(f.webRequest.downloadHandler.text);
        // StartCoroutine(RequestController.get());
        StartCoroutine(RequestController.getCountries(countriesDropdown));
        
        Debug.Log("START");
    }
}
