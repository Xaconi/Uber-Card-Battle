using UnityEngine;
using System;
using System.Xml;
using System.Collections.Generic;

public class ControlGeneralJoc : ControlGeneral {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------

	public ControlGeneralFactory constructor{
		get;
		set;
	}

	public bool fiPantallaX;
	public XmlDocument xDoc;
	private string scenario;
	private string pantallaActual;
	private string dificultat;
	private string terreny;
	private string nivellCarta;
	private string tipusBatalla;
	private int nivellHistoria;
	private int guanyador = 0;
	
	// Dades estadístiques de la partida
	private int atacFet = 0;
	private int atacRebut = 0;
	private int cartesUtilitzades = 0;
	private int cartesNoUtilitzades = 0;
	private int nombreAccions = 0;
	private float tempsPartida = 0.0f;
	private string temps = "";
	private int personatgesUtilitzats = 0;
	private int bonificacionsUtilitzades = 0;
	private List<string> llistaLog = new List<string>();
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	void Awake(){
		this.constructor = new ControlGeneralFactory();
		scenario = "Hivern";
		fiPantallaX = false;
		
		// Carrega d'events
		ControlGeneralBatalla.fiPantalla += fiPantalla;
	}
	
	// Use this for initialization
	void Start () {
		Debug.Log("OK");
//		constructor.crearObjecteControl("Batalla");
//		pantallaActual = "Batalla";
//		constructor.crearObjecteControl("Edicio");
//		pantallaActual = "Edicio";
		constructor.crearObjecteControl("PantallaTitol");
		pantallaActual = "PantallaTitol";
		//Debug.Log("Afegida l'ordre de crear un objecte ControlGeneralBatalla.");
		Debug.Log("Afegida l'ordre de crear un objecte ControlGeneralEdicio.");
	}
	
	// Update is called once per frame
	void Update () {
		if(fiPantallaX){
			switch(pantallaActual){
			case "Batalla":
				// S'ha acabat la batalla i ja ha hagut un guanyador
				Debug.Log ("EVENT DE SORTIDA DE LA PANTALLA DE BATALLA");
				Camera.mainCamera.gameObject.AddComponent("MenuFinalPartida");
				break;
			}
			fiPantallaX = false;
		}
	}
	
	public void acabarPantalla(){
		fiPantallaX = true;
	}
	
	public string getScenario(){
		return scenario;
	}
	
	public void fiPantalla(){
		fiPantallaX = true;
	}
	
	public void assignarDadesPartidaRapida(string a, string b, string c){
		dificultat = a;
		terreny = b;
		nivellCarta = c;
		tipusBatalla = "Quick";
	}
	
	public void assignarDadesPartidaHistoria(string a, string b, string c, int d){
		dificultat = a;
		terreny = b;
		nivellCarta = c;
		tipusBatalla = "Historia";
		nivellHistoria = d;
	}
	
	public void assignarDadesFinalPartida(int a, int b, int c, int d, float e, List<string> llista, int f, int g){
		atacFet = a;
		atacRebut = b;
		cartesUtilitzades = c;
		cartesNoUtilitzades = 21-c;
		nombreAccions = d;
		tempsPartida = Time.time - e;
		TimeSpan timeSpan = TimeSpan.FromSeconds(tempsPartida);
		llistaLog = llista;
		temps = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		personatgesUtilitzats = f;
		bonificacionsUtilitzades = g;
		Debug.Log("Atac Fet: " + atacFet + 
			", Atac Rebut: " + atacRebut + 
			", Cartes Utilitzades: " + cartesUtilitzades + 
			", Cartes No Utilitzades: " + cartesNoUtilitzades +
			", Nombre Accions: " + nombreAccions +
			", Temps Partida: " + temps + 
			", Personatges Utilitzats: " + personatgesUtilitzats + 
			", Bonificacions Utilitzades: " + bonificacionsUtilitzades);
		ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
		conMenu.actualitzarEstadistiques(tipusBatalla, guanyador==1,atacFet, atacRebut, cartesUtilitzades, nombreAccions, tempsPartida, personatgesUtilitzats, bonificacionsUtilitzades, nivellHistoria);
		
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.gameObject.GetComponent("ControlGeneralBatalla");
		List<InformacioCarta> llistaCartesNoves = new List<InformacioCarta>();
		
		string nivellCartes = "";
		if(nivellCarta.Equals("Bronze")){
			nivellCartes = "Plata";
		}else if(nivellCarta.Equals("Plata")){
			nivellCartes = "Or";
		}else if(nivellCarta.Equals("Or")){
			nivellCartes = "Plati";
		}else if(nivellCarta.Equals("Plati")){
			nivellCartes = "Plati";
		}
		
		InformacioCartaPersonatge iC;
		if(guanyador == 1){
			System.Random rnd = new System.Random((int) Time.time);
			for(int i=0;i<3;i++){
				int sort = rnd.Next(1,5);	// Mirem quin personatge aleatori generem
				switch(sort){
					case 1:
						iC = cG.barallaUsuari.generadorCartaPersonatge("Metralleta", nivellCartes);
						Debug.Log(iC);
						llistaCartesNoves.Add(iC); // <--- nivellCarta
					break;
					case 2:
						iC = cG.barallaUsuari.generadorCartaPersonatge("Franco", nivellCartes);
						Debug.Log(iC);
						llistaCartesNoves.Add(iC); // <--- nivellCarta
					break;
					case 3:
						iC = cG.barallaUsuari.generadorCartaPersonatge("Bazooka", nivellCartes);
						Debug.Log(iC);
						llistaCartesNoves.Add(iC); // <--- nivellCarta
					break;
					case 4:
						iC = cG.barallaUsuari.generadorCartaPersonatge("Guerrero", nivellCartes);
						Debug.Log(iC);
						llistaCartesNoves.Add(iC); // <--- nivellCarta
					break;
				}
			}
			
			System.Random rnd2 = new System.Random((int) Time.time);
			for(int i=0;i<2;i++){
				int sort2 = rnd2.Next(1,4);	// Mirem quin personatge aleatori generem
				switch(sort2){
					case 1:
						llistaCartesNoves.Add(cG.barallaUsuari.generadorCartaBonificacio("Atac", nivellCartes)); // <--- nivellCarta
					break;
					case 2:
						llistaCartesNoves.Add(cG.barallaUsuari.generadorCartaBonificacio("Defensa", nivellCartes)); // <--- nivellCarta
					break;
					case 3:
						llistaCartesNoves.Add(cG.barallaUsuari.generadorCartaBonificacio("Uber", nivellCartes)); // <--- nivellCarta
					break;
				}
			}		
			conMenu.assignarNovesCartes(llistaCartesNoves);
		}
	}
	
	public string getDificultat(){
		return dificultat;
	}
	
	public string getNivellCartes(){
		return nivellCarta;
	}
	
	public string getEscenari(){
		return terreny;
	}
	
	public void assignarGuanyador(int g){
		guanyador = g;
	}
	
	public int getGuanyador(){
		return guanyador;
	}
	
	public void carregarPantallaTitol(){
		constructor.crearObjecteControl("Titol");
		pantallaActual = "Titol";
	}
	
	public void carregarPantallaEdicio(){
		constructor.crearObjecteControl("Edicio");
		pantallaActual = "Edicio";
	}
	
	public void carregarPantallaPerfils(){
		constructor.crearObjecteControl("Perfils");
		pantallaActual = "Perfils";
	}
	
	public void carregarPantallaMenuPrincipal(){
		constructor.crearObjecteControl("MenuPrincipal");
		pantallaActual = "MenuPrincipal";
	}
	
	public void carregarPantallaMenuQuick(){
		constructor.crearObjecteControl("MenuQuick");
		pantallaActual = "MenuQuick";
	}
	
	public void carregarPantallaMenuHistoria(){
		constructor.crearObjecteControl("MenuHistoria");
		pantallaActual = "MenuHistoria";
	}
	
	public void carregarPantallaMenuEstadistiques(){
		constructor.crearObjecteControl("MenuEstadistiques");
		pantallaActual = "MenuEstadistiques";
	}
	
	public void carregarPantallaBatalla(){
		constructor.crearObjecteControl("Batalla");
		pantallaActual = "Batalla";
	}
	
	public void carregarPantallaHowTo(){
		constructor.crearObjecteControl("HowTo");
		pantallaActual = "HowTo";
	}
	
	public int getAtacFet(){
		return atacFet;
	}
	
	public int getAtacRebut(){
		return atacRebut;
	}
	
	public int getCartesUtilitzades(){
		return cartesUtilitzades;
	}
	
	public int getCartesNoUtilitzades(){
		return cartesNoUtilitzades;
	}
	
	public int getNombreAccions(){
		return nombreAccions;
	}
	
	public string getTempsPartida(){
		return temps;
	}
	
	public int getPersonatgesUtilitzats(){
		return personatgesUtilitzats;
	}
	
	public int getBonificacionsUtilitzades(){
		return bonificacionsUtilitzades;
	}
	
	public List<string> getLlistaLog(){
		return llistaLog;
	}
}
