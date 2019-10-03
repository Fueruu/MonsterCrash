using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawnerPositions : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawnLocation = new List<GameObject>();

    [SerializeField] private ObjectSpawner spawnerPrefab;



    private void Awake()
    {
        for (int i = 0; i < spawnLocation.Count; i++)
        {
            CreateSpawner(i);
        }
    }

    void CreateSpawner(int index)
    {
        ObjectSpawner spawner = Instantiate<ObjectSpawner>(spawnerPrefab);

        spawner.transform.position = spawnLocation[index].transform.position;


    }

}