using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoriesController : MonoBehaviour
{
    public GameObject _object;
    public Transform parent;
    void Start()
    {
        //Хз как очистить список
        Debug.Log("VOID START");
        StartCoroutine(RequestController.getHistory(Storage.userProfile.id));
        int i = 0;
        float k = .0f;
        //var resolve = (GameObject)Instantiate(_object, new Vector3(0, y, 0), Quaternion.identity, parent);
        GameObject[] arr = new GameObject[Storage.histories.Count] ;
        foreach (var item in Storage.histories)
        {
            //_object.descriptionText = item.description;
            arr[i] = Instantiate(_object, new Vector2(.0f, k), Quaternion.identity, parent);
            arr[i].transform.Translate(.20f, .20f, .20f);
            //arr[i].;
            i++;
            k -= .120f;
            //Instantiate(objects[0], GameObject.Find("Content").transform, false);
        }
        //resolve.transform.Translate(0, -60, 0);
    }

    //Метод для удаление факта

    public void closeHistoryScene(String sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    
}
