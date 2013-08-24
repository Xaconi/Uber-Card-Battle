using UnityEngine;

public class AccioEnsenyarInformacio : Accio {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------

	public Tauler tauler{
		get;
		set;
	}

	public Personatge personatgeUsuari{
		get;
		set;
	}
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public AccioEnsenyarInformacio(Personatge p1){
		personatgeUsuari = p1;
		tauler = (Tauler) (GameObject.FindGameObjectWithTag("Tauler")).GetComponent("Tauler");
	}

	public void executarAccio(){
		//throw new System.NotImplementedException();
		Accio neteja = new AccioNetejarTauler();
		neteja.executarAccio();
		PartGrafica p = (PartGrafica) Camera.mainCamera.GetComponent("PartGrafica");
		p.ensenyarInformacio(personatgeUsuari, tauler);
	}
}
