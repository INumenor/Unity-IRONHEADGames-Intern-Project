using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoorOpenClose : NetworkBehaviour
{
    [SerializeField] public Animator animator;

    [Networked(OnChanged = nameof(DoorOpenClose))] public NetworkBool isOpenDoor { get; set; }
    static bool isOpenCloseDoor = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //animator.SetTrigger("isDoorOpen");
            isOpenDoor = true;
            Rpc_SetOpenDoor(isOpenDoor);
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //animator.SetTrigger("isDoorClose");
            isOpenDoor = false;
            Rpc_SetOpenDoor(isOpenDoor);
        }
    }

    public static void DoorOpenClose(Changed<PlayerDoorOpenClose> changed)
    {
        changed.Behaviour.DoorOpenClose(isOpenCloseDoor);
    }

    private void DoorOpenClose(bool isOpen)
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

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void Rpc_SetOpenDoor(bool isOpenDoor, RpcInfo info = default)
    {
        this.isOpenDoor = isOpenDoor;
    }
}
