using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignPlayerToEnemy : MonoBehaviour
{
    public GameObject fpsc;
    private GameObject fpscInstance; // Reference to the instantiated player

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Method to assign the spawnable player to the enemy
        void AssignPlayer()
        {
            if (fpsc != null)
            {
                fpscInstance = Instantiate(fpsc, transform.position, Quaternion.identity);
                // Assign the spawned player to the enemy or perform other logic
                // For example, you might want to parent the player under the enemy:
                fpscInstance.transform.parent = transform;
            }
            else
            {
                Debug.LogError("Player prefab is not assigned!");
            }
        }


    }


}
