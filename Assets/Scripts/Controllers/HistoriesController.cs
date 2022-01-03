using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HistoriesController : MonoBehaviour
{
    public GameObject _object;
    public Transform parent;
    public List<GameObject> listObjectHistory = new List<GameObject>();

    void Start()
    {
        //Хз как очистить список
        Debug.Log("VOID START");
        StartCoroutine(RequestController.getHistory(this, Storage.userProfile.id, _object, parent));
        //int i = 0;
        //float k = -.0f;
        //var resolve = (GameObject)Instantiate(_object, new Vector3(0, y, 0), Quaternion.identity, parent);
        //GameObject[] arr = new GameObject[Storage.histories.Count];
        //GameObject[] arr = new GameObject[10];
        //foreach (var item in Storage.histories)
        //{
        //    //_object.descriptionText = item.description;
        //    arr[i] = Instantiate(_object, parent);            
        //    k = k-.240f;
        //    arr[i].transform.Translate(.0f, k, .0f);
        //    //arr[i].;
        //    i++;


        //}
        
        

        //resolve.transform.Translate(0, -60, 0);


    }

    //Метод для удаление факта

    public void closeHistoryScene(String sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void loadHistories()
    {
        float k = -.0f;
        int i = 0;
        Debug.Log(Storage.histories.Count);
        listObjectHistory.Add(Instantiate(_object, parent));
        listObjectHistory[0].transform.Translate(.0f, k, .0f);
        for (i = 1; i < Storage.histories.Count; i++)
        {
            listObjectHistory.Add(Instantiate(_object, parent));
            k = k - 1.920f;
            listObjectHistory[i].transform.Translate(.0f, k, .0f);
        }

    }

}
