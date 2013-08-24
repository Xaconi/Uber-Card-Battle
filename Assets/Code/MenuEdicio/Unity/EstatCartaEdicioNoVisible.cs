using UnityEngine;
using System.Collections;

public class EstatCartaEdicioNoVisible : EstatCartaEdicio {

	private CartaEdicio cartaActual;
	private Vector3 posicio;
	private float pas;
	
	public EstatCartaEdicioNoVisible(CartaEdicio c){
		cartaActual = c;
		posicio = cartaActual.transform.position;
		pas = 0.1f;
		cartaActual.gameObject.renderer.enabled = false;
	}
	
	public void pintarCarta(){
	}
}