using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTypes : MonoBehaviour
{
    public Dropdown typesDropdown;
    
    void Start()
    {
        typesDropdown.ClearOptions();
        StartCoroutine(RequestController.getTypes(typesDropdown));
    }
}
