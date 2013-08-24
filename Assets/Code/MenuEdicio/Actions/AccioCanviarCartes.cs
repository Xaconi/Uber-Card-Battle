using UnityEngine;
using System.Collections;

public class AccioCanviarCartes : AccioEdicio {
	
	private CartaEdicio cartaActual;
	private CartaEdicio cartaExterna;
	private float startTime;
	
	public AccioCanviarCartes(CartaEdicio cA, CartaEdicio cE){
		cartaActual = cA;
		cartaExterna = cE;
		startTime = Time.time;
	}
	
	public void executarAccio(){
		AccioEdicio neteja = new AccioNetejaPantalla();
		neteja.executarAccio();
		PartGraficaEdicio p = (PartGraficaEdicio) Camera.mainCamera.GetComponent("PartGraficaEdicio");
		p.canviarCartes(cartaActual, cartaExterna, startTime);
	}
}
