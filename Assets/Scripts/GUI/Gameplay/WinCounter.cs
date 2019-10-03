using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCounter : MonoBehaviour
{
    [SerializeField] private List<GameObject> _WinCounters = new List<GameObject>();
    

    [SerializeField] private int index;

    private void Update()
    {
        var numberOfWins = GameManager.Instance._Players[index]._PlayerWins;
        
        for (int i = 0; i < numberOfWins; i++)
        {
            _WinCounters[i].SetActive(true);
        }
    }

    public void PlayerIndex(int i)
    {
        index = i;
    }
}
