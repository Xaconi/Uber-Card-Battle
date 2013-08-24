using UnityEngine;
using System.Collections.Generic;

public class Personatge : MonoBehaviour {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	public Vector3 posicio;
	public bool moviment;
	public GameObject figura;
	public Material[] textures;
	public Material texturaActual;
	public float startTime;
	public Vector3 startPoint;
	public Vector3 endPoint;
	public int ID;
	public Fitxa fitxaActual;
	public string nom;
	public int distanciaAtac = 3;
	public int distanciaMoviment = 4;
	public int atacCurtaDistancia = 3;
	public int atacLlargaDistancia = 5;
	public int defensa = 10;
	public int propietari;
	public int nMoviments = 0;
	public static int nPersonatges = 0;
	public EstatPersonatge estat;
	public EstatPersonatge estatAnterior;
	private int atacAugmentat = 0;
	private int defensaAugmentada = 0;
	private Bonificacio bonificacioActual;
	public List<Personatge> llistaPersonatgesObservers;
	public delegate void Mort(Personatge p);
  	public static event Mort mortPersonatge;
	public delegate void Block();
  	public static event Block blockUsuari;
	public float midaX;
	public float midaY;
	public float midaZ;
	private bool pausa;
	public bool movimentRival;
	public bool finalPartida;
	public bool block;
	
	// Singleton
	private static Personatge instance;
	
	public static Personatge Instance{
		get
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType(typeof(Personatge)) as Personatge;
			
			return instance;
		}
	}
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	public void Awake(){
		//figura = this.gameObject;
		//moviment = false;
		estat = new EstatPersonatgeNoVisible(this);
		llistaPersonatgesObservers = new List<Personatge>();
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
		//estat = new EstatPersonatgeNormal(this);
		if(propietari == 1) midaX = 0.3f;
		else midaX = -0.3f;
		midaY = 0.3f;
		midaZ = 0.3f;
		Debug.Log(movimentRival);
	}

	public void Update(){
		// Control de totes les accions relacionades amb el personatge
		if(detectaClick()){
			ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
			if(cG.movimentActual.getAccionsDisponibles() != 0 && cG.usuariTorn == propietari && !movimentRival){ 
				if(!cG.IA || propietari != 2){
					// Click sobre un personatge propi
					string state = estat.GetType().ToString();
					if(state.Equals("EstatPersonatgeNormal") || state.Equals("EstatPersonatgeBonificatAtac") 
						|| state.Equals("EstatPersonatgeBonificatDefensa") || state.Equals("EstatPersonatgeBonificatUber")){
						ensenyarInformacio();
					}else if(state.Equals("EstatPersonatgeSeleccionat")){
						treureInformacio();
					}else if(state.Equals("EstatPersonatgeObjectiuBonificacio")){
						aplicarBonificacio();
					}
				}
			}else{
				// Click sobre un personatge rival
				string state = estat.GetType().ToString();
				if(state.Equals("EstatPersonatgeObjectiu")){
					rebreAtac();
				}else if(state.Equals("EstatPersonatgeNormal")){
					// Ensenyar informació actual del personatge
				}
			}
		}else{
			// S'han acabat les accions possibles per aquest jugador!
		}
		estat.pintarPersonatge();
	}
	
	public void iniciMoviment(Vector3 desti){
		gameObject.transform.position = desti;
	}
	
	// Aquest metode es podrà re-implementar en els objectes que heretin de Personatge
	// per tal de poder redefinir l'atac, i el nombre de personatges a atacar de manera
	// dinàmica...
	public void atacarPersonatge(Personatge personatgeAtacar){
		//throw new System.NotImplementedException();
		Accio a = new AccioAtacarPersonatge(this, personatgeAtacar);
		a.executarAccio();
	}
	
	public void atacarBase(Base baseAtacar){
		Accio a = new AccioAtacarBase(this, baseAtacar);
		a.executarAccio();
	}

	private bool detectaClick(){
		bool click = false;
		if(Input.GetMouseButtonUp(0) && !pausa && !finalPartida){
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float marge = 100;
 			if (Physics.Raycast (ray, out hit, marge)){
				if(hit.collider.gameObject.transform.tag.Equals("Personatge") || hit.collider.gameObject.transform.tag.Equals("PersonatgeSeleccionat")){
					Personatge p = (Personatge) hit.collider.gameObject.GetComponent(typeof(Personatge));
					if(p.ID.Equals(this.ID)){
						click = true;
					}
				}
			}
		}
		return click;
	}
	
	public void assignarFitxa(Fitxa f){
		fitxaActual = f;
		gameObject.transform.position = new Vector3(f.gameObject.transform.position.x - 0.8f + 0.13f*f.columna, 
			f.gameObject.transform.position.y + 0.65f - f.fila*0.08f, 
			f.gameObject.transform.position.z + 1);
	}

	public void ensenyarInformacio(){
		//throw new System.NotImplementedException();
		Accio a = new AccioEnsenyarInformacio(this);
		a.executarAccio();
	}
	
	public void treureInformacio(){
		Accio a = new AccioTreureInformacio(this);
		a.executarAccio();
	}
	
	public void assignarInformacio(InformacioCartaPersonatge iC, int p){
		// Repartir la informació de la carta al personatge actual
		nom = iC.nom;
		distanciaAtac = iC.distanciaAtac;
		distanciaMoviment = iC.moviment;
		atacCurtaDistancia = iC.atacCurtaDistancia;
		atacLlargaDistancia = iC.atacLlargaDistancia;
		defensa = iC.defensa;
		propietari = p;
		if(propietari == 2){
			midaX = -midaX;
			gameObject.transform.localScale = new Vector3(midaX,
				gameObject.transform.localScale.y, gameObject.transform.localScale.z);
		}
	}
	
	private void aplicarBonificacio(){
		Carta c = (Carta) GameObject.FindGameObjectWithTag("CartaSeleccionada").GetComponent("Carta");
		Accio a = new AccioAplicarBonificacio(this, c);
		a.executarAccio();
	}
	
	public void activarPersonatge(){
		estat = new EstatPersonatgeNormal(this);
		estatAnterior = estat;
		gameObject.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
			fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
			fitxaActual.gameObject.transform.position.z + 1);
		ID = Personatge.nPersonatges;
		Personatge.nPersonatges++;
	}
	
	public void rebreBonificacioAtac(InformacioCarta informacio){
		atacCurtaDistancia += ((InformacioCartaBonificacio) informacio).atac;
		atacLlargaDistancia += ((InformacioCartaBonificacio) informacio).atac;
		atacAugmentat += ((InformacioCartaBonificacio) informacio).atac;
		estat = new EstatPersonatgeBonificatAtac(this);
		estatAnterior = estat;
		GameObject.Instantiate(Resources.Load("BonificacioAtac"));
		bonificacioActual = (Bonificacio) GameObject.FindGameObjectWithTag("BonificacioNoVisible").GetComponent("Bonificacio");
		bonificacioActual.iniciarBonificacio(this, "Atac", (InformacioCartaBonificacio) informacio);
	}
	
	public void rebreBonificacioDefensa(InformacioCarta informacio){
		defensa += ((InformacioCartaBonificacio) informacio).defensa;
		defensaAugmentada += ((InformacioCartaBonificacio) informacio).defensa;
		estat = new EstatPersonatgeBonificatDefensa(this);
		estatAnterior = estat;
		GameObject.Instantiate(Resources.Load("BonificacioDefensa"));
		bonificacioActual = (Bonificacio) GameObject.FindGameObjectWithTag("BonificacioNoVisible").GetComponent("Bonificacio");
		bonificacioActual.iniciarBonificacio(this, "Defensa", (InformacioCartaBonificacio) informacio);
	}
	
	public void rebreBonificacioUber(InformacioCarta informacio){
		atacCurtaDistancia += ((InformacioCartaBonificacio) informacio).atac;
		atacLlargaDistancia += ((InformacioCartaBonificacio) informacio).atac;
		defensa += ((InformacioCartaBonificacio) informacio).defensa;
		atacAugmentat += ((InformacioCartaBonificacio) informacio).atac;
		defensaAugmentada += ((InformacioCartaBonificacio) informacio).defensa;
		estat = new EstatPersonatgeBonificatUber(this);
		estatAnterior = estat;
		GameObject.Instantiate(Resources.Load("BonificacioUber"));
		bonificacioActual = (Bonificacio) GameObject.FindGameObjectWithTag("BonificacioNoVisible").GetComponent("Bonificacio");
		bonificacioActual.iniciarBonificacio(this, "Uber", (InformacioCartaBonificacio) informacio);
	}
	
	public void rebreAtac(){
		Personatge personatgeAtacant = (Personatge) GameObject.FindGameObjectWithTag("PersonatgeSeleccionat").GetComponent(typeof(Personatge));
		personatgeAtacant.atacarPersonatge(this);
	}
	
	public bool esPersonatgeEnemic(Personatge p){
		return propietari != p.propietari;
	}
	
	public void rebrePuntsAtac(int puntsAtac){
		defensa -= puntsAtac;
	}

	public void afegirObserver(Personatge personatgeObserver){
		llistaPersonatgesObservers.Add(personatgeObserver);
	}

	public void eliminarObserver(Personatge personatgeObserver){
		llistaPersonatgesObservers.Remove(personatgeObserver);
	}
	
	public List<Personatge> getLlistaObservers(){
		return llistaPersonatgesObservers;
	}
	
	public void augmentarDefensa(int augment){
		defensa += augment;
	}
	
	public void colocarPersonatge(){
		gameObject.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
			fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
			fitxaActual.gameObject.transform.position.z + 1);
		gameObject.renderer.enabled = true;
	}
	
	public void mirarMort(){
		if(defensa <= 0){
			// Mort del personatge
			mortPersonatge(this);
		}
	}
	
	public Bonificacio getBonificacioActual(){
		return bonificacioActual;
	}
	
	public void treureBonificacio(){
		bonificacioActual = null;
	}
	
	public void acabar(){
		gameObject.renderer.enabled = false;
		estat = new EstatPersonatgeNoVisible(this);
		fitxaActual.treurePersonatge();
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		if(cG.IA && cG.usuariTorn == 2){
			// Si es tracta d'un moviment de la IA
			cG.avisarIA();
		}
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
	
	public void iniciarLocalScale(){
		if(propietari == 1) midaX = 0.3f;
		else midaX = -0.3f;
		midaY = 0.3f;
		midaZ = 0.3f;
	}
	
	private void blockejaUsuari(){
		block = !block;
	}
	
	public void blockUsuariPublic(){
		blockUsuari();
	}
}
