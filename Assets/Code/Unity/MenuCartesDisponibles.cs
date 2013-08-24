using UnityEngine;
using System.Collections.Generic;

public class MenuCartesDisponibles : MonoBehaviour {

	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	public Vector3 posicio;
	public GameObject figura;
	public Material textura;
	public List<Carta> cartesDisponibles;
	public List<Carta> cartesDisponiblesRival;
	public int nCartes = 0;
	public int nCartesRival = 0;
	public int cartesBase = 5;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public void reduirCartesDisponibles(int jugador){
		//throw new System.NotImplementedException();
		if(jugador == 1) nCartes--;
		else if(jugador == 2) nCartesRival--;
	}
	
	public void augmentarCartesDisponibles(int jugador){
		//throw new System.NotImplementedException();
		if(jugador == 1) nCartes++;
		else if(jugador == 2) nCartesRival++;
	}
	
	public void Awake(){
		figura = this.gameObject;
		float ratio = Camera.mainCamera.aspect;
		float ratioOriginal = 16.0f/9.0f;
		float diferenciaRatio = ratio / ratioOriginal;
		figura.transform.position = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Camera.mainCamera.pixelWidth*0.5f, 
			Camera.mainCamera.pixelHeight*0.143f, 
			-9.999999f - figura.transform.position.z));
		//#if UNITY_EDITOR
		figura.transform.localScale = new Vector3 (figura.transform.localScale.x*diferenciaRatio, 
			figura.transform.localScale.y,
			figura.transform.localScale.z*diferenciaRatio) ;
//		#elif UNITY_ANDROID
//		figura.transform.localScale = new Vector3 ((5.5f * Screen.width)/Screen.width, 
//			figura.transform.localScale.y,
//			figura.transform.localScale.z);
//		#endif
	
		cartesDisponibles = new List<Carta>();
		cartesDisponiblesRival = new List<Carta>();
	}

	public void Start(){
		//throw new System.NotImplementedException();
	}

	public void Update(){
		//throw new System.NotImplementedException();
	}

	public int getEspaisBuits(){
		throw new System.NotImplementedException();
	}
	
	public void demanarInformacioCartesNoves(Baralla b){
		Debug.Log("Entro a la funció de demanar cartes noves i tinc " + nCartes + ".");
		int cartesNecessaries = cartesBase - nCartes;
		Debug.Log("Vaig a demanar " + cartesNecessaries + " cartes.");
		List<InformacioCarta> llistaInformacio  = b.donaCartesNoves(cartesNecessaries);
		int j = 0;
		for(int i = 0; i < cartesBase; i++){
			if(cartesDisponibles[i].estat.GetType().ToString().Equals("EstatCartaUtilitzada") && llistaInformacio.Count > j){
				cartesDisponibles[i].assignarInformacioCarta(llistaInformacio[j]);
				cartesDisponibles[i].donarPosicioLlista(i);
				cartesDisponibles[i].activarCarta();
				nCartes++;
				j++;
			}else if(cartesDisponibles[i].estat.GetType().ToString().Equals("EstatCartaNoVisible")){
				cartesDisponibles[i].donarPosicioLlista(i);
				cartesDisponibles[i].activarCarta();
			}
		}
		Debug.Log("He acabat de demanar cartes i ara tinc" + nCartes + " .");
	}
	
	public void demanarInformacioCartesNovesRival(Baralla b){
		Debug.Log("Entro a la funció de demanar cartes noves.");
		int cartesNecessaries = cartesBase - nCartesRival;
		List<InformacioCarta> llistaInformacio  = b.donaCartesNoves(cartesNecessaries);
		int j = 0;
		for(int i = 0; i < cartesBase; i++){
			if(cartesDisponiblesRival[i].estat.GetType().ToString().Equals("EstatCartaUtilitzada") && llistaInformacio.Count > j){
				cartesDisponiblesRival[i].assignarInformacioCarta(llistaInformacio[j]);
				cartesDisponiblesRival[i].donarPosicioLlista(i);
				cartesDisponiblesRival[i].activarCarta();
				nCartesRival++;
				j++;
			}else if(cartesDisponiblesRival[i].estat.GetType().ToString().Equals("EstatCartaNoVisible")){
				cartesDisponiblesRival[i].donarPosicioLlista(i);
				cartesDisponiblesRival[i].activarCarta();
			}
		}
	}
	
	public void crearCartes(){
		for(int i = 1; i <= cartesBase*2; i++) GameObject.Instantiate(Resources.Load("Carta"));
		GameObject[] llistaCartes = GameObject.FindGameObjectsWithTag("CartaNoVisible");
		for(int i = 0; i < cartesBase; i++){
			llistaCartes[i].tag = "Carta";
			cartesDisponibles.Add((Carta) llistaCartes[i].GetComponent("Carta"));
			cartesDisponibles[i].donarPosicioLlista(i);
		}
		for(int i = cartesBase; i < cartesBase*2; i++){
			llistaCartes[i].gameObject.tag = "Carta";
			cartesDisponiblesRival.Add((Carta) llistaCartes[i].GetComponent("Carta"));
			cartesDisponiblesRival[i-cartesBase].donarPosicioLlista(i-cartesBase);
		}
	}
	
	public void crearCartesIA(int jugador){
		for(int i = 1; i <= cartesBase*2; i++) GameObject.Instantiate(Resources.Load("Carta"));
		GameObject[] llistaCartes = GameObject.FindGameObjectsWithTag("CartaNoVisible");
		for(int i = 0; i < cartesBase; i++){
			llistaCartes[i].tag = "Carta";
			cartesDisponibles.Add((Carta) llistaCartes[i].GetComponent("Carta"));
			cartesDisponibles[i].donarPosicioLlista(i);
		}
		for(int i = cartesBase; i < cartesBase*2; i++){
			llistaCartes[i].gameObject.tag = "Carta";
			cartesDisponiblesRival.Add((Carta) llistaCartes[i].GetComponent("Carta"));
			cartesDisponiblesRival[i-cartesBase].donarPosicioLlista(i-cartesBase);
		}
		
		IA ia = (IA) Camera.mainCamera.gameObject.GetComponent("IA");
		ia.assignarCartes(cartesDisponiblesRival);
	}
	
	public void crearCartesMultijugadorLocal(int jugador){
			crearCartes();
			ocultarCartes(jugador);
	}
	
	public List<Carta> getCartesDisponibles(){
		return cartesDisponibles;
	}
	
	public List<Carta> getCartesDisponiblesRival(){
		return cartesDisponiblesRival;
	}
	
	public void ocultarCartes(int jugador){
		if(jugador == 1){
			for(int i = 0; i < cartesBase; i++){
				cartesDisponiblesRival[i].gameObject.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
				if(!cartesDisponiblesRival[i].estat.GetType().ToString().Equals("EstatCartaUtilitzada")){
					cartesDisponiblesRival[i].estat = new EstatCartaNoVisible(cartesDisponiblesRival[i]);
					Destroy(cartesDisponiblesRival[i].titol);
					Destroy(cartesDisponiblesRival[i].atacLlarg);
					Destroy(cartesDisponiblesRival[i].atacCurt);
					Destroy(cartesDisponiblesRival[i].defensa);
					Destroy(cartesDisponiblesRival[i].moviment);
					Destroy(cartesDisponiblesRival[i].distanciaAtac);
					Destroy(cartesDisponiblesRival[i].iconCarta);
					Destroy(cartesDisponiblesRival[i].textBonificacio);
					Destroy(cartesDisponiblesRival[i].textUber);
					
				}
			}
		}else{
			for(int i = 0; i < cartesBase; i++){
				cartesDisponibles[i].gameObject.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
				if(!cartesDisponibles[i].estat.GetType().ToString().Equals("EstatCartaUtilitzada")){
					cartesDisponibles[i].estat = new EstatCartaNoVisible(cartesDisponibles[i]);
					Destroy(cartesDisponibles[i].titol);
					Destroy(cartesDisponibles[i].atacLlarg);
					Destroy(cartesDisponibles[i].atacCurt);
					Destroy(cartesDisponibles[i].defensa);
					Destroy(cartesDisponibles[i].moviment);
					Destroy(cartesDisponibles[i].distanciaAtac);
					Destroy(cartesDisponibles[i].iconCarta);
					Destroy(cartesDisponibles[i].textBonificacio);
					Destroy(cartesDisponibles[i].textUber);
					
				}
			}
		}
	}
}
