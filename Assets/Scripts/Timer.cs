using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
using Unity.VisualScripting;

public class Timer : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI timertext;
    [SerializeField] float RemainingTime;
    string time;
    [Networked(OnChanged = nameof(ChangedColorChange))] public float networktime { get; set; } 
    
    private static void ChangedColorChange(Changed<Timer> changed)
    {
        changed.Behaviour.timertext.text =changed.Behaviour.time;
    }
    private void ChangedColorChange()
    {

        networktime = RemainingTime;
    }
    void Update()
    {
        
        RemainingTime -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(RemainingTime / 60);
        int seconds = Mathf.FloorToInt(RemainingTime % 60);
        time = string.Format("{0:00}:{1:00}", minutes, seconds);
        timertext.text = time;
    }

    public override void Spawned()
    {
        base.Spawned();
    }
    //  [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    //public void timerchanged( RpcInfo info = default)
    //  {

    //  }
}
