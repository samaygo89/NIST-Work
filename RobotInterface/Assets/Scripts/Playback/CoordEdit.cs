using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoordEdit : MonoBehaviour
{
    public UnityEngine.UI.Dropdown dropdown;
    public List<Vector3> positions;
    private void getObjects()
    {
        positions = GameObject.Find("Target").GetComponent<Mouse_drag>().positions;
        dropdown = GameObject.Find("Dropdown").GetComponent<UnityEngine.UI.Dropdown>();
    }
    public void CoordXEdit()
    {
        getObjects();
        float currValue = float.Parse(GameObject.Find("CoordX").GetComponent<UnityEngine.UI.InputField>().text);
        Vector3 newVector = new Vector3(currValue, positions[dropdown.value].y, positions[dropdown.value].z);
        dropdown.options[dropdown.value].text = GameObject.Find("Target").GetComponent<Mouse_drag>().Vector3_to_String(newVector);
        positions[dropdown.value] = newVector;
        GameObject.Find("Target").transform.position = newVector;
    }
public void CoordYEdit()
    {
        getObjects();
        float currValue = float.Parse(GameObject.Find("CoordY").GetComponent<UnityEngine.UI.InputField>().text);
        Vector3 newVector = new Vector3(positions[dropdown.value].x,currValue, positions[dropdown.value].z);
        dropdown.options[dropdown.value].text = GameObject.Find("Target").GetComponent<Mouse_drag>().Vector3_to_String(newVector);
        positions[dropdown.value] = newVector;
        GameObject.Find("Target").transform.position = newVector;
    }
public void CoordZEdit()
    {
        getObjects();
        float currValue = float.Parse(GameObject.Find("CoordZ").GetComponent<UnityEngine.UI.InputField>().text);
        Vector3 newVector = new Vector3(positions[dropdown.value].x, positions[dropdown.value].y, currValue);
        dropdown.options[dropdown.value].text = GameObject.Find("Target").GetComponent<Mouse_drag>().Vector3_to_String(newVector);
        positions[dropdown.value] = newVector;
        GameObject.Find("Target").transform.position = newVector;
    }

}

