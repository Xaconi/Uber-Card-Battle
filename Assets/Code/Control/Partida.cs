using UnityEngine;
using System.Collections.Generic;

public class Partida {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	public int nMoviments{
		get;
		set;
	}

	public double tempsActual{
		get{return this.tempsActual;}
		set{this.tempsActual = value;}
	}

	public double tempsAcumulat{
		get{return this.tempsAcumulat;}
		set{this.tempsAcumulat = value;}
	}

	public Fitxa[,] estatActualTauler{
		get;
		set;
	}

	public Carta[] estatActualCartes{
		get{return this.estatActualCartes;}
		set{this.estatActualCartes = value;}
	}
	
	public int torns{
		get;
		set;
	}
	
	public List<Moviment> llistaMoviments;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	public Partida(){
		llistaMoviments = new List<Moviment>();
		nMoviments = 0;
		torns = 0;
	}

	public void afegirMoviment(Moviment m){
		//throw new System.NotImplementedException();
		llistaMoviments.Add(m);
		augmentarNombreMoviments();
	}

	public void augmentarTemps(){
		throw new System.NotImplementedException();
	}

	public void augmentarNombreMoviments(){
		nMoviments++;
	}
	
	public void augmentarNombreTorns(){
		torns++;
	}

	public void actualitzarEstatTauler(Tauler t){
		estatActualTauler = t.getLlistaFitxes();
	}
}
