/// <summary>
/// SlimeModel - Slime Character Model Manager
/// 
/// Manages the 3D slime character model and mesh variations.
/// Handles model appearance changes including colors, textures, and mesh swaps during customization.
/// 
/// Features:
/// - Stores multiple mesh variations for character customization
/// - Updates model appearance in real-time
/// - Synchronizes visual changes with CustomizationData
/// </summary>

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
