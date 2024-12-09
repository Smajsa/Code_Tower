using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subByOne : MonoBehaviour, IReceive
{
    private int storedValue = -1;
    private Container childContainer; //reference to container script

    // Start is called before the first frame update
    void Start()
    {
        childContainer = GetComponentInChildren<Container>();

        // Ensure the Container script was found
        if (childContainer == null)
        {
            Debug.LogError("No Container script found on child object!");
        }
    }

    //Receive input, then start the increment
    public void Receive_Input(int value)
    {
        storedValue = value;
        StartCoroutine(entityStart(0.2f));
    }

    //Delay for a split second, then increment and pass to container below
    private IEnumerator entityStart(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        storedValue--;
        print(storedValue);

        // Pass the value to the child Container script
        if (childContainer != null)
        {
            childContainer.ReceiveValue(storedValue);
        }
        else
        {
            Debug.LogError("Child Container is not set or missing!");
        }
    }
}
