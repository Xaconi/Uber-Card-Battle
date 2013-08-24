using UnityEngine;

public class AccioAplicarBonificacio : Accio {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	private Carta cartaBonificacio;
	private Personatge personatgeUsuari;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public AccioAplicarBonificacio(Personatge p1, Carta c){
		personatgeUsuari = p1;
		cartaBonificacio = c;
	}

	public void executarAccio(){
		//throw new System.NotImplementedException();
		Accio neteja = new AccioNetejarTauler();
		neteja.executarAccio();
		PartGrafica p = (PartGrafica) Camera.mainCamera.GetComponent("PartGrafica");
		p.aplicarBonificacio(cartaBonificacio, personatgeUsuari);
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		cG.movimentActual.afegirAccioTemporal(this);
	}
	
	public Carta getCartaActual(){
		return cartaBonificacio;
	}
	
	public Personatge getPersonatgeActual(){
		return personatgeUsuari;
	}
}
