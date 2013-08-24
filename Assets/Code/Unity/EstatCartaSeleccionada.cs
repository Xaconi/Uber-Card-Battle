using UnityEngine;

public class EstatCartaSeleccionada : EstatCarta {
	
	private Carta cartaActual;
	private Vector3 posicio;
	float pas = 0.1f;
	
	public EstatCartaSeleccionada(Carta c){
		cartaActual = c;
		posicio = cartaActual.gameObject.transform.position;
	}

	public void pintarCarta(){
		//throw new System.NotImplementedException();
		if(pas < 1.0f) pas += 0.001f;
		cartaActual.gameObject.renderer.material.color = Color.Lerp(cartaActual.gameObject.renderer.material.color, Color.blue, pas);
	}
}
