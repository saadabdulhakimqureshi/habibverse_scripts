/// <summary>
/// OwnerNetworkAnimator - Client-Owned Animation Sync
/// 
/// Network animator for client-owned animations.
/// Extends NetworkAnimator for owner-authoritative animation synchronization.
/// 
/// Features:
/// - Owner-side animation authority
/// - Client animation synchronization
/// - Network animator integration
/// </summary>

using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class OwnerNetworkAnimator : NetworkAnimator
{
    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
