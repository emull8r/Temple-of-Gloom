using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileManager : MonoBehaviour
{

	public GameObject player;

	public static TileManager manager;

	public List<Tile> tiles;

	public float speed = 2.0f;

	public float destroyDistance = 30.0f;

	public float fallDistance = 15.0f;

	public float zOffset = 3.2f;

	public LayerMask m_layerMask;

	public Vector3 boxPosition;

	public void Start() {

		manager = this;

		if(player == null) {

			player = GameObject.FindWithTag("Player");

			Debug.Log("found player");

			speed = player.GetComponent<Character>().forwardSpeed;
		}
	}


	public void FixedUpdate() {

		if(player != null && player.transform.position.y > -player.GetComponent<Character>().zThreshold) {


			for(int i = 0; i < tiles.Count; i++) {

				if(i == 0) {

					tiles[i].transform.Translate(-1.0f * Vector3.forward * speed * Time.fixedDeltaTime);

				} else {
					tiles[i].transform.position = new Vector3(tiles[i-1].transform.position.x,
						tiles[i-1].transform.position.y,
						tiles[i-1].transform.position.z + zOffset);
				}


					
			}

			if(tiles[0].transform.position.z < player.transform.position.z &&
				Mathf.Abs(tiles[0].transform.position.z-player.transform.position.z) >= fallDistance) {

				tiles[0].rigid_body.isKinematic = false;
				tiles[0].rigid_body.useGravity = true;

			}

			if(tiles[0].transform.position.z < player.transform.position.z &&
				Mathf.Abs(tiles[0].transform.position.z-player.transform.position.z) >= destroyDistance) {

				Destroy(tiles[0].gameObject);

				tiles.RemoveAt(0);

			}

			Collider[] intersectingTiles = Physics.OverlapBox(boxPosition, new Vector3(7.5f,2.0f,3.2f)/2);

			if(intersectingTiles.Length == 0) {

				GameObject temp = GameObject.Instantiate(FloorRoster.roster.RandomFloor(),boxPosition,Quaternion.identity,null);

				tiles.Add(temp.GetComponent<Tile>());

				if(FloorRoster.roster.torchesLast == false && FloorRoster.roster.torches != null) {

					GameObject torches = GameObject.Instantiate (FloorRoster.roster.torches,
						temp.transform.position,
						Quaternion.identity) as GameObject;

					torches.transform.parent = temp.transform;

					torches.transform.localPosition = Vector3.zero;


				}

				FloorRoster.roster.torchesLast = !FloorRoster.roster.torchesLast;
			}
		}
	}


	public void OnDrawGizmos() {

		Gizmos.color = Color.red;


		Gizmos.DrawWireCube(boxPosition, new Vector3(7.5f,2.0f,3.2f));

	}
}
