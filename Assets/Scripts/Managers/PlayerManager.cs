using System.Collections.Generic;
using InControl;
using UnityEngine;
using Cinemachine;



public class PlayerManager : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private GameObject[] _PlayerPrefab;
    [Tooltip("Set index 5 to 0 opacity")]
    [SerializeField] private List<Color> _PlayerColours;
    [SerializeField] private List<GameObject> _SpawnLocation = new List<GameObject>();


    [Header("Player UI")]
    [SerializeField] private List<GameObject> _PlayerGUI = new List<GameObject>();
    [SerializeField] private List<Sprite> _MonsterIcons = new List<Sprite>();


    [Header("Cinemachine Target Group Settings")]
    [SerializeField] private CinemachineTargetGroup _TargetGroup;
    [SerializeField] private int _TargetRadius = 5;


    private int _PlayerCount;
    List<Vector3> spawnVectors = new List<Vector3>();
    private GameObject playerList;

    void Start()
    {
        playerList = new GameObject();
        playerList.name = "---Players---";

        GameManager.Instance._NumberAlive = 0;
        GameManager.Instance._PlayersAlive.Clear();

        _TargetGroup.GetComponent<CinemachineTargetGroup>();

        var inputDevice = InputManager.ActiveDevice;
        // Spawn player at spawn location
        for (int i = 0; i < _SpawnLocation.Count; i++)
        {
            spawnVectors.Add(_SpawnLocation[i].transform.position);
        }

        _PlayerCount = ControllerManager.Instance.ConnectedDevices.Count;

        _TargetGroup.m_Targets = new CinemachineTargetGroup.Target[_PlayerCount];

        CreatePlayers();

        foreach (var item in GameManager.Instance._Players)
        {
            GameManager.Instance._PlayersAlive.Add(item); 
        }
    }


    // TODO: Chewck all this code, check for playerIndex vs characterIndex vs playerNumber
    private void CreatePlayers()
    {
        var playerIndex = 0;

        if (GameManager.Instance == null)
        {
            Debug.LogError($"@CreatePlayers -> GameManagerInstance is null");
            return;
        }

        foreach (KeyValuePair<InputDevice, int> item in ControllerManager.Instance.ConnectedDevices)
        {
            //  Get spawn locations and remove them as players spawn
            var playerPosition = spawnVectors[0];
            spawnVectors.RemoveAt(0);


            //  Get our keys and values from the dictionary
            var controller = item.Key;
            var characterIndex = item.Value;
            var gameObject = (GameObject)Instantiate(_PlayerPrefab[characterIndex], playerPosition, Quaternion.identity);
            var player = gameObject.GetComponent<PlayerController>();

            //  Add our player to the list of alive players in our GameManager.cs
            if (GameManager.Instance.currentRound <= 0)
            {
                GameManager.Instance._Players.Add(new PlayerData { _Player = player, _PlayerWins = 0 });
            }
            else
            {
                print($"@CreatePlayers :: {GameManager.Instance._Players.Count} - {playerIndex}");
                GameManager.Instance._Players[playerIndex]._Player = player;
            }
            GameManager.Instance._NumberAlive += 1;

            //  Add to our CinemachineTargetGroup so player can be tracked
            AddToCinemachine(player.gameObject.transform, playerIndex);

            //  Assign health bar and UI
            _PlayerGUI[playerIndex].SetActive(true);
            _PlayerGUI[playerIndex].gameObject.GetComponentInChildren<HUDHP>()._AffectedTarget = player;
            _PlayerGUI[playerIndex].gameObject.GetComponentInChildren<MonsterIcon>().Icon(_MonsterIcons[item.Value]);
            _PlayerGUI[playerIndex].gameObject.GetComponentInChildren<MonsterIcon>().transform.Rotate(0, 180 * ((playerIndex + 1) % 2), 0);

            //  Set win counters
            _PlayerGUI[playerIndex].GetComponentInChildren<WinCounter>().PlayerIndex(playerIndex);

            //  Assign player colour
            player._LineRenderer.startColor = _PlayerColours[playerIndex];
            var main = player._ChargeUpParticle.main;
            main.startColor = _PlayerColours[playerIndex];

            //  Rename our player to their appropriate number and parrent them to the "---Player---" hiearchy
            var playerNumber = playerIndex + 1;
            gameObject.name = "P" + playerNumber;
            gameObject.transform.parent = playerList.transform;
            player._PlayerNumberText.text = "P" + playerNumber; 
            player._PlayerNumberText.color = _PlayerColours[playerIndex];

            //  Assign the controller
            player.deviceInput = controller;
            player.deviceInput.SetLightColor(_PlayerColours[playerIndex]);

            playerIndex++;
        }
    }

    private void AddToCinemachine(Transform player, int index)
    {
        _TargetGroup.m_Targets[index].target = player;
        _TargetGroup.m_Targets[index].weight = 1;
        _TargetGroup.m_Targets[index].radius = _TargetRadius;
    }
}
