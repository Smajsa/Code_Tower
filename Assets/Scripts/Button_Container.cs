using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Container : MonoBehaviour
{
    private Player_Items playerItems; // Reference to the Player_Items script
    [SerializeField] private GameObject integerPrefab;
    [SerializeField] private GameObject output;//Put the recieving entity here
    private int storedValue = -1; // To store the value in the container
    private bool inRange = false;
    private bool checkReady = false;
    private GameObject currentInstance;
    Integer integerComponent;

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

    // Update is called once per frame
    void Update()
    {
        // Check for player input (e.g., pressing "Z")
        if (Input.GetKeyDown(KeyCode.Z) && inRange)
        {
            StoreIntegerInContainer();
        }
    }

    void OnTriggerEnter2D()
    {
        inRange = true;
    }

    void OnTriggerExit2D()
    {
        inRange = false;
    }

    // Function to store an integer in the container
    private void StoreIntegerInContainer()
    {
        // Get the next integer from the player's inventory
        int storedValue = playerItems.GetNextInteger();

        // If the value is valid, store it in the container
        if (storedValue != -1)
        {
            StoreInteger(storedValue);
        }
    }

    // Function to store an integer in the container
    public void StoreInteger(int value)
    {
        storedValue = value;

        // Put prefab over sprite renderer
        Vector3 containerPosition = transform.position;
        currentInstance = Instantiate(integerPrefab, containerPosition, Quaternion.identity);
        integerComponent = currentInstance.GetComponent<Integer>();

        if (integerComponent != null)
        {
            integerComponent.SetValue(storedValue);
            checkReady = true;
        }
    }

    public void startProcess()
    {
        if (integerComponent != null)
        {
            //Start Coroutine to transfer output
            StartCoroutine(PassValueToOutput(1f));
        }
    }

    //Pass value to whatever the output entity is
    private IEnumerator PassValueToOutput(float delayTime)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(delayTime);

        // Assuming output has a script that can receive the value (you can modify it as needed)
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
        integerComponent = null;
        checkReady = false;
        Debug.Log("Prefab destroyed.");
    }

    public bool Ready()
    {
        return checkReady;
    }
}
