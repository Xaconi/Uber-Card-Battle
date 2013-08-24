using UnityEngine;

public class EstatFitxaAtac : EstatFitxa {
	
	Vector3 posicio;
	Fitxa fitxaActual;
	float pas = 0.1f;
	
	public EstatFitxaAtac(Fitxa f){
		posicio = f.gameObject.transform.position;
		fitxaActual = f;
	}

	public void pintarCasella(){
		//throw new System.NotImplementedException();
		if(pas < 1.0f) pas += 0.001f;
		fitxaActual.gameObject.renderer.material.color = Color.Lerp(fitxaActual.gameObject.renderer.material.color, Color.red, pas);
	}
}
