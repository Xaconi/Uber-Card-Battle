using UnityEngine;

public class BotoConfirmarMoviment : MonoBehaviour {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	public Vector3 posicio;
	public Material textura;
	public GameObject figura;
	public int ID;
	private bool pausa;
	private bool movimentRival;
	public bool finalPartida;
	public bool block;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	public void Awake(){
		figura = this.gameObject;
		figura.transform.position = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Camera.mainCamera.pixelWidth*0.94f, 
			Camera.mainCamera.pixelHeight*0.13f, 
			Camera.mainCamera.transform.position.z - figura.transform.position.z));
		ID = figura.GetInstanceID();
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
		//throw new System.NotImplementedException();
		if(detectaClick()){
			confirmarMoviment();
		}
	}

	public void confirmarMoviment(){
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		if(cG.movimentActual.getAccionsDisponibles() == 0){
			Accio a = new AccioConfirmarMoviment(cG.movimentActual);
			a.executarAccio();
		}
	}

	public bool detectaClick(){
		bool click = false;
		if(Input.GetMouseButtonDown(0) && !pausa && !movimentRival && !finalPartida && !block){
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float marge = 100;
 			if (Physics.Raycast (ray, out hit, marge)){
				if(hit.collider.gameObject.transform.tag.Equals("BotoConfirmarMoviment")){
					BotoConfirmarMoviment c = (BotoConfirmarMoviment) hit.collider.gameObject.GetComponent(typeof(BotoConfirmarMoviment));
					if(c.ID.Equals(this.ID)){
						gameObject.renderer.material = Resources.Load("BotoConfirmarMovimentPolsat") as Material;
					}
				}
			}
		}
		if(Input.GetMouseButtonUp(0) && !pausa && !movimentRival && !finalPartida && !block){
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float marge = 100;
 			if (Physics.Raycast (ray, out hit, marge)){
				if(hit.collider.gameObject.transform.tag.Equals("BotoConfirmarMoviment")){
					BotoConfirmarMoviment c = (BotoConfirmarMoviment) hit.collider.gameObject.GetComponent(typeof(BotoConfirmarMoviment));
					if(c.ID.Equals(this.ID)){
						Debug.Log(gameObject.renderer.material.name);
						if(gameObject.renderer.material.name.Equals("BotoConfirmarMovimentPolsat (Instance)")){
							click = true;
						}
					}
				}
			}
			if(gameObject.renderer.material.name.Equals("BotoConfirmarMovimentPolsat (Instance)")){
				gameObject.renderer.material = Resources.Load("BotoConfirmarMovimentNoPolsat") as Material;
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
