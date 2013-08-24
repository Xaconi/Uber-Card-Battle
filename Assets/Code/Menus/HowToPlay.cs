using UnityEngine;
using System.Collections;

public class HowToPlay : MonoBehaviour {
	
	public Texture2D titolPantalla;
	public Texture2D botoBack;
	
	public GameObject quadreExplicacioGeneral;
	public GameObject quadreExplicacioSigles;
	public GameObject quadreExplicacioVictoria;
	
	public GameObject titolGeneral;
	public GameObject titolSigles;
	public GameObject titolVictoria;
	
	public GameObject explicacioGeneral1;
	public GameObject explicacioGeneral2;
	public GameObject explicacioGeneral3;
	public GameObject explicacioGeneral4;
	public GameObject explicacioGeneral5;
	
	public GameObject explicacioSigles1;
	public GameObject explicacioSigles2;
	public GameObject explicacioSigles3;
	public GameObject explicacioSigles4;
	public GameObject explicacioSigles5;
	
	public GameObject explicacioVictoria1;
	public GameObject explicacioVictoria2;
	
	public int fontSize;
	public int fontSize2;
	
	void Awake(){
		titolPantalla = Resources.Load("boto_comjugar") as Texture2D;
		botoBack = Resources.Load("boto_enrere") as Texture2D;
	}

	// Use this for initialization
	void Start () {
		int fontSize = (int) Mathf.Ceil(20.0f * (Camera.mainCamera.pixelWidth/568.0f));
	
		quadreExplicacioGeneral = new GameObject("quadreExplicacioGeneral");
		quadreExplicacioGeneral.AddComponent("GUITexture");
		quadreExplicacioGeneral.guiTexture.texture = Resources.Load("BlackboardEdicio") as Texture;
		
		quadreExplicacioSigles = new GameObject("quadreExplicacioSigles");
		quadreExplicacioSigles.AddComponent("GUITexture");
		quadreExplicacioSigles.guiTexture.texture = Resources.Load("BlackboardEdicio") as Texture;
		
		quadreExplicacioVictoria = new GameObject("quadreExplicacioVictoria");
		quadreExplicacioVictoria.AddComponent("GUITexture");
		quadreExplicacioVictoria.guiTexture.texture = Resources.Load("BlackboardEdicio") as Texture;
		
		titolGeneral = new GameObject("TextPerfil");
		titolGeneral.AddComponent("GUIText");
		titolGeneral.guiText.font = Resources.Load("ERASERDUST") as Font;
		titolGeneral.guiText.fontSize = fontSize;
		titolGeneral.guiText.text = "Instruccions Generals";
		
		titolSigles = new GameObject("TextPerfil");
		titolSigles.AddComponent("GUIText");
		titolSigles.guiText.font = Resources.Load("ERASERDUST") as Font;
		titolSigles.guiText.fontSize = fontSize;
		titolSigles.guiText.text = "Sigles";
		
		titolVictoria = new GameObject("TextPerfil");
		titolVictoria.AddComponent("GUIText");
		titolVictoria.guiText.font = Resources.Load("ERASERDUST") as Font;
		titolVictoria.guiText.fontSize = fontSize;
		titolVictoria.guiText.text = "Formes de Guanyar";
		
		explicacioGeneral1 = new GameObject("TextPerfil");
		explicacioGeneral1.AddComponent("GUIText");
		explicacioGeneral1.guiText.font = Resources.Load("ERASERDUST") as Font;
		explicacioGeneral1.guiText.fontSize = fontSize;
		explicacioGeneral1.guiText.text = "- Uber Card Battle es un joc d'estrategia de\ntauler amb cartes intercanviables";
		
		explicacioGeneral2 = new GameObject("TextPerfil");
		explicacioGeneral2.AddComponent("GUIText");
		explicacioGeneral2.guiText.font = Resources.Load("ERASERDUST") as Font;
		explicacioGeneral2.guiText.fontSize = fontSize;
		explicacioGeneral2.guiText.text = "- L'objectiu es derrotar al teu rival, per po-\nder aixi guanyar la partida";
		
		explicacioGeneral3 = new GameObject("TextPerfil");
		explicacioGeneral3.AddComponent("GUIText");
		explicacioGeneral3.guiText.font = Resources.Load("ERASERDUST") as Font;
		explicacioGeneral3.guiText.fontSize = fontSize;
		explicacioGeneral3.guiText.text = "- Per poder guanyar, has d'encadenar accions\ndels teus personatges, moure'ls pel tauler,\natacar als personatges rivals, atacar la ba-\nse rival, plantejar estrategies defensives o\nofensives, i definir el teu estil de joc";
	
		explicacioGeneral4 = new GameObject("TextPerfil");
		explicacioGeneral4.AddComponent("GUIText");
		explicacioGeneral4.guiText.font = Resources.Load("ERASERDUST") as Font;
		explicacioGeneral4.guiText.fontSize = fontSize;
		explicacioGeneral4.guiText.text = "- Podras personalitzar la teva baralla de\ncartes amb les cartes noves que vagis a-\nconseguint. Per cada victoria, obtindras 5\ncartes noves per utilitzar";
		
		explicacioGeneral5 = new GameObject("TextPerfil");
		explicacioGeneral5.AddComponent("GUIText");
		explicacioGeneral5.guiText.font = Resources.Load("ERASERDUST") as Font;
		explicacioGeneral5.guiText.fontSize = fontSize;
		explicacioGeneral5.guiText.text = "- La baralla, consisteix en 15 personatges i\n6 bonificacions, de diferents tipus i nivells.\nQuan aconsegueixis millors cartes, les\npodràs canviar per altres de la teva ba-\nralla, itenir així una baralla mes forta";

		explicacioSigles1 = new GameObject("TextPerfil");
		explicacioSigles1.AddComponent("GUIText");
		explicacioSigles1.guiText.font = Resources.Load("ERASERDUST") as Font;
		explicacioSigles1.guiText.fontSize = fontSize;
		explicacioSigles1.guiText.text = "- ATK: Atac Llarga Distancia";
		
		explicacioSigles2 = new GameObject("TextPerfil");
		explicacioSigles2.AddComponent("GUIText");
		explicacioSigles2.guiText.font = Resources.Load("ERASERDUST") as Font;
		explicacioSigles2.guiText.fontSize = fontSize;
		explicacioSigles2.guiText.text = "- ATC: Atac Curta Distancia";
		
		explicacioSigles3 = new GameObject("TextPerfil");
		explicacioSigles3.AddComponent("GUIText");
		explicacioSigles3.guiText.font = Resources.Load("ERASERDUST") as Font;
		explicacioSigles3.guiText.fontSize = fontSize;
		explicacioSigles3.guiText.text = "- DEF: Defensa";
		
		explicacioSigles4 = new GameObject("TextPerfil");
		explicacioSigles4.AddComponent("GUIText");
		explicacioSigles4.guiText.font = Resources.Load("ERASERDUST") as Font;
		explicacioSigles4.guiText.fontSize = fontSize;
		explicacioSigles4.guiText.text = "- MOV: Moviment";
		
		explicacioSigles5 = new GameObject("TextPerfil");
		explicacioSigles5.AddComponent("GUIText");
		explicacioSigles5.guiText.font = Resources.Load("ERASERDUST") as Font;
		explicacioSigles5.guiText.fontSize = fontSize;
		explicacioSigles5.guiText.text = "- DAC: Distancia Atac";
		
		explicacioVictoria1 = new GameObject("TextPerfil");
		explicacioVictoria1.AddComponent("GUIText");
		explicacioVictoria1.guiText.font = Resources.Load("ERASERDUST") as Font;
		explicacioVictoria1.guiText.fontSize = fontSize;
		explicacioVictoria1.guiText.text = "- Guanyaras una partida si redueixes la\ndefensa de la base rival a zero, amb l'atac\ndels teus personatges";
		
		explicacioVictoria2 = new GameObject("TextPerfil");
		explicacioVictoria2.AddComponent("GUIText");
		explicacioVictoria2.guiText.font = Resources.Load("ERASERDUST") as Font;
		explicacioVictoria2.guiText.fontSize = fontSize;
		explicacioVictoria2.guiText.text = "- Tambe guanyaras una partida, si a\nconsegueixes eliminar tots els personatges\nrivals (els 15 que hi han per baralla)";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		fontSize = (int) Mathf.Ceil(17.0f * (Camera.mainCamera.pixelWidth/568.0f));
		fontSize2 = (int) Mathf.Ceil(17.0f * (Camera.mainCamera.pixelWidth/1080.0f));
		
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
		
		quadreExplicacioGeneral.guiTexture.transform.localScale = new Vector3(0.4f, 0.7f, 1);
		quadreExplicacioGeneral.guiTexture.transform.position = new Vector3(0.3f, 0.5f, 1);
		
		quadreExplicacioSigles.guiTexture.transform.localScale = new Vector3(0.4f, 0.3f, 1);
		quadreExplicacioSigles.guiTexture.transform.position = new Vector3(0.75f, 0.7f, 1);
		
		quadreExplicacioVictoria.guiTexture.transform.localScale = new Vector3(0.4f, 0.35f, 1);
		quadreExplicacioVictoria.guiTexture.transform.position = new Vector3(0.75f, 0.33f, 1);
		
		titolGeneral.guiText.fontSize = fontSize;
		titolGeneral.transform.position = new Vector3(0.14f, 0.82f, 2);
		
		titolSigles.guiText.fontSize = fontSize;
		titolSigles.transform.position = new Vector3(0.7f, 0.82f, 2);
		
		titolVictoria.guiText.fontSize = fontSize;
		titolVictoria.transform.position = new Vector3(0.62f, 0.49f, 2);
		
		explicacioGeneral1.guiText.fontSize = fontSize2;
		explicacioGeneral1.transform.position = new Vector3(0.11f, 0.75f, 2);
		
		explicacioGeneral2.guiText.fontSize = fontSize2;
		explicacioGeneral2.transform.position = new Vector3(0.11f, 0.68f, 2);
		
		explicacioGeneral3.guiText.fontSize = fontSize2;
		explicacioGeneral3.transform.position = new Vector3(0.11f, 0.61f, 2);
		
		explicacioGeneral4.guiText.fontSize = fontSize2;
		explicacioGeneral4.transform.position = new Vector3(0.11f, 0.46f, 2);
		
		explicacioGeneral5.guiText.fontSize = fontSize2;
		explicacioGeneral5.transform.position = new Vector3(0.11f, 0.33f, 2);
		
		explicacioSigles1.guiText.fontSize = fontSize2;
		explicacioSigles1.transform.position = new Vector3(0.6f, 0.76f, 2);
		
		explicacioSigles2.guiText.fontSize = fontSize2;
		explicacioSigles2.transform.position = new Vector3(0.6f, 0.72f, 2);
		
		explicacioSigles3.guiText.fontSize = fontSize2;
		explicacioSigles3.transform.position = new Vector3(0.6f, 0.68f, 2);
		
		explicacioSigles4.guiText.fontSize = fontSize2;
		explicacioSigles4.transform.position = new Vector3(0.6f, 0.64f, 2);
		
		explicacioSigles5.guiText.fontSize = fontSize2;
		explicacioSigles5.transform.position = new Vector3(0.6f, 0.6f, 2);
		
		explicacioVictoria1.guiText.fontSize = fontSize2;
		explicacioVictoria1.transform.position = new Vector3(0.57f, 0.4f, 2);
		
		explicacioVictoria2.guiText.fontSize = fontSize2;
		explicacioVictoria2.transform.position = new Vector3(0.57f, 0.3f, 2);
		
		if(detectaClick(rectBack)){
			// Click al botó d'enrere
			Debug.Log("Click al botó d'enrere");
			ControlGeneralHowToPlay cG = (ControlGeneralHowToPlay) Camera.mainCamera.GetComponent("ControlGeneralHowToPlay");
			Destroy (cG);
			
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			
			Destroy(quadreExplicacioGeneral);
			Destroy(quadreExplicacioSigles);
			Destroy(quadreExplicacioVictoria);
			
			Destroy(titolGeneral);
			Destroy(titolSigles);
			Destroy(titolVictoria);
			
			Destroy(explicacioGeneral1);
			Destroy(explicacioGeneral2);
			Destroy(explicacioGeneral3);
			Destroy(explicacioGeneral4);
			Destroy(explicacioGeneral5);
			
			Destroy(explicacioSigles1);
			Destroy(explicacioSigles2);
			Destroy(explicacioSigles3);
			Destroy(explicacioSigles4);
			Destroy(explicacioSigles5);
			
			Destroy(explicacioVictoria1);
			Destroy(explicacioVictoria2);

			ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
			cJ.carregarPantallaMenuPrincipal();
			
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
}

//// Explicacio joc
//
//// Quadre general
//
//- Uber Card Battle es un joc d'estratègia de tauler amb cartes intercanviables.
//
//- L'objectiu es derrotar al teu rival, per poder així guanyar la partida.
//
//- Per poder guanyar, has d'encadenar accions dels teus personatges, moure'ls
//pel tauler, atacar als personatges rivals, atacar la base rival, plantejar 
//estratègies defensives o ofensives, i definir el teu estil de joc.
//
//- Podràs personalitzar la teva baralla de cartes amb les cartes noves que vagis
//aconseguint. Per cada victòria, obtindràs 5 cartes noves per utilitzar.
//
//- La baralla, consisteix en 15 personatges i 6 bonificacions, de diferents tipus
//i nivells. Quan aconsegueixis millors cartes, les podràs canviar per altres de la
//teva baralla, i tenir així una baralla més forta.
//
//// Quadre Sigles
//
//ATK: Atac a Llarga Distància
//ATC: Atac a Curta Distància
//DEF: Defensa
//MOV: Moviment
//DAC: Distància d'Atac
//
//// Quadre Victories
//
//- Guanyaràs una partida si redueixes la defensa de la base rival a zero, amb l'atac
//dels teus personatges.
//
//- També guanyaràs una partida, si aconsegueixes eliminar tots els personatges rivals
//(els 15 que hi han per baralla).
