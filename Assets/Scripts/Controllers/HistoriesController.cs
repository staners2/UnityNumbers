using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoriesController : MonoBehaviour
{
    
    void Start()
    {
        
    }
    
    public void closeHistoryScene(String sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
