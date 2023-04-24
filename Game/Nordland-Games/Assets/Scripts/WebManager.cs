using System.Collections;
using System.Collections.Generic;
using System.Text;
using NLG;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

public class WebManager : MonoBehaviour
{
    private static WebManager instance;
    private static UserAccount account;
    private GameObject connectionPopup;
    private GameObject offlinePopup;
    private List<CharacterPart> characterParts;

    [SerializeField] private string apiURL;
    [SerializeField] private JSONNode jsonResult;
    
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        
        connectionPopup.SetActive(true);
        offlinePopup.SetActive(false);
        GetUserAccount();
    }

    private IEnumerator GetUserAccount()
    {
        // create the web request and download handler
        UnityWebRequest webReq = new UnityWebRequest();
        webReq.downloadHandler = new DownloadHandlerBuffer();
        // build the url and query
        webReq.url = apiURL + "/account";
        yield return webReq.SendWebRequest();
        
        string rawJson = Encoding.Default.GetString(webReq.downloadHandler.data);
        // parse the raw string into a json result we can easily read
        jsonResult = JSON.Parse(rawJson);

        account = new UserAccount(jsonResult["username"], jsonResult["xp"]);
    }
}
