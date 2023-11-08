using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class BallOwner : NetworkBehaviour
{
   
    [Networked(OnChanged = nameof(TransferOwner))] public PlayerRef ownerPlayer { get; set; }

    public void BallGrabbed()
    {
        Debug.Log(Runner.LocalPlayer);
        Rpc_BallOwner(Runner.LocalPlayer);
    }
    public void BallUngrabbed()
    {
        ownerPlayer = -1;
    }
    public static void TransferOwner(Changed<BallOwner> changed)
    {
        changed.Behaviour.TransferOwner();
    }
    private void TransferOwner()
    {
     
    }
    
    [Rpc]
    public void Rpc_BallOwner(PlayerRef ownerplayer, RpcInfo info = default)
    {
        this.ownerPlayer = ownerPlayer;
    }
}
