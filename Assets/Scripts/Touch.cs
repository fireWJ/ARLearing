using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour {

    private bool isTap;//是否第一次点击屏幕
    private float touchTime;//点击屏幕的时间
                            
    void Update () {
        TounchTap();
        StayTap();
    }
    private void TounchTap()//交互点击屏幕
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray,out hit))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)//如果第一次点击屏幕并且手指个数为1
                {
                    if (Input.GetTouch(0).tapCount == 2)//如果点击次数为2
                    {
                        //Todo交互的操作
                        Destroy(hit.collider.gameObject);
                    }          
                }
            } 
        }   
    }
    private void StayTap()//交互长按屏幕
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit)&& hit.collider.gameObject.tag == "Player")
        {
            if (Input.touchCount == 1)//一个手指点击屏幕
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    isTap = true;
                    touchTime = Time.time;//如果为第一次点击屏幕则开始记录时间
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Stationary)//如果点击屏幕保持手指不懂
                {
                    touchTime = Time.time - touchTime;
                    if (touchTime > 2 && isTap == true)//如果为第一次迪点击屏幕并且时间大于2秒
                    {
                        //Todo交互的操作
                        Destroy(hit.collider.gameObject);
                    }
                }
                else
                {
                    isTap = false;
                }
            }
        }

    }
}
