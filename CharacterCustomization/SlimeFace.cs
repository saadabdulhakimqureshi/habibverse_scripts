/// <summary>
/// SlimeFace - Slime Face Material Manager
/// 
/// Manages facial expressions and materials for the slime character.
/// Allows selection between different face textures/materials during customization.
/// 
/// Features:
/// - Face material variations
/// - Face selection interface
/// - Material application to slime model
/// </summary>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SlimeFace : MonoBehaviour
{

    public List<Material> faces;

    public int index;



    void Start()
    {
        /*        int index = RandomNumberGenerator.GetInt32(faces.Count);
                Transform modelTransform = transform.Find("Model");
                SkinnedMeshRenderer sMR = modelTransform.GetComponent<SkinnedMeshRenderer>();

                Material[] mats = sMR.materials;
                Material mat = faces[index];
                mats[1] = mat;
                sMR.materials = mats;*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeFace(int index)
    {
        this.index = index;
        Transform modelTransform = transform.Find("Model");
        SkinnedMeshRenderer sMR = modelTransform.GetComponent<SkinnedMeshRenderer>();

        Material[] mats = sMR.materials;
        Material mat = faces[index];
        mats[1] = mat;
        sMR.materials = mats;
    }


}
