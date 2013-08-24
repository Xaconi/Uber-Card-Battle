using UnityEngine;

public class BotoEsborrarMoviment : MonoBehaviour {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	public Vector3 posicio{
		get;
		set;
	}

	public Material textura{
		get{return this.textura;}
		set{this.textura = value;}
	}
	
	public GameObject figura{
		get;
		set;
	}
	
	private int ID;
	private bool pausa;
	private bool movimentRival;
	public bool finalPartida;
	public bool block;
	
	//-------------------------------
	// Methods, functions and actions
	//--------------------------------
	
	public void Awake(){
		figura = gameObject;
		ID = gameObject.GetInstanceID();
		figura.transform.position = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Camera.mainCamera.pixelWidth*0.06f, 
			Camera.mainCamera.pixelHeight*0.13f, 
			Camera.mainCamera.transform.position.z - figura.transform.position.z));
		pausa = false;
		movimentRival = false;
		finalPartida = false;
		block = false;
		
		// Assignació d'events
		BotoPausa.ferPausa += ferPausa;
		IA.tornRival += tornRival;
		MenuFinalPartida.fiPartida += fiPartida;
		Personatge.blockUsuari += blockejaUsuari;
	}
	
	public void Start(){
		//throw new System.NotImplementedException();
	}

	public void Update(){
		if(detectaClick()){
			esborrarMoviment();
		}
	}

	public void esborrarMoviment(){
		//throw new System.NotImplementedException();
		Debug.Log("Començo a esborrar el moviment");
		Accio neteja = new AccioNetejarTauler();
		neteja.executarAccio();
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		Accio a = new AccioEsborrarMoviment(cG.movimentActual);
		a.executarAccio();
	}

	public bool detectaClick(){
		bool click = false;
		if(Input.GetMouseButtonDown(0) && !pausa && !movimentRival && !finalPartida && !block){
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float marge = 100;
 			if (Physics.Raycast (ray, out hit, marge)){
				if(hit.collider.gameObject.transform.tag.Equals("BotoEsborrarMoviment")){
					BotoEsborrarMoviment c = (BotoEsborrarMoviment) hit.collider.gameObject.GetComponent(typeof(BotoEsborrarMoviment));
					if(c.ID.Equals(this.ID)){
						gameObject.renderer.material = Resources.Load("BotoEsborrarMovimentPolsat") as Material;
					}
				}
			}
		}
		if(Input.GetMouseButtonUp(0) && !pausa && !movimentRival && !finalPartida&& !block){
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float marge = 100;
 			if (Physics.Raycast (ray, out hit, marge)){
				if(hit.collider.gameObject.transform.tag.Equals("BotoEsborrarMoviment")){
					BotoEsborrarMoviment c = (BotoEsborrarMoviment) hit.collider.gameObject.GetComponent(typeof(BotoEsborrarMoviment));
					if(c.ID.Equals(this.ID)){
						Debug.Log(gameObject.renderer.material.name);
						if(gameObject.renderer.material.name.Equals("BotoEsborrarMovimentPolsat (Instance)")){
							click = true;
						}
					}
				}
			}
			if(gameObject.renderer.material.name.Equals("BotoEsborrarMovimentPolsat (Instance)")){
				gameObject.renderer.material = Resources.Load("BotoEsborrarMovimentNoPolsat") as Material;
			}
		}
		return click;
	}
	
	private void ferPausa(){
		pausa = !pausa;
	}
	
	private void tornRival(){
		movimentRival = !movimentRival;
	}
	
	private void fiPartida(){
		finalPartida = !finalPartida;
	}
	
	private void blockejaUsuari(){
		block = !block;
	}
}
