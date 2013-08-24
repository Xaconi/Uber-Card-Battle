using UnityEngine;
using System.Collections;

public class AccioIA {
	
	public Personatge personatgeActual;
	public Personatge personatgeExtern;
	public Carta cartaActual;
	public Fitxa fitxaActual;
	public Fitxa fitxaDesti;
	public Base baseActual;
	public string tipus;
	public float heuristica;
	
	public AccioIA(int h){
		heuristica = h;
	}
	
	// Accio d'aplicació de bonificació a un personatge
	public AccioIA(string t, Carta c, Personatge p, float h){
		tipus = t;
		personatgeActual = p;
		cartaActual = c;
		heuristica = h;
	}
	
	// Accio d'atac a la base
	public AccioIA(string t, Personatge p, Base b, float h){
		tipus = t;
		personatgeActual = p;
		baseActual = b;
		heuristica = h;
	}
	
	// Accio d'atac de personatge
	public AccioIA(string t, Personatge p1, Personatge p2, float h){
		tipus = t;
		personatgeActual = p1;
		personatgeExtern = p2;
		heuristica = h;
	}
	
	// Accio de moviment de personatge
	public AccioIA(string t, Personatge p, Fitxa fA, Fitxa fD, float h){
		tipus = t;
		personatgeActual = p;
		fitxaActual = fA;
		fitxaDesti = fD;
		heuristica = h;
	}
	
	// Accio de sortida del personatge al tauler
	public AccioIA(string t, Carta c, Fitxa fD, float h){
		tipus = t;
		cartaActual = c;
		fitxaDesti = fD;
		heuristica = h;
	}
	
	public void executarAccio(){
		Accio a;
		switch(tipus){
			case "SortidaPersonatge":
				a = new AccioTreurePersonatge(fitxaDesti, cartaActual);
				a.executarAccio();
			break;
			case "BonificacioPersonatge":
				a = new AccioAplicarBonificacio(personatgeActual, cartaActual);
				a.executarAccio();
			break;
			case "MovimentPersonatge":
				a = new AccioMourePersonatge(personatgeActual, fitxaActual, fitxaDesti);
				a.executarAccio();
			break;
			case "AtacPersonatge":
				a = new AccioAtacarPersonatge(personatgeActual, personatgeExtern);
				a.executarAccio();
			break;
			case "AtacBase":
				a = new AccioAtacarBase(personatgeActual, baseActual);
				a.executarAccio();
			break;
		}
	}
}
