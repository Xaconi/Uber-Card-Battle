using UnityEngine;

public class AccioTreureInformacioCarta : Accio {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	private Carta cartaSeleccionada;
	private Vector3 posicio;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public AccioTreureInformacioCarta(Carta c){
		cartaSeleccionada = c;
		posicio = c.gameObject.transform.position;
	}

	public  void executarAccio(){
		//throw new System.NotImplementedException();
		Accio neteja = new AccioNetejarTauler();
		neteja.executarAccio();
		cartaSeleccionada.estat = new EstatCartaNormal(cartaSeleccionada);
		cartaSeleccionada.gameObject.tag = "Carta";
	}
	
}
