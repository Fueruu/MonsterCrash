using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;
using System;


public class GameManager : MonoBehaviour
{
    [SerializeField] private const int _NumberOfRounds = 3;

    [SerializeField] private float rateOfTime;
    public List<PlayerData> _Players = new List<PlayerData>();
    public List<PlayerData> _PlayersAlive = new List<PlayerData>();

    [SerializeField] private bool _APlayerWon = false;
    public int _NumberAlive = 0;
    public string _PlayerWhoWon = "";
    public bool DidMatchStart = false;


    public InputDevice deviceInput;
    public bool isPaused = false;
    public int currentRound = 0;

    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SingletonAwake();
            return;
        }
        Destroy(this);
    }

    private void SingletonAwake()
    {
        DDOLManager.DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        GamePaused();
        PlayerWonGame();
        PlayerWonRound();
        DeathCheck();
        Reset();
        Escape();
    }

    private void DeathCheck()
    {
        if (_APlayerWon) return;

        foreach (var item in _Players)
        {
            if (item._Player.IsDead())
            {
                _PlayersAlive.Remove(item);
                _NumberAlive = _PlayersAlive.Count;
            }
        }
    }

    private void CheckWhoWon()
    {
        if (_Players.Count <= 0) return;

        foreach (var item in _Players)
        {
            if (!item._Player.IsDead())
            {
                _PlayerWhoWon = item._Player.name;
                if (!_APlayerWon)
                {
                    item._PlayerWins = item._PlayerWins + 1;
                }
            }
        }
    }

    public bool PlayerWonRound()
    {
        if (_NumberAlive <= 1)
        {
            CheckWhoWon();
            _APlayerWon = true;
            SlowTime(_APlayerWon);
            return true;
        }
        else
        {
            _APlayerWon = false;
            return false;
        }
    }

    public bool PlayerWonGame()
    {
        foreach (var item in _Players)
        {
            if (item._PlayerWins >= 2)
            {
                return true;
            }
        }
        return false;
    }

    private void GamePaused()
    {

        deviceInput = InputManager.ActiveDevice;

        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if (deviceInput.Command.WasPressed && DidMatchStart)
        {
            isPaused = !isPaused;
        }

    }

    private void Reset()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        };
    }

    private void Escape()
    {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        };
    }

    private void SlowTime(bool enableTimeSlow)
    {
        if (enableTimeSlow)
        {
            //  By the greek god Chronos enable time slow
            Time.timeScale = rateOfTime;
        }
        else
        {
            Time.timeScale = 1;
        }
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}

public class PlayerData
{
    public PlayerController _Player;
    public int _PlayerWins;
}

