/// <summary>
/// TrampolineTrigger - Jump Boost Mechanic
/// 
/// Trigger zone that bounces/launches players upward for platform navigation.
/// Provides vertical momentum for reaching higher areas.
/// 
/// Features:
/// - Trigger detection
/// - Jump force application
/// - Vertical momentum boost
/// - Gameplay platform element
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineTrigger : MonoBehaviour
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
            playerController.EnableSuperJump();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.DisableSuperJump();
        }
    }
}
