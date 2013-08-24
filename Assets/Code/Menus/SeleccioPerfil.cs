using UnityEngine;
using System.Collections;

public class SeleccioPerfil : MonoBehaviour {
	
	public Texture2D titolPantalla;
	public Texture2D botoBack;
	public GameObject descripcioPantalla;
	public GameObject perfil1;
	public GameObject perfil2;
	public GameObject perfil3;
	public GameObject titolPerfil1;
	public GameObject titolPerfil2;
	public GameObject titolPerfil3;
	public GameObject descripcioPerfil1;
	public GameObject descripcioPerfil2;
	public GameObject descripcioPerfil3;
	ConnexioMenus conMenu;
	public int fontSize;
	
	void Awake(){
		int fontSize = (int) Mathf.Ceil(20.0f * (Camera.mainCamera.pixelWidth/568.0f));
		
		titolPantalla = Resources.Load("seleccio_de_perfils") as Texture2D;
		botoBack = Resources.Load("boto_enrere") as Texture2D;
		
		perfil1 = new GameObject("perfil");
		perfil1.AddComponent("GUITexture");
		perfil1.guiTexture.texture = Resources.Load("cartell_perfil") as Texture;
		perfil2 = new GameObject("perfil");
		perfil2.AddComponent("GUITexture");
		perfil2.guiTexture.texture = Resources.Load("cartell_perfil") as Texture;
		perfil3 = new GameObject("perfil");
		perfil3.AddComponent("GUITexture");
		perfil3.guiTexture.texture = Resources.Load("cartell_perfil") as Texture;
		
		titolPerfil1 = new GameObject("TextPerfil");
		titolPerfil1.AddComponent("GUIText");
		titolPerfil1.guiText.font = Resources.Load("ERASERDUST") as Font;
		titolPerfil1.guiText.fontSize = fontSize;
		titolPerfil2 = new GameObject("TextPerfil");
		titolPerfil2.AddComponent("GUIText");
		titolPerfil2.guiText.font = Resources.Load("ERASERDUST") as Font;
		titolPerfil2.guiText.fontSize = fontSize;
		titolPerfil3 = new GameObject("TextPerfil");
		titolPerfil3.AddComponent("GUIText");
		titolPerfil3.guiText.font = Resources.Load("ERASERDUST") as Font;
		titolPerfil3.guiText.fontSize = fontSize;
		
		descripcioPerfil1 = new GameObject("TextPerfil");
		descripcioPerfil1.AddComponent("GUIText");
		descripcioPerfil1.guiText.font = Resources.Load("ERASERDUST") as Font;
		descripcioPerfil1.guiText.fontSize = fontSize;
		descripcioPerfil2 = new GameObject("TextPerfil");
		descripcioPerfil2.AddComponent("GUIText");
		descripcioPerfil2.guiText.font = Resources.Load("ERASERDUST") as Font;
		descripcioPerfil2.guiText.fontSize = fontSize;
		descripcioPerfil3 = new GameObject("TextPerfil");
		descripcioPerfil3.AddComponent("GUIText");
		descripcioPerfil3.guiText.font = Resources.Load("ERASERDUST") as Font;
		descripcioPerfil3.guiText.fontSize = fontSize;
		
		descripcioPantalla = new GameObject("TextPerfil");
		descripcioPantalla.AddComponent("GUIText");
		descripcioPantalla.guiText.font = Resources.Load("ERASERDUST") as Font;
		descripcioPantalla.guiText.fontSize = fontSize;
		descripcioPantalla.guiText.alignment = TextAlignment.Center;
		descripcioPantalla.guiText.material.color = Color.black;
	}
	
	// Use this for initialization
	void Start () {
		conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
		if(conMenu.p.llistaPlayers[0].dU.activat == 1){
			// Jugador 1 activat
			titolPerfil1.guiText.text = conMenu.p.llistaPlayers[0].dU.nom;
			descripcioPerfil1.guiText.text = "V: " + conMenu.p.llistaPlayers[0].e.v + " D: " + conMenu.p.llistaPlayers[0].e.d;
		}else{
			// Jugador 1 no activat
			titolPerfil1.guiText.text = "EMPTY";
		}
		
		if(conMenu.p.llistaPlayers[1].dU.activat == 1){
			// Jugador 2 activat
			titolPerfil2.guiText.text = conMenu.p.llistaPlayers[1].dU.nom;
			descripcioPerfil2.guiText.text = "V: " + conMenu.p.llistaPlayers[1].e.v + " D: " + conMenu.p.llistaPlayers[1].e.d;
		}else{
			// Jugador 2 no activat
			titolPerfil2.guiText.text = "EMPTY";
		}
		
		if(conMenu.p.llistaPlayers[2].dU.activat == 1){
			// Jugador 3 activat
			titolPerfil3.guiText.text = conMenu.p.llistaPlayers[2].dU.nom;
			descripcioPerfil3.guiText.text = "V: " + conMenu.p.llistaPlayers[2].e.v + " D: " + conMenu.p.llistaPlayers[2].e.d;
		}else{
			// Jugador 3 no activat
			titolPerfil3.guiText.text = "EMPTY";
		}
		
		descripcioPantalla.guiText.text = "Selecciona el teu perfil per \niniciar una partida";
	}
	
	// Update is called once per frame
	void Update () {
		if(detectaClickTectura(perfil1.guiTexture)){
			Debug.Log("Click al perfil 1");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			if(titolPerfil1.guiText.text.Equals("EMPTY")){
				conMenu.activarJugador(0);
			}
			conMenu.assignarJugador(0);
			carregarMenuPrincipal();
		}else if(detectaClickTectura(perfil2.guiTexture)){
			Debug.Log("Click al perfil 2");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			if(titolPerfil2.guiText.text.Equals("EMPTY")){
				conMenu.activarJugador(1);
			}
			conMenu.assignarJugador(1);
			carregarMenuPrincipal();
		}else if(detectaClickTectura(perfil3.guiTexture)){
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			if(titolPerfil3.guiText.text.ToString().Equals("EMPTY")){
				Debug.Log("Click al perfil 3");
				conMenu.activarJugador(2);
			}
			conMenu.assignarJugador(2);
			carregarMenuPrincipal();
		}
	}
	
	void OnGUI(){
		fontSize = (int) Mathf.Ceil(20.0f * (Camera.mainCamera.pixelWidth/568.0f));
		
		titolPerfil1.guiText.fontSize = fontSize;
		descripcioPerfil1.guiText.fontSize = fontSize;
		if(!titolPerfil1.guiText.text.Equals("EMPTY")){
			titolPerfil1.transform.position = new Vector3(0.13f, 0.55f, 1);
			descripcioPerfil1.transform.position = new Vector3(0.13f, 0.45f, 1);
		}else{
			titolPerfil1.transform.position = new Vector3(0.145f, 0.55f, 1);
		}
		
		titolPerfil2.guiText.fontSize = fontSize;
		descripcioPerfil2.guiText.fontSize = fontSize;
		if(!titolPerfil2.guiText.text.Equals("EMPTY")){
			titolPerfil2.transform.position = new Vector3(0.43f, 0.55f, 1);
			descripcioPerfil2.transform.position = new Vector3(0.43f, 0.45f, 1);
		}else{
			titolPerfil2.transform.position = new Vector3(0.445f, 0.55f, 1);
		}
		
		titolPerfil3.guiText.fontSize = fontSize;
		descripcioPerfil3.guiText.fontSize = fontSize;
		if(!titolPerfil3.guiText.text.Equals("EMPTY")){
			titolPerfil3.transform.position = new Vector3(0.73f, 0.55f, 1);
			descripcioPerfil3.transform.position = new Vector3(0.73f, 0.45f, 1);
		}else{
			titolPerfil3.transform.position = new Vector3(0.745f, 0.5f, 1);
		}
		
		descripcioPantalla.guiText.fontSize = fontSize;
		descripcioPantalla.transform.position = new Vector3(0.25f, 0.85f, 1);
		
		perfil1.guiTexture.transform.localScale = new Vector3(0.2f, 0.3f, 1);
		perfil1.guiTexture.transform.position = new Vector3(0.2f, 0.45f, 0.5f);
		
		perfil2.guiTexture.transform.localScale = new Vector3(0.2f, 0.3f, 1);
		perfil2.guiTexture.transform.position = new Vector3(0.5f, 0.45f, 0.5f);
		
		perfil3.guiTexture.transform.localScale = new Vector3(0.2f, 0.3f, 1);
		perfil3.guiTexture.transform.position = new Vector3(0.8f, 0.45f, 0.5f);
		
		Rect rectTitol = new Rect(0.3f*Camera.mainCamera.pixelWidth, 
			0.0f*Camera.mainCamera.pixelHeight, 
			0.4f*Camera.mainCamera.pixelWidth, 
			0.1f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rectTitol, titolPantalla);
		
		Rect rectBack = new Rect(0.3f*Camera.mainCamera.pixelWidth, 
			0.9f*Camera.mainCamera.pixelHeight, 
			0.4f*Camera.mainCamera.pixelWidth, 
			0.1f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rectBack, botoBack);
		
		if(detectaClick(rectBack)){
			// Click al botó d'enrere
			Debug.Log("Click al botó d'enrere");
			
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			
			ControlGeneralPerfils cG = (ControlGeneralPerfils) Camera.mainCamera.GetComponent("ControlGeneralPerfils");
			Destroy (cG);
			GameObject b = (GameObject)GameObject.FindGameObjectWithTag("Background");
			Destroy (b);
			
			//Destroy(titolPantalla);
			//Destroy(botoBack);
			//Destroy(howToPlay);
			Destroy(descripcioPantalla);
			Destroy(perfil1);
			Destroy(perfil2);
			Destroy(perfil3);
			Destroy(titolPerfil1);
			Destroy(titolPerfil2);
			Destroy(titolPerfil3);
			Destroy(descripcioPerfil1);
			Destroy(descripcioPerfil2);
			Destroy(descripcioPerfil3);
			
			Destroy(this);
			
			ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
			cJ.carregarPantallaTitol();
			SeleccioPerfil s = (SeleccioPerfil) Camera.mainCamera.GetComponent("SeleccioPerfil");
			Destroy(s);
			Destroy(conMenu);
			
			Destroy(this);
		}
	}
	
	private bool detectaClick(Rect rect){
		bool click = false;
		Event e = Event.current;
		if(e.type == EventType.MouseDown && rect.Contains(e.mousePosition)){
			click = true;
		}
		return click;
	}
	
	private bool detectaClickTectura(GUITexture g){
		bool click = false;
		Event e = Event.current;
		if(Input.GetMouseButton(0) && g.HitTest(Input.mousePosition)){
			click = true;
		}
		return click;
	}
	
	private void carregarMenuPrincipal(){
		
		ControlGeneralPerfils cG = (ControlGeneralPerfils) Camera.mainCamera.GetComponent("ControlGeneralPerfils");
		Destroy (cG);
//		GameObject b = (GameObject)GameObject.FindGameObjectWithTag("Background");
//		Destroy (b);
		
		//Destroy(titolPantalla);
		//Destroy(botoBack);
		//Destroy(howToPlay);
		Destroy(descripcioPantalla);
		Destroy(perfil1);
		Destroy(perfil2);
		Destroy(perfil3);
		Destroy(titolPerfil1);
		Destroy(titolPerfil2);
		Destroy(titolPerfil3);
		Destroy(descripcioPerfil1);
		Destroy(descripcioPerfil2);
		Destroy(descripcioPerfil3);
		
		Destroy(this);
		
		ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
		cJ.carregarPantallaMenuPrincipal();
		SeleccioPerfil s = (SeleccioPerfil) Camera.mainCamera.GetComponent("SeleccioPerfil");
		Destroy(s);
		
		Destroy(this);
	}
}
