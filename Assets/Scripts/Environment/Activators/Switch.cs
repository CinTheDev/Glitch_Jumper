using UnityEngine;
using System.Linq;

public class Switch : MonoBehaviour
{
    // Array of Objects that can trigger the switch
    public GameObject[] triggerObjects;
    // Array of Objects that are triggered when switch is triggered
    public ActivationClass[] activationObjects;

    public Color debugActiveColor;
    public Color debugUnactiveColor;

    public bool active;

    private void Start()
    {
        // Trigger toggle without changing active,
        // so if active is true from the beginning,
        // everything is activated.
        Toggle();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggerObjects.Contains(collision.gameObject)) return;

        // Toggle switch
        active = !active;
        Toggle();
    }

    private void Toggle()
    {
        // If switch was set to active
        if (active)
        {
            Debug.Log("Switch active");
            GetComponent<SpriteRenderer>().color = debugActiveColor;
            foreach (ActivationClass a in activationObjects)
            {
                a.Activate();
            }
        }
        // If switch was set to not active
        else
        {
            Debug.Log("Switch unactive");
            GetComponent<SpriteRenderer>().color = debugUnactiveColor;
            foreach (ActivationClass a in activationObjects)
            {
                a.Deactivate();
            }
        }
    }
}
