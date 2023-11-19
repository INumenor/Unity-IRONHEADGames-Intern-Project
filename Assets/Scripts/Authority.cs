using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.Rendering.UI;
using Photon.Realtime;

public class Authority : NetworkBehaviour
{

    //[SerializeField] private bool isInteractable = true;
    //public bool IsInteractable => isInteractable;
    [Networked(OnChanged =nameof(GetHolderCharachter))] public PlayerRef currentHolder { get; set; }
    public bool isGrabbed => Object != null && holdingTransform != null;
    [SerializeField] Transform holdingTransform;
    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) return;


        if (!isGrabbed) return;
               
        
        transform.position = holdingTransform.position;
    }
    public static void GetHolderCharachter(Changed<Authority> changed)
    {
        changed.Behaviour.holdingTransform = changed.Behaviour.FindHolderCharachter(changed.Behaviour.currentHolder);
    }
    public async void TakeAuthority(PlayerRef playerRef)
    {
        await Object.WaitforStateAuthority();
        Debug.Log(Object.InputAuthority);
        if (Object.HasStateAuthority)
        {
            currentHolder = playerRef;
        }
    }
    public void grab()
    {
        Interact(holdingTransform,currentHolder);
    }
    public void Interact(Transform targetpoint, PlayerRef playerRef)
    {
        TakeAuthority(playerRef);
    }
    public Transform FindHolderCharachter(PlayerRef playerRef)
    {
        Debug.Log(playerRef);
        
         return NetworkManager.Instance.SessionRunner.GetPlayerObject(playerRef).transform;
        Debug.Log(playerRef);
    }

}