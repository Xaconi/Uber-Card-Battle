using UnityEngine;
using System.Collections;

public class EstatCartaNoVisible : EstatCarta {
	
	private Carta cartaActual;
	private Vector3 posicio;
	
	public EstatCartaNoVisible(Carta c){
		cartaActual = c;
		posicio = cartaActual.transform.position;
		cartaActual.gameObject.renderer.enabled = false;
	}
	
	public void pintarCarta(){
		// Aqui, en ja ser utilitzada, no la hauriem de pintar...
	}
}
