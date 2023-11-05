using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorOpenClose : NetworkBehaviour
{
    [SerializeField] public Animator animator;

    [Networked] public NetworkBool isOpenDoor { get; set; }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //animator.SetTrigger("isDoorOpen");
            isOpenDoor = true;
            Rpc_DoorOpenClose(isOpenDoor);
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //animator.SetTrigger("isDoorClose");
            isOpenDoor = false;
            Rpc_DoorOpenClose(isOpenDoor);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void Rpc_DoorOpenClose(NetworkBool isOpen, RpcInfo info = default)
    {
        if (isOpenDoor)
        {
            this.animator.SetTrigger("isDoorOpen");
        }
        else
        {
            this.animator.SetTrigger("isDoorClose");
        }
    }
}
