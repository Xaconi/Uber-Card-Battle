using UnityEngine;
using System.Collections.Generic;

public class AnimacioPantallesEdicioFinal : MonoBehaviour {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	//---------------------------------------------------
	// Posicio porta 1
	// Transform: X = -191.2, Y = -63.24343, Z = 207.9835
	// Rotation: X = 90, Y = 0, Z = 0
	//---------------------------------------------------
	// Posicio porta 2
	// Transform: X = 64.4, Y = -63.24343, Z = 207.9835
	// Rotation: X = 90, Y = 0, Z = 0
	//---------------------------------------------------

	public GameObject figura{
		get;
		set;
	}

	public struct Posicio{
		public float positionX;
		public float positionY;
		public float positionZ;
		public float rotationX;
		public float rotationY;
		public float rotationZ;
		public float scaleX;
		public float scaleY;
		public float scaleZ;
	}
	
	public Posicio posicio{
		get;
		set;
	}

	public Texture2D textura{
		get;
		set;
	}
	
	public float posicioMig{
		get;
		set;
	}
	
	public bool obertura{
		get;
		set;
	}
	
	public float pPantalla = 0.0f;
	public bool afegidaControlEdicio = false;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	public void Awake(){
		Debug.Log("AWAKE del AnimacioPantallesEdicio");
		textura = (Texture2D) Resources.Load("porta") as Texture2D;
		obertura = true;
	}
	
	public void Start(){
		Debug.Log("START del AnimacioPantallesEdicio");
	}
	
	public void Update(){

	}
	
	public void OnGUI(){
		if(obertura){
			if(pPantalla <= 0.5f)pPantalla += 0.01f;
			else if (!afegidaControlEdicio){
				AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
				a.volumeOut();
				
				List<GameObject> g = new List<GameObject>(GameObject.FindGameObjectsWithTag("Carta"));
				for(int i = 0; i < g.Count; i++){
					Destroy (g[i]);
				}
				
				g = new List<GameObject>(GameObject.FindGameObjectsWithTag("Cap"));
				for(int i = 0; i < g.Count; i++){
					Destroy (g[i]);
				}
				
				g = new List<GameObject>(GameObject.FindGameObjectsWithTag("Text"));
				for(int i = 0; i < g.Count; i++){
					Destroy (g[i]);
				}
				
				GameObject o = GameObject.FindGameObjectWithTag("BotoDreta") as GameObject;
				Destroy (o);
				o = GameObject.FindGameObjectWithTag("BotoEsquerre") as GameObject;
				Destroy (o);
				o = GameObject.FindGameObjectWithTag("MenuBaralla") as GameObject;
				Destroy (o);
				o = GameObject.FindGameObjectWithTag("MenuCartesDisponiblesEdicio") as GameObject;
				Destroy (o);
				
				Destroy(Camera.mainCamera.GetComponent("ControlGeneralEdicio"));
				Destroy(Camera.mainCamera.GetComponent("PartGraficaEdicio"));
				Destroy(Camera.mainCamera.GetComponent("ConnexioEdicio"));
				
				ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
				cJ.carregarPantallaMenuPrincipal();
				afegidaControlEdicio = true;
				obertura = false;
			}
			
			Rect pantalla = new Rect(0.0f, 0.0f, (Camera.mainCamera.pixelWidth)*pPantalla, Camera.mainCamera.pixelHeight);
			Rect percentatge1 = new Rect(1.0f-pPantalla, 0.0f, pPantalla, 1.0f);
			Graphics.DrawTexture(pantalla, textura, percentatge1,0,0,0,0,null);
			
			Rect pantalla2 = new Rect(Camera.mainCamera.pixelWidth-(Camera.mainCamera.pixelWidth*pPantalla), 0.0f, (Camera.mainCamera.pixelWidth)*pPantalla, Camera.mainCamera.pixelHeight);
			Rect percentatge = new Rect(0.0f, 0.0f, pPantalla, 1.0f);
			Graphics.DrawTexture(pantalla2, textura, percentatge,0,0,0,0,null);
		}else{
			if(pPantalla >= 0.0f)pPantalla -= 0.01f;
			else{
				AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
				a.playMenuMusic();
				Destroy (this);
			}
			
			Rect pantalla = new Rect(0.0f, 0.0f, (Camera.mainCamera.pixelWidth)*pPantalla, Camera.mainCamera.pixelHeight);
			Rect percentatge1 = new Rect(1.0f-pPantalla, 0.0f, pPantalla, 1.0f);
			Graphics.DrawTexture(pantalla, textura, percentatge1,0,0,0,0,null);
			
			Rect pantalla2 = new Rect(Camera.mainCamera.pixelWidth-(Camera.mainCamera.pixelWidth*pPantalla), 0.0f, (Camera.mainCamera.pixelWidth)*pPantalla, Camera.mainCamera.pixelHeight);
			Rect percentatge = new Rect(0.0f, 0.0f, pPantalla, 1.0f);
			Graphics.DrawTexture(pantalla2, textura, percentatge,0,0,0,0,null);
		}

	}

	public void animacioSortida(){
		throw new System.NotImplementedException();
	}
}
