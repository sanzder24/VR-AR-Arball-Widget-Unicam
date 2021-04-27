using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{//Code adapted from car motion
    public float speed = Mathf.PI / 2;
    public float radii = 12.47f;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        //some corrections added to reduce the speed factor
        float x = Mathf.Cos(speed / 16 * timer);
        float z = Mathf.Sin(speed / 16 * timer);
        transform.position = new Vector3(radii * x, 0.08f, radii * z);
       transform.forward = Vector3.Cross(transform.position, transform.up);
    }

}
