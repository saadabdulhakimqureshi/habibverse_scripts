using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class SlimeModel : MonoBehaviour
{
    public List<Mesh> meshes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetModel(int index)
    {
        Transform modelTransform = transform.Find("Model");
        SkinnedMeshRenderer sMR = modelTransform.GetComponent<SkinnedMeshRenderer>();
        sMR.sharedMesh = meshes[index];
        
    }
}
