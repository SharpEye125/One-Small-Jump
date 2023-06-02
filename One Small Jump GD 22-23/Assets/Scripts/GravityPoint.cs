using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPoint : MonoBehaviour
{
    public float gravityScale;
    public float planetRadius;
    public float gravityMinRange;
    public float gravityMaxRange;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        float gravitationalPower = gravityScale;
        float dist = Vector2.Distance(collision.transform.position, transform.position);
        if (dist > (planetRadius + gravityMinRange))
        {
            float min = planetRadius + gravityMinRange;
            gravitationalPower = (((min + gravityMaxRange - dist) * gravityMaxRange) * gravitationalPower);
        }

        Vector3 dir = (transform.position - collision.transform.position) * gravityScale;
        collision.GetComponent<Rigidbody2D>().AddForce(dir);
        if (collision.tag == "Player")
        {
            collision.transform.up = Vector3.MoveTowards(collision.transform.up, -dir, gravityScale * Time.deltaTime);
            if (dist >= gravityMinRange)
            {
                collision.gameObject.GetComponent<PlayerMovement>().grounded = true;
            }
        }
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius * 5);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, gravityMaxRange);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, gravityMinRange);

    }
}
