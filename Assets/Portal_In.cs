using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_In : MonoBehaviour
{
    [SerializeField] private Container container;  // Reference to the attached Container
    [SerializeField] private Portal_Out outPortal; // Reference to the paired Portal_Out

    private void Start()
    {
        // Ensure container and outPortal are assigned
        if (container == null)
        {
            Debug.LogError("Container is not assigned to Portal_In!");
        }
        if (outPortal == null)
        {
            Debug.LogError("OutPortal is not assigned to Portal_In!");
        }
    }

    // Called by the container to teleport the value
    public void TeleportValue(int value)
    {
        if (outPortal != null)
        {
            Debug.Log("Teleporting value " + value + " to Portal_Out");
            outPortal.ReceiveTeleportedValue(value);
        }
        else
        {
            Debug.LogError("OutPortal is not set!");
        }
    }
}
