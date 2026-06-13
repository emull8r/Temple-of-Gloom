using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public Animator[] characters;

	public int gameLevelIndex = 1;


	public void Select(string characterName) {

		Animator found = characters.First(x => x.gameObject.name == characterName);

		foreach(Transform child in found.gameObject.transform.parent.parent) {

			if(child != found.gameObject.transform.parent)
				child.gameObject.SetActive(false);
		}

		foreach(Transform child in found.gameObject.transform.parent.parent.parent) {

			if(child != found.gameObject.transform.parent.parent)
				child.gameObject.SetActive(false);
		}

		if(found != null) {

			found.Play("Selected");
		}

		if(GameManager.manager != null) {


			GameManager.manager.SetCharacter(characters.ToList().IndexOf(found));

			GameManager.manager.ClearRubies();

			GameManager.manager.ClearTime();
		}

		LoadLevel(gameLevelIndex);

	}


	public void LoadLevel(int index) {

		if(GameManager.manager != null) {

			Debug.Log("Loading level");

			GameManager.manager.LoadLevel(index);
		}
	}




}
