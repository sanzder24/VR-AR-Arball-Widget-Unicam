using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidgetCode : MonoBehaviour
{

    public GameObject prefab;
    private GameObject widget;
    private Vector3 delta = Vector3.zero;
    private Vector3 lastPos = Vector3.zero;
    private float scalefactor = 0.1f;

    static Shader shader;
    static GameObject selected;

    bool x, y, z = false;

    // Start is called before the first frame update
    void Start()
    {

        //widget = (GameObject)Instantiate(prefab);
        //widget.transform.SetParent(transform, false);
        //Debug.Log("Player's Parent: " + widget.transform.parent.name);
        //mainCam.transform.localPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("onMouseCLick");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, hitInfo: out hit))
            {
                Debug.Log("hit");
                Debug.Log(hit.collider.gameObject.name);

                if (widget == null)
                {
                    selected = GameObject.Find(hit.collider.gameObject.name);
                    shader = selected.GetComponent<Renderer>().material.shader;
                    Debug.Log(shader);
                    selected.GetComponent<Renderer>().material.shader = Shader.Find("highlighter");
                    widget = (GameObject)Instantiate(prefab);
                    widget.transform.SetParent(GameObject.Find(hit.collider.gameObject.name).transform, false);
                    Debug.Log("Player's Parent: " + widget.transform.parent.name);
                }
                if (widget != null)
                {

                    if (!widget.transform.parent.name.Equals(hit.collider.gameObject.name) && !(hit.collider.gameObject.name.Contains("x") || hit.collider.gameObject.name.Contains("y")|| hit.collider.gameObject.name.Contains("z")))
                    {
                        Debug.Log("shader destroy: "+shader);
                        selected.GetComponent<Renderer>().material.shader = shader;
                        Destroy(widget);
                    }
                }

                lastPos = Input.mousePosition;

                if (hit.collider.gameObject.name.Contains("x"))
                {
                    x = true;
                    y = false;
                    z = false;
                }
                else if (hit.collider.gameObject.name.Contains("y"))
                {
                    x = false;
                    y = true;
                    z = false;
                }
                else
                {
                    x = false;
                    y = false;
                    z = true;
                }
            }
            else
            {
                
                x = false;
                y = false;
                z = false;
            }
        }

        if (Input.GetButton("Fire1") && widget !=null)
        {
            Debug.Log(Input.mousePosition);

            Debug.Log("Player's Parent: -------" + widget.transform.parent.name);

            delta = (Input.mousePosition - lastPos);

            if (x)
            {
                GameObject Xwidget = widget.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
                Xwidget.transform.localScale = new Vector3(Xwidget.transform.localScale.x, Xwidget.transform.localScale.y - (delta.x * scalefactor * 2), Xwidget.transform.localScale.z);
                widget.transform.parent.transform.localScale = new Vector3(widget.transform.parent.transform.localScale.x - (delta.x * scalefactor), widget.transform.parent.transform.localScale.y, widget.transform.parent.transform.localScale.z);
            }
            else if (y)
            {
                GameObject Ywidget = widget.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject;
                Ywidget.transform.localScale = new Vector3(Ywidget.transform.localScale.x, Ywidget.transform.localScale.y + (delta.y * scalefactor * 2), Ywidget.transform.localScale.z);
                widget.transform.parent.transform.localScale = new Vector3(widget.transform.parent.transform.localScale.x, widget.transform.parent.transform.localScale.y + (delta.y * scalefactor), widget.transform.parent.transform.localScale.z);
            }
            else if (z)
            {
                GameObject Zwidget = widget.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
                Zwidget.transform.localScale = new Vector3(Zwidget.transform.localScale.x, Zwidget.transform.localScale.y + ((delta.x + delta.y) * scalefactor * 2), Zwidget.transform.localScale.z);
                widget.transform.parent.transform.localScale = new Vector3(widget.transform.parent.transform.localScale.x, widget.transform.parent.transform.localScale.y, widget.transform.parent.transform.localScale.z + ((delta.x + delta.y) * scalefactor));
            }

            lastPos = Input.mousePosition;
        }

        //if (Input.GetButtonUp("Fire1"))
        //{
        //    x = false;
        //    y = false;
        //    z = false;
        //}
    }



}
