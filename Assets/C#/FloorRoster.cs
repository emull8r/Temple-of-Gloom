using UnityEngine;
using System.Collections;


public class FloorRoster : MonoBehaviour {

	public GameObject[] floorSelection1;

	public GameObject[] floorSelection2;

	public GameObject torches;

	public static FloorRoster roster;

	protected int lastSelection = 2;


	public bool torchesLast = false;

	void Awake() {
		roster = this;
	}
	/*
	public void Update() {

		if (GameManager.manager != null && GameManager.manager.playerCharacter != null
			&& GameManager.manager.playerCharacter.transform.position.y > -10) {
			foreach(Tile t in Object.FindObjectsOfType(typeof(Tile))) {


				t.transform.Translate (-Vector3.forward * GameManager.manager.playerCharacter.currentSpeed * Time.deltaTime);
			}
		}
	}*/

	public GameObject RandomFloor() {

		if(lastSelection == 1) {

			lastSelection = 2;

			return floorSelection2[Random.Range (0, floorSelection2.Length)];



		} else {

			lastSelection = 1;

			return floorSelection1[Random.Range (0, floorSelection1.Length)];



		}


	}


}
