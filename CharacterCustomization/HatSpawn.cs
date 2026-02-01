using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.Netcode;
using UnityEngine;


public class HatSpawn : MonoBehaviour
{
    public List<Transform> hats;
    public Transform HatSpawnTransform; 
    public Transform currentHat;
    public int index;


    void Start()
    {
        

        
        // SpawnHat(index);
    }

    // Update is called once per frame
    void Update()
    {

    }



    
    public void SpawnHat(int index)
    {
        if (currentHat != null)
        {
            Destroy(currentHat.gameObject);
        }
        this.index = index;
        currentHat = Instantiate(hats[index], HatSpawnTransform);
        Vector3 localPosition = currentHat.localPosition;
        currentHat.parent = HatSpawnTransform;
        currentHat.localPosition = localPosition;
    }
}
