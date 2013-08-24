using UnityEngine;

public class AccioNetejarTauler : Accio {
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	private Tauler tauler;
	private MenuCartesDisponibles menu;

	public AccioNetejarTauler(){
		tauler = (Tauler) (GameObject.FindGameObjectWithTag("Tauler")).GetComponent("Tauler");
		menu = (MenuCartesDisponibles) (GameObject.FindGameObjectWithTag("MenuCartesDisponibles")).GetComponent("MenuCartesDisponibles");
	}

	public void executarAccio(){
		//throw new System.NotImplementedException();
		PartGrafica p = (PartGrafica) Camera.mainCamera.GetComponent("PartGrafica");
		p.netejarTauler(tauler, menu);
	}
	
}
