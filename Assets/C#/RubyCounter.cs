using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubyCounter : MonoBehaviour
{
	public Text rubiesCountText;

	public static RubyCounter counter;

	public bool constantUpdate = false;

	void Start() {

		counter = this;

		if(!constantUpdate)
			StartCoroutine(WaitForManager());
		else
			StartCoroutine(ConstantUpdate());
	}

	IEnumerator ConstantUpdate() {

		while(this.gameObject != null) {
			
			UpdateCount();

			yield return new WaitForFixedUpdate();
		}

	}

	IEnumerator WaitForManager() {

		while(GameManager.manager == null) {

			yield return new WaitForFixedUpdate();
		}



		int i = 0;

		float elapsedTime = 0.0f;

		while(i <= GameManager.manager.GetRubies() && elapsedTime < 5.0f) {

			rubiesCountText.text = ""+(i++);

			elapsedTime += Time.deltaTime;

			yield return new WaitForSeconds(0.03f);
		}

	}

    public void UpdateCount()
    {
		if(rubiesCountText != null && GameManager.manager != null) {

			rubiesCountText.text = ""+GameManager.manager.GetRubies();

		}
    }
}
