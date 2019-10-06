using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;


public class VideoController : MonoBehaviour{

	[SerializeField] private VideoPlayer loadingVideo;
	[SerializeField] private RawImage loadingVideoImage;

	public void Start (){
		loadingVideoImage.color = Color.black;
		loadingVideo = GetComponent<VideoPlayer>();
		loadingVideoImage = GetComponent<RawImage>();
		StartCoroutine (PrepareVideo());
	}	
	
	public void Update(){
		if (loadingVideo.isPlaying)
			loadingVideoImage.color = Color.white;
		else
			loadingVideoImage.color = Color.clear;
	}
	
	private IEnumerator PrepareVideo (){
		loadingVideo.Prepare();
		while (!loadingVideo.isPrepared){
			yield return new WaitForSeconds (1);
			break;
		}
		loadingVideoImage.color = Color.white;
		loadingVideoImage.texture = loadingVideo.texture;
		Debug.Log ("Video Prepared");
		//this.gameObject.SetActive(false);
	}
	
	public void PlayVideo (){
		loadingVideo.Play();
		Debug.Log ("Playing Video");
	}
	
	public void StopVideo (){
		loadingVideo.Stop();
		Debug.Log ("Stopped Video");
	}
	
}