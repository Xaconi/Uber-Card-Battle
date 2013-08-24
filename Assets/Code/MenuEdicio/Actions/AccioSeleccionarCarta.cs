using UnityEngine;
using System.Collections;

public class AccioSeleccionarCarta : AccioEdicio {
	
	CartaEdicio cartaActual;
	
	public AccioSeleccionarCarta(CartaEdicio c){
		cartaActual = c;
	}

	public void executarAccio(){
		AccioEdicio neteja = new AccioNetejaPantalla();
		neteja.executarAccio();
		PartGraficaEdicio p = (PartGraficaEdicio) Camera.mainCamera.GetComponent("PartGraficaEdicio");
		p.seleccionarCarta(cartaActual);
	}
}
