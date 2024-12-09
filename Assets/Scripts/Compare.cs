using System.Collections;
using UnityEngine;

public class Compare : MonoBehaviour, IReceive
{
    private Player_Items playerItems; // Reference to the Player_Items script
    private int storedValue = -1;
    [SerializeField] private int comparisonValue = 0; // Value to compare against
    [SerializeField] private GameObject integerPrefab;//For display purposes
    Integer integerComponent;

    private bool inRange = false;

    // Child containers for directing the output
    private Container greaterContainer;
    private Container lesserContainer;
    private Container equalContainer;

    void Start()
    {
        // Automatically find the Player_Items component on the player object
        playerItems = FindObjectOfType<Player_Items>();  // Finds the first instance of Player_Items in the scene

        // If needed, you can add a check to ensure playerItems was found
        if (playerItems == null)
        {
            Debug.LogError("Player_Items component not found in the scene!");
        }
        // Find child container scripts
        greaterContainer = transform.Find("GreaterContainer")?.GetComponent<Container>();
        lesserContainer = transform.Find("LesserContainer")?.GetComponent<Container>();
        equalContainer = transform.Find("EqualContainer")?.GetComponent<Container>();

        // Ensure all containers are found
        if (greaterContainer == null || lesserContainer == null || equalContainer == null)
        {
            Debug.LogError("One or more child containers are missing or do not have a Container script!");
        }
    }

    void Update()
    {
        // Check for player input (e.g., pressing "Z")
        if (Input.GetKeyDown(KeyCode.Z) && inRange)
        {
            StoreIntegerInContainer();
        }
    }

    // Function to store an integer in the container
    private void StoreIntegerInContainer()
    {
        // Get the next integer from the player's inventory, store as comparison value
        comparisonValue = playerItems.GetNextInteger();

        // If the value is valid, store it in the container
        if (comparisonValue != -1)
        {
            StoreInteger(comparisonValue);
        }
    }

    // Function to store an integer in the container
    public void StoreInteger(int value)
    {
        comparisonValue = value;

        // Put prefab over sprite renderer, briefly reference
        Vector3 containerPosition = transform.position;
        GameObject currentInstance = Instantiate(integerPrefab, containerPosition, Quaternion.identity);
        integerComponent = currentInstance.GetComponent<Integer>();
        //Actually sets the integer value
        if (integerComponent != null)
        {
            integerComponent.SetValue(comparisonValue);
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

    // Receive input and start comparison logic
    public void Receive_Input(int value)
    {
        Debug.Log("Received Value: " + value);
        storedValue = value;
        StartCoroutine(ProcessValue(0.2f)); // Delay for processing
    }

    // Process the received value, compare, and direct it to the appropriate container
    private IEnumerator ProcessValue(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        // Compare the value and send it to the appropriate container
        if (storedValue > comparisonValue)
        {
            Debug.Log(storedValue + " is greater than " + comparisonValue);
            greaterContainer?.ReceiveValue(storedValue);
        }
        else if (storedValue < comparisonValue)
        {
            Debug.Log("Value is less than " + comparisonValue);
            lesserContainer?.ReceiveValue(storedValue);
        }
        else
        {
            Debug.Log("Value is equal to " + comparisonValue);
            equalContainer?.ReceiveValue(storedValue);
        }
    }
}
