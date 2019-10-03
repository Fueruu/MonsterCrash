using InControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace GUIManager
{
    public class CharacterSelectController : MonoBehaviour
    {

        [SerializeField] private CharacterDisplayManager _Manager;
        [SerializeField] private bool _EnableControl = true;
        [SerializeField] private Color _PlayerColor;
        [SerializeField] private Color _DisableColor;

        [Header("Device Properties")]
        [SerializeField] private float _VibrationDuration = 0.2f;
        [SerializeField] private float _VibrationIntensity = 20f;

        public InputDevice DeviceInput;
        private int _CharacterIndex = 0;
        private Button _Button;
        private Slider _Slider;
        private TwoAxisInputControl _FilteredDirection;
        private EventSystem _EventSystem;
        private bool _IsReady = false;

        void Awake()
        {
            _EnableControl = true;
            _Button = GetComponent<Button>();
            _Slider = GetComponent<Slider>();
            _FilteredDirection = new TwoAxisInputControl();
            _FilteredDirection.StateThreshold = 0.5f;

        }

        private void OnEnable()
        {
            _Manager.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            if (_Manager)
            {
                _Manager.gameObject.SetActive(false);
            }
        }

        void Update()
        {
            ControllerInput();
        }

        private void ControllerInput()
        {
            if (DeviceInput != null)
            {
                _FilteredDirection.Filter(DeviceInput.Direction, Time.deltaTime);
            }

            if (_EnableControl)
            {
                if (_FilteredDirection.Left.WasPressed)
                {
                    _Slider.value -= 1;
                }

                if (_FilteredDirection.Right.WasPressed)
                {
                    _Slider.value += 1;
                }

                if (DeviceInput.Action1.WasPressed)
                {
                    if (_Button)
                    {
                        ExecuteEvents.Execute(_Button.gameObject, new BaseEventData(_EventSystem), ExecuteEvents.submitHandler);
                    }
                    LockChoice(_CharacterIndex, DeviceInput);
                }
            }
            if (DeviceInput.Action2.WasPressed && !_EnableControl)
            {
                _IsReady = false;
                _EnableControl = true;

                CharacterSelectManager.Instance.playersReadyCount -= 1;

                _Manager.CharacterAnimator().SetBool("StartCharge", false);
                _Manager.CharacterAnimator().SetTrigger("IsHit");
                _Manager.CharacterAnimator().SetTrigger("Stopped");
            }
        }
        public void CharacterSelectIndex(float index)
        {
            _CharacterIndex = (int)index;
            _Manager.SetCurrentCharacterType(_CharacterIndex);
        }

        private void CreateACharacter()
        {
            CharacterDisplayManager.Instance.CreateCharacter();
        }

        private void LockChoice(int index, InputDevice inputDevice)
        {
            ControllerManager.Instance.ConnectedDevices[inputDevice] = index;

            CharacterSelectManager.Instance.playersReadyCount += 1;

            StartCoroutine(VirbrateDevice());

            _Manager.CharacterAnimator().SetBool("StartCharge", true);

            _IsReady = true;
            _EnableControl = false;
        }

        public bool PlayerReady()
        {
            return _IsReady;
        }

        private IEnumerator VirbrateDevice()
        {
            DeviceInput.Vibrate(_VibrationIntensity, _VibrationIntensity);
            yield return new WaitForSecondsRealtime(_VibrationDuration);

            DeviceInput.StopVibration();
            StopCoroutine(VirbrateDevice());
        }
    }
}