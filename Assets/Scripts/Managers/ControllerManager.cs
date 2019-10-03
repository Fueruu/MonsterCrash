using System.Collections.Generic;
using InControl;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{

    public static ControllerManager Instance;
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


    [SerializeField] private const int maxPlayers = 4;

    public Dictionary<InputDevice, int> ConnectedDevices = new Dictionary<InputDevice, int>(maxPlayers);

    private void SingletonAwake()
    {
        DDOLManager.DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        InputManager.OnDeviceDetached += OnDeviceDetached;
    }

    private void OnDisable()
    {
        InputManager.OnDeviceDetached -= OnDeviceDetached;
    }


    public bool OnJoinButtonPressed(InputDevice inputDevice)
    {
        if(ConnectedDevices.ContainsKey(inputDevice))
        {
            return false;
        }
        ConnectedDevices.Add(inputDevice, -1);
        return true;
    }

    private bool IsDeviceBeingUsed(InputDevice inputDevice)
    {
        return ConnectedDevices.ContainsKey(inputDevice);
    }

    public void OnDeviceDetached(InputDevice inputDevice)
    {
        if (IsDeviceBeingUsed(inputDevice))
        {
            ConnectedDevices.Remove(inputDevice);
        }
    }

}

