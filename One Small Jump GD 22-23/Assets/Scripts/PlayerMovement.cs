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
    float horizontal;
    public float airMoveSpeed;
    Rigidbody2D rb;

    float landSFXTimer;
    float landSFXCooldown = 1f;
    bool landed;

    public AudioClip jumpSFX;
    public AudioClip landSFX;
    //public AudioClip outOfBoundsSFX;
    AudioSource myAudio;

    Color baseColor;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myAudio = GetComponent<AudioSource>();
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
            myAudio.PlayOneShot(jumpSFX);
            rb.AddForce(transform.up * charge, ForceMode2D.Impulse);
            grounded = false;
            charge = 0f;
        }
        if (landed)
        {
            landSFXTimer += Time.deltaTime;
            if (landSFXTimer >= landSFXCooldown)
            {
                landed = false;
            }
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
            float distance = Vector2.Distance(transform.position, collision.transform.position);
            //Debug.Log("Distance to " + collision.name + ": " + distance);
            grounded = distance <= collision.GetComponent<GravityPoint>().gravityMinRange;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Planet")
        {
            rb.drag = 0.1f;
            grounded = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            transform.parent = collision.transform;
            if (!landed)
            {
                myAudio.PlayOneShot(landSFX);
            }
            landSFXTimer = 0f;
            landed = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            transform.parent = null;
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
