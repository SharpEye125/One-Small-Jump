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

    float horizontal;
    public float airMoveSpeed;

    Color baseColor;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetButton("Jump") && grounded)
        {
            charge += Mathf.Lerp(minJump, maxJump, chargeRate) * Time.deltaTime;
            if (charge >= maxJump)
            {
                charge = maxJump;
            }
        }
        else if (Input.GetButtonUp("Jump") && grounded)
        {

            rb.AddForce(transform.up * charge, ForceMode2D.Impulse);
            charge = 0f;
        }
        ChargeJumpColoration();
    }
    private void FixedUpdate()
    {
        if (!grounded || grounded)
        {
            rb.AddForce(transform.right * horizontal * airMoveSpeed);
        }

    }
    void Jump()
    {
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Planet")
        {
            rb.drag = 1;
            float distance = Mathf.Abs(collision.GetComponent<GravityPoint>().planetRadius - Vector2.Distance(transform.position, collision.transform.position));
            if (distance <= 1)
            {
                grounded = distance < 1f || distance < 0.4f;
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
    void ChargeJumpColoration()
    {
        if (charge >= maxJump)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (charge >= maxJump / 2f)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = baseColor;
        }
    }
}
