using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    
    public void StartScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
