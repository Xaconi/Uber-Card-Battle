using UnityEngine;
using System.Collections;

public class MenuQuick : MonoBehaviour {
	
	public Texture2D titolPantalla;
	public Texture2D botoBack;
	public Texture2D botoIniciar;
	
	public GameObject descripcioPantalla;
	
	public GameObject opcio1;
	public GameObject opcio2;
	public GameObject opcio3;
	
	public int fontSize;
	
	public string[] dificultat;
	public string[] dificultatCarta;
	public string[] escenaris;
	
    public Rect Box;
	public Rect comboCarta;
	public Rect comboEscenari;
	
    public string selectedItem = "None";
	public string selectedCarta = "Bronze";
	public string selectedEscenari = "Bosc";
	private bool editing = false;
	private bool editingCarta = false;
	private bool editingEscenari = false;
	
	void Awake(){
		int fontSize = (int) Mathf.Ceil(20.0f * (Camera.mainCamera.pixelWidth/568.0f));
		
		titolPantalla = Resources.Load("partida_rapida") as Texture2D;
		botoBack = Resources.Load("boto_enrere") as Texture2D;
		botoIniciar = Resources.Load("boto_iniciar_partida") as Texture2D;
		
		descripcioPantalla = new GameObject("TextPerfil");
		descripcioPantalla.AddComponent("GUIText");
		descripcioPantalla.guiText.font = Resources.Load("ERASERDUST") as Font;
		descripcioPantalla.guiText.fontSize = fontSize;
		descripcioPantalla.guiText.alignment = TextAlignment.Center;
		descripcioPantalla.guiText.material.color = Color.black;
		
		opcio1 = new GameObject("TextPerfil");
		opcio1.AddComponent("GUIText");
		opcio1.guiText.font = Resources.Load("ERASERDUST") as Font;
		opcio1.guiText.fontSize = fontSize;
		opcio1.guiText.alignment = TextAlignment.Center;
		opcio1.guiText.material.color = Color.black;
		
		opcio2 = new GameObject("TextPerfil");
		opcio2.AddComponent("GUIText");
		opcio2.guiText.font = Resources.Load("ERASERDUST") as Font;
		opcio2.guiText.fontSize = fontSize;
		opcio2.guiText.alignment = TextAlignment.Center;
		opcio2.guiText.material.color = Color.black;
		
		opcio3 = new GameObject("TextPerfil");
		opcio3.AddComponent("GUIText");
		opcio3.guiText.font = Resources.Load("ERASERDUST") as Font;
		opcio3.guiText.fontSize = fontSize;
		opcio3.guiText.alignment = TextAlignment.Center;
		opcio3.guiText.material.color = Color.black;
		
		dificultat = new string[3];
		dificultat[0] = "FACIL";
		dificultat[1] = "MITJA";
		dificultat[2] = "DIFICL";
		
		dificultatCarta = new string[4];
		dificultatCarta[0] = "Bronze";
		dificultatCarta[1] = "Plata";
		dificultatCarta[2] = "Or";
		dificultatCarta[3] = "Plati";
		
		escenaris = new string[3];
		escenaris[0] = "Bosc";
		escenaris[1] = "Winter";
		escenaris[2] = "Cave";
	}
	
	// Use this for initialization
	void Start () {
		opcio1.guiText.text = "Dificultat:";
		opcio2.guiText.text = "Nivell de cartes:";
		opcio3.guiText.text = "Escenari:";
		selectedItem = dificultat[0];
		selectedCarta = dificultatCarta[0];
		selectedEscenari = escenaris[0];
		
		descripcioPantalla.guiText.text = "Configura la teva partida";
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
		
		Rect rectInici = new Rect(0.55f*Camera.mainCamera.pixelWidth, 
			0.9f*Camera.mainCamera.pixelHeight, 
			0.35f*Camera.mainCamera.pixelWidth, 
			0.1f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rectInici, botoIniciar);
		
		descripcioPantalla.guiText.fontSize = fontSize;
		descripcioPantalla.transform.position = new Vector3(0.27f, 0.85f, 1);
		
		opcio1.guiText.fontSize = fontSize;
		opcio1.transform.position = new Vector3(0.25f, 0.65f, 1);
		
		opcio2.guiText.fontSize = fontSize;
		opcio2.transform.position = new Vector3(0.145f, 0.50f, 1);
		
		opcio3.guiText.fontSize = fontSize;
		opcio3.transform.position = new Vector3(0.3f, 0.35f, 1);
		
		GUIStyle myStyle = new GUIStyle(GUI.skin.button);
		myStyle.font = Resources.Load("ERASERDUST") as Font;
		myStyle.fontSize = fontSize;
		
		float ratio = Camera.mainCamera.aspect;
		float ratioOriginal = 16.0f/9.0f;
		float diferenciaRatio = ratio / ratioOriginal;
		
		if(diferenciaRatio > 0.95f){
		
			Box = new Rect(260 * (Camera.mainCamera.pixelWidth/512.0f),
				100 * (Camera.mainCamera.pixelWidth/512.0f),
				100 * (Camera.mainCamera.pixelWidth/512.0f),
				20 * (Camera.mainCamera.pixelWidth/512.0f));
			
			comboCarta = new Rect(260 * (Camera.mainCamera.pixelWidth/512.0f),
				144 * (Camera.mainCamera.pixelWidth/512.0f),
				100 * (Camera.mainCamera.pixelWidth/512.0f),
				20 * (Camera.mainCamera.pixelWidth/512.0f));
			
			comboEscenari = new Rect(260 * (Camera.mainCamera.pixelWidth/512.0f),
				188 * (Camera.mainCamera.pixelWidth/512.0f),
				100 * (Camera.mainCamera.pixelWidth/512.0f),
				20 * (Camera.mainCamera.pixelWidth/512.0f));
		}else if(diferenciaRatio > 0.85f){
			Box = new Rect(260 * (Camera.mainCamera.pixelWidth/512.0f),
				111 * (Camera.mainCamera.pixelWidth/512.0f),
				100 * (Camera.mainCamera.pixelWidth/512.0f),
				20 * (Camera.mainCamera.pixelWidth/512.0f));
			
			comboCarta = new Rect(260 * (Camera.mainCamera.pixelWidth/512.0f),
				161 * (Camera.mainCamera.pixelWidth/512.0f),
				100 * (Camera.mainCamera.pixelWidth/512.0f),
				20 * (Camera.mainCamera.pixelWidth/512.0f));
			
			comboEscenari = new Rect(260 * (Camera.mainCamera.pixelWidth/512.0f),
				208 * (Camera.mainCamera.pixelWidth/512.0f),
				100 * (Camera.mainCamera.pixelWidth/512.0f),
				20 * (Camera.mainCamera.pixelWidth/512.0f));
		}
		
	  	if (GUI.Button(Box, selectedItem, myStyle)){
        	editing = true;
    	}
			
		if (!editingCarta){
		  	if (GUI.Button(Box, selectedItem, myStyle)){
	        	editing = true;
	    	}
		}
		
		if (!editingCarta){
		  	if (GUI.Button(comboEscenari, selectedEscenari, myStyle)){
	        	editingEscenari = true;
	    	}
		}
			
		if (!editing){
			if (GUI.Button(comboCarta, selectedCarta, myStyle)){
	        	editingCarta = true;
	    	}
		}
		
    	if (editing){
        	for (int x = 0; x < dificultat.Length; x++){
                if (GUI.Button(new Rect(Box.x, (Box.height * x) + Box.y + Box.height, Box.width, Box.height), dificultat[x], myStyle)){
                    selectedItem = dificultat[x];
                    editing = false;
                }
            }
    	}
		
		if (editingCarta){
        	for (int x = 0; x < dificultatCarta.Length; x++){
                if (GUI.Button(new Rect(comboCarta.x, (comboCarta.height * x) + comboCarta.y + comboCarta.height, comboCarta.width, comboCarta.height), dificultatCarta[x], myStyle)){
                    selectedCarta = dificultatCarta[x];
                    editingCarta = false;
                }
            }
    	}
		
		if (editingEscenari){
        	for (int x = 0; x < escenaris.Length; x++){
                if (GUI.Button(new Rect(comboEscenari.x, (comboEscenari.height * x) + comboEscenari.y + comboEscenari.height, comboEscenari.width, comboEscenari.height), escenaris[x], myStyle)){
                    selectedEscenari = escenaris[x];
                    editingEscenari = false;
                }
            }
    	}
		
		if(detectaClick(rectBack)){
			// Click al botó d'enrere
			Debug.Log("Click al botó d'enrere");
			ControlGeneralMenuPrincipal cG = (ControlGeneralMenuPrincipal) Camera.mainCamera.GetComponent("ControlGeneralMenuPrincipal");
			Destroy (cG);
			
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			
			Destroy(descripcioPantalla);
			Destroy(opcio1);
			Destroy(opcio2);
			Destroy(opcio3);
			
			ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
			cJ.carregarPantallaMenuPrincipal();
			ControlGeneralMenuQuick s = (ControlGeneralMenuQuick) Camera.mainCamera.GetComponent("ControlGeneralMenuQuick");
			Destroy(s);
			
			Destroy(this);
		}else if(detectaClick(rectInici)){
			// Click al botó d'enrere
			Debug.Log("Click al botó d'inici de partida");
			ControlGeneralMenuPrincipal cG = (ControlGeneralMenuPrincipal) Camera.mainCamera.GetComponent("ControlGeneralMenuPrincipal");
			Destroy (cG);
			
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			
			ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
			cJ.assignarDadesPartidaRapida(selectedItem, selectedEscenari, selectedCarta);
			cJ.carregarPantallaBatalla();
			
			Destroy(descripcioPantalla);
			Destroy(opcio1);
			Destroy(opcio2);
			Destroy(opcio3);
			
			ControlGeneralMenuQuick s = (ControlGeneralMenuQuick) Camera.mainCamera.GetComponent("ControlGeneralMenuQuick");
			Destroy(s);
			
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
}
