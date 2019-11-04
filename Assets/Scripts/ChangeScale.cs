using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour {

    private Vector2 oldPos1;//第一根手指开始的位置
    private Vector2 oldPos2;
    private Vector2 newPos1;
    private Vector2 newPos2;
	
	// Update is called once per frame
	void Update () {
        IsChangeScale();

    }
    private void IsChangeScale()//改变大小交互
    {
        if (Input.touchCount == 2)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)//如果其中有一个手指在移动
            {
                newPos1= Input.GetTouch(0).position;
                newPos2 = Input.GetTouch(1).position;
                if (IsChangeDistance(oldPos1, oldPos2, newPos1, newPos2))
                {
                    float changeScale = transform.localScale.x*1.025f;
                    transform.localScale = new Vector3(changeScale, changeScale, changeScale);
                }
                else
                {
                    float changeScale = transform.localScale.x / 1.025f;
                    transform.localScale = new Vector3(changeScale, changeScale, changeScale);
                }
                oldPos1 = newPos1;
                oldPos2 = newPos2;
            }
        }
    }
    private bool IsChangeDistance(Vector2 oP1,Vector2 oP2,Vector2 nP1,Vector2 nP2)//判断两个手指的前后变化距离
    {
        float oldDistance = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        float newDistance= Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));
        if (oldDistance < newDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
