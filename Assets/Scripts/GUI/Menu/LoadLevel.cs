using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

    [SerializeField] private string SceneName;

    private Button button;

	// Use this for initialization
	void Start ()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => LoadScene(SceneName));
        //  or
        //  button.onClick.AddListener(delegate {LoadScene(SceneName))'
         
	}

    public void LoadScene(string sceneToLoad)
    {
        //  Loads the specific scene name 
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }

}
