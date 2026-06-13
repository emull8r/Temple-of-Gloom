using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class Character : MonoBehaviour {

	public static Character character;

	public Animator animate;

	public float strafeSpeed = 5.0f;
	public float forwardSpeed = 4.0f;
	public float currentSpeed = 0.0f;
	public float jumpHeight = 10.0f;
	public float gravity = 5.0f;
	public float zThreshold =  10.0f;

	protected Vector3 motion;
	protected float ySpeed = 0.0f;
	protected float lastMouseX;
	public bool gameOver = false;

	public Vector3[] lanes; // local positions
	public int currentLane = 0;

	public AudioClip jumpNoise;
	public AudioClip fallNoise;

	public AudioSource soundSource;

	void Start() {
		character = this;

		currentSpeed = forwardSpeed;

		for(int i = 0; i < lanes.Length; i++) {

			lanes[i] = transform.TransformPoint(lanes[i]); // convert local to world
		}

		StartCoroutine(GetUnstuck());
	}


	IEnumerator GetUnstuck() {

		Vector3 tilePos;

		while(gameOver == false) {

			tilePos = TileManager.manager.tiles[0].transform.position;

			yield return new WaitForSeconds(1.0f);

			if(tilePos == TileManager.manager.tiles[0].transform.position) {

				Debug.Log("Getting unstuck!");

				motion.z = -forwardSpeed;
			}
		}
	}

	public void GameOver() {

		gameOver = true;

		if (fallNoise != null && soundSource != null) {

			soundSource.clip = fallNoise;

			soundSource.Play ();
		}

		Debug.Log("GAME OVER");

		GameManager.manager.Save();

		GameManager.manager.GameOver();
	}

	void Update() {

		// Set Animation
		
		animate.SetBool("Grounded", GetComponent<CharacterController>().isGrounded);




		if(transform.position.y <= (-1 * zThreshold) &&
			!GetComponent<CharacterController>().isGrounded &&
			GameManager.manager != null && !gameOver) {

			GameOver();
		}


			



		// Set Y

		if(Input.GetButtonDown("Jump") && GetComponent<CharacterController>().isGrounded) {
			


			ySpeed = jumpHeight;
		}




		ySpeed -= gravity * Time.smoothDeltaTime;

		motion.y = ySpeed;

		// Set X

		motion.x = XMovement() * strafeSpeed;

		 // Set Z

		//motion.z = forwardSpeed;

		GetComponent<CharacterController>().Move(motion * Time.fixedDeltaTime);

		if(Mathf.Abs(transform.position.x-lanes[currentLane].x) < 0.5f) {



			transform.position = Vector3.Lerp(transform.position,new Vector3(lanes[currentLane].x,
				transform.position.y,
				transform.position.z), strafeSpeed * Time.deltaTime);

		}


		// We are on a continuous treadmill. We need to be able to seamlessly teleport ourselves a few steps back.
		/*
		if(transform.position.z >= zThreshold) {
			transform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
		}*/

	}

	float XMovement() {


		float returnVal = 0.0f;
		/*
		//First check mouse position
		if(Input.GetMouseButton(0)) {
			if(Input.mousePosition.x < lastMouseX) {
				returnVal = 1.0f;
			} else if(Input.mousePosition.x > lastMouseX) {
				returnVal = -1.0f;
			} else {
				returnVal = 0.0f;
			}
			lastMouseX = Input.mousePosition.x;
		}
		// If that don't work use arrow keys
		if(returnVal == 0.0f) {
			returnVal = Input.GetAxis("Horizontal") * -1;
		}

		*/

		if(GetComponent<CharacterController>().isGrounded) {

			if(Mathf.RoundToInt(transform.position.x-lanes[currentLane].x) != 0) {
				if(transform.position.x < lanes[currentLane].x) {

					returnVal = 1.0f;

				} else if(transform.position.x > lanes[currentLane].x) {

					returnVal = -1.0f;

				}
			}

		}

		return returnVal;



	}


	public void Jump() {

		if(GetComponent<CharacterController>().isGrounded) {




			if (jumpNoise != null && soundSource != null) {

				soundSource.clip = jumpNoise;
			

				soundSource.Play ();
			}


			ySpeed = jumpHeight;

		}
	}

	public void SwitchLane(int newLane) {


		currentLane = newLane;
	}

	public void OnDrawGizmos() {

		Gizmos.color = Color.green;

		foreach(Vector3 v in lanes) {

			Gizmos.DrawSphere(v,1.1f);

		}

	}
}
