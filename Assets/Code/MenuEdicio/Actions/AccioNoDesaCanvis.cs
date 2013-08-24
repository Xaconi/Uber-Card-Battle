using UnityEngine;
using System.Collections;

public class AccioNoDesaCanvis : AccioEdicio {
	
	public AccioNoDesaCanvis(){
	}

	public void executarAccio(){
		AccioEdicio neteja = new AccioNetejaPantalla();
		neteja.executarAccio();
		PartGraficaEdicio p = (PartGraficaEdicio) Camera.mainCamera.GetComponent("PartGraficaEdicio");
		p.noDesaCanvis();
	}
}
