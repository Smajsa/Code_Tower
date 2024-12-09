using UnityEngine;
using TMPro;

public class Integer : MonoBehaviour
{
    [SerializeField] private int value = 0;
    private TextMeshPro number;  // Use TextMeshPro for world-space text

    void Start()
    {
        // Find the TextMeshPro component on the child GameObject
        number = transform.Find("Display").GetComponent<TextMeshPro>();
        if (number != null)
        {
            number.text = value.ToString();  // Initialize the text
        }
    }

    public void SetValue(int val)
    {
        value = val;
        UpdateText();
    }

    public void increment()
    {
        value++;
        UpdateText();
    }

    public void decrement()
    {
        value--;
        UpdateText();
    }

    public int GetValue()
    {
        return value;
    }

    private void UpdateText()
    {
        if (number != null)
        {
            number.text = value.ToString();
        }
    }
}
