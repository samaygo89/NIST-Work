using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go_to_point_clicked : MonoBehaviour
{
    public UnityEngine.UI.Dropdown dropdown;
    public Vector3 String_to_Vector3(string val)
    {
        Vector3 retval;
        string[] values = new string[3];
        values = val.Split(',');
        retval.x = float.Parse(values[0]);
        retval.y = float.Parse(values[1]);
        retval.z = float.Parse(values[2]);
        
        return retval;

    }
    public void pressed()
    {
        dropdown = GameObject.Find("Dropdown").GetComponent<UnityEngine.UI.Dropdown>();
        Vector3 pos = String_to_Vector3(dropdown.options[dropdown.value].text);
        GameObject.Find("Target").transform.position = pos;
        GameObject.Find("CoordX").GetComponent<UnityEngine.UI.InputField>().text = pos.x.ToString("F2");
        GameObject.Find("CoordY").GetComponent<UnityEngine.UI.InputField>().text = pos.y.ToString("F2");
        GameObject.Find("CoordZ").GetComponent<UnityEngine.UI.InputField>().text = pos.z.ToString("F2");
    }
}
