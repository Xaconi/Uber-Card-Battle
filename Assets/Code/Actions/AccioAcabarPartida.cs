using UnityEngine;

public class AccioAcabarPartida : Accio {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------

	public int usuari{
		get{return this.usuari;}
		set{this.usuari = value;}
	}
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public AccioAcabarPartida(int usuari){
	}

	public void executarAccio(){
		throw new System.NotImplementedException();
	}
}
