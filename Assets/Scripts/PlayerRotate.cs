using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        Rotate();

    }
    private void Rotate()//交互进行旋转
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log(123);
            if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * -150f * Time.deltaTime);
            }
        }
    }
}
