using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool grounded;
    public float minJump = 1f;
    public float maxJump = 5f;
    public float charge = 0;
    public float chargeRate = 0.1f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump") && grounded)
        {
            Jump();
        }
    }
    void Jump()
    {
        if (Input.GetButton("Jump"))
        {
            charge += Mathf.Lerp(minJump, maxJump, chargeRate) * Time.deltaTime;
            if (charge >= maxJump)
            {
                charge = maxJump;
            }
        }
        else if (Input.GetButtonUp("Jump"))
        {

            rb.AddForce(transform.up * charge, ForceMode2D.Impulse);
            charge = 0f;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Planet")
        {
            rb.drag = 1;
            float distance = Mathf.Abs(collision.GetComponent<GravityPoint>().planetRadius - Vector2.Distance(transform.position, collision.transform.position));
            if (distance <= 1)
            {
                //grounded = distance < 0.4f;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            grounded = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Planet")
        {
            rb.drag = 0.1f;
        }
    }
}
