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
   // [Networked(OnChanged =nameof(GetHolderCharachter))] public PlayerRef currentHolder { get; set; }
    public bool isGrabbed=false;
    string LeftAndRightHand = null;
    [SerializeField] Transform LeftholdingTransform;
    [SerializeField] Transform RightholdingTransform;
    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) return;

        if (!isGrabbed) return;
               
        if(LeftAndRightHand == "Left")
        {
            transform.position = LeftholdingTransform.position;
        }
        else if(LeftAndRightHand == "Left")
        {
            transform.position = RightholdingTransform.position;
        }
       
    }
    //public static void GetHolderCharachter(Changed<Authority> changed)
    //{
    //    changed.Behaviour.holdingTransform = changed.Behaviour.FindHolderCharachter(changed.Behaviour.currentHolder);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHand"))
        {
            LeftAndRightHand = "Left";
        }
        else if(other.CompareTag("RightHand"))
        {
            LeftAndRightHand = "Right";
        }
    }
    public async void TakeAuthority()
    {
        await Object.WaitForStateAuthority();
        isGrabbed = true;
        this.Object.GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log(Object.InputAuthority);
        //if (Object.HasStateAuthority)
        //{
        //    currentHolder = Runner.LocalPlayer;
        //}
    }
    [ContextMenu("Grab")]
    public void grab()
    {
        Interact();
    }
    public void Ungrabbed()
    {
        isGrabbed = false;
        this.Object.GetComponent<Rigidbody>().isKinematic = false;
        LeftAndRightHand = null;
    }
    public void Interact()
    {
        TakeAuthority();
    }
    //public Transform FindHolderCharachter(PlayerRef playerRef)
    //{
    //    Debug.Log(playerRef);
        
    //     return NetworkManager.Instance.SessionRunner.GetPlayerObject(playerRef).transform;
    //    Debug.Log(playerRef);
    //}

}