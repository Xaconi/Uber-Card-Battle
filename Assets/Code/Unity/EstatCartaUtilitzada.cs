using UnityEngine;

public class EstatCartaUtilitzada : EstatCarta {
	
	private Carta cartaActual;
	private Vector3 posicio;
	
	public EstatCartaUtilitzada (Carta c){
		cartaActual = c;
		posicio = cartaActual.gameObject.transform.position;
		cartaActual.gameObject.renderer.enabled = false;
	}

	public void pintarCarta(){
		//throw new System.NotImplementedException();
	}
}
