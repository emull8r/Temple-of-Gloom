using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
[RequireComponent (typeof(Rigidbody))]
public class Collectible : MonoBehaviour
{
	//NOTE: Set "isTrigger" to true

	public float rotationSpeed = 35.0f;

	public AudioSource collectSound;

	protected bool collected = false;

	void OnTriggerEnter(Collider other) {

		if(GameManager.manager != null && other.transform.root.gameObject.tag == "Player" && !collected) {

			collected = true;

			GameManager.manager.AddCollectibleToScore(this);

			if(RubyCounter.counter != null)
				RubyCounter.counter.UpdateCount();

			if (collectSound != null) {

				collectSound.Play ();
			}

			StartCoroutine(Expire());

		}


	}


	void Update() {

		if (!GetComponent<SphereCollider> ().isTrigger) {
			GetComponent<SphereCollider> ().isTrigger = true;
		}

		transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

		if(transform.parent != null && transform.parent.position.y < -1) {

			GetComponent<SphereCollider>().isTrigger = false;
			GetComponent<Rigidbody>().isKinematic = false;
			GetComponent<Rigidbody>().useGravity = true;
			transform.parent = null;

		}

		if(transform.position.y < -20) {

			Destroy(this.gameObject);
		}

	}

	IEnumerator Expire() {

		// do or play some kind of affect


		yield return new WaitForSeconds(0.1f);

		Destroy(this.gameObject);


	}


}
