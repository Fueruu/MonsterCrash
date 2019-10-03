using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class Item : PooledObject
{

    [SerializeField] private GameObject destructableModel;

    [SerializeField] private List<Material> _ObjectMaterial = new List<Material>();

    // [SerializeField] private AudioSourceController destructionAudio = null;

    [SerializeField] private float healAmount = 1f;

    [SerializeField] private float multiplier = 0.1f;

    public Rigidbody rb { get; private set; }

    [SerializeField] private bool isDead;

    MeshRenderer[] meshRenderers;

    public void SetMaterial(Material m)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers[i].material = m;
        }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<PlayerController>())
        {
            // if (destructionAudio != null)
            // {
            //     destructionAudio.Play();
            // }
            Instantiate(destructableModel, transform.position, transform.rotation);
            ReturnToPool();
        }
    }

    public float MultiplyDamage()
    {
        return multiplier;
    }

    public float Heal()
    {
        return healAmount;
    }

    private void OnDisable()
    {
        isDead = true;
    }

    private void OnEnable()
    {
        SetMaterial(_ObjectMaterial[Random.Range(0, _ObjectMaterial.Count)]);
        isDead = false;
    }

    public bool GetStatis()
    {
        return isDead;
    }
}