using UnityEngine;
using System.Collections;

public class AccioDesaCanvis : AccioEdicio {
	
	public AccioDesaCanvis(){
	}

	public void executarAccio(){
		AccioEdicio neteja = new AccioNetejaPantalla();
		neteja.executarAccio();
		PartGraficaEdicio p = (PartGraficaEdicio) Camera.mainCamera.GetComponent("PartGraficaEdicio");
		p.desaCanvis();
	}
}
