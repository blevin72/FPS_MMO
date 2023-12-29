using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnPlayerObject : NetworkBehaviour
{
    public GameObject playerPrefab;

    [Command]
    void CmdSpawnPlayer()
    {
        // Instantiate and spawn the player prefab on the server
        GameObject spawnedPlayer = Instantiate(playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);

        // Spawn the player object on the server and associate it with the client connection
        NetworkServer.AddPlayerForConnection(connectionToClient, spawnedPlayer);
    }

    [ClientRpc]
    void RpcSpawnPlayer()
    {
        // This method will be called on all clients, including the client that requested the spawn
        // You can perform any client-specific setup here
    }

    void Start()
    {
        if (isLocalPlayer)
        {
            // Spawn the local player on the server
            CmdSpawnPlayer();
        }
    }
}
