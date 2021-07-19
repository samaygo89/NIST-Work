///////////////////////////////////////////////////////////////////////////////
//
//  Original System: Mouse_drag.cs
//  Subsystem:       Human-Robot Interaction
//  Workfile:        Manus_Open_VR V2
//  Revision:        1.0 - 6/22/2018
//  Author:          Esteban Segarra
//
//  Description
//  ===========
//  Wrapper to move gameobjects in-game
//
///////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Mouse_drag : MonoBehaviour
{
    public UnityEngine.UI.Dropdown dropdown;
    public float distance;
    public float additive_ratio = 1.0f;
    public List<Vector3> positions = new List<Vector3>();
    public bool finished = false;
    public string Vector3_to_String(Vector3 val)
    {
        string retval;
        retval = val.x.ToString("F2") + ",";
        retval += val.y.ToString("F2") + ",";
        retval += val.z.ToString("F2");
        return retval;
    }
    public GameObject createSphere(Color color, Vector3 pos)
    {
        GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        var sphererenderer = newObj.GetComponent<Renderer>();
        sphererenderer.material.SetColor("_Color", color);
        newObj.transform.position = pos;
        newObj.SetActive(false);
        return newObj;


    }
    public void Start()
    {
        dropdown = GameObject.Find("Dropdown").GetComponent<UnityEngine.UI.Dropdown>();

    }



    private void Update()
    {

    }

    private void OnMouseDown()
    {
        distance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
    }


    public Vector3 debug_vector;
    private void OnMouseDrag()
    {
        Vector3 mouse_pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 obj_pos = Camera.main.ScreenToWorldPoint(mouse_pos);
        debug_vector = obj_pos;
        transform.position = obj_pos;

            GameObject.Find("CoordX").GetComponent<UnityEngine.UI.InputField>().text = gameObject.transform.position.x.ToString("F2");
            GameObject.Find("CoordY").GetComponent<UnityEngine.UI.InputField>().text = gameObject.transform.position.y.ToString("F2");
            GameObject.Find("CoordZ").GetComponent<UnityEngine.UI.InputField>().text = gameObject.transform.position.z.ToString("F2");
        

    }
    private void OnMouseUp()
    {
        if (GameObject.Find("Edit").GetComponent<UnityEngine.UI.Toggle>().isOn)
        {
            dropdown.options[dropdown.value].text = Vector3_to_String(transform.position);
            positions[dropdown.value] = transform.position;

        }
        else
        {
            print("Position");

        }

    }


}
