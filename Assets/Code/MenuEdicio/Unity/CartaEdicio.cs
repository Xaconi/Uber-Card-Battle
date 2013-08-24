using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CartaEdicio : MonoBehaviour {
	
	public EstatCartaEdicio estat;
	public EstatCartaEdicio estatAnterior;
	public GameObject figura;
	public Material material;
	public Vector3 posicio;
	public int ID;
	public InformacioCarta iC;
	public TipusCarta tipus;
	public int posicioLlista;
	public int posicioLlistaBaralla;
	public float[] llistaPosicions;
	public float[] llistaPosicionsTitols;
	public float[] llistaPosicionsBaralla;
	public float[] llistaPosicionsBarallaAlt;
	public GameObject titol;
	public GameObject atacLlarg;
	public GameObject atacCurt;
	public GameObject defensa;
	public GameObject distanciaAtac;
	public GameObject moviment;
	public GameObject textBonificacio;
	public GameObject textUber;
	public GameObject iconCarta;
	public bool missatge;
	
	// Use this for initialization
	void Awake () {
		estat = new EstatCartaEdicioNoVisible(this);
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
		llistaPosicionsBaralla = new float[7];
		llistaPosicionsBaralla[0] = 0.14f;
		llistaPosicionsBaralla[1] = 0.26f;
		llistaPosicionsBaralla[2] = 0.38f;
		llistaPosicionsBaralla[3] = 0.5f;
		llistaPosicionsBaralla[4] = 0.62f;
		llistaPosicionsBaralla[5] = 0.74f;
		llistaPosicionsBaralla[6] = 0.86f;
		llistaPosicionsBarallaAlt = new float[3];
		llistaPosicionsBarallaAlt[0] = 0.84f;
		llistaPosicionsBarallaAlt[1] = 0.62f;
		llistaPosicionsBarallaAlt[2] = 0.4f;
		ID = gameObject.GetInstanceID();
		missatge = false;
		
		// Assignació d'events
		ConfirmacioDescartaCanvis.ferMissatge += ferMissatge;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(detectaClick()){
			if(estat.GetType().ToString().Equals("EstatCartaMenu") 
				|| estat.GetType().ToString().Equals("EstatCartaBaralla") ){
				ensenyarInformacioCarta();
			} else if(estat.GetType().ToString().Equals("EstatCartaEdicioSeleccionada")){
				treureInformacioCarta();
			} else if(estat.GetType().ToString().Equals("EstatCartaObjectiuCanvi")){
				canviarCartes();
			}
		}
		estat.pintarCarta();
	}
	
	// Assignem la posició de la llista de cartes disponibles (al menu de cartes disponibles)
	// per ensenyar-la posteriorment. Només entrarem en aquest mètode quan haguem de repartir
	// noves cartes a l'usuari.
	public void donarPosicioLlista(int p){
		posicioLlista = p;
		// S'agafa la posició X del mon a partir de dades guardades segons la posició de la 
		// llista de cartes disponibles en la que es troba aquesta carta.
		gameObject.transform.position = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Camera.mainCamera.pixelWidth*llistaPosicions[p], 
			Camera.mainCamera.pixelHeight*0.144f, 
			-9.999999f + 41.04723f));
		gameObject.transform.localScale = new Vector3(0.6430312f, 
					0.7f, 
					0.7716374f);
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
	
	public void donarPosicioLlistaBaralla(int i, int j){
		posicioLlista = i;
		posicioLlistaBaralla = j;
		gameObject.transform.position = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Camera.mainCamera.pixelWidth*llistaPosicionsBaralla[j], 
			Camera.mainCamera.pixelHeight*llistaPosicionsBarallaAlt[i], 
			-9.999999f + 41.04723f));
		gameObject.transform.localScale = new Vector3(0.6430312f, 
					0.7f, 
					0.7716374f);
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
	
	public void assignarInformacioCarta(InformacioCarta i){
		if(i.GetType().ToString().Equals("InformacioCartaPersonatge")){
			iC = new InformacioCartaPersonatge((InformacioCartaPersonatge)i);
			tipus = new TipusCartaPersonatge();
		}else if(i.GetType().ToString().Equals("InformacioCartaBonificacio")){
			iC = new InformacioCartaBonificacio((InformacioCartaBonificacio)i);
			tipus = new TipusCartaBonificacio();
		}
	}
	
	public void activarCarta(){
		estat = new EstatCartaMenu(this);
		// En aquest punt tenim la informació de la carta amb detall, i podem carregar
		// la carta desitjada desde la carpeta Resources
		figura = gameObject;
		figura.transform.position = posicio;
		ID = gameObject.GetInstanceID();
	}
	
	public void activarCartaBaralla(){
		estat = new EstatCartaBaralla(this);
		// En aquest punt tenim la informació de la carta amb detall, i podem carregar
		// la carta desitjada desde la carpeta Resources
		figura = gameObject;
		figura.transform.position = posicio;
		ID = gameObject.GetInstanceID();
	}
	
	public void desapareixerCarta(){
		
	}
	
	public void desactivarCarta(){
		estat = new EstatCartaEdicioNoVisible(this);

		Destroy(titol);
		Destroy(this.atacLlarg);
		Destroy(this.atacCurt);
		Destroy(this.defensa);
		Destroy(this.moviment);
		Destroy(this.distanciaAtac);
		Destroy(this.iconCarta);
		Destroy(this.textBonificacio);
		Destroy(this.textUber);
		
		titol = null;
		atacLlarg = null;
		atacCurt = null;
		defensa = null;
		moviment = null;
		distanciaAtac = null;
		iconCarta = null;
		textBonificacio = null;
		textUber = null;
	}
	
	private bool detectaClick(){
		bool click = false;
		string state = estat.GetType().ToString();
		if(Input.GetMouseButtonUp(0) && !state.Equals("EstatCartaEdicioNoVisible") && !missatge){
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float marge = 100;
 			if (Physics.Raycast (ray, out hit, marge)){
				if(hit.collider.gameObject.transform.tag.Equals("Carta") 
					|| hit.collider.gameObject.transform.tag.Equals("CartaSeleccionada")){
					CartaEdicio c = (CartaEdicio) hit.collider.gameObject.GetComponent(typeof(CartaEdicio));
					if(c.ID.Equals(this.ID)){
						click = true;
					}
				}else if(hit.collider.gameObject.transform.tag.Equals("Cap")){
					GameObject t1 = hit.collider.gameObject;
					GameObject t2 = iconCarta;
					if(t1.GetInstanceID().Equals(t2.GetInstanceID())){
						click = true;
					}
				}
			}
		}
		return click;
	}
	
	private void ensenyarInformacioCarta(){
		AccioEdicio a = new AccioSeleccionarCarta(this);
		a.executarAccio();
	}
	
	private void treureInformacioCarta(){
		AccioEdicio a = new AccioNetejaPantalla();
		a.executarAccio();
	}
	
	private void canviarCartes(){
		CartaEdicio c = (CartaEdicio) GameObject.FindGameObjectWithTag("CartaSeleccionada").GetComponent("CartaEdicio");
		AccioEdicio a = new AccioCanviarCartes(this, c);
		a.executarAccio();
	}
	
	public void ferMissatge() {
		missatge = !missatge;
	}
}