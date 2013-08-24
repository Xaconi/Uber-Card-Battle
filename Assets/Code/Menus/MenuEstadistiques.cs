using UnityEngine;
using System.Collections;

public class MenuEstadistiques : MonoBehaviour {
	
	public Texture2D titolPantalla;
	public Texture2D botoBack;
	
	public GameObject quadreEstadistiquesHistoria;
	public GameObject quadreEstadistiquesQuick;
	public GameObject quadreEstadistiquesGeneral;
	
	public GameObject titolHistoria;
	public GameObject titolQuick;
	public GameObject titolNormal;
	
	public GameObject victoriesHistoria;
	public GameObject derrotesHistoria;
	public GameObject partidesHistoria;
	public GameObject tempsHistoria;
	
	public GameObject victoriesQuick;
	public GameObject derrotesQuick;
	public GameObject partidesQuick;
	public GameObject tempsQuick;
	
	public GameObject victoriesGeneral;
	public GameObject derrotesGeneral;
	public GameObject partidesGeneral;
	public GameObject tempsGeneral;
	public GameObject atacFetGeneral;
	public GameObject atacRebutGeneral;
	public GameObject cartesTotalsGeneral;
	public GameObject cartesUtilitzadesGeneral;
	
	public Texture2D botoEsborrarDades;
	
	public Texture2D menuPausa;
	public Texture2D botoAbandona;
	public Texture2D botoContinua;
	
	public int fontSize;
	public int fontSize2;
	public bool esborrarDades;
	
	void Awake(){
		int fontSize = (int) Mathf.Ceil(20.0f * (Camera.mainCamera.pixelWidth/568.0f));
		
		titolPantalla = Resources.Load("menu_estadistiques") as Texture2D;
		botoBack = Resources.Load("boto_enrere") as Texture2D;
		
		menuPausa = Resources.Load("menu_confirm_abandona") as Texture2D;
		botoAbandona = Resources.Load("menu_confirm_si") as Texture2D;
		botoContinua = Resources.Load("menu_confirm_no") as Texture2D;
		
		botoEsborrarDades = Resources.Load("boto_esborrar_dades") as Texture2D;
		
		esborrarDades = false;
	}

	// Use this for initialization
	void Start () {
		int fontSize = (int) Mathf.Ceil(15.0f * (Camera.mainCamera.pixelWidth/523.0f));
		int fontSize2 = (int) Mathf.Ceil(15.0f * (Camera.mainCamera.pixelWidth/1080.0f));
		ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
		
		quadreEstadistiquesHistoria = new GameObject("quadreEstadistiquesHistoria");
		quadreEstadistiquesHistoria.AddComponent("GUITexture");
		quadreEstadistiquesHistoria.guiTexture.texture = Resources.Load("BlackboardEdicio") as Texture;
		
		quadreEstadistiquesQuick = new GameObject("quadreEstadistiquesQuick");
		quadreEstadistiquesQuick.AddComponent("GUITexture");
		quadreEstadistiquesQuick.guiTexture.texture = Resources.Load("BlackboardEdicio") as Texture;
		
		quadreEstadistiquesGeneral = new GameObject("quadreEstadistiquesGeneral");
		quadreEstadistiquesGeneral.AddComponent("GUITexture");
		quadreEstadistiquesGeneral.guiTexture.texture = Resources.Load("BlackboardEdicio") as Texture;
		
		titolHistoria = new GameObject("TitolHistoria");
		titolHistoria.AddComponent("GUIText");
		titolHistoria.guiText.font = Resources.Load("ERASERDUST") as Font;
		titolHistoria.guiText.fontSize = fontSize;
		titolHistoria.guiText.text = "Mode Historia";
		
		titolQuick = new GameObject("TitolQuick");
		titolQuick.AddComponent("GUIText");
		titolQuick.guiText.font = Resources.Load("ERASERDUST") as Font;
		titolQuick.guiText.fontSize = fontSize;
		titolQuick.guiText.text = "Mode Partda Rapida";
		
		titolNormal = new GameObject("TitolNormal");
		titolNormal.AddComponent("GUIText");
		titolNormal.guiText.font = Resources.Load("ERASERDUST") as Font;
		titolNormal.guiText.fontSize = fontSize;
		titolNormal.guiText.text = "Dades Generals";
		
		victoriesHistoria = new GameObject("victoriesHistoria");
		victoriesHistoria.AddComponent("GUIText");
		victoriesHistoria.guiText.font = Resources.Load("ERASERDUST") as Font;
		victoriesHistoria.guiText.fontSize = fontSize;
		victoriesHistoria.guiText.text = "- Victories: " + conMenu.p.llistaPlayers[conMenu.jugador].e.vH;
		
		derrotesHistoria = new GameObject("derrotesHistoria");
		derrotesHistoria.AddComponent("GUIText");
		derrotesHistoria.guiText.font = Resources.Load("ERASERDUST") as Font;
		derrotesHistoria.guiText.fontSize = fontSize;
		derrotesHistoria.guiText.text = "- Derrotes: " + conMenu.p.llistaPlayers[conMenu.jugador].e.dH;
		
		partidesHistoria = new GameObject("partidesHistoria");
		partidesHistoria.AddComponent("GUIText");
		partidesHistoria.guiText.font = Resources.Load("ERASERDUST") as Font;
		partidesHistoria.guiText.fontSize = fontSize;
		partidesHistoria.guiText.text = "- Partides: " + (conMenu.p.llistaPlayers[conMenu.jugador].e.vH
			+ conMenu.p.llistaPlayers[conMenu.jugador].e.dH);
		
		tempsHistoria = new GameObject("tempsHistoria");
		tempsHistoria.AddComponent("GUIText");
		tempsHistoria.guiText.font = Resources.Load("ERASERDUST") as Font;
		tempsHistoria.guiText.fontSize = fontSize;
		tempsHistoria.guiText.text = "- Temps Jugat: " + conMenu.p.llistaPlayers[conMenu.jugador].e.tempsHistoria.hores + "H "
			+ conMenu.p.llistaPlayers[conMenu.jugador].e.tempsHistoria.minuts + "M " 
			+ conMenu.p.llistaPlayers[conMenu.jugador].e.tempsHistoria.segons + "S" ;
		
		victoriesQuick = new GameObject("victoriesQuick");
		victoriesQuick.AddComponent("GUIText");
		victoriesQuick.guiText.font = Resources.Load("ERASERDUST") as Font;
		victoriesQuick.guiText.fontSize = fontSize;
		victoriesQuick.guiText.text = "- Victories: " + conMenu.p.llistaPlayers[conMenu.jugador].e.vQ;
		
		derrotesQuick = new GameObject("derrotesQuick");
		derrotesQuick.AddComponent("GUIText");
		derrotesQuick.guiText.font = Resources.Load("ERASERDUST") as Font;
		derrotesQuick.guiText.fontSize = fontSize;
		derrotesQuick.guiText.text = "- Derrotes: " + conMenu.p.llistaPlayers[conMenu.jugador].e.dQ;
		
		partidesQuick = new GameObject("partidesQuick");
		partidesQuick.AddComponent("GUIText");
		partidesQuick.guiText.font = Resources.Load("ERASERDUST") as Font;
		partidesQuick.guiText.fontSize = fontSize;
		partidesQuick.guiText.text = "- Partides: " + (conMenu.p.llistaPlayers[conMenu.jugador].e.vQ
			+ conMenu.p.llistaPlayers[conMenu.jugador].e.dQ);
		
		tempsQuick = new GameObject("tempsQuick");
		tempsQuick.AddComponent("GUIText");
		tempsQuick.guiText.font = Resources.Load("ERASERDUST") as Font;
		tempsQuick.guiText.fontSize = fontSize;
		tempsQuick.guiText.text = "- Temps Jugat: " + conMenu.p.llistaPlayers[conMenu.jugador].e.tempsQuick.hores + "H "
			+ conMenu.p.llistaPlayers[conMenu.jugador].e.tempsQuick.minuts + "M " 
			+ conMenu.p.llistaPlayers[conMenu.jugador].e.tempsQuick.segons + "S" ;
		
		victoriesGeneral = new GameObject("victoriesGeneral");
		victoriesGeneral.AddComponent("GUIText");
		victoriesGeneral.guiText.font = Resources.Load("ERASERDUST") as Font;
		victoriesGeneral.guiText.fontSize = fontSize;
		victoriesGeneral.guiText.text = "- Victories: " + (conMenu.p.llistaPlayers[conMenu.jugador].e.vQ + conMenu.p.llistaPlayers[conMenu.jugador].e.vH);
		
		derrotesGeneral = new GameObject("derrotesGeneral");
		derrotesGeneral.AddComponent("GUIText");
		derrotesGeneral.guiText.font = Resources.Load("ERASERDUST") as Font;
		derrotesGeneral.guiText.fontSize = fontSize;
		derrotesGeneral.guiText.text = "- Derrotes: " + (conMenu.p.llistaPlayers[conMenu.jugador].e.dQ + conMenu.p.llistaPlayers[conMenu.jugador].e.dH);
		
		partidesGeneral = new GameObject("partidesGeneral");
		partidesGeneral.AddComponent("GUIText");
		partidesGeneral.guiText.font = Resources.Load("ERASERDUST") as Font;
		partidesGeneral.guiText.fontSize = fontSize;
		partidesGeneral.guiText.text = "- Partides: " + (conMenu.p.llistaPlayers[conMenu.jugador].e.vQ
			+ conMenu.p.llistaPlayers[conMenu.jugador].e.dQ + conMenu.p.llistaPlayers[conMenu.jugador].e.vH
			+ conMenu.p.llistaPlayers[conMenu.jugador].e.dH);
		
		tempsGeneral = new GameObject("tempsGeneral");
		tempsGeneral.AddComponent("GUIText");
		tempsGeneral.guiText.font = Resources.Load("ERASERDUST") as Font;
		tempsGeneral.guiText.fontSize = fontSize;
		tempsGeneral.guiText.text = "- Temps Jugat: " + conMenu.p.llistaPlayers[conMenu.jugador].e.tempsQuick.hores + "H "
			+ conMenu.p.llistaPlayers[conMenu.jugador].e.tempsQuick.minuts + "M " 
			+ conMenu.p.llistaPlayers[conMenu.jugador].e.tempsQuick.segons + "S" ;
		
		atacFetGeneral = new GameObject("atacFetGeneral");
		atacFetGeneral.AddComponent("GUIText");
		atacFetGeneral.guiText.font = Resources.Load("ERASERDUST") as Font;
		atacFetGeneral.guiText.fontSize = fontSize;
		atacFetGeneral.guiText.text = "- Atac Fet: " + conMenu.p.llistaPlayers[conMenu.jugador].e.atacFet;
		
		atacRebutGeneral = new GameObject("atacRebutGeneral");
		atacRebutGeneral.AddComponent("GUIText");
		atacRebutGeneral.guiText.font = Resources.Load("ERASERDUST") as Font;
		atacRebutGeneral.guiText.fontSize = fontSize;
		atacRebutGeneral.guiText.text = "- Atac Rebut: " + conMenu.p.llistaPlayers[conMenu.jugador].e.atacRebut;
		
		cartesTotalsGeneral = new GameObject("cartesTotalsGeneral");
		cartesTotalsGeneral.AddComponent("GUIText");
		cartesTotalsGeneral.guiText.font = Resources.Load("ERASERDUST") as Font;
		cartesTotalsGeneral.guiText.fontSize = fontSize;
		cartesTotalsGeneral.guiText.text = "- Cartes Aconseguides: " + 
			(conMenu.p.llistaPlayers[conMenu.jugador].baralla.bA.llistaCartesBaralla.Count 
			+ conMenu.p.llistaPlayers[conMenu.jugador].baralla.c.llistaCartesBaralla.Count);
		
		cartesUtilitzadesGeneral = new GameObject("cartesUtilitzadesGeneral");
		cartesUtilitzadesGeneral.AddComponent("GUIText");
		cartesUtilitzadesGeneral.guiText.font = Resources.Load("ERASERDUST") as Font;
		cartesUtilitzadesGeneral.guiText.fontSize = fontSize;
		cartesUtilitzadesGeneral.guiText.text = "- Cartes Utilitzades: " + conMenu.p.llistaPlayers[conMenu.jugador].e.cartesUtilitzades;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		fontSize = (int) Mathf.Ceil(20.0f * (Camera.mainCamera.pixelWidth/568.0f));
		fontSize2 = (int) Mathf.Ceil(25.0f * (Camera.mainCamera.pixelWidth/1080.0f));
		
		Rect eDades = new Rect(0.65f*Camera.mainCamera.pixelWidth, 
			0.78f*Camera.mainCamera.pixelHeight, 
			0.2f*Camera.mainCamera.pixelWidth, 
			0.05f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(eDades, botoEsborrarDades);
		
		if(esborrarDades){
			Rect rectPausa = new Rect(0.2f*Camera.mainCamera.pixelWidth, 
				0.2f*Camera.mainCamera.pixelHeight, 
				0.6f*Camera.mainCamera.pixelWidth, 
				0.6f*Camera.mainCamera.pixelHeight);
			GUI.DrawTexture(rectPausa, menuPausa);
			
			Rect rectContinua = new Rect(0.3f*Camera.mainCamera.pixelWidth, 
				0.47f*Camera.mainCamera.pixelHeight, 
				0.4f*Camera.mainCamera.pixelWidth, 
				0.1f*Camera.mainCamera.pixelHeight);
			GUI.DrawTexture(rectContinua, botoContinua);
			
			Rect rectAbandona = new Rect(0.3f*Camera.mainCamera.pixelWidth, 
				0.62f*Camera.mainCamera.pixelHeight, 
				0.4f*Camera.mainCamera.pixelWidth, 
				0.1f*Camera.mainCamera.pixelHeight);
			GUI.DrawTexture(rectAbandona, botoAbandona);
			
			if(detectaClick(rectContinua)){
				esborrarDades = false;
				AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
				a.playClick();
			}else if(detectaClick(rectAbandona)){
				AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
				a.playClick();
				esborrarDadesUsuari();
			}
		}
		
		quadreEstadistiquesHistoria.guiTexture.transform.localScale = new Vector3(0.45f, 0.4f, 1);
		quadreEstadistiquesHistoria.guiTexture.transform.position = new Vector3(0.25f, 0.68f, 1);
		
		quadreEstadistiquesQuick.guiTexture.transform.localScale = new Vector3(0.45f, 0.4f, 1);
		quadreEstadistiquesQuick.guiTexture.transform.position = new Vector3(0.75f, 0.68f, 1);
		
		quadreEstadistiquesGeneral.guiTexture.transform.localScale = new Vector3(0.95f, 0.35f, 1);
		quadreEstadistiquesGeneral.guiTexture.transform.position = new Vector3(0.5f, 0.29f, 1);
		
		titolHistoria.guiText.fontSize = fontSize;
		titolHistoria.transform.position = new Vector3(0.13f, 0.85f, 2);
		
		titolQuick.guiText.fontSize = fontSize;
		titolQuick.transform.position = new Vector3(0.58f, 0.85f, 2);
		
		titolNormal.guiText.fontSize = fontSize;
		titolNormal.transform.position = new Vector3(0.37f, 0.43f, 2);
		
		victoriesHistoria.guiText.fontSize = fontSize - 5;
		victoriesHistoria.transform.position = new Vector3(0.08f, 0.78f, 2);
		
		derrotesHistoria.guiText.fontSize = fontSize - 5;
		derrotesHistoria.transform.position = new Vector3(0.08f, 0.71f, 2);
		
		partidesHistoria.guiText.fontSize = fontSize - 5;
		partidesHistoria.transform.position = new Vector3(0.08f, 0.64f, 2);
		
		tempsHistoria.guiText.fontSize = fontSize - 5;
		tempsHistoria.transform.position = new Vector3(0.08f, 0.57f, 2);
		
		victoriesQuick.guiText.fontSize = fontSize - 5;
		victoriesQuick.transform.position = new Vector3(0.58f, 0.78f, 2);
		
		derrotesQuick.guiText.fontSize = fontSize - 5;
		derrotesQuick.transform.position = new Vector3(0.58f, 0.71f, 2);
		
		partidesQuick.guiText.fontSize = fontSize - 5;
		partidesQuick.transform.position = new Vector3(0.58f, 0.64f, 2);
		
		tempsQuick.guiText.fontSize = fontSize - 5;
		tempsQuick.transform.position = new Vector3(0.58f, 0.57f, 2);
		
		victoriesGeneral.guiText.fontSize = fontSize2;
		victoriesGeneral.transform.position = new Vector3(0.08f, 0.36f, 2);
		
		derrotesGeneral.guiText.fontSize = fontSize2;
		derrotesGeneral.transform.position = new Vector3(0.08f, 0.29f, 2);
		
		partidesGeneral.guiText.fontSize = fontSize2;
		partidesGeneral.transform.position = new Vector3(0.08f, 0.22f, 2);
		
		tempsGeneral.guiText.fontSize = fontSize2;
		tempsGeneral.transform.position = new Vector3(0.3f, 0.36f, 2);
		
		atacFetGeneral.guiText.fontSize = fontSize2;
		atacFetGeneral.transform.position = new Vector3(0.3f, 0.29f, 2);
		
		atacRebutGeneral.guiText.fontSize = fontSize2;
		atacRebutGeneral.transform.position = new Vector3(0.3f, 0.22f, 2);
		
		cartesTotalsGeneral.guiText.fontSize = fontSize2;
		cartesTotalsGeneral.transform.position = new Vector3(0.62f, 0.36f, 2);
		
		cartesUtilitzadesGeneral.guiText.fontSize = fontSize2;
		cartesUtilitzadesGeneral.transform.position = new Vector3(0.62f, 0.29f, 2);
		
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
			ControlGeneralMenuEstadistiques cG = (ControlGeneralMenuEstadistiques) Camera.mainCamera.GetComponent("ControlGeneralMenuEstadistiques");
			Destroy (cG);
			
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			
			Destroy(quadreEstadistiquesHistoria);
			Destroy(quadreEstadistiquesQuick);
			Destroy(quadreEstadistiquesGeneral);
			
			Destroy(titolHistoria);
			Destroy(titolQuick);
			Destroy(titolNormal);
			
			Destroy(victoriesHistoria);
			Destroy(derrotesHistoria);
			Destroy(partidesHistoria);
			Destroy(tempsHistoria);
			
			Destroy(victoriesQuick);
			Destroy(derrotesQuick);
			Destroy(partidesQuick);
			Destroy(tempsQuick);
			
			Destroy(victoriesGeneral);
			Destroy(derrotesGeneral);
			Destroy(partidesGeneral);
			Destroy(tempsGeneral);
			Destroy(atacFetGeneral);
			Destroy(atacRebutGeneral);
			Destroy(cartesTotalsGeneral);
			Destroy(cartesUtilitzadesGeneral);
			
			ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
			cJ.carregarPantallaMenuPrincipal();
			
			Destroy(this);
		}else if(detectaClick(eDades)){
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			esborrarDades = true;
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
	
	private void esborrarDadesUsuari(){
		ControlGeneralMenuEstadistiques cG = (ControlGeneralMenuEstadistiques) Camera.mainCamera.GetComponent("ControlGeneralMenuEstadistiques");
		Destroy (cG);
		
		GameObject b = (GameObject)GameObject.FindGameObjectWithTag("Background");
		Destroy (b);
		
		Destroy(quadreEstadistiquesHistoria);
		Destroy(quadreEstadistiquesQuick);
		Destroy(quadreEstadistiquesGeneral);
		
		Destroy(titolHistoria);
		Destroy(titolQuick);
		Destroy(titolNormal);
		
		Destroy(victoriesHistoria);
		Destroy(derrotesHistoria);
		Destroy(partidesHistoria);
		Destroy(tempsHistoria);
		
		Destroy(victoriesQuick);
		Destroy(derrotesQuick);
		Destroy(partidesQuick);
		Destroy(tempsQuick);
		
		Destroy(victoriesGeneral);
		Destroy(derrotesGeneral);
		Destroy(partidesGeneral);
		Destroy(tempsGeneral);
		Destroy(atacFetGeneral);
		Destroy(atacRebutGeneral);
		Destroy(cartesTotalsGeneral);
		Destroy(cartesUtilitzadesGeneral);
		
		ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
		conMenu.esborrarJugador();
		Destroy(conMenu);
		
		ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
		cJ.carregarPantallaTitol();
		
		Destroy(this);
	}
}
