using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    Dropdown dropdown;
    public int count = 0;
    public int size = 0;
    public bool isPressed = false;
    public bool playOnRobot = false;
    
    void Start()
    {
        dropdown = GameObject.Find("Dropdown").GetComponent<UnityEngine.UI.Dropdown>();
        target = GameObject.Find("Target");
    }

    public void FixedUpdate()
    {
        count++;
        if (isPressed)
        {


            int index;
            if (count%150 == 0)
            {
                index = count/150;
                if(index<=size){
                target.transform.position = positions[index-1];
                dropdown.value = index-1;
                }
                

                //  Additionally, play on real robot
                //  NOTE:  This is too fast because the robot command is non-blocking....
                //  Need to insert a delay but didn't want to mess up your playback function yet
                /*if (playOnRobot)
                {
                    print("Setting pose on physical robot...");
                    GameObject.Find("Cmd2").GetComponent<Button>().onClick.Invoke();
                }*/
                if(index>size){
                isPressed = false;
                print("Playback Complete");
                }
            }
            

            
        }
        
    }


    public void Pressed()
    {
        positions = target.GetComponent<Mouse_drag>().positions;
        // New - robot playback toggle
        playOnRobot = GameObject.Find("RobotPlayback").GetComponent<Toggle>().isOn;

        size = positions.Count;
        if (size == 0)
        {
            print("Not Enough Points");
            return;
        }

        isPressed = true;
        count = 0;


    }

}