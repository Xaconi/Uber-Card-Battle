using UnityEngine;
using System.Collections;

public class EstatBaseNormal : EstatBase{
	
	private Base baseActual;
	private Vector3 posicio;
	float pas = 0.1f;
	Color color;

	public EstatBaseNormal(Base b){
		baseActual = b;
		posicio = baseActual.gameObject.transform.position;
		color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}
	
	public void pintarBase(){
		if(pas < 1.0f) pas += 0.001f;
		baseActual.gameObject.renderer.material.color = Color.Lerp(baseActual.gameObject.renderer.material.color, color, pas);
	}
}
