using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using InControl;

public class DebugCommands : MonoBehaviour
{
    public List<APlayer> playerControllerScript;

    private void Update()
    {
        Command();
        playerControllerScript = new List<APlayer>();
    }

    private void Command()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Do Damage");
            for (int i = 0; i < playerControllerScript.Count; i++)
            {
                playerControllerScript[i].CalculateHealth(10);
            }
        }

    }

}
