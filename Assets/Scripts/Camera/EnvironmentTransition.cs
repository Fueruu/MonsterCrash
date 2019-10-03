using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class EnvironmentTransition : MonoBehaviour{
	
	//Variables for details regarding camera movement
	[SerializeField] private PlayableDirector cameraMovement;
	
	public void Start (){
		//Based this line from Kevin's ToggleActive script, as this is basically an extension of what he was doing there.
        GetComponent<Button>().onClick.AddListener(MoveCamera);
	}
	
	public void MoveCamera (){
		cameraMovement.Stop();
		cameraMovement.Play();
	}
	
}