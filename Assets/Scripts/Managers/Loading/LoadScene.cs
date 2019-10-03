using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private int _SceneIndex;

    [SerializeField] private LoadSceneMode _SceneMode;

    public void Load()
    {
        //  Loads the specific scene name 
        LoadingScreen.Instance.ShowLoadScreen(SceneManager.LoadSceneAsync(_SceneIndex, _SceneMode));
    }
}
