using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

namespace GUIManager
{
    public class CharacterSelectManager : MonoBehaviour
    {

        [SerializeField] private int _SceneToLoadIndex = 0;
        [SerializeField] TextMeshProUGUI _PlayersLockedText = null;
        [SerializeField] TextMeshProUGUI _CountDownText = null;
        [SerializeField] private int _CountDownTime = 5;

        private CharacterSelectController[] _CharacterSelectors;
        private int _PlayerCount;
        private float currentTime;
        public int playersReadyCount;
        private bool changingScenes;

        public static CharacterSelectManager Instance;
        private void Awake()
        {
            _CharacterSelectors = GetComponentsInChildren<CharacterSelectController>(true);
            _CountDownText.text = "";
            currentTime = _CountDownTime;

            if (Instance == null)
            {
                Instance = this;
                return;
            }
            Destroy(this);
        }

        private void Update()
        {
            _PlayerCount = ControllerManager.Instance.ConnectedDevices.Count;

            PlayersReadyText();

            if (StartGame() && !changingScenes)
            {
                _CountDownText.enabled = true;
                Timer();
            }
            else
            {
                _CountDownText.enabled = false;
                currentTime = _CountDownTime;

            }


            var activeInputDevice = InputManager.ActiveDevice;
            if (ControllerManager.Instance.OnJoinButtonPressed(activeInputDevice))
            {
                _PlayerCount = ControllerManager.Instance.ConnectedDevices.Count;
                _CharacterSelectors[_PlayerCount - 1].DeviceInput = activeInputDevice;
                _CharacterSelectors[_PlayerCount - 1].gameObject.SetActive(true);
            }

            foreach (var item in _CharacterSelectors)
            {
                if (item.isActiveAndEnabled)
                {
                    if (!item.DeviceInput.IsAttached)
                    {
                        item.gameObject.SetActive(false);
                        ControllerManager.Instance.OnDeviceDetached(item.DeviceInput);
                    }
                }
            }
        }

        private bool StartGame()
        {
            if (_PlayerCount < 2)
            {
                return false;
            }

            for (int i = 0; i < _PlayerCount; i++)
            {
                if (_CharacterSelectors[i].PlayerReady() == false)
                {
                    return false;
                }
            }

            return true;
        }

        private void PlayersReadyText()
        {
            if (_PlayerCount < 2)
            {
                _PlayersLockedText.text = "Need 2 Players to Start";
                return;
            }

            _PlayersLockedText.text = $"Locked: {playersReadyCount}/{_PlayerCount}";
        }


        private void Timer()
        {
            _CountDownText.text = currentTime > 1 ? Mathf.RoundToInt(currentTime).ToString() : "GAME STARTING...";

            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                ChangeScene(_SceneToLoadIndex);
            }
        }

        private void ChangeScene(int sceneNumber)
        {
            changingScenes = true;
            LoadingScreen.Instance.ShowLoadScreen(SceneManager.LoadSceneAsync(sceneNumber));
        }
    }
}