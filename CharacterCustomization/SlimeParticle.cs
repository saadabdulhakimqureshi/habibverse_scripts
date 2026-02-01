/// <summary>
/// SlimeParticle - Particle Effect Manager
/// 
/// Manages particle effects for the slime character including color and type variations.
/// Spawns and controls particle systems based on customization choices.
/// 
/// Features:
/// - Particle effect variations
/// - Particle spawning and positioning
/// - Color customization support
/// </summary>

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
