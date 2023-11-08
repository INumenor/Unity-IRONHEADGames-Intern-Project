using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerData : NetworkBehaviour
{
    [Networked(OnChanged = nameof(ChangedPlayerName))] public NetworkString<_32> playerName { get; set; }  = null;

    [Networked(OnChanged = nameof(ChangedColorChange))] public Color bodyColor { get; set; }

    [SerializeField] private InputActionReference _triggerPress;

    [SerializeField] GameObject PlayerBody;

    public TMP_Text PlayerNametext;

    public static void ChangedPlayerName(Changed<PlayerData> changed)
    {
        changed.Behaviour.ChangedPlayerName();
    }
    private void ChangedPlayerName()
    {
        PlayerNametext.text = playerName.ToString();

    }

    private static void ChangedColorChange(Changed<PlayerData> changed)
    {
        changed.Behaviour.PlayerBody.GetComponent<MeshRenderer>().material.color = changed.Behaviour.bodyColor;
    }
    void Update()
    {
        if (_triggerPress.action.ReadValue<float>() > 0.9)
        {
            bodyColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
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
