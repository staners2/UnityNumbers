using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HistoriesController : MonoBehaviour
{
    public GameObject _object;
    public Transform parent;
    public List<GameObject> listObjectHistory = new List<GameObject>();

    void Start()
    {
        Debug.Log("VOID START");
        StartCoroutine(RequestController.getHistory(this, Storage.userProfile.id, _object, parent));
    }

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
        //listObjectHistory[0].transform;
        create(listObjectHistory[0], Storage.histories[0].fact.type.title, Storage.histories[0].fact.number, Storage.histories[0].fact.description, Storage.histories[0].fact.date);
        listObjectHistory[0].transform.Translate(.0f, k, .0f);
        for (i = 1; i < Storage.histories.Count; i++)
        {
            listObjectHistory.Add(Instantiate(_object, parent));
            create(listObjectHistory[i], Storage.histories[i].fact.type.title, Storage.histories[i].fact.number, Storage.histories[i].fact.description, Storage.histories[i].fact.date);
            k = k - 1.920f;
            listObjectHistory[i].transform.Translate(.0f, k, .0f);
        }

    }

    public void create(GameObject parent, String type, int number, String description, String date)
    {
        Debug.Log($"DATE: {date} | TYPE: {type} | NUMBER: {number} | DESCRIPTION: {description}");
        parent.transform.GetChild(0).GetComponent<Text>().text = date;
        parent.transform.GetChild(1).GetComponent<Text>().text += type;
        parent.transform.GetChild(2).GetComponent<Text>().text += number.ToString();
        parent.transform.GetChild(3).GetComponent<Text>().text = description;
    }

}
