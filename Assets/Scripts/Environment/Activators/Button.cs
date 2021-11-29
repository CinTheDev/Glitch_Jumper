using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Button : MonoBehaviour
{
    public bool spriteInverted;
    // Array of Objects that can trigger the button
    public GameObject[] triggerObjects;
    // Array of Objects that are triggered when button is triggered
    public ActivationClass[] activationObjects;

    private bool active = false;
    private List<GameObject> activeCollisions;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        activeCollisions = new List<GameObject>();
    }

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

            animator.SetBool("Active", !spriteInverted);
            active = true;
            FindObjectOfType<AudioManager>().Play("Button_Activate");
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

            animator.SetBool("Active", spriteInverted);
            active = false;
            FindObjectOfType<AudioManager>().Play("Button_Deactivate");
        }
    }

    private bool CheckCollision()
    {
        if (activeCollisions.Count > 0) return true;
        return false;
    }
}
