using UnityEngine;
using System.Linq;

public class BrokenPlatform : MonoBehaviour
{
    public GameObject[] triggerObjects;
    public int stability;

    private Animator animator;
    private int maxStability;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        maxStability = stability;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!triggerObjects.Contains(collision.gameObject)) return;

        stability--;

        int state = Mathf.RoundToInt((5f / maxStability) * (maxStability - stability));
        animator.SetInteger("State", state);

        if (stability == 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;

            Jumppad pad = GetComponentInChildren<Jumppad>();
            if (pad)
            {
                pad.Fall();
            }
        }
    }
}
