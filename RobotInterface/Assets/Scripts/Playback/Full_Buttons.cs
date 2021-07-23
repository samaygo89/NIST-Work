using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

public class Full_Buttons : MonoBehaviour
{
    public UnityEngine.UI.Dropdown dropdown;
    public List<Vector3> positions;
    public GameObject target;
    public bool point_selected;
    public Vector3 selected_point;
    public GameObject selectedDuplicateTarget;
    public List<GameObject> DuplicateTargets;
    public UnityEngine.UI.Toggle ShowAllPointsToggle;

    void Start()
    {
        ShowAllPointsToggle = GameObject.Find("ShowAllPointsToggle").GetComponent<UnityEngine.UI.Toggle>();
        dropdown = GameObject.Find("Dropdown").GetComponent<UnityEngine.UI.Dropdown>();
        target = GameObject.Find("Target");
        positions = target.GetComponent<Mouse_drag>().positions;
        point_selected = false;
        DuplicateTargets= new List<GameObject>();
    }
    ///<summary>
    ///Refreshes the numbering on the dropdown menu
    ///</summary>
    private void RefreshDropdownNumbering()
    {
        for (int i = 0; i < dropdown.options.Count; i++)
        {
            dropdown.options[i].text = (i + 1).ToString() + ": " + dropdown.options[i].text.Split(':')[1];
        }
    }
    private GameObject CreateTargetSphere(Vector3 position, bool render){
        GameObject newobj = Instantiate(GameObject.Find("DuplicateTarget"));
        MeshRenderer[] renderers = newobj.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer renderer in renderers){
           renderer.enabled = render;
        }
        newobj.transform.position = position;
        newobj.transform.localScale = target.transform.localScale;
        return newobj;
    }
    private void TargetPointFunctionRender(bool on){
        foreach (GameObject newobj in DuplicateTargets)
        {
            RenderTarget(newobj,on);
        }
    }
    private void RenderTarget(GameObject obj, bool on){
        MeshRenderer[] renderers = obj.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer renderer in renderers){
           renderer.enabled = on;
        }
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
        dropdown.options.Add(new UnityEngine.UI.Dropdown.OptionData(positions.Count.ToString() + ": " + Vector3_to_String(newVector)));
        dropdown.value = positions.Count - 1;
        dropdown.RefreshShownValue();
        DuplicateTargets.Add(CreateTargetSphere(newVector,ShowAllPointsToggle.isOn));
    }
    ///<summary>
    ///Deletes point currently selected on the dropdown menu
    ///</summary>
    public void Delete_Point()
    {
        if (positions.Count >= 1)
        {
            positions.RemoveRange(dropdown.value, 1);
            dropdown.options.RemoveRange(dropdown.value, 1);
            RefreshDropdownNumbering();
            dropdown.RefreshShownValue();
            RenderTarget(DuplicateTargets[dropdown.value],false);
            DuplicateTargets.RemoveRange(dropdown.value,1);

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
        dropdown.options[dropdown.value].text = (dropdown.value + 1).ToString() + ": " + Vector3_to_String(newVector);
        positions[dropdown.value] = newVector;
        target.transform.position = newVector;
    }
    ///<summary>
    ///Called when the Y coordinate edit field value if end edited, edits the value in the positions array and dropdown menu
    ///</summary>
    public void CoordYEdit()
    {

        float currValue = float.Parse(GameObject.Find("CoordY").GetComponent<UnityEngine.UI.InputField>().text);
        Vector3 newVector = new Vector3(positions[dropdown.value].x, currValue, positions[dropdown.value].z);
        dropdown.options[dropdown.value].text = (dropdown.value + 1).ToString() + ": " + Vector3_to_String(newVector);
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
        dropdown.options[dropdown.value].text = (dropdown.value + 1).ToString() + ": " + Vector3_to_String(newVector);
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
    public void Insert_Point_Before()
    {
        Vector3 newVector = GameObject.Find("Target").transform.position;
        if (point_selected)
        {
            newVector = selected_point;
            point_selected = false;
            DuplicateTargets.Insert(dropdown.value,selectedDuplicateTarget);
        }
        else{
            DuplicateTargets.Insert(dropdown.value,CreateTargetSphere(newVector,ShowAllPointsToggle.isOn));
        }
        positions.Insert(dropdown.value, newVector);
        dropdown.options.Insert(dropdown.value, new UnityEngine.UI.Dropdown.OptionData(":" + Vector3_to_String(newVector)));
        RenderTarget(DuplicateTargets[dropdown.value],ShowAllPointsToggle.isOn);
        RefreshDropdownNumbering();
        dropdown.RefreshShownValue();

    }
    ///<summary>
    ///Inserts the target sphere position as a point after to the point selected on the dropdown menu
    ///</summary>
    public void Insert_Point_After()
    {
        Vector3 newVector = GameObject.Find("Target").transform.position;
        if (point_selected)
        {
            newVector = selected_point;
            point_selected = false;
            DuplicateTargets.Insert(dropdown.value,selectedDuplicateTarget);
        }
        else{
            DuplicateTargets.Insert(dropdown.value,CreateTargetSphere(newVector,ShowAllPointsToggle.isOn));
        }
        positions.Insert(dropdown.value + 1, newVector);
        dropdown.options.Insert(dropdown.value + 1, new UnityEngine.UI.Dropdown.OptionData(":" + Vector3_to_String(newVector)));
        DuplicateTargets.Insert(dropdown.value+1,selectedDuplicateTarget);
        dropdown.value = dropdown.value + 1;
        RenderTarget(DuplicateTargets[dropdown.value+1],ShowAllPointsToggle.isOn);
        RefreshDropdownNumbering();
        dropdown.RefreshShownValue();
    }

    ///<summary>
    ///Loads a .txt file with vectors into the positions array and the dropdown menu, file needs to be in the form: x,y,z on each line
    ///</summary>
    public void Load()
    {
        string path = Application.dataPath + "/Save.txt";
        string[] strings_vector = File.ReadAllLines(path);
        //reset

        dropdown.ClearOptions();
        positions.Clear();
        GameObject.Find("CoordX").GetComponent<UnityEngine.UI.InputField>().text = "";
        GameObject.Find("CoordY").GetComponent<UnityEngine.UI.InputField>().text = "";
        GameObject.Find("CoordZ").GetComponent<UnityEngine.UI.InputField>().text = "";
        //end of reseting
        for (int i = 0; i < strings_vector.GetLength(0); i++)
        {
            Vector3 newVector;
            string[] newString = strings_vector[i].Split(':');
            if (newString.Length == 1)
            {
                newVector = String_to_Vector3(strings_vector[i]);
            }
            else
            {
                newVector = String_to_Vector3(newString[1]);
            }
            positions.Add(new Vector3(newVector.x, newVector.y, newVector.z));
            dropdown.options.Add(new UnityEngine.UI.Dropdown.OptionData((i + 1).ToString() + ": " + Vector3_to_String(newVector)));
            DuplicateTargets.Add(CreateTargetSphere(newVector,ShowAllPointsToggle.isOn));
        }

        dropdown.RefreshShownValue();
    }
    ///<summary>
    ///Saves a .txt file to application.dataPath with a vector in the form of "x,y,z" on each line 
    ///</summary>
    public void Save()
    {
        string path = Application.dataPath + "/Save.txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "");
        }
        else
        {
            File.Delete(path);
            File.WriteAllText(path, "");
        }
        for (int i = 0; i < dropdown.options.Count; i++)
        {
            string content = dropdown.options[i].text + "\n";
            File.AppendAllText(path, content);
        }
    }
    ///<summary>
    ///Selects a point and takes it out of the dropdown menu
    ///</summary>
    public void SelectPoint()
    {
        point_selected = true;
        selected_point = positions[dropdown.value];
        selectedDuplicateTarget = DuplicateTargets[dropdown.value];
        RenderTarget(DuplicateTargets[dropdown.value],false);
        dropdown.options.RemoveRange(dropdown.value, 1);
        DuplicateTargets.RemoveRange(dropdown.value,1);
        RefreshDropdownNumbering();
        dropdown.RefreshShownValue();
    }
    ///<summary>
    ///Rotates the camera around the robot by 10 degrees
    ///</summary>
    public void Rotate_Camera()
    {
        GameObject.Find("CameraRotater").transform.Rotate(0, 10, 0);
    }
    public void ShowPoints(){
        TargetPointFunctionRender(ShowAllPointsToggle.isOn);
    }


    

    


}
