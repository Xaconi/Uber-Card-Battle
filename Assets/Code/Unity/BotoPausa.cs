using UnityEngine;
using System.Collections;

public class BotoPausa : MonoBehaviour {
	
	public delegate void Pausa();
  	public static event Pausa ferPausa;
	public int ID;
	private bool movimentRival;
	public bool pausa;
	
	void Awake(){
		ID = gameObject.GetInstanceID();
		gameObject.transform.position = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Camera.mainCamera.pixelWidth*0.03f, 
			Camera.mainCamera.pixelHeight*0.95f, 
			10-gameObject.transform.position.z));
		movimentRival = false;
		pausa = false;
		
		// Assignació d'events
		IA.tornRival += tornRival;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(detectaClick()){
			ferPausa();
			if(!pausa){
				pausa = true;
				Camera.mainCamera.gameObject.AddComponent("MenuPausa");
			}else{
				pausa = false;
				MenuPausa p = (MenuPausa) Camera.mainCamera.gameObject.GetComponent("MenuPausa");
				Destroy (p);
			}
		}
	}
	
	private bool detectaClick(){
		bool click = false;
		if(Input.GetMouseButtonUp(0) && !movimentRival){
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float marge = 100.0f;
 			if (Physics.Raycast (ray, out hit, marge)){
				if(hit.collider.gameObject.transform.tag.ToString().Equals("BotoPausa")){
					BotoPausa b = (BotoPausa) hit.collider.gameObject.GetComponent(typeof(BotoPausa));
					if(b.ID.Equals(this.ID)){
						click = true;
						Debug.Log("Click en Pause");
					}
				}
			}
		}
		return click;
	}
	
	public void tornRival(){
		movimentRival = !movimentRival;
	}
	
	public void desactivarPausa(){
		pausa = false;
		ferPausa();
	}
}
