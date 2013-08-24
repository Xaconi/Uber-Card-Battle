using UnityEngine;
using System.Collections;

public class AccioConfirmarNoDesa : AccioEdicio {
	
	public AccioConfirmarNoDesa(){
	}
	
	public void executarAccio(){
		AccioEdicio neteja = new AccioNetejaPantalla();
		neteja.executarAccio();
		PartGraficaEdicio p = (PartGraficaEdicio) Camera.mainCamera.GetComponent("PartGraficaEdicio");
		p.confirmarNoDesaCanvis();
	}
}
