using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSettings : MonoBehaviour
{

    [SerializeField] bool _IsCursorVisable = false;
    [SerializeField] CursorLockMode _LockMode;

    public static MouseSettings Instance;
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
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Cursor.visible = _IsCursorVisable;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backslash))
        {
            _IsCursorVisable = !_IsCursorVisable;

            Cursor.visible = _IsCursorVisable;

            Cursor.lockState = !_IsCursorVisable ? _LockMode : CursorLockMode.None;            
        }
    }
}
