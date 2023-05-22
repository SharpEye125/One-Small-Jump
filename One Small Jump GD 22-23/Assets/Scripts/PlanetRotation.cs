using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    public float rotateRate = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 t = GetComponent<Transform>().localEulerAngles;
        t.z += rotateRate;
        transform.localEulerAngles = t;
    }
}
