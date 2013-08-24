using UnityEngine;
using System.Collections.Generic;

public class Tauler : MonoBehaviour {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	public GameObject figura;
	public Vector3 posicio;
	public int llargadaFitxesTauler = 11;
	public int ampladaFitxesTauler = 6;
	public int numeroFitxes = 66;
	public Fitxa[,] llistaFitxes;
	public int ID;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	public void Awake(){
		float ratio = Camera.mainCamera.aspect;
		float ratioOriginal = 16.0f/9.0f;
		float diferenciaRatio = ratio / ratioOriginal;
		figura = this.gameObject;
		Vector3 grandaria = ((Renderer) gameObject.GetComponentInChildren(typeof(Renderer))).bounds.size;
		if(ratio != ratioOriginal){
			if(ratio == 16.0f/10.0f || (Screen.width == 1280.0f && Screen.height == 736.0f)){
				Camera.mainCamera.transform.position = new Vector3(Camera.mainCamera.transform.position.x,
					Camera.mainCamera.transform.position.y,
					-6.498569f);
				gameObject.transform.localScale = new Vector3(1.265875f, gameObject.transform.localScale.y,
					gameObject.transform.localScale.z);
			}else if(ratio == 4.0f/3.0f){
				Camera.mainCamera.transform.position = new Vector3(Camera.mainCamera.transform.position.x,
					Camera.mainCamera.transform.position.y,
					-2.307818f);
				gameObject.transform.localScale = new Vector3(1.510014f, gameObject.transform.localScale.y,
					gameObject.transform.localScale.z);
				gameObject.transform.position = new Vector3(-6.049924f, 1.315941f, -28.26275f);
			}
		}
//		figura.transform.localScale = new Vector3 (figura.transform.localScale.x/diferenciaRatio, 
//			figura.transform.localScale.y*diferenciaRatio,
//			figura.transform.localScale.z*diferenciaRatio);
		posicio = figura.transform.position;
		ID = figura.GetInstanceID();
		assignarLlistaFitxes();
	}

	public void Start(){
		//throw new System.NotImplementedException();
	}

	public void Update(){
		//throw new System.NotImplementedException();
	}
	
	public void assignarLlistaFitxes(){
		List<GameObject> g = new List<GameObject>(GameObject.FindGameObjectsWithTag("Fitxa"));
		llistaFitxes = new Fitxa[6,11];
		for(int i = 0; i < g.Count; i++){
			Fitxa f = (Fitxa) g[i].GetComponent("Fitxa");
			llistaFitxes[f.fila-1, f.columna-1] = f;
		}
	}
	
	public Fitxa[,] getLlistaFitxes(){
		return llistaFitxes;
	}
	
	public bool comprovarPersonatges(int usuariTorn){
		bool personatgeTrobat = false;
		int i = 0, j = 0;
		while(i < ampladaFitxesTauler && ! personatgeTrobat){
			while(j < llargadaFitxesTauler && ! personatgeTrobat){
				if(llistaFitxes[i,j].tePersonatge() && llistaFitxes[i,j].personatge.propietari == usuariTorn) personatgeTrobat = true;
				j++;
			}
			j = 0;
			i++;
		}
		return personatgeTrobat;
	}
}
