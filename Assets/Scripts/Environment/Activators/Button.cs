using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Button : MonoBehaviour
{
    // Array of Objects that can trigger the button
    public GameObject[] triggerObjects;
    // Array of Objects that are triggered when button is triggered
    public ActivationClass[] activationObjects;

    private bool active = false;
    private List<GameObject> activeCollisions = new List<GameObject>();

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (triggerObjects.Contains(collision.gameObject))
        {
            activeCollisions.Add(collision.gameObject);
        }

        if (CheckCollision() && !active)
        {
            // Activate every linked object
            foreach (ActivationClass a in activationObjects)
            {
                a.Activate();
            }

            active = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (triggerObjects.Contains(collision.gameObject))
        {
            activeCollisions.Remove(collision.gameObject);
        }

        if (!CheckCollision() && active)
        {
            // Deactivate every linked object
            foreach (ActivationClass a in activationObjects)
            {
                a.Deactivate();
            }

            active = false;
        }
    }

    private bool CheckCollision()
    {
        if (activeCollisions.Count > 0) return true;
        return false;
    }
}
