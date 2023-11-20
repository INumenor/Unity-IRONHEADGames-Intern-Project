using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Threading.Tasks;

public static class SharedAuthorityExtensions
{
    /**
        * Request state authority and wait for it to be received
        * Relevant in shared topology only
        */
    public static async Task<bool> WaitForStateAuthority(this NetworkObject o, float maxWaitTime = 8)
    {
        float waitStartTime = Time.time;
        o.RequestStateAuthority();
        while (!o.HasStateAuthority && (Time.time - waitStartTime) < maxWaitTime)
        {
            await System.Threading.Tasks.Task.Delay(1);
        }
        return o.HasStateAuthority;
    }
}
