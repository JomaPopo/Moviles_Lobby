using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkObject))]


public class PlayerLobby : NetworkBehaviour
{
    public NetworkVariable<bool> isReady = new NetworkVariable<bool>(
        false,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    public void ToggleReady()
    {
        if (IsOwner)
        {
            SetReadyServerRpc(!isReady.Value);
        }
    }

    [ServerRpc]
    private void SetReadyServerRpc(bool ready, ServerRpcParams rpcParams = default)
    {
        isReady.Value = ready;
        if (LobbyManager.Instance != null)
        {
            LobbyManager.Instance.CheckIfAllReady();
        }
    }
}
