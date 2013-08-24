using UnityEngine;

public class AccioMourePersonatge : Accio {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------

	private Personatge personatgeUsuari;
	private Fitxa fitxaActual;
	private Fitxa fitxaDesti;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public AccioMourePersonatge(Personatge p1, Fitxa f1, Fitxa f2){
		personatgeUsuari = p1;
		fitxaActual = f1;
		fitxaDesti = f2;
	}

	public void executarAccio(){
		//throw new System.NotImplementedException();
		Accio neteja = new AccioNetejarTauler();
		neteja.executarAccio();
		PartGrafica p = (PartGrafica) Camera.mainCamera.GetComponent("PartGrafica");
		p.mourePersonatge(personatgeUsuari, fitxaActual, fitxaDesti);
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		cG.movimentActual.afegirAccioTemporal(this);
	}
	
	public Personatge getPersonatge(){
		return personatgeUsuari;
	}
	
	public Fitxa getFitxaDesti(){
		return fitxaDesti;
	}
	
	public Fitxa getFitxaOrigen(){
		return fitxaActual;
	}
}
