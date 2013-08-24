using UnityEngine;

public class EstatFitxaNormal : EstatFitxa {
	
	Vector3 posicio;
	Fitxa fitxaActual;
	float pas = 0.1f;
	Color color;

	public EstatFitxaNormal(Fitxa f){
		fitxaActual = f;
		posicio = f.gameObject.transform.position;
		color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}
	
	public void pintarCasella(){
		//throw new System.NotImplementedException();
		if(pas < 1.0f) pas += 0.001f;
		fitxaActual.gameObject.renderer.material.color = Color.Lerp(fitxaActual.gameObject.renderer.material.color, color, pas);
	}
}
