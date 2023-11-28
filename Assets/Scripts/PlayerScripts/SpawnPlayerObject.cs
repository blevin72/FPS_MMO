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


//public class SpawnPlayerObject : MonoBehaviour
//{
//    public GameObject playerPrefab;
//    public GameObject connectionToClient;

//    void Start()
//    {
//        if (playerPrefab)
//        {
//            // Spawn the local player on the server
//            CmdSpawnPlayer(); 
//        }
//        else
//        {
//            Debug.LogError("Player prefab is not assigned!");
//        }
//    }

//    [Command]
//    void CmdSpawnPlayer()
//    {
//        // Instantiate and spawn the player prefab on the server
//        GameObject spawnedPlayer = Instantiate(playerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);

//        // Get the NetworkConnectionToClient of the local player
//        NetworkConnectionToClient conn = connectionToClient;

//        // Attach the player object to the local player
//        NetworkServer.AddPlayerForConnection(conn, spawnedPlayer);
//    }
//}
