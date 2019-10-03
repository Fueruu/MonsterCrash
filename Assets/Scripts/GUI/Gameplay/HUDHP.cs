using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDHP : MonoBehaviour
{

    [Header("Health Bar Properties")]
    [Tooltip("The target actor for health display")]
    public APlayer _AffectedTarget;
    [SerializeField] private float _LerpSpeed = 10f;


    [Header("Health Bar Trail Effect")]
    [SerializeField] private Image _HealthBarTrail;
    [SerializeField] private float _LerpTrailSpeed = 10f;


    [SerializeField] private bool _LockRotation = false;


    private Image _HealthBar;

    private void Awake()
    {
        //  gte the image cpmponent
        _HealthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    private void Update()
    {
        _HealthBarTrail.fillAmount = Mathf.Lerp(_HealthBarTrail.fillAmount, _HealthBar.fillAmount, Time.deltaTime * _LerpTrailSpeed);

        //  return early if the target is null
        if (!_AffectedTarget)
        {
            _HealthBar.fillAmount = Mathf.Lerp(_HealthBar.fillAmount, 0, Time.deltaTime * _LerpSpeed);;
            return;
        }
        _HealthBar.fillAmount = Mathf.Lerp(_HealthBar.fillAmount, _AffectedTarget.HealthFill, Time.deltaTime * _LerpSpeed);

        if (_LockRotation) transform.rotation = Quaternion.identity;
    }
}
