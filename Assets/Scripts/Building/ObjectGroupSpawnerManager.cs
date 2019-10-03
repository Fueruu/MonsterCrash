using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGroupSpawnerManager : MonoBehaviour
{
    [SerializeField] ObjectSpawner[] _Spawners;
    [SerializeField] GameObject[] _PowerUps;

    [SerializeField] private int _EmptyCells = 0;
    private bool _BlockEmpty = false;

    private void Update()
    {
        IsCellEmpty();
    }

    private void IsCellEmpty()
    {
        foreach (var item in _Spawners)
        {
            if (!item.IsOccupied())
            {
                if (_EmptyCells >= _Spawners.Length) return;
                _EmptyCells += 1;
            }
            // else
            // {
            //     if (_EmptyCells <= 0) return;
            //     _EmptyCells -= 1;
            // }
        }
    }

}
