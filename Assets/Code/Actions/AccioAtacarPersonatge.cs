using UnityEngine;
using System.Collections.Generic;

public class AccioAtacarPersonatge : Accio {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------

	private Personatge personatgeUsuari;
	private Personatge personatgeAtacar;
	private List<Personatge> llistaPersonatgesAtacar;
	private int atac;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public AccioAtacarPersonatge(Personatge p1, Personatge p2){
		personatgeUsuari = p1;
		personatgeAtacar = p2;
	}

	public void executarAccio(){
		//throw new System.NotImplementedException();
		Accio neteja = new AccioNetejarTauler();
		neteja.executarAccio();
		personatgeUsuari.afegirObserver(personatgeAtacar);
		//llistaPersonatgesAtacar = personatgeUsuari.getLlistaObservers();
		llistaPersonatgesAtacar = new List<Personatge>(personatgeUsuari.getLlistaObservers());
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		cG.movimentActual.afegirAccioTemporal(this);
		PartGrafica p = (PartGrafica) Camera.mainCamera.GetComponent("PartGrafica");
		p.atacarPersonatge(personatgeUsuari, personatgeUsuari.getLlistaObservers());
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
	
	public List<Personatge> getLlistaPersonatgesAtacar(){
		return llistaPersonatgesAtacar;
	}
	
	public int getAtac(){
		return atac;
	}
	
	
	
}
