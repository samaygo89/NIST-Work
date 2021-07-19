using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicked : MonoBehaviour
{
    private bool isInteger(float num)
    {

        if(num == (int)num && num>=1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public List<Vector3> positions = new List<Vector3>();
    public GameObject target;
    

    public int count = 0;
    public int size = 0;
    public bool pressed = false;

    public void FixedUpdate()
    {
        
        if (pressed)
        {
            
            
            if (isInteger(count / 150) && count/150<=size)
            {
                int index = count / 150;

                    target.transform.position = positions[index-1];
                    
            }
            count++;
        }
        
    }


    public void Pressed()
    {
       
        target = GameObject.Find("Target");
        positions = GameObject.Find("Target").GetComponent<Mouse_drag>().positions;
        size = positions.Count;
        if (size == 0)
        {
            print("Not Enough Points");
        }
        pressed = true;
        count = 0;
        

    }

}