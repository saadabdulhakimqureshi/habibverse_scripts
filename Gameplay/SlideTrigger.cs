/// <summary>
/// SlideTrigger - Slide Movement Mechanic
/// 
/// Trigger zone that applies sliding effect to players who enter.
/// Provides momentum-based slide movement for gameplay navigation.
/// 
/// Features:
/// - Trigger detection
/// - Slide velocity application
/// - Collision-based activation
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.EnableSlide();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.DisableSlide();
        }
    }
}
