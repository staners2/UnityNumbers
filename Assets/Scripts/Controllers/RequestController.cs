using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public static class RequestController
{
    public static String SCHEMA = "http";
    public static String URL = "127.0.0.1";
    public static String BASE_PATH = "/api/";
    public static Int32 PORT = 8000;
    public static String URI = $"{SCHEMA}://{URL}:{PORT}{BASE_PATH}";

    public static String API_REGISTRATION = "auth/registration";
    public static String API_AUTHORIZE = "auth/login";
    public static String API_COUNTRIES = "countries";
    public static String API_TYPES = "types";
    public static String API_UPDATE_COUNTRY = "userprofile/{0}/country"; // 0 - profile_id,
    public static String API_HISTORIES = "userprofile/{0}/histories"; // 0 - profile_id,
    public static String API_DELETE_HISTORIES = "userprofile/{0}/histories/{1}"; // 0 - profile_id, 1 - history_id
    public static String API_RANDOM_FACT = "userprofile/fact/random/{0}"; // 0 - type
    public static String API_FACT = "userprofile/fact/{0}/{1}"; // 0 - type, 1 - number

    /* public static IEnumerable get(UnityWebRequestAsyncOperation request)
    {
        yield return request;
        
        if (request.isDone)
        {
            // String json = "{\"countries\":" + request.downloadHandler.text + "}";
            // Countries countries = JsonUtility.FromJson<Countries>(json);
            // Debug.Log(request.downloadHandler.text);
            // Debug.Log(json);
            // Debug.Log(countries.countries[0]);
            
            // Storage.countries = countries.countries.ToList();
        
            List<Country> items = new List<Country>();
            foreach (var item in Storage.countries)
            {
                items.Add(new Country(item.id, item.title, item.prefix));
            }

            List<String> list = Storage.countries.Select(x => x.title).ToList();
            //countriesDropdown.AddOptions(list);
        }
    }
    */
    public static IEnumerator getCountries(Dropdown countriesDropdown)
    { 
        UnityWebRequest request = UnityWebRequest.Get(URI+API_COUNTRIES);
        yield return request.SendWebRequest();
        
        Debug.Log(request.downloadHandler.text);
        String json = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
        // String json = "{\"countries\":" + request.downloadHandler.text + "}";
        // String json = "{\"countries\":[{\"id\": 1, \"title\": \"\u0442\u0435\u0441\u0442\", \"prefix\": \"ru_title\"}]}";
        // json = json.Replace("\\", "");
        Debug.Log(json);
        Countries countries = JsonUtility.FromJson<Countries>(json);

        Debug.Log(countries.countries.Length);
            
        Storage.countries = countries.countries.ToList();
        
        List<Country> items = new List<Country>();
        foreach (var item in Storage.countries)
        {
            items.Add(new Country(item.id, item.title, item.prefix));
        }

        List<String> list = Storage.countries.Select(x => x.title).ToList();
        countriesDropdown.AddOptions(list);
    }
}
