using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class Randomusic : MonoBehaviour
{
	public AudioClip[] tracks;


	void Awake() {

		GetComponent<AudioSource>().clip = tracks[Random.Range(0,tracks.Length-1)];

		GetComponent<AudioSource>().Play();
	}
}
