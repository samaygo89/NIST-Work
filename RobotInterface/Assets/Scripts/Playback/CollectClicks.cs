using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectClicks : MonoBehaviour
{
     public bool was_prev_clicked = false;
    LinkedList<Vector3> touches = new LinkedList<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    if(Input.GetMouseButton(0) && !was_prev_clicked)
        {
            Vector3 position = Input.mousePosition;
            touches.AddLast(position);
            print(touches.Count);
            print(position);
            print(Camera.main.pixelHeight);
            print(Camera.main.pixelWidth);

            print(Camera.main.ScreenToWorldPoint(position));

            //gameObject.transform.position = transform.TransformVector(position);
        }

     was_prev_clicked = Input.GetMouseButton(0);




    }
}
