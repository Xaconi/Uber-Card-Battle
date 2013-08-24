using UnityEngine;

public class AccioTreurePersonatge : Accio {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	private Carta cartaActual;
	private Personatge personatgeUsuari;
	private Fitxa fitxaDesti;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public AccioTreurePersonatge(Fitxa f1, Carta c1){
		cartaActual = c1;
		fitxaDesti = f1;
		personatgeUsuari = cartaActual.creaPersonatge();
	}

	public void executarAccio(){
		//throw new System.NotImplementedException();
		Accio neteja = new AccioNetejarTauler();
		neteja.executarAccio();
		PartGrafica p = (PartGrafica) Camera.mainCamera.GetComponent("PartGrafica");
		p.treurePersonatge(fitxaDesti, cartaActual, personatgeUsuari);
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		cG.movimentActual.afegirAccioTemporal(this);
		if(cG.IA && cG.usuariTorn == 2){
			// Si es tracta d'un moviment de la IA
			cG.avisarIA();
		}
	}
	
	public Personatge getPersonatgeActual(){
		return personatgeUsuari;
	}
	
	public Carta getCartaActual(){
		return cartaActual;
	}
	
	public Fitxa getFitxaActual(){
		return fitxaDesti;
	}
}
