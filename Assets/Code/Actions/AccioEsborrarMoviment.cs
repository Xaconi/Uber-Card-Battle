using UnityEngine;

public class AccioEsborrarMoviment : Accio {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------

	private Moviment movimentEsborrar;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public AccioEsborrarMoviment(Moviment m){
		movimentEsborrar = m;
	}

	public void executarAccio(){
		//throw new System.NotImplementedException();
		PartGrafica p = (PartGrafica) Camera.mainCamera.GetComponent("PartGrafica");
		p.carregarAnimacioFlash(movimentEsborrar);
	}
	
}
