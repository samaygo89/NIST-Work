using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
public class Full_Buttons : MonoBehaviour
{
    public UnityEngine.UI.Dropdown dropdown;
    public List<Vector3> positions;
    public GameObject target;
    void Start()
    {
        dropdown = GameObject.Find("Dropdown").GetComponent<UnityEngine.UI.Dropdown>();
        target = GameObject.Find("Target");
        positions = target.GetComponent<Mouse_drag>().positions;
    }
///<summary>
///turns a string in the form: "x,y,z" into a Vector3 and returns the vector;
///</summary>
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
///<summary>
///takes in a Vector3 and returns a string in the form: "x,y,z"
///</summary>
        public string Vector3_to_String(Vector3 val)
    {
        string retval;
        retval = val.x.ToString("F2") + ",";
        retval += val.y.ToString("F2") + ",";
        retval += val.z.ToString("F2");
        return retval;
    }

///<summary>
///Inserts Current Target Sphere position at the end of the positions array and dropdown list
///</summary>
    public void Add_Point()
    {
        Vector3 newVector = target.transform.position;
        positions.Add(newVector);
        dropdown.options.Add(new UnityEngine.UI.Dropdown.OptionData(Vector3_to_String(newVector)));
        dropdown.value = dropdown.value + 1;
    }
///<summary>
///Deletes point currently selected on the dropdown menu
///</summary>
    public void Delete_Point()
    {
        if(positions.Count >= 1){
        positions.RemoveRange(dropdown.value,1);
        dropdown.options.RemoveRange(dropdown.value,1);
        dropdown.RefreshShownValue();
        }
        else
        {
        print("No points to delete");
        }      
    }
///<summary>
///Called when the X coordinate edit field value if end edited, edits the value in the positions array and dropdown menu
///</summary>
    public void CoordXEdit()
    {
       
        float currValue = float.Parse(GameObject.Find("CoordX").GetComponent<UnityEngine.UI.InputField>().text);
        Vector3 newVector = new Vector3(currValue, positions[dropdown.value].y, positions[dropdown.value].z);
        dropdown.options[dropdown.value].text = Vector3_to_String(newVector);
        positions[dropdown.value] = newVector;
        target.transform.position = newVector;
    }
///<summary>
///Called when the Y coordinate edit field value if end edited, edits the value in the positions array and dropdown menu
///</summary>
    public void CoordYEdit()
    {
        
        float currValue = float.Parse(GameObject.Find("CoordY").GetComponent<UnityEngine.UI.InputField>().text);
        Vector3 newVector = new Vector3(positions[dropdown.value].x,currValue, positions[dropdown.value].z);
        dropdown.options[dropdown.value].text = Vector3_to_String(newVector);
        positions[dropdown.value] = newVector;
        target.transform.position = newVector;
    }
///<summary>
///Called when the Z coordinate edit field value if end edited, edits the value in the positions array and dropdown menu
///</summary>
    public void CoordZEdit()
    {
        
        float currValue = float.Parse(GameObject.Find("CoordZ").GetComponent<UnityEngine.UI.InputField>().text);
        Vector3 newVector = new Vector3(positions[dropdown.value].x, positions[dropdown.value].y, currValue);
        dropdown.options[dropdown.value].text = Vector3_to_String(newVector);
        positions[dropdown.value] = newVector;
        target.transform.position = newVector;
    }
///<summary>
///Called when the dropdown menu value is changed: changes the x,y,and z edit fields and changes the target positions
///</summary>
    public void Change_target_position()
    {
        Vector3 pos = positions[dropdown.value];
        target.transform.position = pos;
        GameObject.Find("CoordX").GetComponent<UnityEngine.UI.InputField>().text = pos.x.ToString("F2");
        GameObject.Find("CoordY").GetComponent<UnityEngine.UI.InputField>().text = pos.y.ToString("F2");
        GameObject.Find("CoordZ").GetComponent<UnityEngine.UI.InputField>().text = pos.z.ToString("F2");
    }
///<summary>
///Inserts the target sphere position as a point prior to the point selected on the dropdown menu
///</summary>
    public void Insert_Point_Before(){
        Vector3 newVector = GameObject.Find("Target").transform.position;
        positions.Insert(dropdown.value,newVector);
        dropdown.options.Insert(dropdown.value, new UnityEngine.UI.Dropdown.OptionData(Vector3_to_String(newVector)));
        dropdown.RefreshShownValue();
    }
///<summary>
///Inserts the target sphere position as a point after to the point selected on the dropdown menu
///</summary>
    public void Insert_Point_After(){
        Vector3 newVector = GameObject.Find("Target").transform.position;
        positions.Insert(dropdown.value+1,newVector);
        dropdown.options.Insert(dropdown.value+1, new UnityEngine.UI.Dropdown.OptionData(Vector3_to_String(newVector)));
        dropdown.value = dropdown.value+1;
        dropdown.RefreshShownValue();
    }
///<summary>
///Resets the dropdown menu, positions array, and coordinate edit fields 
///</summary>

///<summary>
///Loads a .txt file with vectors into the positions array and the dropdown menu, file needs to be in the form: x,y,z on each line
///</summary>
    public void Load(){
        string path = EditorUtility.OpenFilePanel("", "", "txt");
        string[] strings_vector = File.ReadAllLines(path);
        //reset
        target.transform.position = new Vector3(0, 0, 0);
        dropdown.ClearOptions();
        positions.Clear();
        GameObject.Find("CoordX").GetComponent<UnityEngine.UI.InputField>().text = "";
        GameObject.Find("CoordY").GetComponent<UnityEngine.UI.InputField>().text = "";
        GameObject.Find("CoordZ").GetComponent<UnityEngine.UI.InputField>().text = "";
        //end of reseting
        for (int i = 0; i < strings_vector.GetLength(0); i++)
        {
        Vector3 newVector =String_to_Vector3(strings_vector[i]);
        positions.Add(new Vector3(newVector.x,newVector.y,newVector.z));
        dropdown.options.Add(new UnityEngine.UI.Dropdown.OptionData(Vector3_to_String(newVector)));
        }
    }
    public void Save(){   
        string path = EditorUtility.OpenFolderPanel("Select Save Folder", "", "");
        FileStream fs = File.Create(path);
        for (int i = 0; i <dropdown.options.Count; i++)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(dropdown.options[i].text+"\n");
            fs.Write(info,0,info.Length);
        }
    }

}
