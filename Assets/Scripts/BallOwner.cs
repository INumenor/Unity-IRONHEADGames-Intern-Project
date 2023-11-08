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
        Debug.Log(ball.InputAuthority+" runnerrrrrrr locaaaal playerrr "+Runner.LocalPlayer);
       
    }
    public void BallGrabbed()
    {
        ownerPlayer = Runner.LocalPlayer;
        Rpc_BallOwner(ownerPlayer);
        //TransferOwner();
        
    }
    public void BallUngrabbed()
    {

      //  ball.RemoveInputAuthority();
        ownerPlayer = -1;
        Rpc_BallOwner(ownerPlayer);
       
    }
    public static void TransferOwner(Changed<BallOwner> changed)
    {
        changed.Behaviour.TransferOwner();
    }
    private void TransferOwner()
    {
      
        Debug.Log(ownerPlayer+" transferrrowneerrr");
        //ball.RequestStateAuthority();

       
       
    }
    
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void Rpc_BallOwner(PlayerRef ownerplayer, RpcInfo info = default)
    {
        Debug.Log(ownerPlayer+" prcccccc owner");
        this.ownerPlayer = ownerPlayer;
        // ball.AssignInputAuthority(ownerPlayer);
        ball.RequestStateAuthority();
        if (ownerPlayer==PlayerRef.None)
        {
            Debug.Log("Remove");
            ball.RemoveInputAuthority();

        }
        else
        {
            Debug.Log("assign");
            ball.AssignInputAuthority(ownerplayer);
        }

    }
  
}

