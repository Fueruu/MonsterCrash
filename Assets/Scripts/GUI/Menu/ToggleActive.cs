using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleActive : MonoBehaviour {

    [SerializeField] private GameObject[] _ToToggle = new GameObject[0];

	// Use this for initialization
	private void Awake ()
    {
        GetComponent<Button>().onClick.AddListener(Toggle);
	}
	
    private void Toggle()
    {
        for (int i = 0; i < _ToToggle.Length; i++)
        {
            GameObject gameObject = _ToToggle[i];
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }
    }
}
