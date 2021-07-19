using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class Load : MonoBehaviour
{
     List<Vector3> positions;
    UnityEngine.UI.Dropdown dropdown;
    public void Pressed(){
        
        dropdown = GameObject.Find("Dropdown").GetComponent<UnityEngine.UI.Dropdown>();
        positions = GameObject.Find("Target").GetComponent<Mouse_drag>().positions;
        string path = EditorUtility.OpenFilePanel("", "", "txt");
        string[] strings_vector = File.ReadAllLines(path);

        Reset reset = new Reset();
        reset.pressed();
        Go_to_point_clicked stringFunction = new Go_to_point_clicked();
        Mouse_drag vectorFunction = new Mouse_drag();
        for (int i = 0; i < strings_vector.GetLength(0); i++)
        {
        Vector3 newVector = stringFunction.String_to_Vector3(strings_vector[i]);
        positions.Add(new Vector3(newVector.x,newVector.y,newVector.z));
        List<string> temp = new List<string>();
        temp.Add(vectorFunction.Vector3_to_String(newVector));
        dropdown.AddOptions(temp);
        }
    }
}
