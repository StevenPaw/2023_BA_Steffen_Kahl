using TMPro;
using UnityEngine;

namespace NLG
{
    /// <summary>
    /// A Manager that controls the main menu
    /// </summary>
    public class MainMenuManager : MonoBehaviour
    {
        private WebManager webmanager;
        [SerializeField] private TMP_Text usernameText;
        [SerializeField] private TMP_Text xpText;
        [SerializeField] private GameObject characterTutorial;

        void Start()
        {
            webmanager = WebManager.instance;
            usernameText.text = webmanager.UserNickname;
            xpText.text = webmanager.UserXP + " XP";
            characterTutorial.SetActive(PlayerPrefs.GetInt("CharacterTutorial", 0) == 0);
        }

        public void DeleteSaveData()
        {
            PlayerPrefs.DeleteAll();
        }
        
        public void UseCharacterTutorial()
        {
            //Gets executed when clicking on the character creator the first time. Disables the simple Character Tutorial
            characterTutorial.SetActive(false);
            PlayerPrefs.SetInt("CharacterTutorial", 1);
        }
    }
}