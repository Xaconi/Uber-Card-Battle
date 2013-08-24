using UnityEngine;

public class AnimacioPantalles : MonoBehaviour {
	
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
	public bool afegidaControlBatalla = false;
	public delegate void Partida(int pT);
  	public static event Partida carregaBaralles;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	public void Awake(){
		Debug.Log("AWAKE del AnimacioPantalles");
		textura = (Texture2D) Resources.Load("porta") as Texture2D;
		obertura = true;
	}
	
	public void Start(){
		Debug.Log("START del AnimacioPantalles");
	}
	
	public void Update(){

	}
	
	public void OnGUI(){
		if(obertura){
			if(pPantalla <= 0.5f)pPantalla += 0.01f;
			else if (!afegidaControlBatalla){
				Debug.Log("Afegim el Script del ControlGeneralBatalla");
				Camera.mainCamera.gameObject.AddComponent("ControlGeneralBatalla");
				GameObject b = (GameObject)GameObject.FindGameObjectWithTag("Background"); // Ens carreguem el background del menu
				Destroy (b);
				afegidaControlBatalla = true;
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
				carregaBaralles(1);
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
