using UnityEngine;

public class Door : ActivationClass
{
    public bool spriteInverted;
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Act()
    {
        //Debug.Log("Door opened");
        GetComponent<BoxCollider2D>().enabled = false;
        animator.SetBool("Open", !spriteInverted);
        FindObjectOfType<AudioManager>().Play("Door");
    }

    protected override void Deact()
    {
        //Debug.Log("Door closed");
        GetComponent<BoxCollider2D>().enabled = true;
        animator.SetBool("Open", spriteInverted);
        FindObjectOfType<AudioManager>().Play("Door");
    }
}
