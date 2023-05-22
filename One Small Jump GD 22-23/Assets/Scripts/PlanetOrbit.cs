using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    public GameObject planet;

    public float maxGravity;
    public float maxGravityDist;

    float lookAngle;
    Vector3 lookDirection;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (planet != null)
        {
            float dist = Vector2.Distance(planet.transform.position, transform.position);

            Vector3 v = planet.transform.position - transform.position;
            rb.AddForce(v.normalized * (1.0f - dist / maxGravityDist) * maxGravity);

            lookDirection = planet.transform.position - transform.position;
            lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Planet")
        {
            planet = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Planet")
        {
            planet = null;
        }
    }
}
