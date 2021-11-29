using UnityEngine;

public class PlayerBrokenButton : MonoBehaviour
{
    public BrokenButton button;

    private float speed;

    private void FixedUpdate()
    {
        speed = Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        button.Collision(speed);
    }
}
