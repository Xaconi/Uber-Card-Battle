using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuCartesDisponiblesEdicio : MonoBehaviour {
	
	GameObject figura;
	Vector3 posicio;
	Material material;
	public List<CartaEdicio> cartesDisponibles;
	public List<CartaEdicio> llistaCartesNoves;
	int cartesBase = 5;
	int index = 0;
	
	// Use this for initialization
	void Awake () {
		figura = this.gameObject;
		float ratio = Camera.mainCamera.aspect;
		float ratioOriginal = 16.0f/9.0f;
		float diferenciaRatio = ratio / ratioOriginal;
		figura.transform.position = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Camera.mainCamera.pixelWidth*0.5f, 
			Camera.mainCamera.pixelHeight*0.143f, 
			-9.999999f - figura.transform.position.z));
		//#if UNITY_EDITOR
		figura.transform.localScale = new Vector3 (figura.transform.localScale.x*diferenciaRatio, 
			1,
			1.065283f);
		
		cartesDisponibles = new List<CartaEdicio>();
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void crearCartes(){
		for(int i = 1; i <= cartesBase; i++) GameObject.Instantiate(Resources.Load("CartaEdicio"));
		List<GameObject> llistaCartes = new List<GameObject>(GameObject.FindGameObjectsWithTag("CartaNoVisible"));
		for(int i = 0; i < llistaCartes.Count; i++){
			llistaCartes[i].tag = "Carta";
			cartesDisponibles.Add((CartaEdicio) llistaCartes[i].GetComponent("CartaEdicio"));
			cartesDisponibles[i].donarPosicioLlista(i);
		}
	}
	
	public void demanarInformacioCartesNoves(int direccio){
		if(direccio == 0 && index != 0) index--;
		else if(direccio == 1) index++;
		
		ConnexioEdicio con = (ConnexioEdicio) Camera.mainCamera.GetComponent("ConnexioEdicio") as ConnexioEdicio;
		List<InformacioCarta> llistaInformacio  = con.donaCartesNoves(index);
		Debug.Log(llistaInformacio.Count);
		if(llistaInformacio.Count > 0){
			cartesDisponibles[0].desactivarCarta();
			cartesDisponibles[1].desactivarCarta();
			cartesDisponibles[2].desactivarCarta();
			cartesDisponibles[3].desactivarCarta();
			cartesDisponibles[4].desactivarCarta();
			for(int i = 0; i < llistaInformacio.Count; i++){
				cartesDisponibles[i].donarPosicioLlista(i);
				cartesDisponibles[i].assignarInformacioCarta(llistaInformacio[i]);
				cartesDisponibles[i].activarCarta();
			}
		}else{
			index--;
		}
	}
}