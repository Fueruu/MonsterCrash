using UnityEngine;

public class APowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType powerType = PowerUpType.Bomb;

    public PowerUpType GetPowerUpType()
    {
        return powerType;
    }

    // public abstract void ActivatePowerup();
}

public enum PowerUpType
{
    Bomb,
    Heal,
    DamageBoost,
    Hat
}
