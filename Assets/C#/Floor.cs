using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

	protected bool createdNew = false;
	protected GameObject instantiated;
	protected float fallSpeed = 2.0f;
	protected float turnSpeed = 20.0f;
	protected float offset = 4.8f;
	protected int countDown = 10; 

	void OnTriggerStay(Collider other) {
		if(other.tag == "Player") {
			if(!createdNew) {
				instantiated = GameObject.Instantiate(FloorRoster.roster.RandomFloor(), new Vector3(transform.position.x,
				                                                    transform.position.y,
				                                                    transform.position.z+offset),Quaternion.identity) as GameObject;

				instantiated.name = gameObject.name;

			}
			if(instantiated != null) {
				createdNew = true;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if(other.tag == "Player") {
			StartCoroutine(Falling ());
		}
	}

	IEnumerator Falling() {
		int zero = 0;
		while(zero < countDown) {
			transform.Rotate(Vector3.right * -turnSpeed * Time.deltaTime);
			transform.Translate(Vector3.up * -fallSpeed * Time.deltaTime);
			zero++;
			yield return new WaitForEndOfFrame();
		}
		Destroy(gameObject);
	}
}
