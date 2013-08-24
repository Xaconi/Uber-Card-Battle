using UnityEngine;

public class Base : MonoBehaviour {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------

	public Material textura;
	public GameObject figura;
	private EstatBase estat;
	private EstatBase estatAnterior;
	public int ID;
	public int propietari;
	public int defensa = 20;
	public int defensaTotal = 20;
	public delegate void Fi();
  	public static event Fi fiBatalla;
	private bool pausa;
	private bool movimentRival;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	public void Awake(){
		figura = this.gameObject;
		if(propietari == 1){
			figura.transform.position = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Camera.mainCamera.pixelWidth*0.04f, 
				Camera.mainCamera.pixelHeight*0.62f, 
				Camera.mainCamera.transform.position.z - figura.transform.position.z));
			figura.transform.localScale = new Vector3((Camera.mainCamera.pixelWidth*0.5f)/Camera.mainCamera.pixelWidth, 
				figura.transform.localScale.y, 
				(Camera.mainCamera.pixelWidth*0.6f)/Camera.mainCamera.pixelWidth);
		}else if(propietari == 2){
			figura.transform.position = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Camera.mainCamera.pixelWidth*0.96f, 
				Camera.mainCamera.pixelHeight*0.62f, 
				Camera.mainCamera.transform.position.z - figura.transform.position.z));
			figura.transform.localScale = new Vector3((Camera.mainCamera.pixelWidth*0.5f)/Camera.mainCamera.pixelWidth, 
				figura.transform.localScale.y, 
				(Camera.mainCamera.pixelWidth*0.6f)/Camera.mainCamera.pixelWidth);
		}
		ID = figura.GetInstanceID();
		estat = new EstatBaseNormal(this);
		estatAnterior = estat;
		pausa = false;
		movimentRival = false;
		
		// Assignació d'events
		BotoPausa.ferPausa += ferPausa;
		IA.tornRival += tornRival;
	}
	
	public void Start(){
		//throw new System.NotImplementedException();
	}

	public void Update(){
		//throw new System.NotImplementedException();
		if(detectaClick()){
			string state = estat.GetType().ToString();
			if(state.Equals("EstatBaseObjectiu")){
				rebreAtac();
			}
		}
		estat.pintarBase();
	}
	
	public EstatBase getEstat(){
		return estat;
	}
	
	public void setEstat(EstatBase e){
		estat = e;
	}
	
	public int getPropietari(){
		return propietari;
	}
	
	public EstatBase getEstatAnterior(){
		return estatAnterior;
	}
	
	public void rebrePuntsAtac(int atac){
		defensa -= atac;
	}
	
	public void mirarMort(){
		if(defensa <= 0){
			// Mort del personatge
			Debug.Log("Mort de la base");
			ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
			ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
			cG.llistaLog.Add("FINAL PARTIDA PER MORT DE LA BASE");
			if(propietari == 1){
				cJ.assignarGuanyador(2);
				cG.llistaLog.Add("GUANYADOR: Jugador 2");
			}else{
				cJ.assignarGuanyador(1);
				cG.llistaLog.Add("GUANYADOR: Jugador 1");
			}
			cJ.assignarDadesFinalPartida(cG.atacFet, cG.atacRebut, cG.cartesUtilitzades, cG.nombreAccions, cG.startTime, cG.llistaLog, cG.personatgesUtilitzats, cG.bonificacionsUtilitzades);
			fiBatalla();
		}
	}
	
	private bool detectaClick(){
		bool click = false;
		if(Input.GetMouseButtonUp(0) && !pausa && !movimentRival){
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float marge = 100;
 			if (Physics.Raycast (ray, out hit, marge)){
				if(hit.collider.gameObject.transform.tag.Equals("Base")){
					Base p = (Base) hit.collider.gameObject.GetComponent(typeof(Base));
					if(p.ID.Equals(this.ID)){
						click = true;
					}
				}
			}
		}
		return click;
	}
	
	public void rebreAtac(){
		Personatge personatgeAtacant = (Personatge) GameObject.FindGameObjectWithTag("PersonatgeSeleccionat").GetComponent(typeof(Personatge));
		personatgeAtacant.atacarBase(this);
	}
	
	public void augmentarDefensa(int augment){
		defensa += augment;
		if(gameObject.name.ToString().Equals("Base(Clone)")){
			GameObject lifebar = GameObject.FindGameObjectWithTag("LifeBarP1");
			TextMesh t = lifebar.gameObject.GetComponent("TextMesh") as TextMesh;
			float f = ((float)defensa / (float)defensaTotal)*100;
			Debug.Log(f);
			t.text = ((int)f).ToString() + "%";
		}else if(gameObject.name.ToString().Equals("BaseRival(Clone)")){
			GameObject lifebar = GameObject.FindGameObjectWithTag("LifeBarP2");
			TextMesh t = lifebar.gameObject.GetComponent("TextMesh") as TextMesh;
			float f = ((float)defensa / (float)defensaTotal)*100;
			Debug.Log(defensa + "," + defensaTotal);
			t.text = ((int)f).ToString() + "%";
		}
	}
	
	private void ferPausa(){
		pausa = !pausa;
	}
	
	private void tornRival(){
		movimentRival = !movimentRival;
	}
}
