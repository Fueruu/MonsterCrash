using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PowerUpManager : MonoBehaviour
{
    /// <summary>
    /// Notes:
    /// De-couple this code into seperate pieces. This a bad practice and should be fixed
    /// </summary>

    [Header("PowerUp Sprites")]
    [SerializeField] private GameObject[] sprites;

    [Header("Heal")]
    [SerializeField] private float healAmount = 2;
    [SerializeField] private float healDuration = 10;
    [SerializeField] private AudioSource _HealAudioEffect = null;
    [SerializeField] private ParticleSystem _HealParticle = null;

    [Header("Bomb")]
    [SerializeField] private float bombBaseDamage = 10f;
    [SerializeField] private float bombExplosionRadius = 50f;
    [SerializeField] private float bombKnockBackPower = 1000f;
    [SerializeField] private float bombUpForce = 10f;
    [SerializeField] private ForceMode _BombForceMode = ForceMode.Impulse; 
    [SerializeField] private ParticleSystem _BombParticle = null;
    [SerializeField] private TextMeshProUGUI textBombTimer = null;
    [SerializeField] private int bombTimer = 3;
    [SerializeField] private AudioSourceController _BombAudioEffect = null;


    [Header("Damage Boost")]
    [SerializeField] private float damageBoostMultiplier;


    private APlayer player;
    private bool bombEnabled = false;
    private bool healEnabled = false;


    private void Start()
    {
        player = GetComponent<APlayer>();
    }

    private void Update()
    {
        Cheats();
    }

    private void Cheats()
    {
        switch (Input.inputString)
        {
            case "1":
                StartCoroutine(BombPowerUp(bombTimer));
                break;
            case "2":
                StartCoroutine(HealPowerUp());
                break;
            case "3":
                //Debug.Log("Damage: 10");
                player.CalculateHealth(10);
                break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        var otherObject = other.gameObject.GetComponent<APowerUp>();
        if (!otherObject) return;
        switch (otherObject.GetPowerUpType())
        {
            case PowerUpType.Bomb:
                StartCoroutine(BombPowerUp(bombTimer));
                break;

            case PowerUpType.Heal:
                StartCoroutine(HealPowerUp());
                break;

            case PowerUpType.DamageBoost:

                break;
        }
    }

    IEnumerator BombPowerUp(int time)
    {
        sprites[0].SetActive(true);

        if (bombEnabled)
        {
            yield break;
        }

        bombEnabled = true;

        for (int i = time; i >= 0; i--)
        {
            textBombTimer.text = i.ToString();
            yield return new WaitForSeconds(1f);
            Debug.Log("Bomb countdown:" + i);
        }

       _BombParticle.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, bombExplosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            APlayer hitPlayer = hit.GetComponent<APlayer>();
            if (rb != null)
            {
                rb.AddExplosionForce(bombKnockBackPower, transform.position, bombExplosionRadius, bombUpForce, _BombForceMode);
                Debug.LogFormat("Name: {0}", rb.name);
            }
            if (hitPlayer != null)
            {

                float proximity = (transform.position - rb.position).magnitude;
                float effect = 1 - (proximity / bombExplosionRadius);
                float damageDone = bombBaseDamage * effect;

                hitPlayer.CalculateHealth(damageDone);

                Debug.LogFormat("Damaged: {0} for {1}", hitPlayer.name, damageDone);
            }
            sprites[0].SetActive(false);
        }

        if (_BombAudioEffect && !_BombAudioEffect.IsPlaying())
        {
            _BombAudioEffect.PlayOneShot();
        }

        bombEnabled = false;
        StopCoroutine(BombPowerUp(0));
    }

    IEnumerator HealPowerUp()
    {
        //sprites[1].SetActive(true);
        _HealParticle.Play();

        if (healEnabled)
        {
            yield break;
        }

        healEnabled = true;
        _HealAudioEffect.PlayOneShot(_HealAudioEffect.clip);

        for (int i = 0; i < healDuration; i++)
        {
            player.CalculateHealth(-healAmount);
            yield return new WaitForSeconds(1f);
        }

        _HealParticle.Stop();
        healEnabled = false;

        StopCoroutine(HealPowerUp());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        //Use the same vars you use to draw your Overlap SPhere to draw your Wire Sphere.
        Gizmos.DrawWireSphere(transform.position, bombExplosionRadius);
    }
}

