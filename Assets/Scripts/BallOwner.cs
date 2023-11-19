using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Threading.Tasks;



public class BallOwner : NetworkBehaviour
{
   public NetworkObject player;
   // [SerializeField] Transform holdTransform;
    [Networked(OnChanged = nameof(TransferOwner))] public PlayerRef currentHolder { get; set; }

    public void Update()
    {
        Debug.Log("inputtttttttttttttttt" + player.HasStateAuthority);
        Debug.Log(player.StateAuthority+" runnerrrrrrr locaaaal playerrr "+Runner.LocalPlayer);
       
    }
    public void BallGrabbed()
    {
        
        TransferOwner();
        
    }
    //public void BallUngrabbed()
    //{

    //  //  ball.RemoveInputAuthority();
      
    //}
    public static void TransferOwner(Changed<BallOwner> changed)
    {
        changed.Behaviour.TransferOwner();
    }
    private void TransferOwner()
    {
      
       player.RequestStateAuthority();
        if (player.HasStateAuthority)
        {
            
            Debug.Log(player.StateAuthority);
        }
       
       
    }
    
    //[Rpc(RpcSources.All, RpcTargets.All)]
    //public void Rpc_BallOwner(PlayerRef ownerplayer, RpcInfo info = default)
    //{
    //    Debug.Log(ownerPlayer+" prcccccc owner");
    //    this.ownerPlayer = ownerPlayer;
    //    // ball.AssignInputAuthority(ownerPlayer);
    //    ball.RequestStateAuthority();
    //    if (ownerPlayer==PlayerRef.None)
    //    {
    //        Debug.Log("Remove");
    //        ball.RemoveInputAuthority();

    //    }
    //    else
    //    {
    //        Debug.Log("assign");
    //        ball.AssignInputAuthority(ownerplayer);
    //    }

    //}
  
}

