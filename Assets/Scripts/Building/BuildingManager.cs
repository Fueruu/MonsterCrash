using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    #region PrivateVariables
    [Header("Properties")]
    [SerializeField] private float respawnTimer = 5.0f;

    [SerializeField] private Vector3 spawnOffset = new Vector3(0, 0, 0);

    //  TODO: Implement power up spawn chance
    // [SerializeField]
    // private float powerUpChance;

    [SerializeField] private List<GameObject> buildingLibrary = new List<GameObject>();

    [SerializeField] private List<Material> materialLibrary = new List<Material>();

    [Header("Debug")]
    [SerializeField]
    private float timer = 0;

    public static BuildingManager instance = null;
    public bool isDestroyed = true;
    #endregion

    #region UnityFunctions

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // else if (instance != this)
        // {
        //     Destroy(gameObject);
        // }
    }

    private void Start()
    {
        isDestroyed = true;
        spawnOffset = spawnOffset + transform.position;
    }

    private void Update()
    {
        Spawn();
    }
    #endregion


    #region PrivateFunctions
    private void Spawn()
    {
        //  Did the building get destroyed?
        if (isDestroyed)
        {

            //  Start respawn timer
            timer -= Time.deltaTime;

            //  TODO: Re-build animation/particle effect

            if (timer <= 0)
            {
                timer = respawnTimer;
                //  Spawn building
                GameObject spawn = Instantiate(buildingLibrary[Random.Range(0, buildingLibrary.Count)], spawnOffset, transform.rotation) as GameObject;
                spawn.GetComponent<MeshRenderer>().material = materialLibrary[Random.Range(0, materialLibrary.Count)];
                isDestroyed = false;
            }

        }
    }

    #endregion


}
