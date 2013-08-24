using UnityEngine;
using System.Collections;

public class LifeBar : MonoBehaviour {
	
	public GameObject text;
	public GameObject lifeBarPlayer1;
	public GameObject lifeBarPlayer2;
	public int ID;
	
	void Awake(){
		ID = gameObject.GetInstanceID();
		gameObject.transform.position = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Camera.mainCamera.pixelWidth*0.5f, 
			Camera.mainCamera.pixelHeight*0.96f, 
			-gameObject.transform.position.z));
		
		float ratio = Camera.mainCamera.aspect;
		float ratioOriginal = 16.0f/9.0f;
		float diferenciaRatio = ratio / ratioOriginal;
		
		if(diferenciaRatio > 0.95f){
			gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x*1.1f, 
			gameObject.transform.localScale.y, 
			gameObject.transform.localScale.z);
		}
		
		text = GameObject.Instantiate(Resources.Load("TextLifebar")) as GameObject;
		text.transform.position = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Camera.mainCamera.pixelWidth*0.43f, 
			Camera.mainCamera.pixelHeight*0.987f, 
			-text.transform.position.z));
		lifeBarPlayer1 = GameObject.Instantiate(Resources.Load("LifeBarP1")) as GameObject;
		lifeBarPlayer1.transform.position = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Camera.mainCamera.pixelWidth*0.347f, 
			Camera.mainCamera.pixelHeight*0.987f, 
			-lifeBarPlayer1.transform.position.z));
		TextMesh t = lifeBarPlayer1.GetComponent("TextMesh") as TextMesh;
		t.text = "100%";
		lifeBarPlayer2 = GameObject.Instantiate(Resources.Load("LifeBarP2")) as GameObject;
		lifeBarPlayer2.transform.position = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Camera.mainCamera.pixelWidth*0.58f, 
			Camera.mainCamera.pixelHeight*0.987f, 
			-lifeBarPlayer2.transform.position.z));
		t = lifeBarPlayer2.GetComponent("TextMesh") as TextMesh;
		t.text = "100%";
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
