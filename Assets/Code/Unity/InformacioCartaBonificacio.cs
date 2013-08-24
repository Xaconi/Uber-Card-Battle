using UnityEngine;
using System.Collections;

public class InformacioCartaBonificacio : InformacioCarta {

	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	public string nom;
	public string tipus;
	public string nivell;
	public int atac;
	public int defensa;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public InformacioCartaBonificacio(string t, string n, string niv, int a , int d){
		tipus = t;
		nom = n;
		nivell = niv;
		atac = a;
		defensa = d;
	}
	
	public InformacioCartaBonificacio(InformacioCartaBonificacio i){
		nom = i.nom;
		tipus = i.tipus;
		nivell = i.nivell;
		atac = i.atac;
		defensa = i.defensa;
	}
}
