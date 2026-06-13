using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUI : MonoBehaviour
{

	public static GUI levelUI;

	public Text rubiesCount, highScore;

	public GameObject[] characters;

	public RectTransform[] moveRectTransforms;
	public RectTransform[] menuPages;
	public RectTransform currentRectTransform, menu, tutorial;

	private int currentLane;

	public Slider musicSlider;
	public Slider soundFXSlider;
	public Slider qualitySlider;

	public Toggle tutorialToggle;

	public Text loadingText;


	void Awake() {

		levelUI = this;


	}

	void Start() {

		if(characters != null && characters.Length > 0) {
			foreach(GameObject g in characters) {
				if(g != null) {
					g.transform.localScale = new Vector3(g.transform.localScale.x*Screen.height/6,
						g.transform.localScale.y*Screen.height/6,
						g.transform.localScale.z*Screen.height/6);

					g.transform.localPosition = new Vector3(0,0-Screen.height/6,-g.transform.localScale.z);
				}
			}
		}


		if(GameManager.manager != null) {

			if(tutorial != null && currentRectTransform != tutorial) {

				if(GameManager.manager.showTutorial) {

					menu.gameObject.SetActive(false);

					tutorial.gameObject.SetActive(true);

					currentRectTransform = tutorial;

					UpdateMoveRectTransforms();

					GameManager.manager.showTutorial = false;
				} else {


					Pause();
				}
			}


			if(musicSlider != null) {

				musicSlider.value = GameManager.manager.musicVolume;

			}

			if(soundFXSlider != null) {

				soundFXSlider.value = GameManager.manager.soundFXVolume;

			}

			if(qualitySlider != null) {

				qualitySlider.value = GameManager.manager.qualityLevel;

			}

		}

	}

	void Update() {

		DoUpdate();

		UpdateMoveRectTransforms();
	}



	public void LoadLevel(int levelIndex) {

		if(loadingText != null)
			loadingText.gameObject.SetActive(true);

		if(GameManager.manager != null)
			GameManager.manager.SetLoadedLevel(true);

		if(levelIndex < 0) {

			Application.Quit();
		}
		else {
			try {
			SceneManager.LoadScene(levelIndex);
				Time.timeScale = 1;
			} catch (System.Exception e) {

				Debug.Log(e);
			}
		}
	}

	public void DoUpdate() {

		if(GameManager.manager == null) {

			Debug.Log("GameManager.manager is null");
		}

		if(rubiesCount != null && GameManager.manager != null) {

			rubiesCount.text = ""+GameManager.manager.GetRubies();

		}

		if(highScore != null && GameManager.manager != null) {

			highScore.text = ""+GameManager.manager.GetHighScore();
		}

		if(GameManager.manager != null && GameManager.manager.gameOver) {

			currentRectTransform = null;
		}

		if(musicSlider != null) {

			if(GameManager.manager.musicVolume != musicSlider.value) {

				GameManager.manager.musicVolume = musicSlider.value;

				GameManager.manager.SaveVolume();
			}
		}

		if(soundFXSlider != null) {

			if(GameManager.manager.soundFXVolume != soundFXSlider.value) {

				GameManager.manager.soundFXVolume = soundFXSlider.value;

				GameManager.manager.SaveVolume();
			}
		}

		if(qualitySlider != null) {


			if(GameManager.manager.qualityLevel != (int)qualitySlider.value) {

				GameManager.manager.qualityLevel = (int)qualitySlider.value;

				GameManager.manager.SaveQuality();
			}

		}
	}

	public void UpdatePage(int index) {

		if(index < menuPages.Length) {

			if(currentRectTransform != null)
				currentRectTransform.gameObject.SetActive(false);

			foreach(RectTransform page in menuPages) {

				page.gameObject.SetActive(false);

			}

			currentRectTransform = menuPages[index];

			currentRectTransform.gameObject.SetActive(true);

		
		}
	}

	void UpdateMoveRectTransforms() {

		if(moveRectTransforms != null) {
			foreach(RectTransform p in moveRectTransforms) {

				if(p != currentRectTransform) {

					p.gameObject.SetActive(false);

				} else {

					p.gameObject.SetActive(true);

				}

			}
		}
	}

	public void SwitchLane(int newLane) {

		if(GameManager.manager != null && GameManager.manager.playerCharacter != null 
			&& newLane < moveRectTransforms.Length &&
			GameManager.manager.playerCharacter.GetComponent<CharacterController>().isGrounded) {

			currentRectTransform = moveRectTransforms[newLane];

			GameManager.manager.playerCharacter.SwitchLane(newLane);

			UpdateMoveRectTransforms();

			currentLane = newLane;
		}

	}

	public void DoJump() {

		if(GameManager.manager != null && GameManager.manager.playerCharacter != null) {

			GameManager.manager.playerCharacter.Jump();

		}

	}

	public void PlayAgain(int levelIndex) {

		if(GameManager.manager != null) {

			GameManager.manager.gameOver = false;

			GameManager.manager.Save();

			GameManager.manager.ClearRubies();

			GameManager.manager.ClearTime();

			LoadLevel(levelIndex);
		}
	}


	public void CloseTutorial() {

		if(tutorial != null) {

			if(GameManager.manager != null) {

				GameManager.manager.showTutorial = false;
			}

			currentRectTransform.gameObject.SetActive(false);

			Pause();

		}
	}

	public void Pause() {

		if(GameManager.manager != null) {

			GameManager.manager.Pause();


			if(menu != null) {

				if(Time.timeScale == 0) {

					menu.gameObject.SetActive(true);

					currentRectTransform = menu;

					UpdateMoveRectTransforms();
				

				} else if(Time.timeScale == 1) {

					menu.gameObject.SetActive(false);

					currentRectTransform = moveRectTransforms[currentLane];

					UpdateMoveRectTransforms();
				
				}
			}
		}
	}

	public void ToggleTutorial() {

		if(tutorialToggle != null) {

			if(tutorialToggle.isOn) {

				PlayerPrefs.SetInt("tut",1);
			} else {

				PlayerPrefs.SetInt("tut",0);
			}
		}
	}

	// NOTE: Will lose high score
	public void ResetSettings() {

		PlayerPrefs.DeleteAll();

		GameManager.manager.musicVolume = 1.0f;

		GameManager.manager.soundFXVolume = 1.0f;


		if(musicSlider != null) {

			musicSlider.value = GameManager.manager.musicVolume;

		}

		if(soundFXSlider != null) {

			soundFXSlider.value = GameManager.manager.soundFXVolume;

		}

		PlayerPrefs.SetFloat("musv",GameManager.manager.musicVolume);

		PlayerPrefs.SetFloat("fxv",GameManager.manager.soundFXVolume);

	}
}
