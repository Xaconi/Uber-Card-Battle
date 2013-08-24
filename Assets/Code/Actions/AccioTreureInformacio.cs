using UnityEngine;

public class AccioTreureInformacio : Accio {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------

	public Personatge personatgeUsuari;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public AccioTreureInformacio(Personatge p1){
		personatgeUsuari = p1;
	}

	public void executarAccio(){
		//throw new System.NotImplementedException();
		Accio neteja = new AccioNetejarTauler();
		neteja.executarAccio();
		//personatgeUsuari.estat = new EstatPersonatgeNormal(personatgeUsuari);
	}
	
}
