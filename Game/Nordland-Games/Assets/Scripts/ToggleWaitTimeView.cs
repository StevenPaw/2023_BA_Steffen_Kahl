using System.Linq;
using UnityEngine;

namespace NLG
{
    /// <summary>
    /// A simple script to toggle the wait time view in game.
    /// </summary>
    public class ToggleWaitTimeView : MonoBehaviour
    {
        [SerializeField] private Animator viewAnimator;
        [SerializeField] private GameObject pauseMenu;
        private IGameManager gameManager;

        private void Start()
        {
            gameManager = FindObjectsOfType<MonoBehaviour>().OfType<IGameManager>().FirstOrDefault();
            pauseMenu.SetActive(false);
        }

        public void ToggleView()
        {
            Debug.Log("ToggleView");
            viewAnimator.SetBool("isActive", !viewAnimator.GetBool("isActive"));
            pauseMenu.SetActive(viewAnimator.GetBool("isActive"));
            if (gameManager.GetState() == GameStates.INGAME || gameManager.GetState() == GameStates.PAUSED)
            {
                gameManager.TogglePause();
            }
        }
    }
}