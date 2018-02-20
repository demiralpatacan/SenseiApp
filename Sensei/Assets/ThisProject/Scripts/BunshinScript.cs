using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunshinScript : MonoBehaviour {

	private AudioSource asource;
	public AudioClip clip_Start;
	public AudioClip clip_Destroy;
	public GameObject SmokePrefab;

	private float lifetime = 10f;
	private GameObject parent;
	private GameObject gm;
	// Use this for initialization
	void Start () {
		gm = (GameObject)Instantiate (SmokePrefab, transform.position, Quaternion.Euler(-90,0,0));
		gm.GetComponent<ParticleSystem> ().Play ();
		parent = GameObject.Find ("Kakashi");
		asource = GetComponent<AudioSource> ();
		asource.clip = clip_Start;
		asource.Play ();
		StartCoroutine ("KillBunshin");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	IEnumerator KillBunshin(){
		yield return new WaitForSeconds (2);
		Destroy (gm);
		yield return new WaitForSeconds (lifetime-2);
		gm = (GameObject)Instantiate (SmokePrefab, transform.position, Quaternion.Euler(-90,0,0));
		gm.GetComponent<ParticleSystem> ().Play ();
		transform.position = new Vector3 (10000000, 0, 0);
		asource.clip = clip_Destroy;
		asource.Play ();
		yield return new WaitForSeconds (2);
		parent.GetComponent<AnimatorScript> ().SetInstantiate (true);
		Destroy (gm);
		Destroy (gameObject);
	}
		
}
