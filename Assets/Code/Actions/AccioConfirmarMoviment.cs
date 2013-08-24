using UnityEngine;

public class AccioConfirmarMoviment : Accio {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	public Moviment movimentConfirmar{
		get;
		set;
	}
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public AccioConfirmarMoviment(Moviment m){
		movimentConfirmar = m;
	}

	public void executarAccio(){
		PartGrafica p = (PartGrafica) Camera.mainCamera.GetComponent("PartGrafica");
		p.confirmarMoviment(movimentConfirmar);
	}
}
