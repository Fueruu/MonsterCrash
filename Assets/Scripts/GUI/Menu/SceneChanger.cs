using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] LoadSceneMode _SceneMode;

    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber, _SceneMode);
    }
}
 