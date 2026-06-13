using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCam : MonoBehaviour
{

	public float camHeight = 4.0f;
	public float camDistance = 10.0f;

	private Character follow;

    // Start is called before the first frame update
   	public void Setup(Character c)
    {
		follow = c;

		Camera.main.transform.rotation = Quaternion.Euler(new Vector3(30,180,0));

		Camera.main.transform.position = c.transform.TransformPoint(0,camHeight,camDistance);
    }

    // Update is called once per frame
    void Update()
    {
		if(follow != null && follow.transform.position.y > -2) {


			Camera.main.transform.position = follow.transform.TransformPoint(0,4,10);

		}
    }
}
