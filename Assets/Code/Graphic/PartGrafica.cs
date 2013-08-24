using UnityEngine;
using System.Collections.Generic;

public class PartGrafica : MonoBehaviour {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	private int llargadaTauler;
	private int ampladaTauler;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	// Use this for initialization
	void Start () {
		Tauler tauler = (Tauler) (GameObject.FindGameObjectWithTag("Tauler")).GetComponent("Tauler");
		llargadaTauler = tauler.llargadaFitxesTauler;
		ampladaTauler = tauler.ampladaFitxesTauler;
		
		// Carrega d'events
		AnimacioFlash.esborrarMoviment += esborrarMoviment;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void atacarPersonatge(Personatge p, List<Personatge> llistaPersonatgesObservers){
		//throw new System.NotImplementedException();
		
		bool llargaDistancia = false;
		string tipus = "AtacCurtaDistancia";
		int i = 0;
		while(i < llistaPersonatgesObservers.Count && !llargaDistancia){
			if(calcularDistancia(p.fitxaActual, llistaPersonatgesObservers[i].fitxaActual) > 1){
				llargaDistancia = true;
				tipus = "AtacLlargaDistancia";
			}
			i++;
		}
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		List<Accio> accionsTemporals = cG.movimentActual.getAccionsTemporals();
		AccioAtacarPersonatge a = (AccioAtacarPersonatge) accionsTemporals[accionsTemporals.Count-1];
		a.assignarAtac(tipus);
		p.renderer.enabled = false;
		p.estat = new EstatPersonatgeAtacant(p, tipus);
		cG.llistaLog.Add("Jugador " + cG.usuariTorn + " ataca a un altre personatge amb el personatge " + p.nom);
	}

	public void atacarBase(Personatge p, Base b){
		bool curtaDistancia = false;
		string tipus = "AtacLlargaDistancia";
		int i = 0;
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		List<Accio> accionsTemporals = cG.movimentActual.getAccionsTemporals();
		AccioAtacarBase a = (AccioAtacarBase) accionsTemporals[accionsTemporals.Count-1];
		a.assignarAtac(tipus);
		p.renderer.enabled = false;
		p.estat = new EstatPersonatgeAtacant(p, b, tipus);
		cG.llistaLog.Add("Jugador " + cG.usuariTorn + " ataca la base rival amb el personatge " + p.nom);
	}

	public void mourePersonatge(Personatge personatgeUsuari, Fitxa fitxaOrigen, Fitxa fitxaDesti){
		//throw new System.NotImplementedException();
		personatgeUsuari.estat = new EstatPersonatgeMoviment(personatgeUsuari, fitxaOrigen, fitxaDesti);
		personatgeUsuari.gameObject.tag = "Personatge";
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		cG.llistaLog.Add("Jugador " + cG.usuariTorn + " mou el personatge " + personatgeUsuari.nom);
	}

	public void aplicarBonificacio(Carta c, Personatge p){
		//throw new System.NotImplementedException();
		string tipus = ((InformacioCartaBonificacio)c.iC).nom;
		if(tipus.Equals("Atac") || tipus.Equals("Atac+") || tipus.Equals("Atac++")){
			p.rebreBonificacioAtac(c.iC);
		}else if(tipus.Equals("Defensa") || tipus.Equals("Defensa+") || tipus.Equals("Defensa++")){
			p.rebreBonificacioDefensa(c.iC);
		}else if(tipus.Equals("Uber") || tipus.Equals("Uber+") || tipus.Equals("Uber++")){
			p.rebreBonificacioUber(c.iC);
		}
		c.desactivarCarta();
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		cG.estadistiques.augmentarCartes();
		cG.estadistiques.augmentarBonificacions();
		cG.augmentarCartesUtilitzades();
		cG.augmentarBonificacionsUtilitzades();
		cG.llistaLog.Add("Jugador " + cG.usuariTorn + " aplica una bonificació al personatge " + p.nom);
		if(cG.IA && cG.usuariTorn == 2){
			// Si es tracta d'un moviment de la IA
			cG.avisarIA();
		}
	}

	public void treurePersonatge(Fitxa fitxa, Carta cartaActual, Personatge personatge){
		//throw new System.NotImplementedException();
		fitxa.assignarPersonatge(personatge);
		personatge.activarPersonatge();
		cartaActual.desactivarCarta();
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		cG.estadistiques.augmentarCartes();
		cG.estadistiques.augmentarPersonatges();
		if(cG.IA && cG.usuariTorn == 2){
			// Si es tracta d'un moviment de la IA
			personatge.estat = personatge.estatAnterior;
		}
		cG.augmentarCartesUtilitzades();
		cG.augmentarPersonatgesUtilitzats();
		if(personatge.movimentRival) personatge.movimentRival = false;
		cG.llistaLog.Add("Jugador " + cG.usuariTorn + " treu el personatge " + personatge.nom);
	}

	public void ensenyarInformacio(Personatge personatgeUsuari, Tauler tauler){
		Fitxa[,] llistaFitxes = tauler.getLlistaFitxes();
		Fitxa fitxaUsuari = personatgeUsuari.fitxaActual;
		personatgeUsuari.gameObject.tag = "PersonatgeSeleccionat";
		personatgeUsuari.estatAnterior = personatgeUsuari.estat;
		personatgeUsuari.estat = new EstatPersonatgeSeleccionat(personatgeUsuari);
		int distanciaAtac = personatgeUsuari.distanciaAtac;
		int distanciaMoviment = personatgeUsuari.distanciaMoviment;
		// Recòrre el tauler pintant les fitxes del color que sigui depenent del moviment que
		// estigui relacionat amb aquestes...
		if(personatgeUsuari.nom.ToString().Equals("Metralleta")){
			Debug.Log("HOLA");
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = llistaFitxes[i,j];
					int distanciaFitxa = calcularDistancia(fitxaUsuari, fitxaActual);
					if(!fitxaActual.tePersonatge() && distanciaFitxa <= distanciaMoviment && fitxaActual != fitxaUsuari){
						fitxaActual.estat = new EstatFitxaDisponible(fitxaActual);
					}else if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && personatgeUsuari.esPersonatgeEnemic(fitxaActual.personatge)){
						fitxaActual.estat = new EstatFitxaAtac(fitxaActual);
						fitxaActual.personatge.estat = new EstatPersonatgeObjectiu(fitxaActual.personatge);
					}else if(fitxaActual == fitxaUsuari){
						fitxaActual.estat = new EstatFitxaNormal(fitxaActual);
					}
				}
			}
		}else if(personatgeUsuari.nom.Equals("Franco")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = llistaFitxes[i,j];
					int distanciaFitxa = calcularDistancia(fitxaUsuari, fitxaActual);
					if(!fitxaActual.tePersonatge() && distanciaFitxa <= distanciaMoviment && fitxaActual != fitxaUsuari){
						fitxaActual.estat = new EstatFitxaDisponible(fitxaActual);
					}else if(fitxaActual == fitxaUsuari){
						fitxaActual.estat = new EstatFitxaNormal(fitxaActual);
					}
				}
			}
			for(int i=0;i<llargadaTauler; i++){
				Fitxa fitxaActual = llistaFitxes[personatgeUsuari.fitxaActual.fila-1,i];
				int distanciaFitxa = calcularDistancia(fitxaUsuari, fitxaActual);
				if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac 
					&& personatgeUsuari.esPersonatgeEnemic(fitxaActual.personatge)
					&& !fitxaActual.personatge.estat.GetType().ToString().Equals("EstatPersonatgeNoVisible")){
					Debug.Log(personatgeUsuari.estat.GetType());
					fitxaActual.estat = new EstatFitxaAtac(fitxaActual);
					fitxaActual.personatge.estat = new EstatPersonatgeObjectiu(fitxaActual.personatge);
				}else if(fitxaActual == fitxaUsuari){
					fitxaActual.estat = new EstatFitxaNormal(fitxaActual);
				}
			}
		}else if(personatgeUsuari.nom.Equals("Guerrero")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = llistaFitxes[i,j];
					int distanciaFitxa = calcularDistancia(fitxaUsuari, fitxaActual);
					if(!fitxaActual.tePersonatge() && distanciaFitxa <= distanciaMoviment && fitxaActual != fitxaUsuari){
						fitxaActual.estat = new EstatFitxaDisponible(fitxaActual);
					}else if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && personatgeUsuari.esPersonatgeEnemic(fitxaActual.personatge)){
						fitxaActual.estat = new EstatFitxaAtac(fitxaActual);
						fitxaActual.personatge.estat = new EstatPersonatgeObjectiu(fitxaActual.personatge);
					}else if(fitxaActual == fitxaUsuari){
						fitxaActual.estat = new EstatFitxaNormal(fitxaActual);
					}
				}
			}
		}else if(personatgeUsuari.nom.Equals("Bazooka")){
			for(int i=0;i<ampladaTauler; i++){
				for(int j=0;j<llargadaTauler;j++){
					Fitxa fitxaActual = llistaFitxes[i,j];
					int distanciaFitxa = calcularDistancia(fitxaUsuari, fitxaActual);
					if(!fitxaActual.tePersonatge() && distanciaFitxa <= distanciaMoviment && fitxaActual != fitxaUsuari){
						fitxaActual.estat = new EstatFitxaDisponible(fitxaActual);
					}else if(fitxaActual.tePersonatge() && distanciaFitxa <= distanciaAtac && personatgeUsuari.esPersonatgeEnemic(fitxaActual.personatge)){
						fitxaActual.estat = new EstatFitxaAtac(fitxaActual);
						fitxaActual.personatge.estat = new EstatPersonatgeObjectiu(fitxaActual.personatge);
					}else if(fitxaActual == fitxaUsuari){
						fitxaActual.estat = new EstatFitxaNormal(fitxaActual);
					}
				}
			}
		}
		// Mirar si la fitxa del personatge es a dins de la zona segura enemiga 
		// per atacar la seva base
		if(!personatgeUsuari.nom.Equals("Guerrero")){
			if(personatgeUsuari.propietari == 1){
				if(fitxaUsuari.columna >= 9){
					// L'usuari pot atacar la base del rival
					GameObject[] b = GameObject.FindGameObjectsWithTag("Base");
					Base b1 = (Base) b[0].GetComponent("Base");
					Base b2 = (Base) b[1].GetComponent("Base");
					Base baseObjectiu;
					if(b1.propietari == 2) baseObjectiu = b1;
					else baseObjectiu = b2;
					baseObjectiu.setEstat(new EstatBaseObjectiu(baseObjectiu));
				}
			}else if(personatgeUsuari.propietari == 2){
				if(fitxaUsuari.columna <= 3){
					// El rival pot atacar la base de l'usuari
					GameObject[] b = GameObject.FindGameObjectsWithTag("Base");
					Base b1 = (Base) b[0].GetComponent("Base");
					Base b2 = (Base) b[1].GetComponent("Base");
					Base baseObjectiu;
					if(b1.propietari == 1) baseObjectiu = b1;
					else baseObjectiu = b2;
					baseObjectiu.setEstat(new EstatBaseObjectiu(baseObjectiu));
				}
			}
		}
	}
	
	public int calcularDistancia(Fitxa fitxaUsuari, Fitxa fitxaActual){
		int calculColumna = Mathf.Abs(fitxaUsuari.columna - fitxaActual.columna);
		int calculFila = Mathf.Abs(fitxaUsuari.fila - fitxaActual.fila);
		if(calculColumna > calculFila) return calculColumna;
		else return calculFila;
	}

	public void treureInformacio(Personatge personatgeUsuari, Tauler tauler){
		throw new System.NotImplementedException();
	}
	
	public void carregarAnimacioFlash(Moviment movimentEsborrar){
		GUITexture.Instantiate(Resources.Load ("flash_screen"));
		AnimacioFlash aF = (AnimacioFlash) GUITexture.FindObjectOfType(typeof(AnimacioFlash));
		aF.assignarMoviment(movimentEsborrar);
	}

	public void esborrarMoviment(Moviment movimentEsborrar){
		List<Accio> accionsTemporals = movimentEsborrar.getAccionsTemporals();
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		for(int i = accionsTemporals.Count-1; i >= 0; i--){
			string state = accionsTemporals[i].GetType().ToString();
			if(state.Equals("AccioMourePersonatge")){
				AccioMourePersonatge a = (AccioMourePersonatge) accionsTemporals[i];
				Debug.Log("Detecto una accio de moviment a les accions temporals");
				Fitxa fitxaOrigen = a.getFitxaOrigen();
				Fitxa fitxaDesti = a.getFitxaDesti();
				Personatge personatgeActual = a.getPersonatge();
				fitxaDesti.traspassarPersonatge(fitxaOrigen, personatgeActual);
				movimentEsborrar.treureAccioTemporal();
			}else if(state.Equals("AccioAtacarPersonatge")){
				AccioAtacarPersonatge a = (AccioAtacarPersonatge) accionsTemporals[i];
				Debug.Log("Detecto una accio d'atac a un personatge a les accions temporals.");
				List<Personatge> llistaPersonatges = a.getLlistaPersonatgesAtacar();
				Personatge personatgeAtacant = a.getPersonatgeUsuari();
				foreach(Personatge p in llistaPersonatges){
					string statePersonatge = p.estat.GetType().ToString();
					Debug.Log(statePersonatge);
					if(statePersonatge.Equals("EstatPersonatgeNoVisible")){
						Debug.Log("Revivim un personatge.");
						p.estat = p.estatAnterior;
						p.gameObject.transform.localScale = new Vector3(p.midaX, p.midaY, p.midaZ);
						if(p.nom.ToString().Equals("Metralleta")) p.renderer.material = Resources.Load("PersonatgeNormal") as Material;
						else if(p.nom.Equals("Franco")) p.renderer.material = Resources.Load("FrancoIdle") as Material;
						else if(p.nom.Equals("Guerrero")) p.renderer.material = Resources.Load("GuerreroIdle") as Material;
						else if(p.nom.Equals("Bazooka")) p.renderer.material = Resources.Load("BazookaIdle") as Material;
						p.fitxaActual.assignarPersonatge(p);
						p.colocarPersonatge();
					}
					p.augmentarDefensa(a.getAtac());
					cG.reduirAtacFet(a.getAtac());
				}
				movimentEsborrar.treureAccioTemporal();
			}else if(state.Equals("AccioTreurePersonatge")){
				AccioTreurePersonatge a = (AccioTreurePersonatge) accionsTemporals[i];
				Debug.Log("Detecto una accio de sortida d'un personatge al tauler.");
				Carta cartaActual = a.getCartaActual();
				Personatge personatgeActual = a.getPersonatgeActual();
				Fitxa fitxaActual = a.getFitxaActual();
				MenuCartesDisponibles menu = (MenuCartesDisponibles) (GameObject.FindGameObjectWithTag("MenuCartesDisponibles")).GetComponent("MenuCartesDisponibles");
				menu.augmentarCartesDisponibles(cG.usuariTorn);
				cartaActual.activarCarta();
				if(fitxaActual.personatge == personatgeActual) fitxaActual.treurePersonatge();
				else{
					personatgeActual.fitxaActual.treurePersonatge();
				}
				Destroy (personatgeActual.gameObject);
				movimentEsborrar.treureAccioTemporal();
				cG.estadistiques.reduirCartes();
				cG.estadistiques.reduirPersonatges();
				cG.reduirCartesUtilitzades();
				cG.reduirPersonatgesUtilitzats();
			}else if(state.Equals("AccioAplicarBonificacio")){
				AccioAplicarBonificacio a = (AccioAplicarBonificacio) accionsTemporals[i];
				//Debug.Log("Detecto una accio d'aplicació de bonificació a un personatge.");
				Carta cartaActual = a.getCartaActual();
				Personatge personatgeActual = a.getPersonatgeActual();
				Bonificacio b = personatgeActual.getBonificacioActual();
				if(b.getNom().Equals("Atac")){
					personatgeActual.atacCurtaDistancia -= b.getInformacio().atac;
					personatgeActual.atacLlargaDistancia -= b.getInformacio().atac;
				}else if(b.getNom().Equals("Defensa")){
					personatgeActual.defensa -= b.getInformacio().defensa;
				}else if(b.getNom().Equals("Uber")){
					personatgeActual.atacCurtaDistancia -= b.getInformacio().atac;
					personatgeActual.atacLlargaDistancia -= b.getInformacio().atac;
					personatgeActual.defensa -= b.getInformacio().defensa;
				}
				MenuCartesDisponibles menu = (MenuCartesDisponibles) (GameObject.FindGameObjectWithTag("MenuCartesDisponibles")).GetComponent("MenuCartesDisponibles");
				menu.augmentarCartesDisponibles(cG.usuariTorn);
				cartaActual.activarCarta();
				personatgeActual.treureBonificacio();
				Destroy (b.gameObject);
				movimentEsborrar.treureAccioTemporal();
				cG.estadistiques.reduirCartes();
				cG.estadistiques.reduirBonificacions();
				cG.reduirCartesUtilitzades();
				cG.reduirBonificacionsUtilitzades();
			}else if(state.Equals("AccioAtacarBase")){
				AccioAtacarBase a = (AccioAtacarBase) accionsTemporals[i];
				Debug.Log("Detecto una accio d'atac a la base.");
				Personatge personatgeActual = a.getPersonatgeUsuari();
				Base baseActual = a.getBaseActual();
				baseActual.augmentarDefensa(a.getAtac());
				movimentEsborrar.treureAccioTemporal();
			}
			cG.llistaLog.RemoveAt(cG.llistaLog.Count-1);
		}
		AnimacioFlash aF = (AnimacioFlash) GUITexture.FindObjectOfType(typeof(AnimacioFlash));
		aF.invertirAnimacio();
	}

	public void confirmarMoviment(Moviment movimentActual){
		movimentActual.confirmarAccions();
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		cG.partida.afegirMoviment(movimentActual);
		cG.augmentarAccions();
		cG.ferCanviTorn();
	}

	public void ensenyarInformacioCarta(Carta cartaSeleccionada){
		//throw new System.NotImplementedException();
		//Debug.Log (cartaSeleccionada.tipus.GetType().ToString());
		cartaSeleccionada.gameObject.tag = "CartaSeleccionada";
		cartaSeleccionada.estat = new EstatCartaSeleccionada(cartaSeleccionada);
		Tauler tauler = (Tauler) (GameObject.FindGameObjectWithTag("Tauler")).GetComponent("Tauler");
		Fitxa[,] llistaFitxes = tauler.getLlistaFitxes();
		cartaSeleccionada.estat = new EstatCartaSeleccionada(cartaSeleccionada);
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		if(cartaSeleccionada.tipus.GetType().ToString().Equals("TipusCartaPersonatge")){
			// Fem un recorregut de les tres primeres posicions del tauler per tal de marcar-li a l'usuari
			// on pot treure el personatge
			if(!cG.multijugadorLocal || (cG.multijugadorLocal && cG.usuariTorn == 1)){
				for(int i=0;i<6; i++){
					for(int j=0;j<3;j++){
						Fitxa fitxaActual = llistaFitxes[i,j];
						if(!fitxaActual.tePersonatge()){
							fitxaActual.estat = new EstatFitxaDisponible(fitxaActual);
						}
					}
				}
			}else if(cG.multijugadorLocal && cG.usuariTorn == 2){
				for(int i=0;i<6; i++){
					for(int j=8;j<11;j++){
						Fitxa fitxaActual = llistaFitxes[i,j];
						if(!fitxaActual.tePersonatge()){
							fitxaActual.estat = new EstatFitxaDisponible(fitxaActual);
						}
					}
				}
			}
		}else if(cartaSeleccionada.tipus.GetType().ToString().Equals("TipusCartaBonificacio")){
			Debug.Log ("Fins aqui, seleccionem la carta de la bonificació");
			// Fem un recorregut de tot el tauler en cerca de personatges als quals els hi
			// poguem aplicar la bonificació..		
			string tipusBonificacio = ((InformacioCartaBonificacio)cartaSeleccionada.iC).tipus;
			string nivellBonificacio = ((InformacioCartaBonificacio)cartaSeleccionada.iC).nivell;
			for(int i=0;i<6; i++){
				for(int j=0;j<11;j++){
					Fitxa fitxaActual = llistaFitxes[i,j];
					if(fitxaActual.tePersonatge() && fitxaActual.personatge.propietari == cG.usuariTorn){
						fitxaActual.personatge.estat = new EstatPersonatgeObjectiuBonificacio(fitxaActual.personatge);
					}
				}
			}
		}
	}

	public void acabarPartida(int guanyador){
		throw new System.NotImplementedException();
	}
	
	public void netejarTauler(Tauler tauler, MenuCartesDisponibles menu){
		Fitxa[,] llistaFitxes = tauler.getLlistaFitxes();
		for(int i=0;i<6; i++){
			for(int j=0;j<11;j++){
				Fitxa fitxaActual = llistaFitxes[i,j];
				if(!fitxaActual.tePersonatge() && fitxaActual.estat.GetType().ToString().Equals("EstatFitxaDisponible")){
					fitxaActual.estat = new EstatFitxaNormal(fitxaActual);
				}else if(fitxaActual.tePersonatge() && fitxaActual.estat.GetType().ToString().Equals("EstatFitxaAtac") 
					&& fitxaActual.personatge.estat.GetType().ToString().Equals("EstatPersonatgeObjectiu")){
					fitxaActual.estat = new EstatFitxaNormal(fitxaActual);
					fitxaActual.personatge.estat = fitxaActual.personatge.estatAnterior;
					fitxaActual.personatge.gameObject.tag = "Personatge";
					fitxaActual.personatge.gameObject.transform.localScale = new Vector3(fitxaActual.personatge.midaX,
						fitxaActual.personatge.midaY, fitxaActual.personatge.midaZ);
					fitxaActual.personatge.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}else if(fitxaActual.tePersonatge() && !fitxaActual.personatge.estat.GetType().ToString().Equals("EstatPersonatgeNoVisible")){
					fitxaActual.estat = new EstatFitxaNormal(fitxaActual);
					fitxaActual.personatge.estat = fitxaActual.personatge.estatAnterior;
					fitxaActual.personatge.gameObject.renderer.enabled = false;
					if(fitxaActual.personatge.nom.ToString().Equals("Metralleta")) fitxaActual.personatge.renderer.material = Resources.Load("PersonatgeNormal") as Material;
					else if(fitxaActual.personatge.nom.Equals("Franco")) fitxaActual.personatge.renderer.material = Resources.Load("FrancoIdle") as Material;
					else if(fitxaActual.personatge.nom.Equals("Guerrero")) fitxaActual.personatge.renderer.material = Resources.Load("GuerreroIdle") as Material;
					else if(fitxaActual.personatge.nom.Equals("Bazooka")) fitxaActual.personatge.renderer.material = Resources.Load("BazookaIdle") as Material;
					fitxaActual.personatge.gameObject.tag = "Personatge";
					fitxaActual.personatge.gameObject.transform.localScale = new Vector3(fitxaActual.personatge.midaX,
						fitxaActual.personatge.midaY, fitxaActual.personatge.midaZ);
					fitxaActual.personatge.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}
			}
		}
		
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		if(!cG.multijugadorLocal || (cG.multijugadorLocal && cG.usuariTorn == 1)){
			List<Carta> llistaCartes = menu.getCartesDisponibles();
			for(int i=0; i<menu.cartesBase; i++){
				Carta cartaActual = llistaCartes[i];
				if(cartaActual.estat.GetType().ToString().Equals("EstatCartaSeleccionada")){
					cartaActual.estat = new EstatCartaNormal(cartaActual);
					cartaActual.gameObject.tag = "Carta";
				}
			}
		}else if(cG.multijugadorLocal && cG.usuariTorn == 2){
			List<Carta> llistaCartes = menu.getCartesDisponiblesRival();
			for(int i=0; i<menu.cartesBase; i++){
				Carta cartaActual = llistaCartes[i];
				if(cartaActual.estat.GetType().ToString().Equals("EstatCartaSeleccionada")){
					cartaActual.estat = new EstatCartaNormal(cartaActual);
					cartaActual.gameObject.tag = "Carta";
				}
			}
		}
		
		GameObject[] b = GameObject.FindGameObjectsWithTag("Base");
		Base b1 = (Base) b[0].GetComponent("Base");
		Base b2 = (Base) b[1].GetComponent("Base");
		b1.setEstat(b1.getEstatAnterior());
		b2.setEstat(b2.getEstatAnterior());
	}
}
