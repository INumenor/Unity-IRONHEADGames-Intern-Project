using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class PlayerData : NetworkBehaviour
{
    [Networked(OnChanged = nameof(ChangedPlayerName))] public NetworkString<_32> playerName { get; set; }  = null;

    public TMP_Text PlayerNametext;

    public static void ChangedPlayerName(Changed<PlayerData> changed)
    {
        changed.Behaviour.ChangedPlayerName();
    }
    private void ChangedPlayerName()
    {
        PlayerNametext.text = playerName.ToString();

    }
    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void Rpc_SetPlayerName(string PlayerName, RpcInfo info = default)
    {
        this.playerName = PlayerName;
    }
    public override void Spawned()
    {

        Rpc_SetPlayerName(PlayerPrefs.GetString("PlayerName"));
        base.Spawned();
    }
}
