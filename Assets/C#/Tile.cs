using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

	public float speed = 4.0f;

	public Rigidbody rigid_body;

	public BoxCollider box_collider;

	public Tile nextTile;

	public static Tile tail;

	public bool repeatable = true;

	//float zOffset = 3.2f;


	void Awake() {

		if(nextTile == null)
			tail = this;

		if (rigid_body != null) {
			rigid_body.useGravity = false;
			rigid_body.isKinematic = true;
		}
	}

	/*
    void Update()
    {


		if(rigid_body != null && rigid_body.transform.position.y < -40) {

			Destroy(rigid_body.gameObject);


		}



		if (GameManager.manager != null && GameManager.manager.playerCharacter != null) {



			if (box_collider != null && rigid_body != null) {

				if (GameManager.manager.playerCharacter.transform.position.y > -10)
					transform.Translate (-Vector3.forward * GameManager.manager.playerCharacter.currentSpeed * Time.deltaTime);

				if(rigid_body.useGravity) {


				}

				if(GameManager.manager.playerCharacter.transform.TransformPoint (this.transform.position).z < -15)
				{

					//Debug.Log("Destroying: "+this.gameObject.name);
					Destroy(this.gameObject);
				}

				if (!rigid_body.useGravity && GameManager.manager.playerCharacter.transform.TransformPoint (this.transform.position).z < -4) {

					GameObject temp = null;

					if (tail != null) {

						temp = GameObject.Instantiate (FloorRoster.roster.RandomFloor (),
							new Vector3 ((float)Mathf.RoundToInt(tail.transform.position.x),
								(float)Mathf.RoundToInt(tail.transform.position.y),
								(float)Mathf.RoundToInt(tail.transform.position.z) + zOffset),
							                  Quaternion.identity) as GameObject;

						Debug.Log((float)Mathf.RoundToInt(tail.transform.position.z) + zOffset);

						tail = temp.GetComponent<Tile> ();


					} else {

						temp = GameObject.Instantiate (FloorRoster.roster.RandomFloor (),
							new Vector3 ((float)Mathf.RoundToInt(transform.position.x),
								(float)Mathf.RoundToInt(transform.position.y),
								(float)Mathf.RoundToInt(transform.position.z) + box_collider.size.z),
							                  Quaternion.identity) as GameObject;

						Debug.Log((float)Mathf.RoundToInt(tail.transform.position.z) + box_collider.size.z);


						tail = temp.GetComponent<Tile> ();


					}

					//float zOffset = box_collider.size.z;


					if(FloorRoster.roster.torchesLast == false && FloorRoster.roster.torches != null) {

						GameObject torches = GameObject.Instantiate (FloorRoster.roster.torches,
							new Vector3 ((float)Mathf.RoundToInt(transform.position.x),
								(float)Mathf.RoundToInt(transform.position.y),
								(float)Mathf.RoundToInt(transform.position.z) + zOffset),
							Quaternion.identity) as GameObject;

						torches.transform.parent = tail.transform;

						torches.transform.localPosition = Vector3.zero;

						Debug.Log((float)Mathf.RoundToInt(tail.transform.position.z) + box_collider.size.z);

					}

					FloorRoster.roster.torchesLast = !FloorRoster.roster.torchesLast;

					rigid_body.isKinematic = false;
					rigid_body.useGravity = true;


				} 
			}
		}
    }*/
}
