using System.Collections;
using UnityEngine;
using InControl;
using TMPro;

public class APlayer : MonoBehaviour
{
    [Header("Health Properties")]
    [SerializeField] private float maxHP = 100;
    [SerializeField] protected float currentHP = 100;

    [Header("Damage Propertie")]
    [SerializeField] protected float maxChargeDamage = 10;


    [Header("Device Properties")]
    [SerializeField] private float _VibrationDuration = 0.2f;
    [SerializeField] private float _VibrationIntensity = 20f;


    [Header("Particles")]
    [SerializeField] protected ParticleSystem _OnCollisionParticle = null;
    [SerializeField] public ParticleSystem _ChargeUpParticle = null;
    [SerializeField] protected ParticleSystem _DashParticle = null;


    [Header("Sound")]
    [SerializeField] protected AudioSourceController dashAudio;
    [SerializeField] protected AudioSourceController chargeUpAudio;
    [SerializeField] protected AudioSourceController hurtAudio;
    [SerializeField] protected AudioSourceController impactAudio;
    [SerializeField] protected AudioSourceController deathAudio;
    [SerializeField] protected AudioSourceController fallingAudio;

    [SerializeField] private bool isDead = false;
    [SerializeField] public TextMeshProUGUI _PlayerNumberText;

    protected float damage;
    protected float totalDamage;
    protected float damageMultiplier = 1;
    protected Animator anim;
    public InputDevice deviceInput;
    private float _HPLastFrame = 1000;

    public float CurrentHP
    {
        //  returns current health from currentHP
        get { return currentHP; }
        //  sets currentHP to new value, protected scope
        protected set { currentHP = Mathf.Clamp(value, 0f, maxHP); }
    }

    public float HealthFill
    {
        get
        {
            return Mathf.Clamp(currentHP / maxHP, 0f, 1f);
        }
    }

    public virtual void CalculateHealth(float amount)
    {
        //  return if actor is already dead 
        if (CurrentHP > 0f)
        {
            _HPLastFrame = CurrentHP;
            CurrentHP -= amount;

            //  reduce hp by damage output
            if (hurtAudio && _HPLastFrame > CurrentHP && CurrentHP < maxHP)
            {
                anim.SetTrigger("IsHit");
                hurtAudio.PlayOneShot();
                StartCoroutine(VirbrateDevice());
            }
        }

        if (CurrentHP <= 0)
        {
            if (deathAudio && !isDead)
            {
                deathAudio.Play();
            }
            anim.SetTrigger("IsDead");
            isDead = true;
        }
    }

    private IEnumerator VirbrateDevice()
    {
        deviceInput.Vibrate(_VibrationIntensity, _VibrationIntensity);
        yield return new WaitForSecondsRealtime(_VibrationDuration);

        deviceInput.StopVibration();
        StopCoroutine(VirbrateDevice());
    }

    public virtual void CalculateDamage(float damage, float multiplier)
    {
        totalDamage = damage * multiplier;
    }

    public bool IsDead()
    {
        return isDead;
    }

}
