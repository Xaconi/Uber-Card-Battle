using UnityEngine;

public class Carta : MonoBehaviour {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
		
	public Vector3 posicio;
	public float[] llistaPosicions;
	public float[] llistaPosicionsTitols;
	public Material textura;
	public GameObject figura;
	public EstatCarta estat;
	public InformacioCarta iC;
	public TipusCarta tipus;
	public int posicioLlista = 0;
	public int ID;
	public GameObject titol;
	public GameObject atacLlarg;
	public GameObject atacCurt;
	public GameObject defensa;
	public GameObject distanciaAtac;
	public GameObject moviment;
	public GameObject textBonificacio;
	public GameObject textUber;
	public GameObject iconCarta;
	private bool pausa;
	private bool movimentRival;
	public bool finalPartida;
	public bool block;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	public void Awake(){
		//figura = (GameObject) GameObject.Instantiate(Resources.Load("Carta"));
		estat = new EstatCartaUtilitzada(this);
		llistaPosicions = new float[5];
		llistaPosicions[0] = 0.22f;
		llistaPosicions[1] = 0.36f;
		llistaPosicions[2] = 0.50f;
		llistaPosicions[3] = 0.64f;
		llistaPosicions[4] = 0.78f;
		llistaPosicionsTitols = new float[5];
		llistaPosicionsTitols[0] = 0.18f;
		llistaPosicionsTitols[1] = 0.36f;
		llistaPosicionsTitols[2] = 0.50f;
		llistaPosicionsTitols[3] = 0.64f;
		llistaPosicionsTitols[4] = 0.78f;
		ID = gameObject.GetInstanceID();
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
			ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
			if(cG.movimentActual.getAccionsDisponibles() != 0){
				if(estat.GetType().ToString().Equals("EstatCartaNormal")){
					ensenyarInformacioCarta();
				} else if(estat.GetType().ToString().Equals("EstatCartaSeleccionada")){
					treureInformacioCarta();
				}
			}
		}
		estat.pintarCarta();
	}
	
	public void assignarInformacioCarta(InformacioCarta i){
		if(i.GetType().ToString().Equals("InformacioCartaPersonatge")){
			iC = new InformacioCartaPersonatge((InformacioCartaPersonatge)i);
			tipus = new TipusCartaPersonatge();
		}else if(i.GetType().ToString().Equals("InformacioCartaBonificacio")){
			iC = new InformacioCartaBonificacio((InformacioCartaBonificacio)i);
			tipus = new TipusCartaBonificacio();
		}
	}
	
	// Assignem la posició de la llista de cartes disponibles (al menu de cartes disponibles)
	// per ensenyar-la posteriorment. Només entrarem en aquest mètode quan haguem de repartir
	// noves cartes a l'usuari.
	public void donarPosicioLlista(int p){
		posicioLlista = p;
		// S'agafa la posició X del mon a partir de dades guardades segons la posició de la 
		// llista de cartes disponibles en la que es troba aquesta carta.
		gameObject.transform.position = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Camera.mainCamera.pixelWidth*llistaPosicions[p], 
			Camera.mainCamera.pixelHeight*0.143f, 
			-9.999999f + 41.04723f));
		posicio = gameObject.transform.position;
		
		float ratio = Camera.mainCamera.aspect;
		float ratioOriginal = 16.0f/9.0f;
		float diferenciaRatio = ratio / ratioOriginal;
		if(ratio != ratioOriginal){
			if(ratio == 16.0f/10.0f || ratio == 1.73913f){
				gameObject.transform.localScale = new Vector3(0.6430312f, 
					0.7f, 
					0.7716374f);
			}else if(ratio == 4.0f/3.0f){
				gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x*0.86f, 
					gameObject.transform.localScale.y, 
					gameObject.transform.localScale.z*0.86f);
			}
		}
		
	}
	
	// La carta, prèviament inactiva, canvia el seu estat a EstatNormal, per tal de mostrar-se
	// per pantalla.
	public void activarCarta(){
		estat = new EstatCartaNormal(this);
		// En aquest punt tenim la informació de la carta amb detall, i podem carregar
		// la carta desitjada desde la carpeta Resources
		figura = gameObject;
		figura.transform.position = posicio;
		ID = gameObject.GetInstanceID();
	}
	
	public void desactivarCarta(){
		estat = new EstatCartaUtilitzada(this);
		MenuCartesDisponibles m = (MenuCartesDisponibles) (GameObject.FindGameObjectWithTag("MenuCartesDisponibles")).GetComponent("MenuCartesDisponibles");
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		m.reduirCartesDisponibles(cG.usuariTorn);

		Destroy(this.titol);
		Destroy(this.atacLlarg);
		Destroy(this.atacCurt);
		Destroy(this.defensa);
		Destroy(this.moviment);
		Destroy(this.distanciaAtac);
		Destroy(this.iconCarta);
		Destroy(this.textBonificacio);
		Destroy(this.textUber);
	}
	
	private bool detectaClick(){
		bool click = false;
		if(Input.GetMouseButtonUp(0) && !pausa && !movimentRival && !finalPartida && !block){
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float marge = 100;
 			if (Physics.Raycast (ray, out hit, marge)){
				if(hit.collider.gameObject.transform.tag.Equals("Carta") 
					|| hit.collider.gameObject.transform.tag.Equals("CartaSeleccionada")){
					Carta c = (Carta) hit.collider.gameObject.GetComponent(typeof(Carta));
					if(c.ID.Equals(this.ID)){
						click = true;
					}
				}else if(hit.collider.gameObject.transform.tag.Equals("Cap")){
					GameObject t1 = hit.collider.gameObject;
					GameObject t2 = iconCarta.gameObject;
					if(t1.GetInstanceID().Equals(t2.GetInstanceID())){
						click = true;
					}
				}
			}
		}
		return click;
	}
	
	private void ensenyarInformacioCarta(){
		Accio a = new AccioEnsenyarInformacioCarta(this);
		a.executarAccio();
	}
	
	private void treureInformacioCarta(){
		Accio a = new AccioTreureInformacioCarta(this);
		a.executarAccio();
	}
	
	public Personatge creaPersonatge(){
		// Llegir l'objecte InformacioCarta i decidir de quin tipus es el personatge,
		// per poder escollir així el tipus de GameObjeect a instanciar...
		GameObject.Instantiate(Resources.Load("Personatge"));
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		Personatge personatgeActual = (Personatge) GameObject.FindGameObjectWithTag("PersonatgeInvisible").GetComponent("Personatge");
		personatgeActual.assignarInformacio((InformacioCartaPersonatge)iC, cG.usuariTorn);
		if(cG.IA && personatgeActual.propietari == 2){
			personatgeActual.iniciarLocalScale();
			gameObject.transform.localScale = new Vector3(personatgeActual.midaX, personatgeActual.midaY, personatgeActual.midaZ);
			Debug.Log(personatgeActual.gameObject.transform.localScale);
		}
		return personatgeActual;
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
