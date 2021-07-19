using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public List<Vector3> positions;
    public UnityEngine.UI.Dropdown dropdown;
    // Start is called before the first frame update
public void pressed()
    {
        GameObject.Find("Target").transform.position = new Vector3(0, 0, 0);
        GameObject.Find("Dropdown").GetComponent<UnityEngine.UI.Dropdown>().ClearOptions();
        GameObject.Find("Target").GetComponent<Mouse_drag>().positions.Clear();
        GameObject.Find("CoordX").GetComponent<UnityEngine.UI.InputField>().text = "";
        GameObject.Find("CoordY").GetComponent<UnityEngine.UI.InputField>().text = "";
        GameObject.Find("CoordZ").GetComponent<UnityEngine.UI.InputField>().text = "";

    }
}
