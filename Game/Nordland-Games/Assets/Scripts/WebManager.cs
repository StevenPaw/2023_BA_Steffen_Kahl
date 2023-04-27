using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NLG;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class WebManager : MonoBehaviour
{
    public static WebManager instance;
    
    [SerializeField] private GameObject connectionPopup;
    [SerializeField] private GameObject noAccountPopup;
    [SerializeField] private GameObject offlinePopup;
    private Dictionary<int, CharacterPart> characterParts;
    [SerializeField] private TMP_Text errorText;
    [SerializeField] private TMP_Text welcomeText;
    [SerializeField] private GameObject gameStartButton;
    [SerializeField] private string apiURL;
    [SerializeField] private JSONNode jsonResult;
    [SerializeField] private GameObject startCanvas;
    
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

    public Dictionary<int, CharacterPart> CharacterParts => characterParts;

    public int SelectedBottom => selectedBottom;
    public int SelectedTop => selectedTop;
    public int SelectedHat => selectedHat;
    public int SelectedSkinColor => selectedSkinColor;
    public int SelectedEyes => selectedEyes;
    public int SelectedMouth => selectedMouth;
    public int SelectedHair => selectedHair;

    public string UserNickname => userNickname;
    public int UserXP => userXP;

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

        characterParts = new Dictionary<int, CharacterPart>();
        
        connectionPopup.SetActive(true);
        offlinePopup.SetActive(false);
        noAccountPopup.SetActive(false);
        errorText.gameObject.SetActive(false);
        gameStartButton.SetActive(false);
        if (PlayerPrefs.HasKey("UserKey"))
        {
            StartCoroutine(GetUserAccount(PlayerPrefs.GetString("UserKey")));
        }
        else
        {
            noAccountPopup.SetActive(true);
        }
        StartCoroutine(GetCharacterParts());
        
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
            selectedSkinColor = jsonResult["User"]["SelectedSkinColorID"];
            selectedEyes = jsonResult["User"]["SelectedEyesID"];
            selectedMouth = jsonResult["User"]["SelectedMouthID"];
            selectedHair = jsonResult["User"]["SelectedHairID"];
            selectedBottom = jsonResult["User"]["SelectedBottomID"];
            selectedTop = jsonResult["User"]["SelectedTopID"];
            selectedHat = jsonResult["User"]["SelectedHatID"];
            welcomeText.text = "Willkommen zurück " + userNickname;
            gameStartButton.SetActive(true);
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
            connectionPopup.SetActive(true);
            noAccountPopup.SetActive(false);
            welcomeText.text = "Willkommen " + userNickname;
            gameStartButton.SetActive(true);
        }
        else
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Nickname bereits vergeben!";
            Debug.Log("Creation of User unsuccessful!");
        }
    }

    private IEnumerator GetCharacterParts()
    {
        // create the web request and download handler
        UnityWebRequest webReq = new UnityWebRequest();
        webReq.downloadHandler = new DownloadHandlerBuffer();
        // build the url and query
        webReq.url = apiURL + "/characterparts";
        yield return webReq.SendWebRequest();

        string rawJson = Encoding.Default.GetString(webReq.downloadHandler.data);
        // parse the raw string into a json result we can easily read
        jsonResult = JSON.Parse(rawJson);
        
        if (jsonResult["Status"] == "OK")
        {
            foreach (JSONNode part in jsonResult["CharacterParts"])
            {
                CharacterPart newPart = new CharacterPart();
                
                //Get Image for Characterpart
                UnityWebRequest request = UnityWebRequestTexture.GetTexture(part["Image"]);
                yield return request.SendWebRequest();
                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.Log(request.error);
                }
                else
                {
                    Texture2D tex = ((DownloadHandlerTexture) request.downloadHandler).texture;
                    tex.filterMode = FilterMode.Point;
                    Sprite spr = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
                    newPart.Image = spr;
                }

                newPart.Title = part["Title"];
                newPart.RequiredXp = part["RequiredXP"];
                newPart.PartID = part["ID"];
                
                if (part["Type"].ToString().ToUpper().Contains("SKINCOLOR")) {
                    newPart.Type = CharacterPartTypes.SKINCOLOR;
                } else if (part["Type"].ToString().ToUpper().Contains("EYES")) {
                    newPart.Type = CharacterPartTypes.EYES;
                } else if (part["Type"].ToString().ToUpper().Contains("MOUTH")) {
                    newPart.Type = CharacterPartTypes.MOUTH;
                } else if (part["Type"].ToString().ToUpper().Contains("HAIR")) {
                    newPart.Type = CharacterPartTypes.HAIR;
                } else if (part["Type"].ToString().ToUpper().Contains("BOTTOM")) {
                    newPart.Type = CharacterPartTypes.BOTTOM;
                } else if(part["Type"].ToString().ToUpper().Contains("TOP")) {
                    newPart.Type = CharacterPartTypes.TOP;
                } else if (part["Type"].ToString().ToUpper().Contains("HAT")) {
                    newPart.Type = CharacterPartTypes.HAT;
                }
                
                characterParts.Add(newPart.PartID, newPart);
            }
        }
    }

    public void GenerateUser(TMP_InputField sourceField)
    {
        Debug.Log(sourceField.text);
        StartCoroutine(CreateUserAccount(sourceField.text));
    }

    public void StartGame()
    {
        SceneManager.LoadScene(menuScene);
        startCanvas.SetActive(false);
    }
}
