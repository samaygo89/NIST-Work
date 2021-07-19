using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class add_point_clicked : MonoBehaviour
{
    List<Vector3> positions;
    UnityEngine.UI.Dropdown dropdown;
    public void Pressed()
    {

        dropdown = GameObject.Find("Dropdown").GetComponent<UnityEngine.UI.Dropdown>();
        positions = GameObject.Find("Target").GetComponent<Mouse_drag>().positions;
        Vector3 newVector = GameObject.Find("Target").transform.position;
        positions.Add(newVector);
        List<string> temp = new List<string>();
        temp.Add(GameObject.Find("Target").GetComponent<Mouse_drag>().Vector3_to_String(newVector));
        dropdown.AddOptions(temp);
        dropdown.value = dropdown.value+1;
    }
    public void Delete_Point()
    {
        
        dropdown = GameObject.Find("Dropdown").GetComponent<UnityEngine.UI.Dropdown>();
        positions = GameObject.Find("Target").GetComponent<Mouse_drag>().positions;
        if(positions.Count >= 1){
        positions.RemoveRange(dropdown.value,1);
        dropdown.options.RemoveRange(dropdown.value,1);
        }
        else
        {
        print("No points to delete");
        }

      
    }
}
