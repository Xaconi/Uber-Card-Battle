using UnityEngine;
using System.Collections;

public class AccioCartesDreta : AccioEdicio {
	
	int direccio;
	
	public AccioCartesDreta(int d){
		direccio = d;
	}
	
	public void executarAccio(){
		AccioEdicio neteja = new AccioNetejaPantalla();
		neteja.executarAccio();
		PartGraficaEdicio p = (PartGraficaEdicio) Camera.mainCamera.GetComponent("PartGraficaEdicio");
		p.ensenyarCartesDreta(direccio);
	}
}
