using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCameraTransitionManager : MonoBehaviour
{
    public static LobbyCameraTransitionManager instance;
    public CinemachineVirtualCamera ServerCamera;
    public List<CinemachineVirtualCamera> Cameras;
    public float TransitionTime;

    [SerializeField] int camerasCount;
    [SerializeField] bool cameraChanging;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        camerasCount = 0;
        cameraChanging = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraChanging)
        {
            if (camerasCount == Cameras.Count)
            {
                camerasCount = 0;
            }
            StartCoroutine(ChangeCamera(camerasCount));
            camerasCount++;
        }
    }

    public void EnableTransitions()
    {
        ServerCamera.Priority = 0;
    }

    public void DisableTransitions()
    {
        ServerCamera.Priority = 2;
    }

    IEnumerator ChangeCamera(int index)
    {
        cameraChanging = false;
        Cameras[index].Priority = 1;
        // Smoothly wait for a short duration (you can adjust the duration)
        yield return new WaitForSeconds(TransitionTime);
        Cameras[index].Priority = 0;
        cameraChanging = true;
    }
}
