using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;



public class Authority : NetworkBehaviour
{
   
public async void Grab()
    {
        await Object.WaitforStateAuthority();
        
    }
    public void Grabbing()
    {
        Grab();
    }
    public override void FixedUpdateNetwork()
    {
        // We only update the object position if we have the state authority
        if (!Object.HasStateAuthority) return;

        //if (!IsGrabbed) return;
        // Follow grabber, adding position/rotation offsets
        //Follow(followingtransform: transform, followedTransform: CurrentGrabber.transform, LocalPositionOffset, LocalRotationOffset);
    }
    //void Follow(Transform followingtransform, Transform followedTransform, Vector3 localPositionOffsetToFollowed, Quaternion localRotationOffsetTofollowed)
    //{
    //    followingtransform.position = followedTransform.TransformPoint(localPositionOffsetToFollowed);
    //    followingtransform.rotation = followedTransform.rotation * localRotationOffsetTofollowed;
    //}
}