using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CleanUp : MonoBehaviour
{
    [SerializeField] private float cleanUpTimer = 5;

    [SerializeField] private float alwaysDestroy = -10;

    [Header("Debug")]
    [SerializeField] private float timer;

    private void Start()
    {
        timer = cleanUpTimer;
    }

    private void Update()
    {
        Clean(true);
    }

    private void Clean(bool enableTimer)
    {
        if (enableTimer)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                Physics.IgnoreLayerCollision(9, 11);
                if (timer <= alwaysDestroy)
                {
                    Destroy(gameObject);
                }
            }
        }

        //  Remove the empty gameObject if it has no children in it
        if (transform.childCount <= 0)
        {
            Destroy(gameObject);
        }
    }
}
