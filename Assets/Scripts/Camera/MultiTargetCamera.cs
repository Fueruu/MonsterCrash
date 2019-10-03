using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class MultiTargetCamera : MonoBehaviour
{

    [Header("Cinemachine Target Group Settings")]
    [SerializeField] private CinemachineTargetGroup _TargetGroup;
    [SerializeField] private List<CinemachineTargetGroup.Target> _Targets;
    [SerializeField] private CinemachineTargetGroup.PositionMode _PositionMode;
    [SerializeField] private CinemachineTargetGroup.RotationMode _RotationMode;
    [SerializeField] private CinemachineTargetGroup.UpdateMethod _UpdateMethod;


    private int _PlayerCount;

    // Start is called before the first frame update
    void Start()
    {
        // for (int i = 0; i < _Camera.m_Targets.Length; i++)
        // {
        //     if (_Camera.m_Targets[i].target == null)
        //     {
        //         _Camera.m_Targets.SetValue(target, i);
        //         return;
        //     }
        // }

        foreach (var item in ControllerManager.Instance.ConnectedDevices)
        {
            
        }
    }

    private void AddToCinemachine(Transform player, int index)
    {
        _TargetGroup.m_Targets = new CinemachineTargetGroup.Target[_PlayerCount];

        CinemachineTargetGroup.Target _Target;
        _Target.target = player;
        _Target.weight = 1;
        _Target.radius = 0;

        _TargetGroup.m_Targets.SetValue(player, 0);

        //Debug.Log("Target set");

        if (_TargetGroup.m_Targets[index].target == null)
        {
        }


    }
}
