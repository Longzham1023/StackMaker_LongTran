using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobleInput : MonoBehaviour
{
    public static MobleInput Instance { set; get; }
    public bool tap , swipeLeft, swipeRight, swipeUp, swipeDown;
    public Vector2 swipeDelta, startTounch;
    private const float deadZone = 100;

    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;   
    }

    // Update is called once per frame
    private void Update()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;
        //Nhan click chuot tu man hinh//
        if(Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTounch = Input.mousePosition;
        }else if (Input.GetMouseButtonUp(0))
        {
            startTounch = swipeDelta = Vector2.zero;
        }

        swipeDelta = Vector2.zero;
        //Khi nhan huong vector tinh do dai//
        if(startTounch != Vector2.zero)
        {
            if(Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startTounch;
            }else if(Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTounch;
            }
        }

        //Tinh vach dich//
        if(swipeDelta.magnitude > deadZone)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                if(x<0)
                {
                    swipeLeft = true;
                }
                else
                {
                    swipeRight = true;
                }
            }
            else
            {
                if(y < 0)
                {
                    swipeDown = true;
                }
                else
                {
                    swipeUp = true;
                }
            }

            startTounch = swipeDelta = Vector2.zero ;
        }
    }
}
