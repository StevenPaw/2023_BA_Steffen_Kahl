using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NLG;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class WebManager : MonoBehaviour
{
    private static WebManager instance;
    
    [SerializeField] private GameObject connectionPopup;
    [SerializeField] private GameObject noAccountPopup;
    [SerializeField] private GameObject offlinePopup;
    [SerializeField] private List<CharacterPart> characterParts;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private string apiURL;
    [SerializeField] private JSONNode jsonResult; 
    
    [Header("UserData")]
    [SerializeField] private string userNickname;
    [SerializeField] private string userKey;
    [SerializeField] private int userXP;
    [SerializeField] private int selectedSkinColor;
    [SerializeField] private int selectedEyes;
    [SerializeField] private int selectedMouth;
    [SerializeField] private int selectedHair;
    [SerializeField] private int selectedBottom;
    [SerializeField] private int selectedTop;
    [SerializeField] private int selectedHat;

    [Header("MenuScene")] 
    [SerializeField] private string menuScene;
    
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        
        connectionPopup.SetActive(true);
        offlinePopup.SetActive(false);
        noAccountPopup.SetActive(false);
        errorText.gameObject.SetActive(false);
        if (PlayerPrefs.HasKey("UserKey"))
        {
            StartCoroutine(GetUserAccount(PlayerPrefs.GetString("UserKey")));
        }
        else
        {
            noAccountPopup.SetActive(true);
        }
        
    }

    private IEnumerator GetUserAccount(string userKey)
    {
        // create the web request and download handler
        UnityWebRequest webReq = new UnityWebRequest();
        webReq.downloadHandler = new DownloadHandlerBuffer();
        // build the url and query
        webReq.url = apiURL + "/account?UserKey=" + userKey;
        yield return webReq.SendWebRequest();

        string rawJson = Encoding.Default.GetString(webReq.downloadHandler.data);
        // parse the raw string into a json result we can easily read
        jsonResult = JSON.Parse(rawJson);
    
        Debug.Log(jsonResult);
        if (jsonResult["Status"] == "OK")
        {
            this.userKey = jsonResult["User"]["UserKey"];
            userNickname = jsonResult["User"]["Nickname"];
            userXP = jsonResult["User"]["XP"];
            selectedSkinColor = jsonResult["User"]["SelectedSkinColor"];
            selectedEyes = jsonResult["User"]["SelectedEyes"];
            selectedMouth = jsonResult["User"]["SelectedMouth"];
            selectedHair = jsonResult["User"]["SelectedHair"];
            selectedBottom = jsonResult["User"]["SelectedBottom"];
            selectedTop = jsonResult["User"]["SelectedTop"];
            selectedHat = jsonResult["User"]["SelectedHat"];
        }
        else
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Nickname bereits vergeben!";
            Debug.Log("Creation of User unsuccessful!");
        }
    }
    
    private IEnumerator CreateUserAccount(string nickname)
    {
        // create the web request and download handler
        UnityWebRequest webReq = new UnityWebRequest();
        webReq.downloadHandler = new DownloadHandlerBuffer();
        // build the url and query
        webReq.url = apiURL + "/account?Nickname=" + nickname;
        yield return webReq.SendWebRequest();

        string rawJson = Encoding.Default.GetString(webReq.downloadHandler.data);
        // parse the raw string into a json result we can easily read
        jsonResult = JSON.Parse(rawJson);
    
        Debug.Log(jsonResult);
        if (jsonResult["Status"] == "OK")
        {
            userKey = jsonResult["User"]["UserKey"];
            userNickname = jsonResult["User"]["Nickname"];
            userXP = jsonResult["User"]["XP"];
            selectedSkinColor = 0;
            selectedEyes = 0;
            selectedMouth = 0;
            selectedHair = 0;
            selectedBottom = 0;
            selectedTop = 0;
            selectedHat = 0;
            PlayerPrefs.SetString("UserKey", userKey);
        }
        else
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Nickname bereits vergeben!";
            Debug.Log("Creation of User unsuccessful!");
        }
    }

    public void GenerateUser(TMP_InputField sourceField)
    {
        Debug.Log(sourceField.text);
        StartCoroutine(CreateUserAccount(sourceField.text));
    }
}
