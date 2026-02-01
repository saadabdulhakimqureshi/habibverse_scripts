/// <summary>
/// BulletPickup - Ammunition Pickup Item
/// 
/// Collectible item that replenishes player ammunition when picked up.
/// Network-synchronized pickup with visual effects.
/// 
/// Features:
/// - Spinning visual effect
/// - Pickup detection and collection
/// - Ammunition refill on collection
/// - Network synchronization
/// - Collection effects/feedback
/// 
/// Dependencies: Netcode
/// </summary>

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletPickup : NetworkBehaviour
{
    public float spinSpeed;

    public GameObject PickupEffect;
    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        //GetComponent<NetworkObject>().Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.ammo += 1;
            playerController.PlayerGameManager.Ammo = playerController.ammo;
            gameObject.SetActive(false);

            GameObject effect = Instantiate(PickupEffect, playerController.transform.position, Quaternion.identity);



        }
    }


}
