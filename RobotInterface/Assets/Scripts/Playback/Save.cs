using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
public class Save : MonoBehaviour
{
public UnityEngine.UI.Dropdown dropdown;

public void Pressed(){   
dropdown = GameObject.Find("Dropdown").GetComponent<UnityEngine.UI.Dropdown>();    
string path = EditorUtility.OpenFolderPanel("Select Save Folder", "", "");
FileStream fs = File.Create(path);
for (int i = 0; i <dropdown.options.Count; i++)
{
    byte[] info = new UTF8Encoding(true).GetBytes(dropdown.options[i].text+"\n");
    fs.Write(info,0,info.Length);
}
}
}
