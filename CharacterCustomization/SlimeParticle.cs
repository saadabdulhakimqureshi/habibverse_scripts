using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SlimeParticle : MonoBehaviour
{
    public List<GameObject> particlesList;
    public Transform particleSpawnTransform;
    public GameObject currentParticles;
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeParticles(int index)
    {
        if (currentParticles != null)
        {
            Destroy(currentParticles);
        }
        currentParticles = Instantiate(particlesList[index], particleSpawnTransform);
        Vector3 localPosition = currentParticles.transform.localPosition;
        currentParticles.transform.parent = particleSpawnTransform;
        currentParticles.transform.localPosition = localPosition;
    }
}
