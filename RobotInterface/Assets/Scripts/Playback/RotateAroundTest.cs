using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundTest : MonoBehaviour
{
 GameObject ur5;
public void test(){
        ur5 = GameObject.Find("ur5");
        ur5.transform.RotateAroundLocal(new Vector3(0,1,0),50);
    }


    
        
    
}
