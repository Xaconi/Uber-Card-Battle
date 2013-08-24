using UnityEngine;
using System.Collections.Generic;

public class Moviment {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	private  List<Accio> accionsTemporals{
		get;
		set;
	}

	private  List<Accio> accionsConfirmades{
		get;
		set;
	}

	public bool movimentConfirmat{
		get;
		set;
	}

	public int jugador{
		get;
		set;
	}
	
	private int accionsDisponibles = 5;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	public Moviment(int j){
		movimentConfirmat = false;
		accionsTemporals = new List<Accio>();
		accionsConfirmades = new List<Accio>();
		jugador = j;
	}

	public void afegirAccioTemporal( Accio a){
		accionsTemporals.Add(a);
		accionsDisponibles--;
		Debug.Log("Afegida a la llista d'accions temporals una accio del tipus " + a.GetType().ToString());
	}

	public void treureAccioTemporal(){
		Debug.Log("Treta de la llista d'accions temporals una accio del tipus " + accionsTemporals[accionsTemporals.Count-1].GetType().ToString());
		accionsTemporals.RemoveAt(accionsTemporals.Count-1);
		accionsDisponibles++;
	}

	public void confirmarAccions(){
		accionsConfirmades = accionsTemporals;
		movimentConfirmat = true;
	}

	public void enviarAccionsAPartida(){
		throw new System.NotImplementedException();
	}

	public int nombreAccionsTemporals(){
		throw new System.NotImplementedException();
	}
	
	public int getAccionsDisponibles(){
		return accionsDisponibles;
	}
	
	public List<Accio> getAccionsTemporals(){
		return accionsTemporals;
	}
}
