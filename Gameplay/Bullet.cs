using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bullet : NetworkBehaviour
{

    [Header("Bullet Physics")]
    public float speed;
    public float spinSpeed;
    public string shooter;
    public Vector3 position;
    public Vector3 direction;
    public float bulletForce;
    public Rigidbody rb;

    [Header("Bullet Trail")]
    public float AutoDestroyTime = 5f;

    public BulletTrailScriptableObject TrailConfig;
    public TrailRenderer Trail;

    private bool IsDisabling = false;

    protected const string DISABLE_METHOD_NAME = "Disable";
    protected const string DO_DISABLE_METHOD_NAME = "DoDisable";
    // Start is called before the first frame update
    void Start()
    {
        Trail = GetComponent<TrailRenderer>();
        IsDisabling = false;
        CancelInvoke(DISABLE_METHOD_NAME);
        ConfigureTrail();
        Invoke(DISABLE_METHOD_NAME, AutoDestroyTime);
        rb = GetComponent<Rigidbody>();
    }

    private void ConfigureTrail()
    {
        if (Trail != null && TrailConfig != null)
        {
            TrailConfig.SetupTrail(Trail);
        }
    }

    protected void Disable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        CancelInvoke(DO_DISABLE_METHOD_NAME);
        rb.velocity = Vector3.zero;
        if (Trail != null)
        {
            Trail.enabled = false;
        }


        if (Trail != null && TrailConfig != null)
        {
            IsDisabling = true;
            Invoke(DO_DISABLE_METHOD_NAME, TrailConfig.Time);
        }
        else
        {
            DoDisable();
        }
    }

    protected void DoDisable()
    {
        if (Trail != null && TrailConfig != null)
        {
            Trail.Clear();
        }

        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
        rb.velocity = direction * speed;
    }

    public void Setup(Vector3 shootPosition, Vector3 direction, string shooter)
    {
        
        transform.LookAt(shootPosition); // Normalize the direction vector

        this.position = shootPosition;
        this.direction = direction;
        this.shooter = shooter;
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        if (playerController != null )
        {
            playerController.Fall(transform.forward * bulletForce);
        }

        GetComponent<NetworkObject>().Despawn();
        Disable();   
    }
}