using UnityEngine;

public class EstatBaseAtacat : EstatBase {

	private Base baseActual;
	private Vector3 posicio;
	float pas = 0.1f;
	Color color;

	public EstatBaseAtacat(Base b){
		baseActual = b;
		posicio = baseActual.gameObject.transform.position;
	}
	
	public void pintarBase(){
		if(baseActual.gameObject.name.ToString().Equals("Base(Clone)")){
			GameObject lifebar = GameObject.FindGameObjectWithTag("LifeBarP1");
			TextMesh t = lifebar.gameObject.GetComponent("TextMesh") as TextMesh;
			float f = ((float)baseActual.defensa / (float)baseActual.defensaTotal)*100;
			Debug.Log(f);
			if(f > 0.0f) t.text = ((int)f).ToString() + "%";
			else t.text = "0%";
		}else if(baseActual.gameObject.name.ToString().Equals("BaseRival(Clone)")){
			GameObject lifebar = GameObject.FindGameObjectWithTag("LifeBarP2");
			TextMesh t = lifebar.gameObject.GetComponent("TextMesh") as TextMesh;
			float f = ((float)baseActual.defensa / (float)baseActual.defensaTotal)*100;
			Debug.Log(baseActual.defensa + "," + baseActual.defensaTotal);
			if(f > 0.0f) t.text = ((int)f).ToString() + "%";
			else t.text = "0%";
		}
		baseActual.setEstat(baseActual.getEstatAnterior());
		baseActual.mirarMort();
		if(baseActual.defensa > 0){
			ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
			if(cG.IA && cG.usuariTorn == 2){
				// Si es tracta d'un moviment de la IA
				cG.avisarIA();
			}
		}
	}
}
