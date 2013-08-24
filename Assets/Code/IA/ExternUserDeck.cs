using UnityEngine;

public class ExternUserDeck{
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	public bool repartirCartes{
		get{return this.repartirCartes;}
		set{this.repartirCartes = value;}
	}
	
	public Carta[] deck{
		get{return this.deck;}
		set{this.deck = value;}
	}
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	public void repartirCartesIA(ExternMenuCartesDisponibles menu){
		throw new System.NotImplementedException();
	}
}
