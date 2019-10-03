using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    [SerializeField] float _DestructionDelay = 1.0f;

    private PlayerController _PlayerController;
    private void OnCollisionEnter(Collision other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player)
        {
            player.CalculateHealth(1000);
            Debug.LogFormat("{0} takes {1} damage.", player.name, 1000);
        }
        Destroy(other.gameObject, _DestructionDelay);
    }
}
