
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using System;
using Unity.Collections;

public struct CustomizationData : INetworkSerializable, IEquatable<CustomizationData>
{
    public FixedString64Bytes playerName;
    public ulong clientId;
    public Color modelColor;
    public Color particlesColor;
    public int modelIndex;
    public int hatIndex;
    public int faceIndex;
    public int auraIndex;

    // Start is called before the first frame update
    public CustomizationData(ulong clientId, Color modelColor, Color particlesColor, int modelIndex, int hatIndex, int faceIndex, int auraIndex) :this()
    {
        
        this.playerName = "";
        this.modelIndex = modelIndex;
        this.clientId = clientId;
        this.modelColor = modelColor;
        this.particlesColor = particlesColor;
        this.hatIndex = hatIndex;   
        this.faceIndex = faceIndex;
        this.auraIndex = auraIndex;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        // // Make sure to ref else these variables will not be synchronized across the network clients.
        serializer.SerializeValue(ref playerName);
        serializer.SerializeValue(ref clientId);
        serializer.SerializeValue(ref modelIndex);
        serializer.SerializeValue(ref modelColor);
        serializer.SerializeValue(ref particlesColor);
        serializer.SerializeValue(ref hatIndex);
        serializer.SerializeValue(ref faceIndex);
        serializer.SerializeValue(ref auraIndex);
    }
    public bool Equals(CustomizationData other)
    {
        return this.playerName == other.playerName && other.modelIndex == this.modelIndex && other.hatIndex == this.hatIndex && other.faceIndex == this.faceIndex && other.auraIndex == this.auraIndex
            && other.modelColor == this.modelColor && other.particlesColor == this.particlesColor;
    }

    
}
