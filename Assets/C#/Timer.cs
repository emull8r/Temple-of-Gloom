using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public Text timerText;

	public Text totalTimeText;

	public Text longestTimeText;

	public static Timer timer;


	void Awake() {

		timer = this;


	}

	void Update() {


		if(GameManager.manager != null) {

			if(timerText != null && !GameManager.manager.gameOver) {


				int minutes = ((int)GameManager.manager.totalTime) / 60;

				int seconds = ((int)GameManager.manager.totalTime) % 60;

				int hours = minutes / 60;

				timerText.text = ""+hours.ToString()+":"+minutes.ToString("D2")+":"+seconds.ToString("D2");
				

			}

			if(totalTimeText != null && GameManager.manager.gameOver) {



				int minutes = ((int)GameManager.manager.totalTime) / 60;

				int seconds = ((int)GameManager.manager.totalTime) % 60;

				int hours = minutes / 60;

				totalTimeText.text = ""+hours.ToString()+":"+minutes.ToString("D2")+":"+seconds.ToString("D2");

			}

			if(longestTimeText != null && GameManager.manager.gameOver) {

				int minutes = ((int)GameManager.manager.longestTime) / 60;

				int seconds = ((int)GameManager.manager.longestTime) % 60;

				int hours = minutes / 60;

				longestTimeText.text = ""+hours.ToString()+":"+minutes.ToString("D2")+":"+seconds.ToString("D2");



			}

		}
	}
}
