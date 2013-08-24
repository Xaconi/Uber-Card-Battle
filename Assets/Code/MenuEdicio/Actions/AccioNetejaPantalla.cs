using UnityEngine;
using System.Collections;

public class AccioNetejaPantalla : AccioEdicio {
	
	public AccioNetejaPantalla(){
	}

	public void executarAccio(){
		PartGraficaEdicio p = (PartGraficaEdicio) Camera.mainCamera.GetComponent("PartGraficaEdicio");
		p.netejarPantallaEdicio();
	}
}
