using System.Collections;
using UnityEngine;

public class PowerUpBomb : APowerUp
{

    [Header("Bomb")]
    [SerializeField] private float bombBaseDamage = 10f;
    [SerializeField] private float bombExplosionRadius = 50f;
    [SerializeField] private float bombKnockBackPower = 1000f;
    [SerializeField] private float bombUpForce = 10f;
    [SerializeField] private int bombTimer = 3;
    [SerializeField] private AudioSource bombPickUpAudio = null;

    private bool bombEnabled = false;

    // public override void ActivatePowerup()
    // {
    //     //Do Bomb Stuff
    //     StartCoroutine(BombPowerUp(bombTimer));
    // }

    IEnumerator BombPowerUp(int time)
    {
        //sprites[0].SetActive(true);

        if (bombEnabled)
        {
            yield break;
        }

        bombEnabled = true;

        for (int i = time; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Bomb countdown:" + i);
        }

        Collider[] colliders = Physics.OverlapSphere(transform.position, bombExplosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            APlayer hitPlayer = hit.GetComponent<APlayer>();
            if (rb != null)
            {
                rb.AddExplosionForce(bombKnockBackPower, transform.position, bombExplosionRadius, 0.0F, ForceMode.VelocityChange);
            }
            if (hitPlayer != null)
            {

                float proximity = (transform.position - rb.position).magnitude;
                float effect = 1 - (proximity / bombExplosionRadius);
                float damageDone = bombBaseDamage * effect;

                hitPlayer.CalculateHealth(damageDone);

                Debug.LogFormat("Damaged: {0} for {1}", hitPlayer.name, damageDone);
            }
            //sprites[0].SetActive(false);
        }

        bombEnabled = false;

        StopCoroutine(BombPowerUp(0));
    }
}
