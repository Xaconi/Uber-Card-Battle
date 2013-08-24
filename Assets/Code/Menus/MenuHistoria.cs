using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuHistoria : MonoBehaviour {
	
	public List<int> llistaNivells;
	
	public Texture2D titolPantalla;
	public Texture2D botoBack;
	public GameObject descripcioPantalla;
	
	public GameObject nivell1;
	public GameObject nivell2;
	public GameObject nivell3;
	public GameObject nivell4;
	public GameObject nivell5;
	public GameObject nivell6;
	public GameObject nivell7;
	public GameObject nivell8;
	public GameObject nivell9;
	
	public GameObject numeroNivell1;
	public GameObject numeroNivell2;
	public GameObject numeroNivell3;
	public GameObject numeroNivell4;
	public GameObject numeroNivell5;
	public GameObject numeroNivell6;
	public GameObject numeroNivell7;
	public GameObject numeroNivell8;
	public GameObject numeroNivell9;
	
	public GameObject marcaNivell1;
	public GameObject marcaNivell2;
	public GameObject marcaNivell3;
	public GameObject marcaNivell4;
	public GameObject marcaNivell5;
	public GameObject marcaNivell6;
	public GameObject marcaNivell7;
	public GameObject marcaNivell8;
	public GameObject marcaNivell9;
	
	public bool clickat = false;
	
	int fontSize;
	
	void Awake(){
		int fontSize = (int) Mathf.Ceil(20.0f * (Camera.mainCamera.pixelWidth/568.0f));
		ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
		
		titolPantalla = Resources.Load("menu_principal") as Texture2D;
		botoBack = Resources.Load("boto_enrere") as Texture2D;
		
		descripcioPantalla = new GameObject("TextPerfil");
		descripcioPantalla.AddComponent("GUIText");
		descripcioPantalla.guiText.font = Resources.Load("ERASERDUST") as Font;
		descripcioPantalla.guiText.fontSize = fontSize;
		descripcioPantalla.guiText.alignment = TextAlignment.Center;
		descripcioPantalla.guiText.material.color = Color.black;
		
		llistaNivells = conMenu.p.llistaPlayers[conMenu.jugador].dU.dH.llistaNivells;
		
		nivell1 = new GameObject("perfil");
		nivell1.AddComponent("GUITexture");
		nivell1.guiTexture.texture = Resources.Load("panell_historia_bronze") as Texture;
		if(llistaNivells[0] == 1){
			marcaNivell1 = new GameObject("TextPerfil");
			marcaNivell1.AddComponent("GUIText");
			marcaNivell1.guiText.font = Resources.Load("ERASERDUST") as Font;
			marcaNivell1.guiText.fontSize = fontSize-2;
			marcaNivell1.guiText.material.color = Color.black;
			marcaNivell1.guiText.text = "X";
		}
		
		nivell2 = new GameObject("perfil");
		nivell2.AddComponent("GUITexture");
		nivell2.guiTexture.texture = Resources.Load("panell_historia_bronze") as Texture;
		if(llistaNivells[1] == 1){
			marcaNivell2 = new GameObject("TextPerfil");
			marcaNivell2.AddComponent("GUIText");
			marcaNivell2.guiText.font = Resources.Load("ERASERDUST") as Font;
			marcaNivell2.guiText.fontSize = fontSize-2;
			marcaNivell2.guiText.material.color = Color.black;
			marcaNivell2.guiText.text = "X";
		}
		
		nivell3 = new GameObject("perfil");
		nivell3.AddComponent("GUITexture");
		nivell4 = new GameObject("perfil");
		nivell4.AddComponent("GUITexture");
		if(llistaNivells[0] == 1 && llistaNivells[1] == 1){
			nivell3.guiTexture.texture = Resources.Load("panell_historia_plata") as Texture;
			nivell4.guiTexture.texture = Resources.Load("panell_historia_plata") as Texture;
		}else{
			nivell3.guiTexture.texture = Resources.Load("panell_historia_blocked") as Texture;
			nivell4.guiTexture.texture = Resources.Load("panell_historia_blocked") as Texture;
		}
		
		
		if(llistaNivells[2] == 1){
			marcaNivell3 = new GameObject("TextPerfil");
			marcaNivell3.AddComponent("GUIText");
			marcaNivell3.guiText.font = Resources.Load("ERASERDUST") as Font;
			marcaNivell3.guiText.fontSize = fontSize-2;
			marcaNivell3.guiText.material.color = Color.black;
			marcaNivell3.guiText.text = "X";
		}
		
		
		if(llistaNivells[3] == 1){
			marcaNivell4 = new GameObject("TextPerfil");
			marcaNivell4.AddComponent("GUIText");
			marcaNivell4.guiText.font = Resources.Load("ERASERDUST") as Font;
			marcaNivell4.guiText.fontSize = fontSize-2;
			marcaNivell4.guiText.material.color = Color.black;
			marcaNivell4.guiText.text = "X";
		}
		
		nivell5 = new GameObject("perfil");
		nivell5.AddComponent("GUITexture");
		nivell6 = new GameObject("perfil");
		nivell6.AddComponent("GUITexture");
		nivell7 = new GameObject("perfil");
		nivell7.AddComponent("GUITexture");
		if(llistaNivells[2] == 1 && llistaNivells[3] == 1){
			nivell5.guiTexture.texture = Resources.Load("panell_historia_or") as Texture;
			nivell6.guiTexture.texture = Resources.Load("panell_historia_or") as Texture;
			nivell7.guiTexture.texture = Resources.Load("panell_historia_or") as Texture;
		}else{
			nivell5.guiTexture.texture = Resources.Load("panell_historia_blocked") as Texture;
			nivell6.guiTexture.texture = Resources.Load("panell_historia_blocked") as Texture;
			nivell7.guiTexture.texture = Resources.Load("panell_historia_blocked") as Texture;
		}
		
		if(llistaNivells[4] == 1){
			marcaNivell5 = new GameObject("TextPerfil");
			marcaNivell5.AddComponent("GUIText");
			marcaNivell5.guiText.font = Resources.Load("ERASERDUST") as Font;
			marcaNivell5.guiText.fontSize = fontSize-2;
			marcaNivell5.guiText.material.color = Color.black;
			marcaNivell5.guiText.text = "X";
		}
		
		if(llistaNivells[5] == 1){
			marcaNivell6 = new GameObject("TextPerfil");
			marcaNivell6.AddComponent("GUIText");
			marcaNivell6.guiText.font = Resources.Load("ERASERDUST") as Font;
			marcaNivell6.guiText.fontSize = fontSize-2;
			marcaNivell6.guiText.material.color = Color.black;
			marcaNivell6.guiText.text = "X";
		}
		
		if(llistaNivells[6] == 1){
			marcaNivell7 = new GameObject("TextPerfil");
			marcaNivell7.AddComponent("GUIText");
			marcaNivell7.guiText.font = Resources.Load("ERASERDUST") as Font;
			marcaNivell7.guiText.fontSize = fontSize-2;
			marcaNivell7.guiText.material.color = Color.black;
			marcaNivell7.guiText.text = "X";
		}
		
		nivell8 = new GameObject("perfil");
		nivell8.AddComponent("GUITexture");
		nivell9 = new GameObject("perfil");
		nivell9.AddComponent("GUITexture");
		if(llistaNivells[4] == 1 && llistaNivells[5] == 1 && llistaNivells[6] == 1){
			nivell8.guiTexture.texture = Resources.Load("panell_historia_plati") as Texture;
			nivell9.guiTexture.texture = Resources.Load("panell_historia_plati") as Texture;
		}else{
			nivell8.guiTexture.texture = Resources.Load("panell_historia_blocked") as Texture;
			nivell9.guiTexture.texture = Resources.Load("panell_historia_blocked") as Texture;
		}
		
		if(llistaNivells[7] == 1){
			nivell8.guiTexture.texture = Resources.Load("panell_historia_plati") as Texture;
			marcaNivell8 = new GameObject("TextPerfil");
			marcaNivell8.AddComponent("GUIText");
			marcaNivell8.guiText.font = Resources.Load("ERASERDUST") as Font;
			marcaNivell8.guiText.fontSize = fontSize-2;
			marcaNivell8.guiText.material.color = Color.black;
			marcaNivell8.guiText.text = "X";
		}
		
		if(llistaNivells[8] == 1){
			nivell9.guiTexture.texture = Resources.Load("panell_historia_plati") as Texture;
			marcaNivell9 = new GameObject("TextPerfil");
			marcaNivell9.AddComponent("GUIText");
			marcaNivell9.guiText.font = Resources.Load("ERASERDUST") as Font;
			marcaNivell9.guiText.fontSize = fontSize-2;
			marcaNivell9.guiText.material.color = Color.black;
			marcaNivell9.guiText.text = "X";
		}
		
		numeroNivell1 = new GameObject("TextPerfil");
		numeroNivell1.AddComponent("GUIText");
		numeroNivell1.guiText.font = Resources.Load("ERASERDUST") as Font;
		numeroNivell1.guiText.fontSize = fontSize-2;
		numeroNivell1.guiText.material.color = Color.black;
		numeroNivell1.guiText.text = "1";
		
		numeroNivell2 = new GameObject("TextPerfil");
		numeroNivell2.AddComponent("GUIText");
		numeroNivell2.guiText.font = Resources.Load("ERASERDUST") as Font;
		numeroNivell2.guiText.fontSize = fontSize-2;
		numeroNivell2.guiText.material.color = Color.black;
		numeroNivell2.guiText.text = "2";
		
		numeroNivell3 = new GameObject("TextPerfil");
		numeroNivell3.AddComponent("GUIText");
		numeroNivell3.guiText.font = Resources.Load("ERASERDUST") as Font;
		numeroNivell3.guiText.fontSize = fontSize-2;
		numeroNivell3.guiText.material.color = Color.black;
		numeroNivell3.guiText.text = "3";
		
		numeroNivell4 = new GameObject("TextPerfil");
		numeroNivell4.AddComponent("GUIText");
		numeroNivell4.guiText.font = Resources.Load("ERASERDUST") as Font;
		numeroNivell4.guiText.fontSize = fontSize-2;
		numeroNivell4.guiText.material.color = Color.black;
		numeroNivell4.guiText.text = "4";
		
		numeroNivell5 = new GameObject("TextPerfil");
		numeroNivell5.AddComponent("GUIText");
		numeroNivell5.guiText.font = Resources.Load("ERASERDUST") as Font;
		numeroNivell5.guiText.fontSize = fontSize-2;
		numeroNivell5.guiText.material.color = Color.black;
		numeroNivell5.guiText.text = "5";
		
		numeroNivell6 = new GameObject("TextPerfil");
		numeroNivell6.AddComponent("GUIText");
		numeroNivell6.guiText.font = Resources.Load("ERASERDUST") as Font;
		numeroNivell6.guiText.fontSize = fontSize-2;
		numeroNivell6.guiText.material.color = Color.black;
		numeroNivell6.guiText.text = "6";
		
		numeroNivell7 = new GameObject("TextPerfil");
		numeroNivell7.AddComponent("GUIText");
		numeroNivell7.guiText.font = Resources.Load("ERASERDUST") as Font;
		numeroNivell7.guiText.fontSize = fontSize-2;
		numeroNivell7.guiText.material.color = Color.black;
		numeroNivell7.guiText.text = "7";
		
		numeroNivell8 = new GameObject("TextPerfil");
		numeroNivell8.AddComponent("GUIText");
		numeroNivell8.guiText.font = Resources.Load("ERASERDUST") as Font;
		numeroNivell8.guiText.fontSize = fontSize-2;
		numeroNivell8.guiText.material.color = Color.black;
		numeroNivell8.guiText.text = "8";
		
		numeroNivell9 = new GameObject("TextPerfil");
		numeroNivell9.AddComponent("GUIText");
		numeroNivell9.guiText.font = Resources.Load("ERASERDUST") as Font;
		numeroNivell9.guiText.fontSize = fontSize-2;
		numeroNivell9.guiText.material.color = Color.black;
		numeroNivell9.guiText.text = "9";
	}

	// Use this for initialization
	void Start () {
		descripcioPantalla.guiText.text = "Selecciona una fase";
	}
	
	// Update is called once per frame
	void Update () {
		if(detectaClickTextura(nivell1.guiTexture) && !clickat){
			Debug.Log("Click al nivell 1");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			carregarMenuPantallaBatalla(1, "FACIL", "Bronze", "Bosc");
			clickat = true;
		}else if(detectaClickTextura(nivell2.guiTexture) && !clickat){
			Debug.Log("Click al nivell 2");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			carregarMenuPantallaBatalla(2, "FACIL", "Bronze", "Winter");
			clickat = true;
		}else if(detectaClickTextura(nivell3.guiTexture) && !clickat){
			Debug.Log("Click al nivell 3");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			carregarMenuPantallaBatalla(3, "FACIL", "Plata", "Cave");
			clickat = true;
		}else if(detectaClickTextura(nivell4.guiTexture) && !clickat){
			Debug.Log("Click al nivell 4");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			carregarMenuPantallaBatalla(4, "MITJA", "Plata", "Winter");
			clickat = true;
		}else if(detectaClickTextura(nivell5.guiTexture) && !clickat){
			Debug.Log("Click al nivell 5");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			carregarMenuPantallaBatalla(5, "MITJA", "Or", "Bosc");
			clickat = true;
		}else if(detectaClickTextura(nivell6.guiTexture) && !clickat){
			Debug.Log("Click al nivell 6");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			carregarMenuPantallaBatalla(6, "MITJA", "Or", "Cave");
			clickat = true;
		}else if(detectaClickTextura(nivell7.guiTexture) && !clickat){
			Debug.Log("Click al nivell 7");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			carregarMenuPantallaBatalla(7, "DIFICIL", "Or", "Winter");
			clickat = true;
		}else if(detectaClickTextura(nivell8.guiTexture) && !clickat){
			Debug.Log("Click al nivell 8");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			carregarMenuPantallaBatalla(8, "DIFICIL", "Plati", "Bosc");
			clickat = true;
		}else if(detectaClickTextura(nivell9.guiTexture) && !clickat){
			Debug.Log("Click al nivell 9");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			carregarMenuPantallaBatalla(9, "DIFICIL", "Plati", "Cave");
			clickat = true;
		}
	}
	
	void OnGUI(){
		fontSize = (int) Mathf.Ceil(20.0f * (Camera.mainCamera.pixelWidth/568.0f));
		
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
		
		descripcioPantalla.guiText.fontSize = fontSize;
		descripcioPantalla.transform.position = new Vector3(0.325f, 0.87f, 1);
		
		nivell1.guiTexture.transform.localScale = new Vector3(0.2f, 0.2f, 1);
		nivell1.guiTexture.transform.position = new Vector3(0.2f, 0.68f, 0.5f);
		
		nivell2.guiTexture.transform.localScale = new Vector3(0.2f, 0.2f, 1);
		nivell2.guiTexture.transform.position = new Vector3(0.5f, 0.68f, 0.5f);
		
		nivell3.guiTexture.transform.localScale = new Vector3(0.2f, 0.2f, 1);
		nivell3.guiTexture.transform.position = new Vector3(0.8f, 0.68f, 0.5f);
		
		nivell4.guiTexture.transform.localScale = new Vector3(0.2f, 0.2f, 1);
		nivell4.guiTexture.transform.position = new Vector3(0.2f, 0.45f, 0.5f);
		
		nivell5.guiTexture.transform.localScale = new Vector3(0.2f, 0.2f, 1);
		nivell5.guiTexture.transform.position = new Vector3(0.5f, 0.45f, 0.5f);
		
		nivell6.guiTexture.transform.localScale = new Vector3(0.2f, 0.2f, 1);
		nivell6.guiTexture.transform.position = new Vector3(0.8f, 0.45f, 0.5f);
		
		nivell7.guiTexture.transform.localScale = new Vector3(0.2f, 0.2f, 1);
		nivell7.guiTexture.transform.position = new Vector3(0.2f, 0.22f, 0.5f);
		
		nivell8.guiTexture.transform.localScale = new Vector3(0.2f, 0.2f, 1);
		nivell8.guiTexture.transform.position = new Vector3(0.5f, 0.22f, 0.5f);
		
		nivell9.guiTexture.transform.localScale = new Vector3(0.2f, 0.2f, 1);
		nivell9.guiTexture.transform.position = new Vector3(0.8f, 0.22f, 0.5f);
		
		
		numeroNivell1.guiText.fontSize = fontSize-4;
		numeroNivell1.transform.position = new Vector3(0.111f, 0.775f, 1);
		
		numeroNivell2.guiText.fontSize = fontSize-4;
		numeroNivell2.transform.position = new Vector3(0.411f, 0.78f, 1);
		
		numeroNivell3.guiText.fontSize = fontSize-4;
		numeroNivell3.transform.position = new Vector3(0.711f, 0.78f, 1);
		
		numeroNivell4.guiText.fontSize = fontSize-4;
		numeroNivell4.transform.position = new Vector3(0.110f, 0.547f, 1);
		
		numeroNivell5.guiText.fontSize = fontSize-4;
		numeroNivell5.transform.position = new Vector3(0.411f, 0.545f, 1);
		
		numeroNivell6.guiText.fontSize = fontSize-4;
		numeroNivell6.transform.position = new Vector3(0.708f, 0.547f, 1);
		
		numeroNivell7.guiText.fontSize = fontSize-4;
		numeroNivell7.transform.position = new Vector3(0.111f, 0.32f, 1);
		
		numeroNivell8.guiText.fontSize = fontSize-4;
		numeroNivell8.transform.position = new Vector3(0.411f, 0.32f, 1);
		
		numeroNivell9.guiText.fontSize = fontSize-4;
		numeroNivell9.transform.position = new Vector3(0.708f, 0.32f, 1);
		
		if(llistaNivells[0] == 1){
			marcaNivell1.guiText.fontSize = fontSize-4;
			marcaNivell1.transform.position = new Vector3(0.275f, 0.778f, 1);
		}
		
		if(llistaNivells[1] == 1){
			marcaNivell2.guiText.fontSize = fontSize-4;
			marcaNivell2.transform.position =  new Vector3(0.575f, 0.778f, 1);
		}
		
		if(llistaNivells[2] == 1){
			marcaNivell3.guiText.fontSize = fontSize-4;
			marcaNivell3.transform.position = new Vector3(0.875f, 0.778f, 1);
		}
		
		if(llistaNivells[3] == 1){
			marcaNivell4.guiText.fontSize = fontSize-4;
			marcaNivell4.transform.position = new Vector3(0.275f, 0.551f, 1);
		}
		
		if(llistaNivells[4] == 1){
			marcaNivell5.guiText.fontSize = fontSize-4;
			marcaNivell5.transform.position = new Vector3(0.575f, 0.551f, 1);
		}
		
		if(llistaNivells[5] == 1){
			marcaNivell6.guiText.fontSize = fontSize-4;
			marcaNivell6.transform.position = new Vector3(0.875f, 0.551f, 1);
		}
		
		if(llistaNivells[6] == 1){
			marcaNivell7.guiText.fontSize = fontSize-4;
			marcaNivell7.transform.position = new Vector3(0.275f, 0.321f, 1);
		}
		
		if(llistaNivells[7] == 1){
			marcaNivell8.guiText.fontSize = fontSize-4;
			marcaNivell8.transform.position = new Vector3(0.575f, 0.321f, 1);
		}
		
		if(llistaNivells[8] == 1){
			marcaNivell9.guiText.fontSize = fontSize-4;
			marcaNivell9.transform.position = new Vector3(0.875f, 0.321f, 1);
		}
		
		if(detectaClick(rectBack)){
			// Click al botó d'enrere
			Debug.Log("Click al botó d'enrere");
			ControlGeneralMenuHistoria cG = (ControlGeneralMenuHistoria) Camera.mainCamera.GetComponent("ControlGeneralMenuHistoria");
			Destroy (cG);
			
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
		
			Destroy(descripcioPantalla);
			
			Destroy(nivell1);
			Destroy(nivell2);
			Destroy(nivell3);
			Destroy(nivell4);
			Destroy(nivell5);
			Destroy(nivell6);
			Destroy(nivell7);
			Destroy(nivell8);
			Destroy(nivell9);
			
			Destroy(numeroNivell1);
			Destroy(numeroNivell2);
			Destroy(numeroNivell3);
			Destroy(numeroNivell4);
			Destroy(numeroNivell5);
			Destroy(numeroNivell6);
			Destroy(numeroNivell7);
			Destroy(numeroNivell8);
			Destroy(numeroNivell9);
			
			Destroy(marcaNivell1);
			Destroy(marcaNivell2);
			Destroy(marcaNivell3);
			Destroy(marcaNivell4);
			Destroy(marcaNivell5);
			Destroy(marcaNivell6);
			Destroy(marcaNivell7);
			Destroy(marcaNivell8);
			Destroy(marcaNivell9);
			
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
	
	private bool detectaClickTextura(GUITexture g){
		bool click = false;
		Event e = Event.current;
		if(Input.GetMouseButton(0) && g.HitTest(Input.mousePosition)){
			click = true;
		}
		return click;
	}
	
	private void carregarMenuPantallaBatalla(int nivell, string dificultat, string nivellCarta, string escenari){
		
		ControlGeneralMenuHistoria cG = (ControlGeneralMenuHistoria) Camera.mainCamera.GetComponent("ControlGeneralMenuHistoria");
		Destroy (cG);
	
		Destroy(descripcioPantalla);
		
		Destroy(nivell1);
		Destroy(nivell2);
		Destroy(nivell3);
		Destroy(nivell4);
		Destroy(nivell5);
		Destroy(nivell6);
		Destroy(nivell7);
		Destroy(nivell8);
		Destroy(nivell9);
		
		Destroy(numeroNivell1);
		Destroy(numeroNivell2);
		Destroy(numeroNivell3);
		Destroy(numeroNivell4);
		Destroy(numeroNivell5);
		Destroy(numeroNivell6);
		Destroy(numeroNivell7);
		Destroy(numeroNivell8);
		Destroy(numeroNivell9);
		
		Destroy(marcaNivell1);
		Destroy(marcaNivell2);
		Destroy(marcaNivell3);
		Destroy(marcaNivell4);
		Destroy(marcaNivell5);
		Destroy(marcaNivell6);
		Destroy(marcaNivell7);
		Destroy(marcaNivell8);
		Destroy(marcaNivell9);
		
		ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
		cJ.assignarDadesPartidaHistoria(dificultat, escenari, nivellCarta, nivell);
		cJ.carregarPantallaBatalla();
		
		Destroy(this);
	}
}
