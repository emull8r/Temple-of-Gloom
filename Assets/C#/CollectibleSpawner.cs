using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CollectibleSpawner : MonoBehaviour
{
	public GameObject collectible;

	public float radius = 4.0f;

	public float spawnDistance = 2.0f;

	public int maxSpawn = 1;
	public int minSpawn = 0;

	void Start() {


		StartCoroutine(Spawn());


	}


	IEnumerator Spawn() {

		int spawnCount = Random.Range(minSpawn, maxSpawn+1);

		/*
		Vector3 pos = Vector3.zero;

		List<Vector3> oldPositions = new List<Vector3>();

		for(int i = 0; i < spawnCount; i++) {

			bool foundRandom = false;

			while(!foundRandom) {

				pos = new Vector3(Random.Range(transform.position.x-radius,transform.position.x+radius),
					Random.Range(transform.position.y-radius,transform.position.y+radius),
					Random.Range(transform.position.z-radius,transform.position.z+radius));

				if(oldPositions.Count(x => Vector3.Distance(x,pos) <= spawnDistance) == 0) {

					foundRandom = true;
				}


				yield return new WaitForFixedUpdate();

			}

			GameObject temp = GameObject.Instantiate(collectible, pos, Quaternion.identity) as GameObject;

			temp.transform.parent = this.transform.parent;

			oldPositions.Add(pos);
		}*/


		if(spawnCount >= maxSpawn-1) {

			GameObject temp = GameObject.Instantiate(collectible, transform.position, Quaternion.identity) as GameObject;

			temp.transform.parent = this.transform.parent;

		}



		yield return new WaitForFixedUpdate();


	}



	void OnDrawGizmos() {

		Gizmos.color = Color.red;


		Gizmos.DrawWireSphere(transform.position, radius);


	}


}
