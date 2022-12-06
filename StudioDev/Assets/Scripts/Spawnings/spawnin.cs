using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnin : MonoBehaviour
{
    public GameObject ghost;

    [SerializeField] private Transform[] spawnpoints;
    // Start is called before the first frame update
    void Start()
    {
        Transform randomSelectedSpawn = spawnpoints[Random.Range(0, spawnpoints.Length)];
        GameObject ghostClone = Instantiate(ghost, randomSelectedSpawn.transform.position, randomSelectedSpawn.rotation);
        ghostClone.transform.parent = GameObject.Find("Shadows").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
