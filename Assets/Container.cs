using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    private Player_Items playerItems; // Reference to the Player_Items script
    [SerializeField] private GameObject integerPrefab;
    [SerializeField] private GameObject output;
    [SerializeField] private Portal_In InPortal;
    private int storedValue = -1; // To store the value in the container
    private GameObject currentInstance;
    private bool canTeleport = true;

    private void Start()
    {
        // Automatically find the Player_Items component on the player object
        playerItems = FindObjectOfType<Player_Items>();  // Finds the first instance of Player_Items in the scene

        // If needed, you can add a check to ensure playerItems was found
        if (playerItems == null)
        {
            Debug.LogError("Player_Items component not found in the scene!");
        }

        // Initialize stored value if needed
        storedValue = -1;
    }

    public void ReceiveValue(int value)
    {
        storedValue = value;
        StoreInteger(storedValue);
    }

    private IEnumerator Teleport(float delay)
    {
        //Check in case of multiple teleports
        if (!canTeleport) yield break;
        canTeleport = false;
        Debug.Log("TELELELELELELEPORTING");
        yield return new WaitForSeconds(delay);
        InPortal.TeleportValue(storedValue);

        // Destroy the prefab (this object) after passing the value
        Destroy(currentInstance);  // Destroys the current object (the prefab)
        storedValue = -1;
        Debug.Log("Prefab destroyed.");
        canTeleport = true;
    }

    // Function to store an integer in the container
    private void StoreInteger(int value)
    {
        storedValue = value;

        // Put prefab over sprite renderer
        Vector3 containerPosition = transform.position;
        currentInstance = Instantiate(integerPrefab, containerPosition, Quaternion.identity);
        Integer integerComponent = currentInstance.GetComponent<Integer>();
        if (integerComponent != null)
        {
            integerComponent.SetValue(value);
        }
        // If linked to a Portal_In, initiate teleportation
        if (InPortal != null)
        {
            Debug.Log("TESTS");
            StartCoroutine(Teleport(0.5f));
            return;
        }

        StartCoroutine(PassValueToOutput(1f));
    }

    //Pass value to whatever the output entity is
    private IEnumerator PassValueToOutput(float delayTime)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(delayTime);

        //If no output, this is the end, leave the integer here
        if (output == null)
        {
            yield break;
        }

        //Theres somewhere for this to go
        if (output != null)
        {
            // Get the generic Ireceive interface
            IReceive receiveScript = output.GetComponent<IReceive>();
            if (receiveScript != null)
            {
                receiveScript.Receive_Input(storedValue);
                Debug.Log("Value passed to output: " + storedValue);
            }
            else
            {
                Debug.LogError("Output entity doesn't have OutputEntity script attached.");
            }
        }
        else
        {
            Debug.LogError("Output object is not assigned!");
        }

        // Destroy the prefab (this object) after passing the value
        Destroy(currentInstance);  // Destroys the current object (the prefab)
        storedValue = -1;
        Debug.Log("Prefab destroyed.");
    }
}
