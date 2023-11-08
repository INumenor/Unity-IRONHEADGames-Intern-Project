using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using Unity.VisualScripting;

public class BallOwner : NetworkBehaviour
{
   public NetworkObject ball;
    [Networked(OnChanged = nameof(TransferOwner))] public PlayerRef ownerPlayer { get; set; }

    public void Update()
    {
        Debug.Log("inputtttttttttttttttt" + ball.HasStateAuthority);
       
    }
    public void BallGrabbed()
    {
      
        Rpc_BallOwner(Runner.LocalPlayer);
        TransferOwner();
        
    }
    public void BallUngrabbed()
    {
       // ball.ReleaseStateAuthority();
        Rpc_BallOwner(-1);
    }
    public static void TransferOwner(Changed<BallOwner> changed)
    {
        changed.Behaviour.TransferOwner();
    }
    private void TransferOwner()
    {
        
        ball.RequestStateAuthority();
    }
    
    [Rpc]
    public void Rpc_BallOwner(PlayerRef ownerplayer, RpcInfo info = default)
    {
        this.ownerPlayer = ownerPlayer;

    }
  
}

