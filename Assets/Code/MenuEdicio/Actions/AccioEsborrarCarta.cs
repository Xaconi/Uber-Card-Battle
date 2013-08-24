using UnityEngine;
using System.Collections;

public class AccioEsborrarCarta : AccioEdicio {
	
	CartaEdicio cartaEsborrar;
	
	public AccioEsborrarCarta(CartaEdicio c){
		cartaEsborrar = c;
	}

	public void executarAccio(){
		AccioEdicio neteja = new AccioNetejaPantalla();
		neteja.executarAccio();
		PartGraficaEdicio p = (PartGraficaEdicio) Camera.mainCamera.GetComponent("PartGraficaEdicio");
		p.esborrarCarta(cartaEsborrar);
	}
}
