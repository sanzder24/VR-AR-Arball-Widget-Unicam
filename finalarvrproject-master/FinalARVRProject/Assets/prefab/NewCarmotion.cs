using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCarmotion : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);
    }
}

