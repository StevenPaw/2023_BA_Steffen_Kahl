using UnityEngine;
using UnityEngine.SceneManagement;

namespace NLG
{
    /// <summary>
    /// A simple script that loads a scene with a specific name.
    /// Used in many Menu buttons
    /// </summary>
    public class SceneButton : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad;

        public void StartScene()
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}