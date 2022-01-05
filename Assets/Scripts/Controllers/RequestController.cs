using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

public static class RequestController
{
    public static String SCHEMA = "http";
    public static String URL = "10.0.2.2"; //10.0.2.2
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
            UserProfile userProfile = JsonUtility.FromJson<UserProfile>(request.downloadHandler.text);
            Storage.userProfile = userProfile;
        }
        else
        {
            Storage.userProfile = null;
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
            UserProfile userProfile = JsonUtility.FromJson<UserProfile>(request.downloadHandler.text);
            Storage.userProfile = userProfile;
        }
        else
        {
            Storage.userProfile = null;
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

    public static IEnumerator getFactByNumber(int userProfileId, int number, String typeName, Text descriptionField)
    {
        Storage.isOperation = true;
        
        var body = new WWWForm();
        body.AddField("userprofile_id", userProfileId);

        String urlApiFact = String.Format(API_FACT, typeName, number);
        Debug.Log($"URI: {URI+urlApiFact}");
        
        Debug.Log($"BODY = {body}");
        
        Debug.Log("GET FACT BY NUMBER REQUEST START");
        UnityWebRequest request = UnityWebRequest.Post(URI+urlApiFact, body);
        yield return request.SendWebRequest();
        
        Debug.Log("GET FACT BY NUMBER REQUEST END");
        Debug.Log($"TEXT = {request.downloadHandler.text}");
        if (!request.downloadHandler.text.Contains("errors"))
        {
            Fact fact = JsonUtility.FromJson<Fact>(request.downloadHandler.text);
            descriptionField.text = fact.description;
        }
        
        Storage.isOperation = false;
    }
    
    public static IEnumerator getFactRandom(int userProfileId, String typeName, Text descriptionField, InputField inputNumberField)
    {
        Storage.isOperation = true;
        
        var body = new WWWForm();
        body.AddField("userprofile_id", userProfileId);

        String urlApiFact = String.Format(API_RANDOM_FACT, typeName);
        Debug.Log($"URI: {URI+urlApiFact}");
        
        Debug.Log($"BODY = {body}");
        
        Debug.Log("GET FACT RANDOM REQUEST START");
        UnityWebRequest request = UnityWebRequest.Post(URI+urlApiFact, body);
        yield return request.SendWebRequest();
        
        Debug.Log("GET FACT RANDOM REQUEST END");
        Debug.Log($"TEXT = {request.downloadHandler.text}");
        if (!request.downloadHandler.text.Contains("errors"))
        {
            Fact fact = JsonUtility.FromJson<Fact>(request.downloadHandler.text);
            descriptionField.text = fact.description;
            inputNumberField.text = fact.number.ToString();
        }
        
        Storage.isOperation = false;
    }

    public static IEnumerator getHistory(HistoriesController controller, int userProfileId, GameObject _object, Transform parent)
    {        

        Storage.isOperation = true;

        String urlHistory = String.Format(API_HISTORIES, userProfileId);
        Debug.Log(urlHistory);
        UnityWebRequest request = UnityWebRequest.Get(URI + urlHistory);
        yield return request.SendWebRequest();

        Debug.Log("TYPES REQUEST END");
        Debug.Log($"TEXT = {request.downloadHandler.text}");
        String json = "{\"histories\":" + request.downloadHandler.text + "}";        
        if (!request.downloadHandler.text.Contains("errors"))
        {
            Histories histories = JsonUtility.FromJson<Histories>(json);
            var list = histories.histories.ToList();
            foreach (var item in list)
            {
                item.fact.date = item.fact.date.Replace("T", " ");
                item.fact.date = item.fact.date.Replace("Z", "");
            }
            Storage.histories = list;
            controller.loadHistories();
        }

        Storage.isOperation = false;
    }

    public static IEnumerator deleteHistory(int historyId)
    {
        Storage.isOperation = true;

        String urlHistory = String.Format(API_DELETE_HISTORIES, Storage.userProfile.id, historyId);
        Debug.Log(urlHistory);
        UnityWebRequest request = UnityWebRequest.Delete(URI + urlHistory);
        yield return request.SendWebRequest();

        Storage.isOperation = false;
    }
}
