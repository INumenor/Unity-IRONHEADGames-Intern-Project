using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class PlayerMovement : NetworkBehaviour
{

    public float MoveSpeed;

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if(GetInput<PlayerInputData>(out var inputData))
        {
            transform.Translate(inputData.Direction*Runner.DeltaTime* MoveSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
