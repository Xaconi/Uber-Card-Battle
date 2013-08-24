using UnityEngine;
using System.Collections;

public class AccioCartesEsquerre : AccioEdicio {
	
	int direccio;
	
	public AccioCartesEsquerre(int d){
		direccio = d;
	}
	
	public void executarAccio(){
		AccioEdicio neteja = new AccioNetejaPantalla();
		neteja.executarAccio();
		PartGraficaEdicio p = (PartGraficaEdicio) Camera.mainCamera.GetComponent("PartGraficaEdicio");
		p.ensenyarCartesEsquerre(direccio);
	}
}
