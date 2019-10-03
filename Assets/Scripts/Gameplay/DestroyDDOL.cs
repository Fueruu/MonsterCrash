using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyDDOL : MonoBehaviour
{
    public void DestroyAllDDOLs()
    {
        DDOLManager.DestroyAll();
        Debug.Log("Destroyed: ");
    }
}
