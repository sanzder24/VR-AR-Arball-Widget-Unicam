using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unicam : MonoBehaviour
{
    private Camera camera;
    float horizontalSpeed = 2.0f;
    float verticalSpeed = 2.0f;
    public float speed = Mathf.PI / 2;
    public float radii = 12.47f;
    float timer = 0.0f;
  
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
       
    }

    // Update is called once per frame
    void Update()
    {
        
        float ScrollWheelChange = Input.GetAxis("Mouse ScrollWheel");           //This little peece of code is written by JelleWho https://github.com/jellewie
        float mouseLtRt =Input.GetAxis("Mouse X");
        float mouseUpDn = Input.GetAxis("Mouse Y");
        
        //Check if the mouse is clicked with left click
        //Reference basic Unicam idea from 3D User interfaces
        if (Input.GetMouseButton(0))
        {   //Check if mouse trajectory is vertical for applying zooming into the scene capability
            if (mouseUpDn > 0.2 || mouseUpDn < -0.2){
                print("Zooming " + mouseUpDn);
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + mouseUpDn);
            }
            //Check if the mouse trajector is horizontal to apply camera translation
            if (mouseLtRt > 0.2 || mouseLtRt < -0.2) {
                print("Translating" + mouseLtRt);
                transform.position = new Vector3(transform.position.x-mouseLtRt, transform.position.y, transform.position.z);
            }
        }
       
        //Right click rotate arround the y axis 
        if (Input.GetMouseButton(1)) {
            timer += mouseLtRt;
            //some corrections added to reduce the speed factor
            float x = Mathf.Cos(speed/16  * timer);
            float z = Mathf.Sin(speed/16 * timer);
            
            transform.position = new Vector3(radii * x, 0.5f, radii * z);
            transform.forward = Vector3.Cross(transform.position, transform.up);
            var q = Quaternion.LookRotation(new Vector3(0,1,0) - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, timer);
        }

        //Click-to-focus on scroll click
        //click on any object to get a view of it 

        if (Input.GetMouseButtonDown(2))
        {
            GameObject lastClicked;
            Ray ray;
            RaycastHit rayHit;
          
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out rayHit))
            {
                lastClicked = rayHit.transform.gameObject;
                if (lastClicked != null)
                {    
                    print("Last Clicked Object:"+lastClicked.name);
                    Transform clickedObject = lastClicked.transform;
                    Vector3 translateLocation = new Vector3(3,-1,3);
                    if (clickedObject.position.x > 0) {
                        translateLocation.x *= -1;
                    }
                    if (clickedObject.position.z > 0) {
                        translateLocation.z *= -1; 
                    }
                    transform.position = clickedObject.position - translateLocation;
                    transform.LookAt(clickedObject);
                    Vector3 eulerAngles = transform.rotation.eulerAngles;
                    eulerAngles.x = 0;
                    eulerAngles.z = 0;
                    transform.rotation = Quaternion.Euler(eulerAngles);
                }
            }
        }

    }
}
