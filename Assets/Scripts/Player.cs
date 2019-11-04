using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (transform.localPosition.y >0)
        {
            return;
        }
        transform.Translate(new Vector3(0, 0.5f, 0) * Time.deltaTime);
    }
    private void PlayerUp()
    {
        if (this.transform.position.y > 2.3f)
        {
            return;
        }
        Debug.Log(this.transform.position);
        this.transform.position = Vector3.Lerp(this.transform.localPosition , this.transform.localPosition+new Vector3(0,1,0), 0.1f * Time.deltaTime);
    }
}
