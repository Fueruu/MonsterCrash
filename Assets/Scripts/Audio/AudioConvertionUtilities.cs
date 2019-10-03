using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioConvertionUtilities : MonoBehaviour {

    public static float dbtoLinear(float db)
    {
        if (db > -80f)
        {
            return Mathf.Clamp01(Mathf.Pow(10.0f, db / 20.0f));
        }
        else { return 0f; }
    }

    public static float linearToDecibel(float linear)
    {
        if(linear > 0)
        {
            return Mathf.Clamp(20.0f * Mathf.Log10(linear), -80f, 0f);

        }
        else { return -80.0f; }
    }

    private static float twelfRootOfTwo = Mathf.Pow(2, 1f / 12f);

    public static float stToPitch(float st)
    {
        return Mathf.Clamp(Mathf.Pow(twelfRootOfTwo, st), 0f, 4f);
    }

    public static float pitchToSt(float pitch)
    {
        return Mathf.Log(pitch, twelfRootOfTwo);
    }
}
