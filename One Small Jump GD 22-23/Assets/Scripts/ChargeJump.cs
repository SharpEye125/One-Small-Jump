using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeJump : MonoBehaviour
{
    public float minJump = 1f;
    public float maxJump = 5f;
    public float charge = 0;
    public float chargeRate = 0.1f;

    public Rigidbody2D rb;

    Color baseColor;
    Color chargeColor;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseColor = GetComponent<SpriteRenderer>().color;
        chargeColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        ChargeJumpColoration();
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
