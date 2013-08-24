using UnityEngine;

public class AccioAtacarBase : Accio {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------

	private Personatge personatgeUsuari;
	private Base baseAtacar;
	private int atac;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public AccioAtacarBase(Personatge personatge, Base baseObjectiu){
		personatgeUsuari = personatge;
		baseAtacar = baseObjectiu;
	}

	public void executarAccio(){
		//throw new System.NotImplementedException();
		Accio neteja = new AccioNetejarTauler();
		neteja.executarAccio();
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		cG.movimentActual.afegirAccioTemporal(this);
		PartGrafica p = (PartGrafica) Camera.mainCamera.GetComponent("PartGrafica");
		p.atacarBase(personatgeUsuari, baseAtacar);
	}
	
	public void assignarAtac(string tipus){
		if(tipus.Equals("AtacCurtaDistancia")){
			atac = personatgeUsuari.atacCurtaDistancia;
		}else if(tipus.Equals("AtacLlargaDistancia")){
			atac = personatgeUsuari.atacLlargaDistancia;
		}
	}
	
	public Personatge getPersonatgeUsuari(){
		return personatgeUsuari;
	}
	
	public Base getBaseActual(){
		return baseAtacar;
	}
	
	public int getAtac(){
		return atac;
	}
}
