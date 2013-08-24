using UnityEngine;

public class EstatFitxaDisponible : EstatFitxa {
	
	Vector3 posicio;
	Fitxa fitxa;
	float pas = 0.1f;
	
	public EstatFitxaDisponible(Fitxa fitxaActual){
		posicio = fitxaActual.transform.position;
		fitxa = fitxaActual;
	}

	public void pintarCasella(){
		//throw new System.NotImplementedException();
		if(pas < 1.0f) pas += 0.001f;
		fitxa.gameObject.renderer.material.color = Color.Lerp(fitxa.gameObject.renderer.material.color, Color.blue, pas);
	}
}
