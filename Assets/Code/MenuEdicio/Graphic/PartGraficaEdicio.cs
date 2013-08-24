using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartGraficaEdicio : MonoBehaviour {
	
	// Use this for initialization
	void Awake () {
	
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void confirmarNoDesaCanvis(){
		ConfirmacioDescartaCanvis cD = (ConfirmacioDescartaCanvis) Camera.mainCamera.gameObject.GetComponent("ConfirmacioDescartaCanvis");
		Destroy(cD);
		BotoDescartaCanvis bD = (BotoDescartaCanvis) Camera.mainCamera.gameObject.GetComponent("BotoDescartaCanvis");
		Destroy(bD);
		BotoEsborrarCarta bE = (BotoEsborrarCarta) Camera.mainCamera.gameObject.GetComponent("BotoEsborrarCarta");
		Destroy(bE);
		BotoDesaCanvis bC = (BotoDesaCanvis) Camera.mainCamera.gameObject.GetComponent("BotoDesaCanvis");
		Destroy(bC);
		
		Camera.mainCamera.gameObject.AddComponent("AnimacioPantallesEdicioFinal");
	}
	
	public void desaCanvis(){
		ConnexioEdicio con = (ConnexioEdicio) Camera.mainCamera.GetComponent("ConnexioEdicio") as ConnexioEdicio;
		#if UNITY_ANDROID
			con.Save(Application.persistentDataPath+ "/database.xml", con.p);
		#elif UNITY_STANDALONE_WIN
			con.Save(Application.persistentDataPath + "/database.xml", con.p);
		#endif
		
		ConfirmacioDescartaCanvis cD = (ConfirmacioDescartaCanvis) Camera.mainCamera.gameObject.GetComponent("ConfirmacioDescartaCanvis");
		Destroy(cD);
		BotoDescartaCanvis bD = (BotoDescartaCanvis) Camera.mainCamera.gameObject.GetComponent("BotoDescartaCanvis");
		Destroy(bD);
		BotoEsborrarCarta bE = (BotoEsborrarCarta) Camera.mainCamera.gameObject.GetComponent("BotoEsborrarCarta");
		Destroy(bE);
		BotoDesaCanvis bC = (BotoDesaCanvis) Camera.mainCamera.gameObject.GetComponent("BotoDesaCanvis");
		Destroy(bC);
		
		//Camera.mainCamera.gameObject.AddComponent("AnimacioPantallesEdicioFinal");
		
//		List<GameObject> g = new List<GameObject>(GameObject.FindGameObjectsWithTag("Carta"));
//		for(int i = 0; i < g.Count; i++){
//			Destroy (g[i]);
//		}
//		
//		g = new List<GameObject>(GameObject.FindGameObjectsWithTag("Cap"));
//		for(int i = 0; i < g.Count; i++){
//			Destroy (g[i]);
//		}
//		
//		g = new List<GameObject>(GameObject.FindGameObjectsWithTag("Text"));
//		for(int i = 0; i < g.Count; i++){
//			Destroy (g[i]);
//		}
//		
//		GameObject o = GameObject.FindGameObjectWithTag("BotoDreta") as GameObject;
//		Destroy (o);
//		o = GameObject.FindGameObjectWithTag("BotoEsquerre") as GameObject;
//		Destroy (o);
//		o = GameObject.FindGameObjectWithTag("MenuBaralla") as GameObject;
//		Destroy (o);
//		o = GameObject.FindGameObjectWithTag("MenuCartesDisponiblesEdicio") as GameObject;
//		Destroy (o);
//		
//		Destroy(Camera.mainCamera.GetComponent("ControlGeneralEdicio"));
//		Destroy(Camera.mainCamera.GetComponent("PartGraficaEdicio"));
//		Destroy(Camera.mainCamera.GetComponent("ConnexioEdicio"));
//		
//		ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
//		cJ.carregarPantallaMenuPrincipal();
		
		Camera.mainCamera.gameObject.AddComponent("AnimacioPantallesEdicioFinal");
	}
	
	public void noDesaCanvis(){
		Camera.mainCamera.gameObject.AddComponent("ConfirmacioDescartaCanvis");
	}
	
	public void ensenyarCartesEsquerre(int direccio){
		MenuCartesDisponiblesEdicio mC = (MenuCartesDisponiblesEdicio) GameObject.FindGameObjectWithTag("MenuCartesDisponiblesEdicio").GetComponent("MenuCartesDisponiblesEdicio");
		mC.demanarInformacioCartesNoves(direccio);
	}
	
	public void ensenyarCartesDreta(int direccio){
		MenuCartesDisponiblesEdicio mC = (MenuCartesDisponiblesEdicio) GameObject.FindGameObjectWithTag("MenuCartesDisponiblesEdicio").GetComponent("MenuCartesDisponiblesEdicio");
		mC.demanarInformacioCartesNoves(direccio);
	}
	
	public void esborrarCarta(CartaEdicio cartaActual){
		cartaActual.desactivarCarta();
		ConnexioEdicio con = (ConnexioEdicio) Camera.mainCamera.GetComponent("ConnexioEdicio") as ConnexioEdicio;
		ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
		
		int e;
		if(cartaActual.tipus.GetType().ToString().Equals("TipusCartaPersonatge")){
			e = con.searchCartaUsuariMenu((InformacioCartaPersonatge)cartaActual.iC);
			con.p.llistaPlayers[conMenu.jugador].baralla.c.llistaCartesBaralla.RemoveAt(e);
			con.cartesNoUtilitzades.RemoveAt(e);
		}
		else if(cartaActual.tipus.GetType().ToString().Equals("TipusCartaBonificacio")){
			e = con.searchCartaUsuariMenu((InformacioCartaBonificacio)cartaActual.iC);
			con.p.llistaPlayers[conMenu.jugador].baralla.c.llistaCartesBaralla.RemoveAt(e);
			con.cartesNoUtilitzades.RemoveAt(e);
		}
		
		MenuCartesDisponiblesEdicio mC = (MenuCartesDisponiblesEdicio) GameObject.FindGameObjectWithTag("MenuCartesDisponiblesEdicio").GetComponent("MenuCartesDisponiblesEdicio");
		mC.demanarInformacioCartesNoves(2);
	}
	
	public void netejarPantallaEdicio(){
		MenuBaralla mB =  (MenuBaralla) GameObject.FindGameObjectWithTag("MenuBaralla").GetComponent("MenuBaralla");
		for(int i = 0; i < mB.cartesDisponibles.Count; i++){
			string estat = mB.cartesDisponibles[i].estat.GetType().ToString();
			if(!estat.Equals("EstatCartaEdicioNoVisible")){
				mB.cartesDisponibles[i].estat = new EstatCartaBaralla(mB.cartesDisponibles[i]);
			}
		}
		MenuCartesDisponiblesEdicio mE = (MenuCartesDisponiblesEdicio) GameObject.FindGameObjectWithTag("MenuCartesDisponiblesEdicio").GetComponent("MenuCartesDisponiblesEdicio");
		for(int i = 0; i < mE.cartesDisponibles.Count; i++){
			string estat = mE.cartesDisponibles[i].estat.GetType().ToString();
			if(!estat.Equals("EstatCartaEdicioNoVisible")){
				mE.cartesDisponibles[i].estat = new EstatCartaMenu(mE.cartesDisponibles[i]);
			}
		}
		BotoEsborrarCarta bE = (BotoEsborrarCarta) Camera.mainCamera.gameObject.GetComponent("BotoEsborrarCarta");
		bE.desactivarBoto();
	}
	
	public void canviarCartes(CartaEdicio cartaActual, CartaEdicio cartaExterna, float startTime){
		Vector3 startPoint1;
		Vector3 endPoint1;
		Vector3 startPoint2;
		Vector3 endPoint2;
		
		startPoint1 = cartaActual.transform.position;
		endPoint1 = cartaExterna.transform.position;
		startPoint2 = cartaExterna.transform.position;
		endPoint2 = cartaActual.transform.position;
		
		cartaActual.estat = new EstatCartaMoviment(cartaActual, cartaExterna, startPoint1, endPoint1, startTime);
		cartaExterna.estat = new EstatCartaMoviment(cartaExterna, cartaActual, startPoint2, endPoint2, startTime);
	}
	
	public void seleccionarCarta(CartaEdicio c){
		c.estatAnterior = c.estat;
		c.estat = new EstatCartaEdicioSeleccionada(c);
		c.tag = "CartaSeleccionada";
		// Destaquem en color les cartes que poden ser 
		// substituïdes per la nostra carta actual
		if(c.estatAnterior.GetType().ToString().Equals("EstatCartaMenu")){
			MenuBaralla mB =  (MenuBaralla) GameObject.FindGameObjectWithTag("MenuBaralla").GetComponent("MenuBaralla");
			foreach(CartaEdicio cartaActual in mB.cartesDisponibles){
				string tipus = cartaActual.tipus.GetType().ToString();
				if(tipus.Equals(c.tipus.GetType().ToString())){
					cartaActual.estatAnterior = cartaActual.estat;
					cartaActual.estat = new EstatCartaObjectiuCanvi(cartaActual, c);
				}
			}
			BotoEsborrarCarta bE = (BotoEsborrarCarta) Camera.mainCamera.gameObject.GetComponent("BotoEsborrarCarta");
			bE.activarBoto();
		}else if(c.estatAnterior.GetType().ToString().Equals("EstatCartaBaralla")){
			MenuCartesDisponiblesEdicio mE =  (MenuCartesDisponiblesEdicio) GameObject.FindGameObjectWithTag("MenuCartesDisponiblesEdicio").GetComponent("MenuCartesDisponiblesEdicio");
			foreach(CartaEdicio cartaActual in mE.cartesDisponibles){
				if(!cartaActual.estat.GetType().ToString().Equals("EstatCartaEdicioNoVisible")){
					string tipus = cartaActual.tipus.GetType().ToString();
					if(tipus.Equals(c.tipus.GetType().ToString())){
						cartaActual.estatAnterior = cartaActual.estat;
						cartaActual.estat = new EstatCartaObjectiuCanvi(cartaActual, c);
					}
				}
			}
		}
	}
}
