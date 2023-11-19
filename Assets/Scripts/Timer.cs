using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class Timer : NetworkBehaviour
{
    [Networked] private TickTimer life { get; set; }

    public void Start()
    {
        Init();
    }
    public void Init()
    {
       
        life = TickTimer.CreateFromSeconds(Runner, 5.0f);
        Debug.Log(life+ "life");
    }

    public override void FixedUpdateNetwork()
    {


        if (life.Expired(Runner))
        {
            
            Debug.Log(life + " life ifin içindeki");
            //Runner.Despawn(Object);
        }

        else
            Debug.Log(life.RemainingTime(Runner));
        
    }
    public override void Spawned()
    {

       // Rpc_SetPlayerName(PlayerPrefs.GetString("PlayerName"));
        base.Spawned();
    }
}
