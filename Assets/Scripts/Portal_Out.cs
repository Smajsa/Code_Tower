using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Out : MonoBehaviour
{
    [SerializeField] private Container container;  // Reference to the output Container

    private void Start()
    {
        if (container == null)
        {
            Debug.LogError("Container is not assigned to Portal_Out!");
        }
    }

    // Called by Portal_In to pass the teleported value to the container
    public void ReceiveTeleportedValue(int value)
    {
        if (container != null)
        {
            Debug.Log("Received teleported value: " + value);
            container.ReceiveValue(value); // Pass the value to the Container
        }
        else
        {
            Debug.LogError("Output container is not set!");
        }
    }
}
