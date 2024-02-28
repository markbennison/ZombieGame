using UnityEngine;

public class PlayerMovementComplete : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public float moveSpeed = 6f;
    public float jumpForce = 10f;

    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
        if(rb.velocity.x > 0)
		{
            sr.flipX = false;
		}
        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
        }

		if (Input.GetButtonDown("Jump") && rb.velocity.y == 0) //SpaceBar pressed down
		{
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
		}
    }
}
