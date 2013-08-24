using UnityEngine;

public class Estadistiques {
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	public float tempsPartidaInici;
	public float tempsPartidaFinal;
	public int nPersonatgesUtilitzats;
	public int nBonificacionsUtilitzades;
	public int nCartesUtilitzades;
	public string scenario;
	public bool victoria;
	public string formacio;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	public Estadistiques(string s){
		tempsPartidaInici = Time.time;
		nCartesUtilitzades = 0;
		scenario = s;
	}
	
	public void augmentarCartes(){
		Debug.Log("He utilitzat una carta");
		nCartesUtilitzades++;
	}
	
	public void reduirCartes(){
		Debug.Log("M'han tornat una carta");
		nCartesUtilitzades--;
	}
	
	public void augmentarPersonatges(){
		Debug.Log("He utilitzat una personatge");
		nPersonatgesUtilitzats++;
	}
	
	public void reduirPersonatges(){
		Debug.Log("M'han tornat un personatge");
		nPersonatgesUtilitzats--;
	}
	
	public void augmentarBonificacions(){
		Debug.Log("He utilitzat una bonificacio");
		nBonificacionsUtilitzades++;
	}
	
	public void reduirBonificacions(){
		Debug.Log("M'han tornat una bonificacio");
		nBonificacionsUtilitzades--;
	}

	public void finalitzarPartida(){
		tempsPartidaFinal = Time.time - tempsPartidaInici;
	}
	
	public void assignarFormacio(string f){
		formacio = f;
	}
}
