using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private WebManager webmanager;
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private TMP_Text xpText;
    
    // Start is called before the first frame update
    void Start()
    {
        webmanager = WebManager.instance;
        usernameText.text = webmanager.UserNickname;
        xpText.text = webmanager.UserXP + " XP";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
