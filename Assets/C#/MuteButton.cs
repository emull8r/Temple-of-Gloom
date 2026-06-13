using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{

	public Sprite nonmuted;
	public Sprite muted;

	public Image replaceImage;

	void Update() {

		if(GameManager.manager != null) {

			UpdateImage();
		}
	}


	public void ToggleMute() {

		if(GameManager.manager != null) {

			GameManager.manager.ToggleMute ();

			UpdateImage();
		}

	}

	public void UpdateImage() {

		if(GameManager.manager != null) {

			if(GameManager.manager.muted && replaceImage.sprite == nonmuted) {

				replaceImage.sprite = muted;

			} else if(!GameManager.manager.muted && replaceImage.sprite == muted) {

				replaceImage.sprite = nonmuted;
			}


		}

	}
}
