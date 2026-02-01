using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SizePickup : NetworkBehaviour
{
    public float spinSpeed;
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
            playerController.SizeUp();
            GetComponent<NetworkObject>().Despawn();
            Destroy(gameObject);
        }


    }
}
