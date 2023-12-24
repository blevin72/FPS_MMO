using Mirror;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(CharacterController))] // Add required CharacterController component
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemoteLayer";

    [SerializeField]
    string dontDrawLayerName = "DontDraw";

    [SerializeField]
    GameObject playerGraphics;

    Camera sceneCamera;
    CharacterController characterController; // Reference to CharacterController component

    private void Start()
    {
        characterController = GetComponent<CharacterController>(); // Get CharacterController component

        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemoteLayer();
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }

            // Disable player graphics for local player
            // Since the camera is FPS, if the player is wearing a helmet (example), you don't want to see the helmet over the player's head,
            // but still want the other players to be able to see the helmet
            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));
        }

        GetComponent<Player>().Setup();
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();
        GameManager.RegisterPlayer(_netID, _player);
    }

    // Made adjustment here
    private void AssignRemoteLayer()
    {
        int layerNumber = LayerMask.NameToLayer(remoteLayerName);

        // Check if the layer number is within the valid range (0 to 31)
        if (layerNumber >= 0 && layerNumber <= 31)
        {
            gameObject.layer = layerNumber;
        }
        else
        {
            Debug.LogError("Invalid layer number for remote layer: " + remoteLayerName);
        }
    }

    private void DisableComponents()
    {
        foreach (var component in componentsToDisable)
        {
            component.enabled = false;
        }

        // Disable CharacterController component
        if (characterController != null)
        {
            characterController.enabled = false;
        }
    }

    private void OnDisable()
    {
        if (sceneCamera != null)
        {
            sceneCamera.gameObject.SetActive(true);
        }

        GameManager.UnRegisterPlayer(transform.name);
    }
}



















//using Mirror;
//using UnityEngine;

//[RequireComponent(typeof(Player))]
//public class PlayerSetup : NetworkBehaviour
//{
//    [SerializeField]
//    Behaviour[] componetsToDisable;

//    [SerializeField]
//    string remoteLayerName = "RemoteLayer";

//    [SerializeField]
//    string dontDrawLayerName = "DontDraw";

//    [SerializeField]
//    GameObject playerGraphics;

//    Camera sceneCamera;

//    private void Start()
//    {
//        if (!isLocalPlayer)
//        {
//            DisableComponents();
//            AssignRemoteLayer();
//        }
//        else
//        {
//            sceneCamera = Camera.main;
//            if (sceneCamera != null)
//            {
//                sceneCamera.gameObject.SetActive(false);
//            }

//            //Disable player graphics for local player
//            /*Since the camera is FPS if the player is wearing a helmet (example) you dont want to see the helmet over the player's head, but
//             still want the other players to be able to see the helmet */
//            SetLayerRecursively(playerGraphics, LayerMask.NameToLayer(dontDrawLayerName));
//        }

//        GetComponent<Player>().Setup();
//    }

//    void SetLayerRecursively(GameObject obj, int newLayer)
//    {
//        obj.layer = newLayer;
//        foreach (Transform child in obj.transform)
//        {
//            SetLayerRecursively(child.gameObject, newLayer);
//        }
//    }

//    public override void OnStartClient()
//    {
//        base.OnStartClient();

//        string _netID = GetComponent<NetworkIdentity>().netId.ToString();
//        Player _player = GetComponent<Player>();
//        GameManager.RegisterPlayer(_netID, _player);
//    }

//    //made adjustment here
//    private void AssignRemoteLayer()
//    {
//        int layerNumber = LayerMask.NameToLayer(remoteLayerName);

//        // Check if the layer number is within the valid range (0 to 31)
//        if (layerNumber >= 0 && layerNumber <= 31)
//        {
//            gameObject.layer = layerNumber;
//        }
//        else
//        {
//            Debug.LogError("Invalid layer number for remote layer: " + remoteLayerName);
//        }
//    }

//    private void DisableComponents()
//    {
//        for (int i = 0; i < componetsToDisable.Length; i++)
//        {
//            componetsToDisable[i].enabled = false;
//        }
//    }

//    private void OnDisable()
//    {
//        if (sceneCamera != null)
//        {
//            sceneCamera.gameObject.SetActive(true);
//        }

//        GameManager.UnRegisterPlayer(transform.name);
//    }
//}
