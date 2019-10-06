using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _LoadFillImage;
	[SerializeField] private GameObject[] toToogle = new GameObject[0];
    [SerializeField] private TextMeshProUGUI _PercentLoadedText;
    [SerializeField] private const float minTimeToShow = 8f;
	
	[SerializeField] private VideoPlayer loadingVideo;

    public AsyncOperation currentLoadingOperation;
    private float currentLoad;
    private bool isLoading;
    private float timeElapsed;
    private Animator anim;

    public static LoadingScreen Instance;
    private void Start()
    {
        _PercentLoadedText.text = "Loading... 0%";
        _LoadFillImage.fillAmount = currentLoad;
        anim = GetComponent<Animator>();
        //HideLoadScreen();

        if (Instance == null)
        {
            Instance = this;
            SingletonAwake();
        }
        else
        {
            Destroy(this);
            return;
        }

		loadingVideo.loopPointReached += VideoLooped;
		loadingVideo.prepareCompleted += HideLoadScreen;
    }

    private void SingletonAwake()
    {
        DDOLManager.DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (isLoading)
        {
            SetProgress(currentLoadingOperation.progress);

            if (currentLoadingOperation.isDone)
            {
                //anim.SetTrigger("Hide");
            }
            else
            {
                timeElapsed += Time.fixedUnscaledDeltaTime;
                if (timeElapsed >= minTimeToShow)
                {
                    currentLoadingOperation.allowSceneActivation = true;
                }
            }
        }
    }
    private void SetProgress(float progress)
    {
        currentLoad = progress;

        _LoadFillImage.fillAmount = Mathf.Lerp(_LoadFillImage.fillAmount, currentLoad, Time.fixedUnscaledDeltaTime * 2);

        _PercentLoadedText.text = "Loading... " + Mathf.CeilToInt(progress * 100).ToString() + "%";
    }

    public void ShowLoadScreen(AsyncOperation loadingOperation)
    {
		//gameObject.SetActive(true);
		//loadingVideo.gameObject.SetActive (true);
        ToogleElements();
		anim.SetTrigger("Show");
        currentLoadingOperation = loadingOperation;
        currentLoadingOperation.allowSceneActivation = false;
        timeElapsed = 0f;
        SetProgress(0);
        isLoading = true;
    }

    public void HideLoadScreen(UnityEngine.Video.VideoPlayer vp)
    {
        //gameObject.SetActive(false);
		ToogleElements();
        currentLoadingOperation = null;
        isLoading = false;
		loadingVideo.prepareCompleted -= HideLoadScreen;
    }
	
	//Added following functions to play and stop video by calling a function (for use with animation events)
	public void PlayVideo (){
		loadingVideo.Play();
	}
	
	public void VideoLooped(UnityEngine.Video.VideoPlayer vp){
		anim.SetTrigger("Hide");
	}
	
	public void StopVideo (){
		loadingVideo.Stop();
	}
	
	private void ToogleElements(){
		foreach (GameObject i in toToogle){
			i.SetActive (!i.activeSelf);
		}
	}

}
