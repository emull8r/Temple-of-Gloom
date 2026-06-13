using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpecificEnding : MonoBehaviour
{
	public GameObject[] prefabs;

	void Update() {

		if(GameManager.manager != null) {

			if(GameManager.manager.chosenCharacterIndex < prefabs.Length) {

				GameObject temp = GameObject.Instantiate(prefabs[GameManager.manager.chosenCharacterIndex],
					transform.position, Quaternion.identity);



			}

			Destroy(this.gameObject);

		}

        
    }
}
