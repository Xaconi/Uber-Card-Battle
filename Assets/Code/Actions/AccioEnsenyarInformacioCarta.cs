using UnityEngine;

public class AccioEnsenyarInformacioCarta : Accio {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------

	public Carta cartaSeleccionada{
		get;
		set;
	}
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public AccioEnsenyarInformacioCarta(Carta c){
		cartaSeleccionada = c;
	}

	public void executarAccio(){
		//throw new System.NotImplementedException();
		Accio neteja = new AccioNetejarTauler();
		neteja.executarAccio();
		PartGrafica p = (PartGrafica) Camera.mainCamera.GetComponent("PartGrafica");
		p.ensenyarInformacioCarta(cartaSeleccionada);
	}
	
}
