using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleControl : MonoBehaviour
{
    [SerializeField] private float rateOfTime;

    private void Start()
    {
        Time.timeScale = rateOfTime;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
