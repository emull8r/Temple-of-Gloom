using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public Character[] characters;

	public Character playerCharacter;

	public static GameManager manager;

	public bool loadedLevel = false;
	public bool showTutorial = true;

	public int chosenCharacterIndex = 0;
	protected int highScore = 0;
	protected int rubies = 0;

	public float totalTime = 0.0f;

	public float longestTime = 0.0f;

	public bool gameOver = false;

	public bool muted = false;

	public float musicVolume = 1.0f;

	public float soundFXVolume = 1.0f;

	public int qualityLevel = 2;

	public int maxQualityLevel = 5;

	public string musicVolumeString;

	public string soundFXString;

	public string highScoreString;

	public string longestTimeString;

	public string qualityString;

	void Start() {

		musicVolume = PlayerPrefs.GetFloat(musicVolumeString,musicVolume);

		soundFXVolume = PlayerPrefs.GetFloat(soundFXString,soundFXVolume);

		// set volume

		SetVolumes();

		qualityLevel = PlayerPrefs.GetInt(qualityString,qualityLevel);

		// set quality

		SetQualityLevel();


		highScore = PlayerPrefs.GetInt(highScoreString,highScore);

		longestTime = PlayerPrefs.GetFloat(longestTimeString,longestTime);

		if(PlayerPrefs.GetInt("tut",0) > 0) {

			showTutorial = false;

		} else {

			showTutorial = true;

		}


	}

	void Awake() {

		DontDestroyOnLoad(this.gameObject);
		SceneManager.sceneLoaded += ManagerStart;


	}

	void Update() {



		int mCount = 0;

		foreach(GameManager m in GameObject.FindObjectsOfType(typeof(GameManager))) {


			mCount++;
		}

		if(mCount == 1) {

			manager = this;
		}

		if(!gameOver && loadedLevel && playerCharacter != null) {
			totalTime += Time.deltaTime;
		}


		Save();



		if(playerCharacter != null && playerCharacter.gameOver) {

			musicVolume = 0.0f;

			foreach(GameObject src in GameObject.FindGameObjectsWithTag("MusicAudio")) {

				if(src.GetComponent<AudioSource>() != null)
					src.GetComponent<AudioSource>().Stop();
			}
		}


		SetVolumes();


	}

	void ManagerStart(Scene scene, LoadSceneMode mode) {




		int mCount = 0;

		foreach(GameManager m in GameObject.FindObjectsOfType(typeof(GameManager))) {

			mCount++;

			if(m.loadedLevel && m == this) {

				manager = this;

			} else if (!m.loadedLevel && m != this) {

				if(m.playerCharacter != null) {

					Destroy(m.playerCharacter.gameObject);
				}

				Destroy(m.gameObject);

			}
		}

		if(mCount == 1) {

			manager = this;
		}


		if(manager == this && GameObject.Find("PlayerSpawn") != null) {

			if(playerCharacter != null) {

				Destroy(playerCharacter.gameObject);
			}

			if(this != null) {

				manager = this;

				playerCharacter = GameObject.Instantiate(characters[chosenCharacterIndex],
					GameObject.Find("PlayerSpawn").transform.position,Quaternion.identity) as Character;

				if(Camera.main != null) {

					if(Camera.main.GetComponent<RunnerCam>() != null) {

						Camera.main.GetComponent<RunnerCam>().Setup(playerCharacter);

					} else {

						Camera.main.transform.parent = playerCharacter.transform;

						Camera.main.transform.localRotation = Quaternion.Euler(new Vector3(30,180,0));

						Camera.main.transform.localPosition = new Vector3(0,4,10);
					}
				}

				/*
				if(showTutorial) {

					Pause();

				}*/

			}

		}




	}


	public void SetLoadedLevel(bool value) {

		loadedLevel = value;
	}

	public void SetCharacter(int index) {

		chosenCharacterIndex = index;
	}

	public void AddCollectibleToScore(Collectible c) {

		rubies = Mathf.Clamp(rubies+1,0,int.MaxValue);


	}

	public int GetRubies() {

		return rubies;
	}

	public void ClearRubies() {

		rubies = 0;
	}

	public void ClearTime() {

		totalTime = 0.0f;

	}

	public int GetHighScore() {

		return highScore;
	}


	public void Save() {

		if(rubies > highScore && highScore >= PlayerPrefs.GetInt(highScoreString,0)) {

			highScore = rubies;

			PlayerPrefs.SetInt(highScoreString,highScore);


			PlayerPrefs.Save();
		}

		if(totalTime > longestTime && longestTime >= PlayerPrefs.GetFloat(longestTimeString,0.0f)) {

			longestTime = totalTime;

			PlayerPrefs.SetFloat(longestTimeString,longestTime);

			PlayerPrefs.Save();

		}



	}

	public void GameOver() {

		gameOver = true;

		StartCoroutine(GameOverNumerator());

	}

	IEnumerator GameOverNumerator() {


		yield return new WaitForSeconds(3.0f);



		SceneManager.LoadScene("GameOver");
	}



	public void LoadLevel(int index) {

		loadedLevel = true;

		gameOver = false;

		StartCoroutine(LevelNumerator(index));
	}

	IEnumerator LevelNumerator(int index) {

		Debug.Log("Loading level ...");


		yield return new WaitForSeconds(4.0f);



		SceneManager.LoadScene(index);

		Debug.Log("Loaded level!");
	}

	public void Pause() {

		// pull up menu



		if(Time.timeScale == 1) {

			Time.timeScale = 0;

		} else {

			Time.timeScale = 1;

		}



	}

	public void SaveVolume() {

		PlayerPrefs.SetFloat(musicVolumeString,musicVolume);

		PlayerPrefs.SetFloat(soundFXString,soundFXVolume);

		PlayerPrefs.Save();

		SetVolumes();

	}


	public void SaveQuality() {


		PlayerPrefs.SetInt(qualityString,qualityLevel);

		SetQualityLevel();

		PlayerPrefs.Save();

	}

	public void SetVolumes() {

		if(muted) {

			foreach(GameObject src in GameObject.FindGameObjectsWithTag("MusicAudio")) {

				if(src.GetComponent<AudioSource>() != null)
					src.GetComponent<AudioSource>().volume = 0;
			}

			foreach(GameObject src in GameObject.FindGameObjectsWithTag("SoundFXAudio")) {

				if(src.GetComponent<AudioSource>() != null)
					src.GetComponent<AudioSource>().volume = 0;
			}

		} else {

			foreach(GameObject src in GameObject.FindGameObjectsWithTag("MusicAudio")) {

				if(src.GetComponent<AudioSource>() != null)
					src.GetComponent<AudioSource>().volume = musicVolume;
			}

			foreach(GameObject src in GameObject.FindGameObjectsWithTag("SoundFXAudio")) {

				if(src.GetComponent<AudioSource>() != null)
					src.GetComponent<AudioSource>().volume = soundFXVolume;
			}
		}
	}


	public void SetQualityLevel() {

		QualitySettings.SetQualityLevel(qualityLevel);



	}

	public void ToggleMute() {



		muted = !muted;

		SetVolumes ();


	}
}
