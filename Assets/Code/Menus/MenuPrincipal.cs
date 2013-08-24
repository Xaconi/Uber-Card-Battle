using UnityEngine;
using System.Collections;

public class MenuPrincipal : MonoBehaviour {
	
	public Texture2D titolPantalla;
	public Texture2D botoBack;
	public Texture2D howToPlay;
	public GameObject descripcioPantalla;
	public Texture2D modeHistoria;
	public Texture2D modeQuick;
	public Texture2D modeEdicio;
	public Texture2D modeEstadistiques;
	ConnexioMenus conMenu;
	public int fontSize;
	
	void Awake(){
		int fontSize = (int) Mathf.Ceil(20.0f * (Camera.mainCamera.pixelWidth/568.0f));
		
		titolPantalla = Resources.Load("menu_principal") as Texture2D;
		botoBack = Resources.Load("boto_enrere") as Texture2D;
		howToPlay = Resources.Load("boto_comjugar") as Texture2D;
		modeHistoria = Resources.Load("mode_historia") as Texture2D;
		modeQuick = Resources.Load("mode_quick") as Texture2D;
		modeEdicio = Resources.Load("mode_edicio") as Texture2D;
		modeEstadistiques = Resources.Load("mode_estadistiques") as Texture2D;
		
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
		
		descripcioPantalla.guiText.text = "Benvingut a Uber Card Battle!";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		fontSize = (int) Mathf.Ceil(20.0f * (Camera.mainCamera.pixelWidth/568.0f));
		
		Rect rectTitol = new Rect(0.3f*Camera.mainCamera.pixelWidth, 
			0.0f*Camera.mainCamera.pixelHeight, 
			0.4f*Camera.mainCamera.pixelWidth, 
			0.1f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rectTitol, titolPantalla);
		
		Rect rectBack = new Rect(0.1f*Camera.mainCamera.pixelWidth, 
			0.9f*Camera.mainCamera.pixelHeight, 
			0.4f*Camera.mainCamera.pixelWidth, 
			0.1f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rectBack, botoBack);
		
		Rect rectHowTo = new Rect(0.6f*Camera.mainCamera.pixelWidth, 
			0.9f*Camera.mainCamera.pixelHeight, 
			0.3f*Camera.mainCamera.pixelWidth, 
			0.1f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rectHowTo, howToPlay);
		
		Rect rectHistoria = new Rect(0.04f*Camera.mainCamera.pixelWidth, 
			0.3f*Camera.mainCamera.pixelHeight, 
			0.2f*Camera.mainCamera.pixelWidth, 
			0.5f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rectHistoria, modeHistoria);
		
		Rect rectQuick = new Rect(0.28f*Camera.mainCamera.pixelWidth, 
			0.3f*Camera.mainCamera.pixelHeight, 
			0.2f*Camera.mainCamera.pixelWidth, 
			0.5f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rectQuick, modeQuick);
		
		Rect rectEdicio = new Rect(0.52f*Camera.mainCamera.pixelWidth, 
			0.3f*Camera.mainCamera.pixelHeight, 
			0.2f*Camera.mainCamera.pixelWidth, 
			0.5f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rectEdicio, modeEdicio);
		
		Rect rectEstadistiques = new Rect(0.76f*Camera.mainCamera.pixelWidth, 
			0.3f*Camera.mainCamera.pixelHeight, 
			0.2f*Camera.mainCamera.pixelWidth, 
			0.5f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rectEstadistiques, modeEstadistiques);
		
		descripcioPantalla.guiText.fontSize = fontSize;
		descripcioPantalla.transform.position = new Vector3(0.25f, 0.85f, 1);
		
		if(detectaClick(rectBack)){
			// Click al botó d'enrere
			Debug.Log("Click al botó d'enrere");
			ControlGeneralMenuPrincipal cG = (ControlGeneralMenuPrincipal) Camera.mainCamera.GetComponent("ControlGeneralMenuPrincipal");
			Destroy (cG);
			
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			
			//Destroy(titolPantalla);
			//Destroy(botoBack);
			//Destroy(howToPlay);
			Destroy(descripcioPantalla);
			
			Destroy(this);
			
			ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
			cJ.carregarPantallaPerfils();
			MenuPrincipal s = (MenuPrincipal) Camera.mainCamera.GetComponent("MenuPrincipal");
			Destroy(s);
			Destroy(conMenu);
			
			Destroy(this);
		}else if(detectaClick(rectHowTo)){
			Debug.Log("Click al boto de How To");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			carregarModeHowTo();
		}else if(detectaClick(rectHistoria)){
			Debug.Log("Click al boto de mode Historia");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			carregarModeHistoria();
		}else if(detectaClick(rectQuick)){
			Debug.Log("Click al boto de mode Quick");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			carregarModeQuick();
		}else if(detectaClick(rectEdicio)){
			Debug.Log("Click al boto de mode Edicio");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			carregarModeEdicio();
		}else if(detectaClick(rectEstadistiques)){
			Debug.Log("Click al boto de mode Estadistiques");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			carregarModeEstadistiques();
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
	
	private void carregarModeEdicio(){
		
		ControlGeneralMenuPrincipal cM = (ControlGeneralMenuPrincipal) Camera.mainCamera.GetComponent("ControlGeneralMenuPrincipal");
		Destroy (cM);

		Destroy(descripcioPantalla);
		
		ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
		cJ.carregarPantallaEdicio();
		
		Destroy(this);
	}
	
	private void carregarModeQuick(){
		ControlGeneralMenuPrincipal cM = (ControlGeneralMenuPrincipal) Camera.mainCamera.GetComponent("ControlGeneralMenuPrincipal");
		Destroy (cM);

		Destroy(descripcioPantalla);
		
		ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
		cJ.carregarPantallaMenuQuick();
		
		Destroy(this);
	}
	
	private void carregarModeHistoria(){
		ControlGeneralMenuPrincipal cM = (ControlGeneralMenuPrincipal) Camera.mainCamera.GetComponent("ControlGeneralMenuPrincipal");
		Destroy (cM);

		Destroy(descripcioPantalla);
		
		ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
		cJ.carregarPantallaMenuHistoria();
		
		Destroy(this);
	}
	
	private void carregarModeEstadistiques(){
		ControlGeneralMenuPrincipal cM = (ControlGeneralMenuPrincipal) Camera.mainCamera.GetComponent("ControlGeneralMenuPrincipal");
		Destroy (cM);

		Destroy(descripcioPantalla);
		
		ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
		cJ.carregarPantallaMenuEstadistiques();
		
		Destroy(this);
	}
	
	private void carregarModeHowTo(){
		ControlGeneralMenuPrincipal cM = (ControlGeneralMenuPrincipal) Camera.mainCamera.GetComponent("ControlGeneralMenuPrincipal");
		Destroy (cM);

		Destroy(descripcioPantalla);
		
		ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
		cJ.carregarPantallaHowTo();
		
		Destroy(this);
	}
}
