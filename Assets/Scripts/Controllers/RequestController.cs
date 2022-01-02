using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;
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

    public static IEnumerator getCountries(Dropdown countriesDropdown)
      {
          Storage.isOperation = true;
          
          UnityWebRequest request = UnityWebRequest.Get(URI+API_COUNTRIES);
          yield return request.SendWebRequest();

          Debug.Log(request.downloadHandler.text);

          String test = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);

          Debug.Log(test);

          String json = "{\"countries\":" + request.downloadHandler.text + "}";


          Debug.Log(json);
          Countries countries = JsonUtility.FromJson<Countries>(json);
          
          Storage.countries = countries.countries.ToList();

          List<Country> items = new List<Country>();
          foreach (var item in Storage.countries)
          {
              items.Add(new Country(item.id, item.title, item.prefix));
          }
          List<String> list = Storage.countries.Select(x => x.title).ToList();
          countriesDropdown.AddOptions(list);
          
          Storage.isOperation = false;
      }

    public static IEnumerator login(String login, String password, Int32 country_id)
    {
        Storage.isOperation = true;
        
        Debug.Log("LOGIN REQUEST START");
        var body = new WWWForm();
        body.AddField("login", login);    
        body.AddField("password", password);  
        body.AddField("country_id", country_id.ToString());
        
        Debug.Log($"BODY = {body}");
        UnityWebRequest request = UnityWebRequest.Post(URI+API_AUTHORIZE, body);
        yield return request.SendWebRequest();
        Debug.Log("LOGIN REQUEST END");
        Debug.Log($"TEXT = {request.downloadHandler.text}");
        if (!request.downloadHandler.text.Contains("errors"))
        {
            Profile profile = JsonUtility.FromJson<Profile>(request.downloadHandler.text);
            Storage.profile = profile;
        }
        else
        {
            Storage.profile = null;
        }
        Storage.isOperation = false;
    }

    public static IEnumerator registration(String login, String password, Int32 country_id)
    {
        Storage.isOperation = true;
        
        Debug.Log("REGISTRATION REQUEST START");
        var body = new WWWForm();
        body.AddField("login", login);    
        body.AddField("password", password);  
        body.AddField("country_id", country_id.ToString());
        
        Debug.Log($"BODY = {body}");
        UnityWebRequest request = UnityWebRequest.Post(URI+API_REGISTRATION, body);
        yield return request.SendWebRequest();
        
        Debug.Log("REGISTRATION REQUEST END");
        Debug.Log($"TEXT = {request.downloadHandler.text}");
        if (!request.downloadHandler.text.Contains("errors"))
        {
            Profile profile = JsonUtility.FromJson<Profile>(request.downloadHandler.text);
            Storage.profile = profile;
        }
        else
        {
            Storage.profile = null;
        }
        Storage.isOperation = false;
    }

    public static IEnumerator getTypes(Dropdown typesDropdown)
    {
        Storage.isOperation = true;
        
        Debug.Log("TYPES REQUEST START");
        UnityWebRequest request = UnityWebRequest.Get(URI+API_TYPES);
        yield return request.SendWebRequest();
        
        Debug.Log("TYPES REQUEST END");
        Debug.Log($"TEXT = {request.downloadHandler.text}");
        
        String json = "{\"types\":" + request.downloadHandler.text + "}";
        
        if (!request.downloadHandler.text.Contains("errors"))
        {
            Types types = JsonUtility.FromJson<Types>(json);
            Storage.types = types.types.ToList();
            List<String> list = Storage.types.Select(x => x.title).ToList();
            typesDropdown.AddOptions(list);
        }

        Storage.isOperation = false;
    }
}
