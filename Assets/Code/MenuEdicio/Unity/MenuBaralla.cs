using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuBaralla : MonoBehaviour {
	
	GameObject figura;
	Vector3 posicio;
	Texture2D textura;
	public List<CartaEdicio> cartesDisponibles;
	public int cartesBase = 21;
	
	// Use this for initialization
	void Awake () {
		figura = this.gameObject;
		float ratio = Camera.mainCamera.aspect;
		float ratioOriginal = 16.0f/9.0f;
		float diferenciaRatio = ratio / ratioOriginal;
		//#if UNITY_EDITOR
		figura.transform.localScale = new Vector3 (figura.transform.localScale.x*diferenciaRatio, 
			figura.transform.localScale.y,
			figura.transform.localScale.z);
		cartesDisponibles = new List<CartaEdicio>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void emplenarTauler(){
		crearCartes ();
		demanarInformacioCartesBaralla();
	}
	
	public void crearCartes(){
		for(int i = 1; i <= cartesBase; i++) GameObject.Instantiate(Resources.Load("CartaEdicio"));
		List<GameObject> llistaCartes = new List<GameObject>(GameObject.FindGameObjectsWithTag("CartaNoVisible"));
		int iPesonatges = 0;
		int jPersonatges = 0;
		int iBonificacio = 5;
		int jBonificacio = 0;
		for(int i = 0; i < llistaCartes.Count; i++){
			llistaCartes[i].tag = "Carta";
			CartaEdicio c = (CartaEdicio) llistaCartes[i].GetComponent("CartaEdicio");
			cartesDisponibles.Add(c);
		}
	}
	
	public void demanarInformacioCartesBaralla(){
		ConnexioEdicio con = (ConnexioEdicio) Camera.mainCamera.GetComponent("ConnexioEdicio") as ConnexioEdicio;
		List<InformacioCarta> llistaInformacio  = con.donaCartesBaralla();
		Debug.Log(llistaInformacio.Count);
		int iPesonatges = 0;
		int jPersonatges = 0;
		int iBonificacio = 0;
		int jBonificacio = 5;
		for(int i = 0; i < llistaInformacio.Count; i++){
			cartesDisponibles[i].assignarInformacioCarta(llistaInformacio[i]);
			if(llistaInformacio[i].GetType().ToString().Equals("InformacioCartaPersonatge")){
				cartesDisponibles[i].donarPosicioLlistaBaralla(iPesonatges, jPersonatges);
				jPersonatges++;
				if(jPersonatges == 5){
					iPesonatges++;
					jPersonatges = 0;
				}
			}else if(llistaInformacio[i].GetType().ToString().Equals("InformacioCartaBonificacio")){
				cartesDisponibles[i].donarPosicioLlistaBaralla(iBonificacio, jBonificacio);
				jBonificacio++;
				if(jBonificacio == 7){
					iBonificacio++;
					jBonificacio = 5;
				}
			}
			cartesDisponibles[i].activarCartaBaralla();
		}
	}
}
