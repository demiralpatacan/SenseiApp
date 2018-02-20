using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour {

	public GameObject Kakashi;
	public AudioClip clip_Idle;
	public AudioClip clip_HeadNod;
	public AudioClip clip_BunshinStart;
	public AudioClip clip_RasenganStart;
	public AudioClip clip_ChidoriStart;
	public AudioClip clip_BunshinSeal;
	public AudioClip clip_Destroy;
	public AudioClip clip_GotIt;
	public AudioClip clip_seal1;
	public AudioClip clip_seal2;
	public AudioClip clip_seal3;
	public AudioClip clip_Bubling;
	public AudioClip clip_Yeah;
	public AudioClip clip_Bye;
	private AudioSource audioSource_Kakashi;
	// Use this for initialization
	void Start () {
		audioSource_Kakashi = Kakashi.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaySound_Listen(){
		audioSource_Kakashi.clip = clip_Yeah;
		audioSource_Kakashi.Play ();
	}

	public void PlaySound_Idle(){
		audioSource_Kakashi.clip = clip_Idle;
		audioSource_Kakashi.Play ();
	}

	public void PlaySound_HeadNod(){
		audioSource_Kakashi.clip = clip_HeadNod;
		audioSource_Kakashi.Play ();
	}

	public void PlaySound_BunshinStart(){
		audioSource_Kakashi.clip = clip_BunshinStart;
		audioSource_Kakashi.Play ();
	}

	public void PlaySound_RasenganStart(){
		audioSource_Kakashi.clip = clip_RasenganStart;
		audioSource_Kakashi.Play ();
	}

	public void PlaySound_ChidoriStart(){
		audioSource_Kakashi.clip = clip_ChidoriStart;
		audioSource_Kakashi.Play ();
	}

	public void PlaySound_BunshinSeal(){
		audioSource_Kakashi.clip = clip_BunshinSeal;
		audioSource_Kakashi.Play ();
	}

	public void PlaySound_GotIt(){
		audioSource_Kakashi.clip = clip_GotIt;
		audioSource_Kakashi.Play ();
	}

	public void PlaySound_Bubling(){
		audioSource_Kakashi.clip = clip_Bubling;
		audioSource_Kakashi.Play ();
	}

	public IEnumerator PlaySound_ChidoriSeals(){
		audioSource_Kakashi.clip = clip_seal1;
		audioSource_Kakashi.Play ();
		yield return new WaitForSeconds(0.38f);
		audioSource_Kakashi.clip = clip_seal3;
		audioSource_Kakashi.Play ();
		yield return new WaitForSeconds(0.38f);
		audioSource_Kakashi.clip = clip_seal1;
		audioSource_Kakashi.Play ();
		yield return new WaitForSeconds(0.43f);
		audioSource_Kakashi.clip = clip_seal2;
		audioSource_Kakashi.Play ();
	}

	public void PlaySound_Bye(){
		audioSource_Kakashi.clip = clip_Bye;
		audioSource_Kakashi.Play ();
	}

}
