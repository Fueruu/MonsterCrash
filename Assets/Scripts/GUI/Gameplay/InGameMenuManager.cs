using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


public class InGameMenuManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _RoundText = null;
    [SerializeField] private GameObject _WinRoundScreen = null;
    [SerializeField] private GameObject _WinGameScreen = null;
    [SerializeField] private GameObject pauseScreen = null;
    [SerializeField] private int _EndGameScene = 0;
    [SerializeField] private TextMeshProUGUI _CountDownText = null;
    [SerializeField] private int _CountDownTime = 5;
    [SerializeField] private AudioSourceController _CountDownSoundLow;
    [SerializeField] private AudioSourceController _CountDownSoundHigh;

    private int currentRound = 0;

    private void Start()
    {
        GameManager.Instance.currentRound += 1;

        GameManager.Instance.DidMatchStart = false;
        GameManager.Instance.isPaused = true;

        StartCoroutine(CountDownTimer(_CountDownTime));

        currentRound = GameManager.Instance.currentRound;
        GameRound();
    }


    private void Update()
    {
        Pause();
        if (PlayerWonGame()) return;
        PlayerWonRound();
    }

    private bool PlayerWonGame()
    {
        if (GameManager.Instance.PlayerWonGame())
        {
            _WinGameScreen.SetActive(true);
            _WinGameScreen.GetComponent<Text>().text = GameManager.Instance._PlayerWhoWon + " Victory!";
            return true;
        }
        return false;
    }

    private void PlayerWonRound()
    {
        if (GameManager.Instance.PlayerWonRound() && !GameManager.Instance.PlayerWonGame())
        {
            _WinRoundScreen.SetActive(true);
            if (GameManager.Instance._NumberAlive <= 0)
            {
                _WinRoundScreen.GetComponent<Text>().text = "TIE GAME";
                return;
            }
            _WinRoundScreen.GetComponent<Text>().text = GameManager.Instance._PlayerWhoWon + " Wins!";
        }
    }

    private void GameRound()
    {
        _RoundText.text = "ROUND: " + currentRound;
    }


    IEnumerator CountDownTimer(int time)
    {
        for (int i = time; i >= 0; i--)
        {

            _CountDownText.text = i > 0 ? i.ToString() : "FIGHT";
            if (i > 0)
            {
                _CountDownSoundLow.PlayOneShot();
            }
            else
            {
                _CountDownSoundHigh.PlayOneShot();
            }

            yield return new WaitForSecondsRealtime(1f);
        }


        _CountDownText.enabled = false;

        GameManager.Instance.DidMatchStart = true;
        GameManager.Instance.isPaused = false;
        StopCoroutine(CountDownTimer(0));
    }

    private void Pause()
    {
        if (!GameManager.Instance.DidMatchStart) return;
        pauseScreen.SetActive(GameManager.Instance.isPaused);
    }
}
