using UnityEngine;
using System.Collections;

public class TrapDoor : MonoBehaviour {

	public Animator animate;
	public AudioSource sound;
	protected bool startFall;
	public static float fallWait = 0.0f;
	public BoxCollider boobyTrap;
	public BoxCollider doorPart;
	public bool startFallen = false;

	void Start() {

		if(startFallen) {

			animate.SetTrigger("touched");
		} else {


			StartCoroutine(Door());
		}
	}

	IEnumerator Door() {
		
		while(this.gameObject != null) {
			if(GameManager.manager != null && 
				GameManager.manager.playerCharacter != null &&
				GameManager.manager.playerCharacter.gameObject.GetComponent<CharacterController>().bounds.Intersects(boobyTrap.bounds)) {

				animate.Play("TrapDoorFunction");

				if(doorPart != null) {
					doorPart.enabled = false;
				}
				if(sound != null) {

					sound.Play();
				}

				GameManager.manager.playerCharacter.forwardSpeed = 0.0f;

				GameManager.manager.playerCharacter.currentSpeed = 0.0f;

				//GameManager.manager.playerCharacter.GameOver();
			}
			yield return new WaitForFixedUpdate();
		}
	}
}
