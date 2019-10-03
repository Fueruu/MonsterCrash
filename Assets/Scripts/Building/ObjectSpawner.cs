using UnityEngine;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    [System.Serializable]
    private struct Spawnable
    {
        public Item gameItem;
        public float objectWeight;
        public ParticleSystem objectSpawnParticle;
    }

    [SerializeField] private Spawnable[] _SpawnList;

    [SerializeField] private Vector3 spawnOffset = new Vector3(0, 0, 0);
    [SerializeField] private float spawnTimer;
    [SerializeField] private ParticleSystem _RebuildingParticle;

    [Header("Debug")]
    [SerializeField] private float timer;
    [SerializeField] private Item occupyingObject = null;

    private bool beginSpawnPrep = false;
    private Item spawn = null;
    private Item prefab = null;
    private float totalSpawnWeight;
    private List<int> _ObjectAngles = new List<int>(new int[] { 45, 135, 225, 315 });

    private void Start()
    {
        OnValidate();
        PrepSpawn();
        SpawnItem();
    }

    private void FixedUpdate()
    {
        Timer();
        IsOccupied();
    }

    private void Timer()
    {
        if (occupyingObject == null)
        {

            if (beginSpawnPrep)
            {
                PrepSpawn();
                _RebuildingParticle.Play();
            }

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SpawnItem();
                _RebuildingParticle.Stop();

                timer = spawnTimer;
            }
        }
    }


    public bool IsOccupied()
    {
        if (occupyingObject != null && occupyingObject.GetStatis())
        {
            occupyingObject = null;
            return false;
        }
        return true;
    }

    private void OnValidate()
    {
        totalSpawnWeight = 0f;
        foreach (var spawnable in _SpawnList)
        {
            totalSpawnWeight += spawnable.objectWeight;
        }
    }

    private void PrepSpawn()
    {
        if (_SpawnList == null)
        {
            Debug.LogError("Spawn list is null. Please fill the list.");
        }

        // Generate a random position in the list.
        float pick = Random.value * totalSpawnWeight;
        int index = 0;
        float cumulativeWeight = _SpawnList[0].objectWeight;

        // Step through the list until we've accumulated more weight than this.
        while (pick > cumulativeWeight && index < _SpawnList.Length - 1)
        {
            index++;
            cumulativeWeight += _SpawnList[index].objectWeight;
        }

        prefab = _SpawnList[index].gameItem;

        _RebuildingParticle = _SpawnList[index].objectSpawnParticle;

        beginSpawnPrep = false;

    }

    private void SpawnItem()
    {
        //  Grab the item from the pool and spawn it
        spawn = prefab.GetPooledInstance<Item>();

        occupyingObject = spawn;

        spawn.transform.rotation = Quaternion.Euler(0, _ObjectAngles[Random.Range(0, _ObjectAngles.Count)], 0);

        spawn.transform.localPosition = transform.position + spawnOffset;

        beginSpawnPrep = true;

    }
}