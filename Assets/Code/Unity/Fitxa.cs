using UnityEngine;

public class Fitxa : MonoBehaviour {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	public GameObject figura;
	public int ID;
	public Material material;
	public EstatFitxa estat;
	public Personatge personatge = null;
	public int fila;
	public int columna;
	private bool pausa;
	private bool movimentRival;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	public void Awake(){
		figura = this.gameObject;
		ID = this.gameObject.GetInstanceID();
		Vector3 dir = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Camera.mainCamera.pixelWidth/2,
			Camera.mainCamera.pixelHeight/2, figura.transform.position.z));
		pausa = false;
		movimentRival = false;
		
		// Assignació d'events
		BotoPausa.ferPausa += ferPausa;
		IA.tornRival += tornRival;
		ControlGeneralJoc cJ = (ControlGeneralJoc) GameObject.FindObjectOfType(typeof(ControlGeneralJoc));
		if(cJ.getEscenari().Equals("Bosc")){
			gameObject.transform.renderer.material.mainTexture = (Texture) Resources.Load("grass_ground");
		}
		else if(cJ.getEscenari().Equals("Winter")){
			gameObject.transform.renderer.material.mainTexture = (Texture) Resources.Load("snow_ground");
		}
		else if(cJ.getEscenari().Equals("Cave")){
			gameObject.transform.renderer.material.mainTexture = (Texture) Resources.Load("cave_ground");
		}
	}
	
	public void Start(){
		//throw new System.NotImplementedException();
		estat = new EstatFitxaNormal(this);
		
		// PROVES //
		
//		if(fila == 3 && columna == 3){
//			GameObject.Instantiate(Resources.Load("Personatge"));
//			ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
//			Personatge personatgeActual = (Personatge) GameObject.FindGameObjectWithTag("PersonatgeInvisible").GetComponent("Personatge");
//			assignarPersonatge(personatgeActual);
//			personatgeActual.propietari = 2;
//			personatgeActual.nom = "Bazooka";
//			personatgeActual.gameObject.transform.localScale = new Vector3(-personatgeActual.gameObject.transform.localScale.x, 
//				personatgeActual.gameObject.transform.localScale.y, 
//				personatgeActual.gameObject.transform.localScale.z);
//			personatgeActual.iniciMoviment(new Vector3(figura.transform.position.x - 0.8f + 0.13f*columna, 
//				figura.transform.position.y + 0.65f - fila*0.08f, figura.transform.position.z + 1));
//			personatgeActual.activarPersonatge();
//		}
		
		// FI PROVES //
		
//		}else if(fila == 2 && columna == 2){
//			assignarPersonatge((Personatge) GameObject.FindGameObjectsWithTag("Personatge")[1].GetComponent("Personatge"));
//			personatge.gameObject.tag = "Personatge";
//			personatge.iniciMoviment(new Vector3(figura.transform.position.x - 0.8f + 0.13f*columna, 
//				figura.transform.position.y + 0.65f - fila*0.08f, figura.transform.position.z + 1));
//		}
	}

	public void Update(){
		
		// Control de totes les accions relacionades amb la fitxa
		if(detectaClick()){
			Debug.Log(estat.GetType().ToString());
			if(estat.GetType().ToString().Equals("EstatFitxaNormal")){

			}else if(estat.GetType().ToString().Equals("EstatFitxaDisponible")){
				string tipus = comprovarTipusClick();
				if(tipus.Equals("MourePersonatge")){
					mourePersonatge();
				}else if(tipus.Equals("TreurePersonatge")){
					treurePersonatgeFitxa();
				}
			}
		}
		estat.pintarCasella();
	}

	public void assignarPersonatge(Personatge p){
		personatge = p;
		personatge.assignarFitxa(this);
	}
	
	public bool tePersonatge(){
		return personatge != null;
	}
	
	private bool detectaClick(){
		bool click = false;
		if(Input.GetMouseButtonUp(0) && !pausa && !movimentRival){
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float marge = 100;
 			if (Physics.Raycast (ray, out hit, marge)){
				if(hit.collider.gameObject.transform.tag.Equals("Fitxa")){
					Fitxa f = (Fitxa) hit.collider.gameObject.GetComponent(typeof(Fitxa));
					if(f.ID.Equals(this.ID)){
						click = true;
					}
				}
			}
		}
		return click;
	}
	
	public void traspassarPersonatge(Fitxa fitxaDesti, Personatge personatgeUsuari){
		fitxaDesti.assignarPersonatge(personatgeUsuari);
		personatge = null;
	}
	
	public void treurePersonatge(){
		personatge = null;
	}
	
	private string comprovarTipusClick(){
		string tipus = "";
		if(GameObject.FindGameObjectWithTag("PersonatgeSeleccionat") != null){
			tipus = "MourePersonatge";
		}else if(GameObject.FindGameObjectWithTag("CartaSeleccionada") != null){
			tipus = "TreurePersonatge";
		}
		return tipus;
	}
	
	private void mourePersonatge(){
		Personatge personatgeActual = (Personatge) GameObject.FindGameObjectWithTag("PersonatgeSeleccionat").GetComponent(typeof(Personatge));
		Fitxa fitxaOrigen = personatgeActual.fitxaActual;
		Accio a = new AccioMourePersonatge(personatgeActual, fitxaOrigen, this);
		a.executarAccio();
	}
	
	private void treurePersonatgeFitxa(){
		Carta cartaSeleccionada = (Carta) GameObject.FindGameObjectWithTag("CartaSeleccionada").GetComponent(typeof(Carta));
		Accio a = new AccioTreurePersonatge(this, cartaSeleccionada);
		a.executarAccio();
	}
	
	private void ferPausa(){
		pausa = !pausa;
	}
	
	private void tornRival(){
		movimentRival = !movimentRival;
	}
}
