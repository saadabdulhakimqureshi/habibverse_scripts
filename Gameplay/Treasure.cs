/// <summary>
/// Treasure - Treasure Hunt Collectible
/// 
/// Treasure collectible in treasure hunt mode with scoring value.
/// Players must locate and collect treasures to gain points.
/// 
/// Features:
/// - Hint system for treasure location
/// - Collection and scoring logic
/// - Treasure information display
/// - Game mode integration
/// </summary>

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Treasure : MonoBehaviour
{

    public string hint;
    public string information;

    public bool host;

    // Start is called before the first frame update
    public float rotationSpeed = 10f;

    void Update()
    {
        // Rotate the object around its own y-axis
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            if (HabibVerseTreasureHuntManager.Instance != null)
            {
                if (other.GetComponent<PlayerController>().NextClue())
                {
                    //gameObject.SetActive(false);
                }
            }
        }
    }
}
