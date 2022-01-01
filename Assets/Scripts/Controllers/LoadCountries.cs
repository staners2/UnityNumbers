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
        StartCoroutine(RequestController.getCountries(countriesDropdown));
    }
}
