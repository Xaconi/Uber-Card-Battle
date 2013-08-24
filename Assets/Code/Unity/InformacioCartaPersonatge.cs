using UnityEngine;
using System.Collections;

public class InformacioCartaPersonatge : InformacioCarta {

	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	public string nom;
	public string tipus;
	public string nivell;
	public int atacLlargaDistancia;
	public int atacCurtaDistancia;
	public int defensa;
	public int moviment;
	public int distanciaAtac;
	public int posicioLlista;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public InformacioCartaPersonatge(string t, string n, string niv, int aL, int aC, int d, int m, int dA){
		tipus = t;
		nom = n;
		nivell = niv;
		atacLlargaDistancia = aL;
		atacCurtaDistancia = aC;
		defensa = d;
		moviment = m;
		distanciaAtac = dA;
	}
	
	public InformacioCartaPersonatge(InformacioCartaPersonatge i){
		tipus = i.tipus;
		nom = i.nom;
		nivell = i.nivell;
		atacLlargaDistancia = i.atacLlargaDistancia;
		atacCurtaDistancia = i.atacCurtaDistancia;
		defensa = i.defensa;
		moviment = i.moviment;
		distanciaAtac = i.distanciaAtac;
	}
}
