using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatorScript : MonoBehaviour {

	private AudioManagerScript audioManager;
	private Animator myAnimator;
	public AudioClip aclip;
	private AudioSource asource;
	//public GameObject Bunshin1;
	//public GameObject Bunshin2;
	public GameObject BunshinPrefab;
	public GameObject RasenganPrefab;
	public GameObject ChidoriPrefab;
	public GameObject SmokePrefab, gm;
	public GameObject Spawner;
	public GameObject Spawner2;
	private GameObject rasengan;
	private GameObject chidori;

	public GameObject BunshinHolder1;
	public GameObject BunshinHolder2;

	private GameObject myCanvas;

	public bool canInstantiate;

	int gestureHash = Animator.StringToHash("SetGesture");
	int headNodHash = Animator.StringToHash("SetHeadNod");
	int startBunshinHash = Animator.StringToHash("StartBunshin");
	int startRasenganHash = Animator.StringToHash("StartRasengan");
	int endRasenganHash = Animator.StringToHash("EndRasengan");
	int startChidoriHash = Animator.StringToHash("StartChidori");
	int endChidoriHash = Animator.StringToHash("EndChidori");
	// Use this for initialization
	void Start () {
		myCanvas = GameObject.Find ("Canvas");
		canInstantiate = true;
		myAnimator = GetComponent<Animator> ();
		audioManager = GameObject.Find ("AudioManager").GetComponent<AudioManagerScript> ();
	}
		
	public void IdleAnswer(){
		StartCoroutine ("AnswerYes");
	}
		
	public IEnumerator AnswerYes(){
		audioManager.PlaySound_Listen ();
		yield return new WaitForSeconds(0.5f);
		myCanvas.GetComponent<UIScript> ().ButtonsClickable ();
	}

	public void IdleToTalk(){
		StartCoroutine ("StartGesture");
	}

	public IEnumerator StartGesture(){
		myAnimator.SetTrigger (gestureHash);
		audioManager.PlaySound_Idle ();
		yield return new WaitForSeconds(3f);
		myCanvas.GetComponent<UIScript> ().ButtonsClickable ();
	}

	public void IdleToHeadNod(){
		StartCoroutine ("StartHeadNod");
	}

	public IEnumerator StartHeadNod(){
		myAnimator.SetTrigger (headNodHash);
		audioManager.PlaySound_HeadNod ();
		yield return new WaitForSeconds(3f);
		myCanvas.GetComponent<UIScript> ().ButtonsClickable ();
	}

	public void IdleToBunshin(){
		StartCoroutine ("StartBunshin");
	}

	public IEnumerator StartBunshin(){
		if (canInstantiate) {
			audioManager.PlaySound_BunshinStart ();
			yield return new WaitForSeconds (0.7f);
			myAnimator.SetTrigger (startBunshinHash);
			yield return new WaitForSeconds (0.8f);
			audioManager.PlaySound_BunshinSeal ();
			canInstantiate = !canInstantiate;
			yield return new WaitForSeconds (0.5f);
			Instantiate (BunshinPrefab, BunshinHolder1.transform.position, Quaternion.Euler(0f,180f,0));
			yield return new WaitForSeconds (0.35f);
			Instantiate (BunshinPrefab, BunshinHolder2.transform.position, Quaternion.Euler(0f,180f,0));
			yield return new WaitForSeconds (1.35f);
			myCanvas.GetComponent<UIScript> ().ButtonsClickable ();
		} else
			StartCoroutine ("Bubling");
	}

	public IEnumerator Bubling(){
		audioManager.PlaySound_Bubling ();
		yield return new WaitForSeconds (2.1f);
		myCanvas.GetComponent<UIScript> ().ButtonsClickable ();
	}

	public void IdleToRasengan(){
		StartCoroutine ("StartRasengan");
	}

	public IEnumerator StartRasengan(){
		audioManager.PlaySound_RasenganStart ();
		yield return new WaitForSeconds(0.7f);
		myAnimator.SetTrigger (startRasenganHash);
		yield return new WaitForSeconds(1f);
		Vector3 pos = Spawner.transform.position + new Vector3 (0f, -0.18f, -0.1f);
		rasengan = (GameObject)Instantiate (RasenganPrefab, pos, Quaternion.identity);
		rasengan.transform.SetParent (Spawner.transform);
		yield return new WaitForSeconds(3f);
		audioManager.PlaySound_GotIt ();
		yield return new WaitForSeconds(10.4f);
		StartCoroutine ("EndRasengan");
	}

	public IEnumerator EndRasengan(){
		myAnimator.SetTrigger (endRasenganHash);
		Destroy (rasengan);
		yield return new WaitForSeconds(1f);
		myCanvas.GetComponent<UIScript> ().ButtonsClickable ();
	}

	public void IdleToChidori(){
		StartCoroutine ("StartChidori");
	}

	public IEnumerator StartChidori(){
		audioManager.PlaySound_ChidoriStart ();
		myAnimator.SetTrigger (startChidoriHash);
		yield return new WaitForSeconds(4.4f);
		audioManager.StartCoroutine("PlaySound_ChidoriSeals");
		yield return new WaitForSeconds(1.2f);
		Vector3 pos = Spawner2.transform.position + new Vector3 (0, 0, 0);
		//chidori = (GameObject)Instantiate (ChidoriPrefab, pos, Quaternion.Euler(-115f,30f,0f));
		chidori = (GameObject)Instantiate (ChidoriPrefab, pos, Quaternion.Euler(90f,40f,20f));
		chidori.transform.SetParent (Spawner2.transform);
		yield return new WaitForSeconds(6f);
		StartCoroutine ("EndChidori");
	}

	public IEnumerator EndChidori(){
		myAnimator.SetTrigger (endChidoriHash);
		Destroy (chidori);
		yield return new WaitForSeconds(1f);
		myCanvas.GetComponent<UIScript> ().ButtonsClickable ();;
	}

	public void IdleToBye(){
		StartCoroutine ("StartBye");
	}

	public IEnumerator StartBye(){
		audioManager.PlaySound_Bye ();
		yield return new WaitForSeconds(2.5f);
		gm = (GameObject)Instantiate (SmokePrefab, transform.position, Quaternion.Euler(-90,0,0));
		gm.GetComponent<ParticleSystem> ().Play ();
		transform.position = new Vector3 (10000000, 0, 0);
		//gm.GetComponent<AudioSource>().clip = gm.GetComponent<BunshinScript>().clip_Destroy;
		//gm.GetComponent<AudioSource>().Play ();
		asource = gameObject.GetComponent<AudioSource> ();
		asource.clip = aclip;
		asource.Play ();
		yield return new WaitForSeconds (2);
		Destroy (gm);
		Destroy (gameObject);
	}

	public void SetInstantiate(bool k){
		canInstantiate = k;
	}
}
