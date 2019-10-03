using System.Collections.Generic;
using InControl;
using UnityEngine;

public class ResetControllerManager : MonoBehaviour
{
    public void ResetManager()
    {
        ControllerManager.Instance.ConnectedDevices.Clear();
    }
}
