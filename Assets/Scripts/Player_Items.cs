using System.Collections.Generic;
using UnityEngine;

public class Player_Items : MonoBehaviour
{
    [SerializeField] private GameObject integerPrefab; // Reference to the Integer prefab
    private List<int> collectedValues = new List<int>(); // Store only values

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the trigger is an Integer prefab
        if (other.CompareTag("Integer") && Input.GetKey(KeyCode.LeftShift))
        {
            // Collect the Integer value
            CollectInteger(other.gameObject);
        }
    }

    private void CollectInteger(GameObject integerInstance)
    {
        // Store the value of the Integer
        Integer integerComponent = integerInstance.GetComponent<Integer>();
        if (integerComponent != null)
        {
            collectedValues.Add(integerComponent.GetValue());
        }

        // Destroy the collected instance
        Destroy(integerInstance);

        Debug.Log("Collected Integer!");
    }

    public int GetNextInteger()
    {
        // Get the next integer (if any) to be transferred to the container
        if (collectedValues.Count > 0)
        {
            int lastValue = collectedValues[collectedValues.Count - 1];
            collectedValues.RemoveAt(collectedValues.Count - 1);
            return lastValue;
        }
        else
        {
            Debug.Log("No Integer to store");
            return -1; // Or handle accordingly if no integers are available
        }
    }
}
