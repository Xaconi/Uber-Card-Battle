using UnityEngine;
using System.Collections;

public class EstatPersonatgeNoVisible : EstatPersonatge {

	Personatge personatgeUsuari;
	
	public EstatPersonatgeNoVisible(Personatge p){
		personatgeUsuari = p;
		personatgeUsuari.gameObject.renderer.enabled = false;
		personatgeUsuari.gameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
//		if(personatgeUsuari.propietari == 2){
//			personatgeUsuari.transform.localScale = new Vector3(-personatgeUsuari.transform.localScale.x,
//				personatgeUsuari.transform.localScale.y,
//				personatgeUsuari.transform.localScale.z);
//		}
	}

	public void pintarPersonatge(){
		//throw new System.NotImplementedException();
	}
}
