using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IA : MonoBehaviour {
	
	// IA: BIA - Best Immediate Action
	
	// Intel·ligència artificial central del joc. El seu funcionament es basa en el càlcul de la seva
	// millor acció possible, separant els moviments, i analitzant el tauler de joc en cada càlcul.
	// Així, la IA fará 5 càlculs de millor moviment possible, en correspondència amb les 5 accions que
	// té al seu torn. Per cada càlcul, analitzarà totes les possibilitats que té, tant en moviments
	// de personatges ja presents al tauler de joc, com al fet de treure al mateix tauler personatges
	// nous o aplicar bonificacions. A totes aquestes accions, li aplicarà una heurística en correspon-
	// dència amb el valor de la jugada feta. No serà el mateix atacar a un personatge rival, que moure
	// un personatge cap a darrere del tauler, o situar-ho, per exemple, en una posicio perillosa. Aques-
	// ta heurística, ve determinada pel valor del moviment fet, amb el valor d'una possible acció futura.
	// Per això, valorarem més moure un personatge a una posició on després, si ataca, pugui destruir la
	// base rival, que moure'l a una posició més defensiva. D'aquesta manera, quan la IA ja te llests els 
	// 5 moviments, va executant una per una les ordres d'aquests, enviant les dades pertinents a l'objecte
	// PartGrafica, per tal que aquest modifiqui la vista del tauler, amb el moviment pertinent.
	
	// TODO
	// · Controlar el tema del temps que ha de deixar passar abans de executar la següent acció
	// · HACER!!!!! Personatge en posició per matar la base rival -> Prioritat!!!
	// · HACER!!!!! Personatge en posició per atacar la base rival -> Prioritat!!!
	
	// DONE
	// · Fer que el joc, donat una partida d'un jugador, agafi les característiques de dificultat i nivell
	// de l'objecte ControlGeneralJoc, les guardi, i pugui inicialitzar-se
	// · Fer que el joc, quan l'usuari acabi el seu torn, mantingui la pantalla intacta, esperant la IA
	// · Convertir el tauler en un objecte de software, el qual la IA pugui llegir fàcilment
	// · Llegir l'estat del tauler i que, donat el recorregut en cerca de les accions possibles, pugui inter-
	// pretar les possibilitats d'una acció, si en troba una.
	// · Que pugui fer una acció "virtual" interpretant els resultats obtinguts.
	// · Que, donats aquests resultats de l'acció "virtual" assigni una heurística al moviment, tenint en compte
	// tots els factors possibles
	// · Que es pugui quedar amb el millor moviment, donades les heurístiques d'aquests
	// · Que, donats les cinc accions, pugui enviar l'ordre a l'objecte PartGrafica, per tal d'executar-les
	// en ordre
	
	// HEURÍSTICA
	// · Sortida del personatge al tauler
	//		* Personatge en posició per matar a un persontge rival
	//		* Personatge en posició per atacar a un persontge rival
	//		* Personatge en posició normal i només ell a l'equip de la IA
	//		* Personatge en posició normal
	// 		* Personatge en posicio de perill
	// · Moviment del personatge
	//		* HACER!!!!! Personatge en posició per matar la base rival
	//		* Personatge en posició per matar a un persontge rival
	//		* HACER!!!!! Personatge en posició per atacar la base rival
	//		* Personatge en posició per atacar a un persontge rival
	//		* Personatge en posició normal
	// 		* Personatge en posicio de perill
	// · Atac del personatge a personatge
	//		* Rival mort i personatge en posició per matar a un persontge rival
	//		* Rival mort i personatge en posició per atacar la base rival
	//		* Rival mort i personatge en posició per atacar personatge rival
	//		* Rival atacat i personatge en posició per matar personatge rival
	//		* Rival atacat i personatge en posició per atacar personatge rival
	// 		* Personatge en posicio de perill
	// · Atac del personatge a base
	//		* Base morta
	//		* Base atacada i base morta
	//		* Base atacada i base atacada
	//		* Base atacada i rival mort
	// 		* Base atacada i rival atacat
	//		* Base atacada i personatge normal
	// 		* Base atacada i personatge en perill
	// · Aplicació d'una bonificació
	//		* Bonificació assignada i base morta (per l'augment d'atac)
	//		* Bonificació assignada i personatge mort (per l'augment d'atac)
	//		* Bonificació assignada i base atacada
	//		* Bonificació assignada i personatge atacat
	//		* Bonificació assignada i personatge normal
	// 		* Bonificació assignada i personatge en perill
	
	private bool activada;
	private Fitxa[,] fitxesTauler;
	private List<Carta> llistaCartes;
	private Baralla baralla;
	private int cartesBase = 5;
	private int cartesActuals = 0;
	private int propietariIA = 2;
	private int llargadaTauler;
	private int ampladaTauler;
	private int accionsFetes = 0;
	private int accionsMaximes = 5;
	private float actitudDefensiva = 0.5f;	// Float pel qual multipliquem les accions defensives
	private float actitudAtacant = 1;		// Float pel qual multipliquem les accions ofensives
	private float bonificacioUber = 1.1f;	// Float pel qual multipliquem l'assignació de bonificacions Uber
	
	public delegate void TornRival();			// Event per bloquejar qualsevol acció de l'usuari amb
  	public static event TornRival tornRival;	// la pantalla...
	
	void Awake(){
		activada = false;
		llistaCartes = new List<Carta>();
		ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
		string dificultat = cJ.getDificultat();
		switch(dificultat){
			case "FACIL":
				actitudAtacant = 0.5f;
				actitudDefensiva = 1;
			break;
			case "MITJA":
				actitudAtacant = 0.7f;
				actitudDefensiva = 0.7f;
			break;
			case "DIFICIL":
				actitudAtacant = 1;
				actitudDefensiva = 0.5f;
			break;
		}
	}

	// Use this for initialization
	void Start () {
		Tauler tauler = (Tauler) (GameObject.FindGameObjectWithTag("Tauler")).GetComponent("Tauler");
		llargadaTauler = tauler.llargadaFitxesTauler;
		ampladaTauler = tauler.ampladaFitxesTauler;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public bool tornarMoviments(){
		return true;
	}
	
	public void activar(){
		if(accionsFetes == 0){
			// Si acabem d'entrar a la IA, bloquejem al jugador rival
			// per tal que aquest no pugui interactuar amb la pantalla
			tornRival();
		}
		if(accionsFetes < accionsMaximes){
			activada = true;
			recollirDadesTauler();
			accionsFetes++;
			//cercarAccionsTauler();
			AccioIA accioMillor = cercarAccionsBaralla();
			AccioIA accioMillorTauler = cercarAccionsTauler();
			if(accioMillorTauler.heuristica > accioMillor.heuristica){
				accioMillor = accioMillorTauler;
				Debug.Log("L'accio del tauler amb heuristica " + accioMillorTauler.heuristica + " es canviada es millor que la de la baralla");
			}
			//Debug.Log("La millor acció es del tipus " + accioMillor.tipus + ", involucra al personatge " + ((InformacioCartaPersonatge)accioMillor.cartaActual.iC).nom + " amb destí a la fitxa " + accioMillor.fitxaDesti.fila + ", " + accioMillor.fitxaDesti.columna);
			Debug.Log("Accions fetes " + accionsFetes);
			accioMillor.executarAccio();
		}else{
			Accio neteja = new AccioNetejarTauler();
			neteja.executarAccio();
			acabarTornIA();
			accionsFetes = 0;
			
			// Finalitzades les accions de la IA, desactivem el bloqueig
			// al jugador rival
			tornRival();
		}
	}
	
	private void recollirDadesTauler(){
		Tauler tauler = (Tauler) (GameObject.FindGameObjectWithTag("Tauler")).GetComponent("Tauler");
		fitxesTauler = tauler.getLlistaFitxes();
		if(accionsFetes == 0){
			List<InformacioCarta> lC = baralla.donaCartesNoves(cartesBase - cartesActuals);
			cartesActuals += lC.Count;
			int j = 0;
			Debug.Log("La baralla nova de la IA es porta " + lC.Count);
			if(lC.Count != 0){
				for(int i = 0; i < llistaCartes.Count; i++){
					if(llistaCartes[i].estat.GetType().ToString().Equals("EstatCartaUtilitzada")){
						if(j != lC.Count){
							Debug.Log("Assigno la carta " + i);
							llistaCartes[i].assignarInformacioCarta(lC[j]);
							llistaCartes[i].estat = new EstatCartaNoVisible(llistaCartes[i]);
							j++;
						}
					}
				}
			}
		}
	}
	
	public void assignarBaralla(Baralla b){
		baralla = b;
	}
	
	public void assignarCartes(List<Carta> lC){
		Debug.Log("Soc la IA i rebo " + lC.Count + " cartes");
		llistaCartes = lC;
	}
	
	private AccioIA cercarAccionsBaralla(){
		AccioIA accioActual = new AccioIA(0);
		for(int i = 0; i < llistaCartes.Count; i++){
			Carta cartaActual = llistaCartes[i];
			if(cartaActual.estat.GetType().ToString().Equals("EstatCartaNoVisible")){
				if(cartaActual.tipus.GetType().ToString().Equals("TipusCartaPersonatge")){
					AccioIA a = mirarSortidaPersonatge(cartaActual);
					if(a.heuristica > accioActual.heuristica){
						Debug.Log("Trobo una acció millor, amb heurística" + a.heuristica);
						accioActual = a;
					}
				}else if(cartaActual.tipus.GetType().ToString().Equals("TipusCartaBonificacio")){
					AccioIA a = mirarBonificacioPersonatge(cartaActual);
					if(a.heuristica > accioActual.heuristica){
						Debug.Log("Trobo una acció millor, amb heurística" + a.heuristica);
						accioActual = a;
					}
				}
			}
		}
		return accioActual;
	}
	
	private AccioIA mirarBonificacioPersonatge(Carta cartaActual){
		AccioIA accioActual = new AccioIA(0);
		for(int i=0;i<ampladaTauler; i++){
			for(int j=0;j<llargadaTauler;j++){
				Fitxa fitxaActual = fitxesTauler[i,j];
				if(fitxaActual.tePersonatge() && fitxaActual.personatge.propietari == 2){
					// El personatge es de la IA i se li pot donar una aplicació
					AccioIA a = valorarAccioBonificacioPersonatge(cartaActual, fitxaActual.personatge);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}
			}
		}
		return accioActual;
	}
	
	private AccioIA cercarAccionsTauler(){
		AccioIA accioActual = new AccioIA(0);
		for(int i = 0; i < ampladaTauler; i++){
			for(int j = 0; j < llargadaTauler; j++){
				Fitxa fitxaActual = fitxesTauler[i,j];
				if(fitxaActual.tePersonatge() && fitxaActual.personatge.propietari == propietariIA){
					// La fitxa te un personatge de la IA a sobre
					AccioIA a = mirarMovimentsPersonatge(fitxaActual.personatge);
					if(a.heuristica > accioActual.heuristica){
						Debug.Log("Trobo una acció millor, amb heurística" + a.heuristica);
						accioActual = a;
					}
					a = mirarAtacsPersonatge(fitxaActual.personatge);
					if(a.heuristica > accioActual.heuristica){
						Debug.Log("Trobo una acció millor, amb heurística" + a.heuristica);
						accioActual = a;
					}
				}
			}
		}
		return accioActual;
	}
	
	private AccioIA valorarAccioBonificacioPersonatge(Carta cartaActual, Personatge personatgeUsuari){
		AccioIA accioActual = new AccioIA(0);
		InformacioCartaBonificacio iC;
		if(personatgeUsuari.fitxaActual.columna <= 3){
			// El rival pot atacar la base de l'usuari
			GameObject[] b = GameObject.FindGameObjectsWithTag("Base");
			Base b1 = (Base) b[0].GetComponent("Base");
			Base b2 = (Base) b[1].GetComponent("Base");
			Base baseObjectiu;
			if(b1.propietari == 2) baseObjectiu = b2;
			else baseObjectiu = b1;
			
			iC = ((InformacioCartaBonificacio) cartaActual.iC);
			if(iC.nom.ToString().Equals("Atac")){
				if((personatgeUsuari.atacLlargaDistancia + iC.atac) >= baseObjectiu.defensa
				&& (personatgeUsuari.atacLlargaDistancia) < baseObjectiu.defensa){
					// L'atac del personatge només serà mortal si li augmentem l'atac
					AccioIA a = new AccioIA("BonificacioPersonatge", cartaActual, personatgeUsuari, DadesHeuristiques.aplicacioBonificacio.mataBase);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}else if((personatgeUsuari.atacLlargaDistancia + iC.atac) < baseObjectiu.defensa){
					// L'atac del personatge no serà mortal ni augmentant-li l'atac
					AccioIA a = new AccioIA("BonificacioPersonatge", cartaActual, personatgeUsuari, DadesHeuristiques.aplicacioBonificacio.atacaBase);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}
			}else if(iC.nom.ToString().Equals("Defensa")){
				if(personatgeUsuari.defensa < personatgeUsuari.defensa/2){
					// El personatge actual es troba ferit
					AccioIA a = new AccioIA("BonificacioPersonatge", cartaActual, personatgeUsuari, DadesHeuristiques.aplicacioBonificacio.personatgeFerit);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}else{
					AccioIA a = new AccioIA("BonificacioPersonatge", cartaActual, personatgeUsuari, DadesHeuristiques.aplicacioBonificacio.personatgeNormal);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}
			}else if(iC.nom.ToString().Equals("Uber")){
				if((personatgeUsuari.atacLlargaDistancia + iC.atac) >= baseObjectiu.defensa
				&& (personatgeUsuari.atacLlargaDistancia) < baseObjectiu.defensa){
					// L'atac del personatge només serà mortal si li augmentem l'atac
					AccioIA a = new AccioIA("BonificacioPersonatge", cartaActual, personatgeUsuari, DadesHeuristiques.aplicacioBonificacio.mataBase*bonificacioUber);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}else if((personatgeUsuari.atacLlargaDistancia + iC.atac) < baseObjectiu.defensa){
					// L'atac del personatge no serà mortal ni augmentant-li l'atac
					AccioIA a = new AccioIA("BonificacioPersonatge", cartaActual, personatgeUsuari, DadesHeuristiques.aplicacioBonificacio.atacaBase*bonificacioUber);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}
			}
		}
		
		// Part d'atac a altres personatges
		for(int i=0;i<ampladaTauler; i++){
			for(int j=0;j<llargadaTauler;j++){
				Fitxa fitxaActual = fitxesTauler[i,j];
				int distanciaFitxa = calcularDistancia(personatgeUsuari.fitxaActual, fitxaActual);
				int distanciaAtac = personatgeUsuari.distanciaAtac;
				if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && personatgeUsuari.esPersonatgeEnemic(fitxaActual.personatge)){
					// El personatge pot atacar al personatge rival
					iC = ((InformacioCartaBonificacio) cartaActual.iC);
					Personatge personatgeRival = fitxaActual.personatge;
					if(iC.nom.ToString().Equals("Atac")){
						if((personatgeUsuari.atacLlargaDistancia + iC.atac) >= personatgeRival.defensa
						&& (personatgeUsuari.atacLlargaDistancia) < personatgeRival.defensa){
							// L'atac del personatge només serà mortal si li augmentem l'atac
							AccioIA a = new AccioIA("BonificacioPersonatge", cartaActual, personatgeUsuari, DadesHeuristiques.aplicacioBonificacio.mataPersonatge);
							if(a.heuristica > accioActual.heuristica){
								accioActual = a;
							}
						}else if((personatgeUsuari.atacLlargaDistancia + iC.atac) < personatgeRival.defensa){
							// L'atac del personatge no serà mortal ni augmentant-li l'atac
							AccioIA a = new AccioIA("BonificacioPersonatge", cartaActual, personatgeUsuari, DadesHeuristiques.aplicacioBonificacio.atacaPersonatge);
							if(a.heuristica > accioActual.heuristica){
								accioActual = a;
							}
						}
					}else if(iC.nom.ToString().Equals("Defensa")){
						if(personatgeUsuari.defensa < personatgeUsuari.defensa/2){
							// El personatge actual es troba ferit
							AccioIA a = new AccioIA("BonificacioPersonatge", cartaActual, personatgeUsuari, DadesHeuristiques.aplicacioBonificacio.personatgeFerit);
							if(a.heuristica > accioActual.heuristica){
								accioActual = a;
							}
						}else{
							AccioIA a = new AccioIA("BonificacioPersonatge", cartaActual, personatgeUsuari, DadesHeuristiques.aplicacioBonificacio.personatgeNormal);
							if(a.heuristica > accioActual.heuristica){
								accioActual = a;
							}
						}
					}else if(iC.nom.ToString().Equals("Uber")){
						if((personatgeUsuari.atacLlargaDistancia + iC.atac) >= personatgeRival.defensa
						&& (personatgeUsuari.atacLlargaDistancia) < personatgeRival.defensa){
							// L'atac del personatge només serà mortal si li augmentem l'atac
							AccioIA a = new AccioIA("BonificacioPersonatge", cartaActual, personatgeUsuari, DadesHeuristiques.aplicacioBonificacio.mataPersonatge*bonificacioUber);
							if(a.heuristica > accioActual.heuristica){
								accioActual = a;
							}
						}else if((personatgeUsuari.atacLlargaDistancia + iC.atac) < personatgeRival.defensa){
							// L'atac del personatge no serà mortal ni augmentant-li l'atac
							AccioIA a = new AccioIA("BonificacioPersonatge", cartaActual, personatgeUsuari, DadesHeuristiques.aplicacioBonificacio.atacaPersonatge*bonificacioUber);
							if(a.heuristica > accioActual.heuristica){
								accioActual = a;
							}
						}
					}
				}
			}
		}
		return accioActual;
	}
	
	private AccioIA mirarSortidaPersonatge(Carta cartaActual){
		AccioIA accioActual = new AccioIA(0);
		for(int i=0;i<6; i++){
			for(int j=8;j<11;j++){
				Fitxa fitxaActual = fitxesTauler[i,j];
				if(!fitxaActual.tePersonatge()){
					AccioIA a = valorarAccioSortidaPersonatge(cartaActual, fitxaActual);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}
			}
		}
		return accioActual;
	}
	
	private AccioIA mirarAtacsPersonatge(Personatge personatgeUsuari){
		AccioIA accioActual = new AccioIA(0);
		Fitxa fitxaUsuari = personatgeUsuari.fitxaActual;
		int distanciaMoviment = personatgeUsuari.distanciaMoviment;
		int distanciaAtac = personatgeUsuari.distanciaAtac;
		if(personatgeUsuari.nom.ToString().Equals("Metralleta")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(fitxaUsuari, fitxaActual);
					if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && personatgeUsuari.esPersonatgeEnemic(fitxaActual.personatge)){
						// El personatge pot atacar al personatge rival
						AccioIA a;
						if(distanciaFitxa == 1) a = valorarAccioAtacPersonatge(personatgeUsuari, fitxaActual.personatge, personatgeUsuari.atacCurtaDistancia);
						else a = valorarAccioAtacPersonatge(personatgeUsuari, fitxaActual.personatge, personatgeUsuari.atacLlargaDistancia);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
		}else if(personatgeUsuari.nom.Equals("Franco")){
			for(int i=0;i<llargadaTauler; i++){
				Fitxa fitxaActual = fitxesTauler[personatgeUsuari.fitxaActual.fila-1,i];
				int distanciaFitxa = calcularDistancia(fitxaUsuari, fitxaActual);
				if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac 
				&& personatgeUsuari.esPersonatgeEnemic(fitxaActual.personatge)
				&& !fitxaActual.personatge.estat.GetType().ToString().Equals("EstatPersonatgeNoVisible")){
					// El personatge pot atacar al personatge rival
					AccioIA a;
					if(distanciaFitxa == 1) a = valorarAccioAtacPersonatge(personatgeUsuari, fitxaActual.personatge, personatgeUsuari.atacCurtaDistancia);
					else a = valorarAccioAtacPersonatge(personatgeUsuari, fitxaActual.personatge, personatgeUsuari.atacLlargaDistancia);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}
			}
		}else if(personatgeUsuari.nom.Equals("Guerrero")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(fitxaUsuari, fitxaActual);
					if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && personatgeUsuari.esPersonatgeEnemic(fitxaActual.personatge)){
						// El personatge pot atacar al personatge rival
						AccioIA a = valorarAccioAtacPersonatge(personatgeUsuari, fitxaActual.personatge, personatgeUsuari.atacCurtaDistancia);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
		}else if(personatgeUsuari.nom.Equals("Bazooka")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(fitxaUsuari, fitxaActual);
					if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && personatgeUsuari.esPersonatgeEnemic(fitxaActual.personatge)){
						// El personatge pot atacar al personatge rival
						AccioIA a = valorarAccioAtacPersonatge(personatgeUsuari, fitxaActual.personatge, personatgeUsuari.atacLlargaDistancia);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
		}
		
		// Mirar si la fitxa del personatge es a dins de la zona segura enemiga 
		// per atacar la seva base
		if(!personatgeUsuari.nom.ToString().Equals("Guerrero")){
			if(personatgeUsuari.propietari == 2){
				if(fitxaUsuari.columna <= 3){
					// El rival pot atacar la base de l'usuari
					GameObject[] b = GameObject.FindGameObjectsWithTag("Base");
					Base b1 = (Base) b[0].GetComponent("Base");
					Base b2 = (Base) b[1].GetComponent("Base");
					Base baseObjectiu;
					if(b1.propietari == 2) baseObjectiu = b2;
					else baseObjectiu = b1;
					AccioIA a = valorarAccioAtacBase(personatgeUsuari, baseObjectiu);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}
			}
		}
		return accioActual;
	}
	
	private AccioIA valorarAccioAtacBase(Personatge personatgeUsuari, Base baseObjectiu){
		int distanciaAtac = personatgeUsuari.distanciaAtac;
		AccioIA accioActual = new AccioIA(0);
		Fitxa f = personatgeUsuari.fitxaActual;
		if(personatgeUsuari.nom.ToString().Equals("Metralleta")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(f, fitxaActual);
					if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && esPersonatgeEnemic(fitxaActual.personatge)){
						// Te un personatge rival al seu abast
						if(distanciaAtac == 1){
							// Atac curta distancia
							if(baseObjectiu.defensa <= personatgeUsuari.atacLlargaDistancia){
								// Si el moviment previ ha sigut la mort de la base
								AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.mataBase);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								// Si el moviment previ ha sigut l'atac a la base sense mort
								if(fitxaActual.personatge.defensa <= personatgeUsuari.atacCurtaDistancia){
									// Si amb el moviment següent suposat, podem matar a un personatge
									AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseMataPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}else{
									AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseAtacaPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}
							}
						}else{
							// Atac llarga distancia
							if(baseObjectiu.defensa <= personatgeUsuari.atacLlargaDistancia){
								// Si el moviment previ ha sigut la mort de la base
								AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.mataBase);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								// Si el moviment previ ha sigut l'atac a la base sense mort
								if(fitxaActual.personatge.defensa <= personatgeUsuari.atacLlargaDistancia){
									// Si amb el moviment següent suposat, podem matar a un personatge
									AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseMataPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}else{
									AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseAtacaPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}
							}
						}
					}else{
						AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBasePersonatgeNormal);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
			// Part tornar a atacar la base
			if(baseObjectiu.defensa <= personatgeUsuari.atacLlargaDistancia){
				// Si el moviment previ ha sigut la mort de la base
				AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.mataBase);
				if(a.heuristica > accioActual.heuristica){
					accioActual = a;
				}
			}else{
				// Si el moviment previ ha sigut l'atac a la base sense mort
				if(baseObjectiu.defensa <= personatgeUsuari.atacLlargaDistancia){
					// Si amb el moviment següent suposat, podem matar a un personatge
					AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseMataBase);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}else{
					AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseAtacaBase);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}
			}
		}else if(personatgeUsuari.nom.ToString().Equals("Franco")){
			for(int i=0;i<llargadaTauler; i++){
				Fitxa fitxaActual = fitxesTauler[f.fila-1,i];
				int distanciaFitxa = calcularDistancia(f, fitxaActual);
				if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac 
				&& esPersonatgeEnemic(fitxaActual.personatge)
				&& !fitxaActual.personatge.estat.GetType().ToString().Equals("EstatPersonatgeNoVisible")){
					// Te un personatge rival al seu abast
					if(distanciaAtac == 1){
						// Atac curta distancia
						if(baseObjectiu.defensa <= personatgeUsuari.atacLlargaDistancia){
							// Si el moviment previ ha sigut la mort de la base
							AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.mataBase);
							if(a.heuristica > accioActual.heuristica){
								accioActual = a;
							}
						}else{
							// Si el moviment previ ha sigut l'atac a la base sense mort
							if(fitxaActual.personatge.defensa <= personatgeUsuari.atacCurtaDistancia){
								// Si amb el moviment següent suposat, podem matar a un personatge
								AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseMataPersonatge);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseAtacaPersonatge);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}
						}
					}else{
						// Atac llarga distancia
						if(baseObjectiu.defensa <= personatgeUsuari.atacLlargaDistancia){
							// Si el moviment previ ha sigut la mort de la base
							AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.mataBase);
							if(a.heuristica > accioActual.heuristica){
								accioActual = a;
							}
						}else{
							// Si el moviment previ ha sigut l'atac a la base sense mort
							if(fitxaActual.personatge.defensa <= personatgeUsuari.atacLlargaDistancia){
								// Si amb el moviment següent suposat, podem matar a un personatge
								AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseMataPersonatge);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseAtacaPersonatge);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}
						}
					}
				}else{
					AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBasePersonatgeNormal);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}
			}
			// Part tornar a atacar la base
			if(baseObjectiu.defensa <= personatgeUsuari.atacLlargaDistancia){
				// Si el moviment previ ha sigut la mort de la base
				AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.mataBase);
				if(a.heuristica > accioActual.heuristica){
					accioActual = a;
				}
			}else{
				// Si el moviment previ ha sigut l'atac a la base sense mort
				if(baseObjectiu.defensa <= personatgeUsuari.atacLlargaDistancia){
					// Si amb el moviment següent suposat, podem matar a un personatge
					AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseMataBase);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}else{
					AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseAtacaBase);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}
			}
		}else if(personatgeUsuari.nom.ToString().Equals("Bazooka")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(f, fitxaActual);
					if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && esPersonatgeEnemic(fitxaActual.personatge)){
						// Te un personatge rival al seu abast
						if(distanciaAtac == 1){
							// Atac curta distancia
							if(baseObjectiu.defensa <= personatgeUsuari.atacLlargaDistancia){
								// Si el moviment previ ha sigut la mort de la base
								AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.mataBase);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								// Si el moviment previ ha sigut l'atac a la base sense mort
								if(fitxaActual.personatge.defensa <= personatgeUsuari.atacCurtaDistancia){
									// Si amb el moviment següent suposat, podem matar a un personatge
									AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseMataPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}else{
									AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseAtacaPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}
							}
						}else{
							// Atac llarga distancia
							if(baseObjectiu.defensa <= personatgeUsuari.atacLlargaDistancia){
								// Si el moviment previ ha sigut la mort de la base
								AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.mataBase);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								// Si el moviment previ ha sigut l'atac a la base sense mort
								if(fitxaActual.personatge.defensa <= personatgeUsuari.atacLlargaDistancia){
									// Si amb el moviment següent suposat, podem matar a un personatge
									AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseMataPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}else{
									AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseAtacaPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}
							}
						}
					}else{
						AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBasePersonatgeNormal);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
			// Part tornar a atacar la base
			if(baseObjectiu.defensa <= personatgeUsuari.atacLlargaDistancia){
				// Si el moviment previ ha sigut la mort de la base
				AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.mataBase);
				if(a.heuristica > accioActual.heuristica){
					accioActual = a;
				}
			}else{
				// Si el moviment previ ha sigut l'atac a la base sense mort
				if(baseObjectiu.defensa <= personatgeUsuari.atacLlargaDistancia){
					// Si amb el moviment següent suposat, podem matar a un personatge
					AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseMataBase);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}else{
					AccioIA a = new AccioIA("AtacBase", personatgeUsuari, baseObjectiu, DadesHeuristiques.atacBase.atacaBaseAtacaBase);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}
			}
		}
		return accioActual;
	}
	
	private AccioIA valorarAccioAtacPersonatge(Personatge personatgeUsuari, Personatge personatgeAtacat, int atacPersonatge){
		int distanciaAtac = personatgeUsuari.distanciaAtac;
		AccioIA accioActual = new AccioIA(0);
		Fitxa f = personatgeUsuari.fitxaActual;
		if(personatgeUsuari.nom.ToString().Equals("Metralleta")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(f, fitxaActual);
					if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && esPersonatgeEnemic(fitxaActual.personatge)){
						// Te un personatge rival al seu abast
						if(distanciaAtac == 1){
							// Atac curta distancia
							if(personatgeAtacat.defensa <= atacPersonatge){
								// Si el moviment previ ha sigut la mort
								if(fitxaActual.personatge.defensa <= personatgeUsuari.atacCurtaDistancia){
									// Si amb el moviment següent suposat, podem matar a un personatge
									AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeMortMataPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}else{
									AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeMortAtacaPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}
							}else{
								// Si el moviment previ ha sigut l'atac sense mort
								if(fitxaActual.personatge.defensa <= personatgeUsuari.atacCurtaDistancia){
									// Si amb el moviment següent suposat, podem matar a un personatge
									AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeAtacatMataPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}else{
									AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeAtacatAtacaPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}
							}
						}else{
							// Atac llarga distancia
							if(personatgeAtacat.defensa <= atacPersonatge){
								// Si el moviment previ ha sigut la mort
								if(fitxaActual.personatge.defensa <= personatgeUsuari.atacLlargaDistancia){
									// Si amb el moviment següent suposat, podem matar a un personatge
									AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeMortMataPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}else{
									AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeMortAtacaPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}
							}else{
								// Si el moviment previ ha sigut l'atac sense mort
								if(fitxaActual.personatge.defensa <= personatgeUsuari.atacLlargaDistancia){
									// Si amb el moviment següent suposat, podem matar a un personatge
									AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeAtacatMataPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}else{
									AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeAtacatAtacaPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}
							}
						}
					}else{
						AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeNormal);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
		}else if(personatgeUsuari.nom.ToString().Equals("Franco")){
			for(int i=0;i<llargadaTauler; i++){
				Fitxa fitxaActual = fitxesTauler[f.fila-1,i];
				int distanciaFitxa = calcularDistancia(f, fitxaActual);
				if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac 
				&& esPersonatgeEnemic(fitxaActual.personatge)
				&& !fitxaActual.personatge.estat.GetType().ToString().Equals("EstatPersonatgeNoVisible")){
					// Te un personatge rival al seu abast
					if(distanciaAtac == 1){
						// Atac curta distancia
						if(personatgeAtacat.defensa <= atacPersonatge){
							// Si el moviment previ ha sigut la mort
							if(fitxaActual.personatge.defensa <= personatgeUsuari.atacCurtaDistancia){
								// Si amb el moviment següent suposat, podem matar a un personatge
								AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeMortMataPersonatge);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeMortAtacaPersonatge);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}
						}else{
							// Si el moviment previ ha sigut l'atac sense mort
							if(fitxaActual.personatge.defensa <= personatgeUsuari.atacCurtaDistancia){
								// Si amb el moviment següent suposat, podem matar a un personatge
								AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeAtacatMataPersonatge);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeAtacatAtacaPersonatge);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}
						}
					}else{
						// Atac llarga distancia
						if(personatgeAtacat.defensa <= atacPersonatge){
							// Si el moviment previ ha sigut la mort
							if(fitxaActual.personatge.defensa <= personatgeUsuari.atacLlargaDistancia){
								// Si amb el moviment següent suposat, podem matar a un personatge
								AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeMortMataPersonatge);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeMortAtacaPersonatge);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}
						}else{
							// Si el moviment previ ha sigut l'atac sense mort
							if(fitxaActual.personatge.defensa <= personatgeUsuari.atacLlargaDistancia){
								// Si amb el moviment següent suposat, podem matar a un personatge
								AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeAtacatMataPersonatge);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeAtacatAtacaPersonatge);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}
						}
					}
				}else{
					AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeNormal);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}
			}
		}else if(personatgeUsuari.nom.ToString().Equals("Guerrero")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(f, fitxaActual);
					if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && esPersonatgeEnemic(fitxaActual.personatge)){
						// Te un personatge rival al seu abast
						if(distanciaAtac == 1){
							// Atac curta distancia
							if(personatgeAtacat.defensa <= atacPersonatge){
								// Si el moviment previ ha sigut la mort
								if(fitxaActual.personatge.defensa <= personatgeUsuari.atacCurtaDistancia){
									// Si amb el moviment següent suposat, podem matar a un personatge
									AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeMortMataPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}else{
									AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeMortAtacaPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}
							}else{
								// Si el moviment previ ha sigut l'atac sense mort
								if(fitxaActual.personatge.defensa <= personatgeUsuari.atacCurtaDistancia){
									// Si amb el moviment següent suposat, podem matar a un personatge
									AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeAtacatMataPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}else{
									AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeAtacatAtacaPersonatge);
									if(a.heuristica > accioActual.heuristica){
										accioActual = a;
									}
								}
							}
						}
					}else{
						AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeNormal);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
		}else if(personatgeUsuari.nom.ToString().Equals("Bazooka")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(f, fitxaActual);
					if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && esPersonatgeEnemic(fitxaActual.personatge)){
						// Te un personatge rival al seu abast
						
						// Atac llarga distancia
						if(personatgeAtacat.defensa <= atacPersonatge){
							// Si el moviment previ ha sigut la mort
							if(fitxaActual.personatge.defensa <= personatgeUsuari.atacLlargaDistancia){
								// Si amb el moviment següent suposat, podem matar a un personatge
								AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeMortMataPersonatge);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeMortAtacaPersonatge);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}
						}else{
							// Si el moviment previ ha sigut l'atac sense mort
							if(fitxaActual.personatge.defensa <= personatgeUsuari.atacLlargaDistancia){
								// Si amb el moviment següent suposat, podem matar a un personatge
								AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeAtacatMataPersonatge);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeAtacatAtacaPersonatge);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}
						}
					}else{
						AccioIA a = new AccioIA("AtacPersonatge", personatgeUsuari, personatgeAtacat, DadesHeuristiques.atacPersonatge.personatgeNormal);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
		}
		return accioActual;
	}
	
	private AccioIA valorarAccioSortidaPersonatge(Carta cartaActual, Fitxa f){
		InformacioCartaPersonatge iC = ((InformacioCartaPersonatge) cartaActual.iC);
		int distanciaAtac = iC.distanciaAtac;
		AccioIA accioActual = new AccioIA(0);
		if(iC.nom.ToString().Equals("Metralleta")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(f, fitxaActual);
					if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && esPersonatgeEnemic(fitxaActual.personatge)){
						// Te un personatge rival al seu abast
						if(distanciaAtac == 1){
							// Atac curta distancia
							if(fitxaActual.personatge.defensa <= iC.atacCurtaDistancia){
								AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.mataPersonatge*actitudDefensiva);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.atacaPersonatge*actitudDefensiva);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}
						}else{
							// Atac llarga distancia
							if(fitxaActual.personatge.defensa <= iC.atacLlargaDistancia){
								AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.mataPersonatge*actitudDefensiva);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.atacaPersonatge*actitudDefensiva);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}
						}
					}else if(unicPersonatgeIATauler()){
						// Només estaría el personatge sol al tauler, per tant, prioritat...
						AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.personatgeSol*actitudDefensiva);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}else{
						AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.personatgeNormal*actitudDefensiva);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
		}else if(iC.nom.ToString().Equals("Franco")){
			for(int i=0;i<llargadaTauler; i++){
				Fitxa fitxaActual = fitxesTauler[f.fila-1,i];
				int distanciaFitxa = calcularDistancia(f, fitxaActual);
				if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac 
					&& esPersonatgeEnemic(fitxaActual.personatge)
					&& !fitxaActual.personatge.estat.GetType().ToString().Equals("EstatPersonatgeNoVisible")){
						// Te un personatge rival al seu abast
						if(distanciaAtac == 1){
							// Atac curta distancia
							if(fitxaActual.personatge.defensa <= iC.atacCurtaDistancia){
								AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.mataPersonatge*actitudDefensiva);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.atacaPersonatge*actitudDefensiva);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}
						}else{
							// Atac llarga distancia
							if(fitxaActual.personatge.defensa <= iC.atacLlargaDistancia){
								AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.mataPersonatge*actitudDefensiva);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.atacaPersonatge*actitudDefensiva);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}
						}
				}else if(unicPersonatgeIATauler()){
						// Només estaría el personatge sol al tauler, per tant, prioritat...
						AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.personatgeSol*actitudDefensiva);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}else{
						AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.personatgeNormal*actitudDefensiva);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
			}
		}else if(iC.nom.ToString().Equals("Guerrero")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(f, fitxaActual);
					if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && esPersonatgeEnemic(fitxaActual.personatge)){
						// Te un personatge rival al seu abast
						if(distanciaAtac == 1){
							// Atac curta distancia
							if(fitxaActual.personatge.defensa <= iC.atacCurtaDistancia){
								AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.mataPersonatge*actitudDefensiva);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.atacaPersonatge*actitudDefensiva);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}
						}
					}else if(unicPersonatgeIATauler()){
						// Només estaría el personatge sol al tauler, per tant, prioritat...
						AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.personatgeSol*actitudDefensiva);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}else{
						AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.personatgeNormal*actitudDefensiva);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
		}else if(iC.nom.ToString().Equals("Bazooka")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(f, fitxaActual);
					if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && esPersonatgeEnemic(fitxaActual.personatge)){
						// Te un personatge rival al seu abast
						
						// Atac llarga distancia
						if(fitxaActual.personatge.defensa <= iC.atacLlargaDistancia){
							AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.mataPersonatge*actitudDefensiva);
							if(a.heuristica > accioActual.heuristica){
								accioActual = a;
							}
						}else{
							AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.atacaPersonatge*actitudDefensiva);
							if(a.heuristica > accioActual.heuristica){
								accioActual = a;
							}
						}
					}else if(unicPersonatgeIATauler()){
						// Només estaría el personatge sol al tauler, per tant, prioritat...
						AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.personatgeSol*actitudDefensiva);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}else{
						AccioIA a = new AccioIA("SortidaPersonatge", cartaActual, f, DadesHeuristiques.sortidaPersonatge.personatgeNormal*actitudDefensiva);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
		}
		return accioActual;
	}
	
	private AccioIA mirarMovimentsPersonatge(Personatge personatgeUsuari){
		AccioIA accioActual = new AccioIA(0);
		Fitxa fitxaUsuari = personatgeUsuari.fitxaActual;
		int distanciaMoviment = personatgeUsuari.distanciaMoviment;
		if(personatgeUsuari.nom.ToString().Equals("Metralleta")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(fitxaUsuari, fitxaActual);
					if(!fitxaActual.tePersonatge() && distanciaFitxa <= distanciaMoviment && fitxaActual != fitxaUsuari){
						// El personatge es pot moure a aquesta casella
						AccioIA a = valorarAccioMovimentPersonatge(personatgeUsuari, fitxaActual);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
		}else if(personatgeUsuari.nom.ToString().Equals("Franco")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(fitxaUsuari, fitxaActual);
					if(!fitxaActual.tePersonatge() && distanciaFitxa <= distanciaMoviment && fitxaActual != fitxaUsuari){
						// El personatge es pot moure a aquesta casella
						AccioIA a = valorarAccioMovimentPersonatge(personatgeUsuari, fitxaActual);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
		}else if(personatgeUsuari.nom.ToString().Equals("Guerrero")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(fitxaUsuari, fitxaActual);
					if(!fitxaActual.tePersonatge() && distanciaFitxa <= distanciaMoviment && fitxaActual != fitxaUsuari){
						// El personatge es pot moure a aquesta casella
						AccioIA a = valorarAccioMovimentPersonatge(personatgeUsuari, fitxaActual);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
		}else if(personatgeUsuari.nom.ToString().Equals("Bazooka")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(fitxaUsuari, fitxaActual);
					if(!fitxaActual.tePersonatge() && distanciaFitxa <= distanciaMoviment && fitxaActual != fitxaUsuari){
						// El personatge es pot moure a aquesta casella
						AccioIA a = valorarAccioMovimentPersonatge(personatgeUsuari, fitxaActual);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
		}
		return accioActual;
	}
	
	private AccioIA valorarAccioMovimentPersonatge(Personatge personatgeUsuari, Fitxa f){
		int distanciaAtac = personatgeUsuari.distanciaAtac;
		AccioIA accioActual = new AccioIA(0);
		if(personatgeUsuari.nom.ToString().Equals("Metralleta")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(f, fitxaActual);
					if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && esPersonatgeEnemic(fitxaActual.personatge)){
						// Te un personatge rival al seu abast
						if(distanciaAtac == 1){
							// Atac curta distancia
							if(fitxaActual.personatge.defensa <= personatgeUsuari.atacCurtaDistancia){
								AccioIA a = new AccioIA("MovimentPersonatge", personatgeUsuari, personatgeUsuari.fitxaActual, f, DadesHeuristiques.movimentPersonatge.mataPersonatge*actitudAtacant);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								AccioIA a = new AccioIA("MovimentPersonatge", personatgeUsuari, personatgeUsuari.fitxaActual, f, DadesHeuristiques.movimentPersonatge.atacaPersonatge*actitudAtacant);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}
						}else{
							// Atac llarga distancia
							if(fitxaActual.personatge.defensa <= personatgeUsuari.atacLlargaDistancia){
								AccioIA a = new AccioIA("MovimentPersonatge", personatgeUsuari, personatgeUsuari.fitxaActual, f, DadesHeuristiques.movimentPersonatge.mataPersonatge*actitudAtacant);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								AccioIA a = new AccioIA("MovimentPersonatge", personatgeUsuari, personatgeUsuari.fitxaActual, f, DadesHeuristiques.movimentPersonatge.atacaPersonatge*actitudAtacant);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}
						}
					}else{
						AccioIA a = new AccioIA("MovimentPersonatge", personatgeUsuari, personatgeUsuari.fitxaActual, f, DadesHeuristiques.movimentPersonatge.personatgeNormal*actitudAtacant);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
		}else if(personatgeUsuari.nom.ToString().Equals("Franco")){
			for(int i=0;i<llargadaTauler; i++){
				Fitxa fitxaActual = fitxesTauler[f.fila-1,i];
				int distanciaFitxa = calcularDistancia(f, fitxaActual);
				if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac 
				&& esPersonatgeEnemic(fitxaActual.personatge)
				&& !fitxaActual.personatge.estat.GetType().ToString().Equals("EstatPersonatgeNoVisible")){
					// Te un personatge rival al seu abast
					if(distanciaAtac == 1){
						// Atac curta distancia
						if(fitxaActual.personatge.defensa <= personatgeUsuari.atacCurtaDistancia){
							AccioIA a = new AccioIA("MovimentPersonatge", personatgeUsuari, personatgeUsuari.fitxaActual, f, DadesHeuristiques.movimentPersonatge.mataPersonatge*actitudAtacant);
							if(a.heuristica > accioActual.heuristica){
								accioActual = a;
							}
						}else{
							AccioIA a = new AccioIA("MovimentPersonatge", personatgeUsuari, personatgeUsuari.fitxaActual, f, DadesHeuristiques.movimentPersonatge.atacaPersonatge*actitudAtacant);
							if(a.heuristica > accioActual.heuristica){
								accioActual = a;
							}
						}
					}else{
						// Atac llarga distancia
						if(fitxaActual.personatge.defensa <= personatgeUsuari.atacLlargaDistancia){
							AccioIA a = new AccioIA("MovimentPersonatge", personatgeUsuari, personatgeUsuari.fitxaActual, f, DadesHeuristiques.movimentPersonatge.mataPersonatge*actitudAtacant);
							if(a.heuristica > accioActual.heuristica){
								accioActual = a;
							}
						}else{
							AccioIA a = new AccioIA("MovimentPersonatge", personatgeUsuari, personatgeUsuari.fitxaActual, f, DadesHeuristiques.movimentPersonatge.atacaPersonatge*actitudAtacant);
							if(a.heuristica > accioActual.heuristica){
								accioActual = a;
							}
						}
					}
				}else{
					AccioIA a = new AccioIA("MovimentPersonatge", personatgeUsuari, personatgeUsuari.fitxaActual, f, DadesHeuristiques.movimentPersonatge.personatgeNormal*actitudAtacant);
					if(a.heuristica > accioActual.heuristica){
						accioActual = a;
					}
				}
			}
		}else if(personatgeUsuari.nom.ToString().Equals("Guerrero")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(f, fitxaActual);
					if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && esPersonatgeEnemic(fitxaActual.personatge)){
						// Te un personatge rival al seu abast
						if(distanciaAtac == 1){
							// Atac curta distancia
							if(fitxaActual.personatge.defensa <= personatgeUsuari.atacCurtaDistancia){
								AccioIA a = new AccioIA("MovimentPersonatge", personatgeUsuari, personatgeUsuari.fitxaActual, f, DadesHeuristiques.movimentPersonatge.mataPersonatge*actitudAtacant);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}else{
								AccioIA a = new AccioIA("MovimentPersonatge", personatgeUsuari, personatgeUsuari.fitxaActual, f, DadesHeuristiques.movimentPersonatge.atacaPersonatge*actitudAtacant);
								if(a.heuristica > accioActual.heuristica){
									accioActual = a;
								}
							}
						}
					}else{
						AccioIA a = new AccioIA("MovimentPersonatge", personatgeUsuari, personatgeUsuari.fitxaActual, f, DadesHeuristiques.movimentPersonatge.personatgeNormal*actitudAtacant);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
		}else if(personatgeUsuari.nom.ToString().Equals("Bazooka")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = fitxesTauler[i,j];
					int distanciaFitxa = calcularDistancia(f, fitxaActual);
					if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && esPersonatgeEnemic(fitxaActual.personatge)){
						// Te un personatge rival al seu abast
						
						// Atac llarga distancia
						if(fitxaActual.personatge.defensa <= personatgeUsuari.atacLlargaDistancia){
							AccioIA a = new AccioIA("MovimentPersonatge", personatgeUsuari, personatgeUsuari.fitxaActual, f, DadesHeuristiques.movimentPersonatge.mataPersonatge*actitudAtacant);
							if(a.heuristica > accioActual.heuristica){
								accioActual = a;
							}
						}else{
							AccioIA a = new AccioIA("MovimentPersonatge", personatgeUsuari, personatgeUsuari.fitxaActual, f, DadesHeuristiques.movimentPersonatge.atacaPersonatge*actitudAtacant);
							if(a.heuristica > accioActual.heuristica){
								accioActual = a;
							}
						}
					}else{
						AccioIA a = new AccioIA("MovimentPersonatge", personatgeUsuari, personatgeUsuari.fitxaActual, f, DadesHeuristiques.movimentPersonatge.personatgeNormal*actitudAtacant);
						if(a.heuristica > accioActual.heuristica){
							accioActual = a;
						}
					}
				}
			}
		}
		return accioActual;
	}
	
	private int calcularDistancia(Fitxa fitxaUsuari, Fitxa fitxaActual){
		int calculColumna = Mathf.Abs(fitxaUsuari.columna - fitxaActual.columna);
		int calculFila = Mathf.Abs(fitxaUsuari.fila - fitxaActual.fila);
		if(calculColumna > calculFila) return calculColumna;
		else return calculFila;
	}
	
	private bool esPersonatgeEnemic(Personatge personatgeActual){
		return personatgeActual.propietari != propietariIA;
	}
	
	private void acabarTornIA(){
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		Debug.Log("La IA intenta acabar i els moviments disponibles que te son " + cG.movimentActual.getAccionsDisponibles());
		if(cG.movimentActual.getAccionsDisponibles() == 0){
			Accio a = new AccioConfirmarMoviment(cG.movimentActual);
			a.executarAccio();
		}
	}
	
	private bool unicPersonatgeIATauler(){
		bool personatgeSol = true;
		int i = 0, j = 0;
		while(i < ampladaTauler && personatgeSol){
			while(j < llargadaTauler && personatgeSol){
				Fitxa fitxaActual = fitxesTauler[i,j];
				if(fitxaActual.tePersonatge() && fitxaActual.personatge.propietari == propietariIA) 
					personatgeSol = false;
				j++;
			}
			i++;
		}
		return personatgeSol;
	}
}
