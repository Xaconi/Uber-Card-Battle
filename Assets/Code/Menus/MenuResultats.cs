using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuResultats : MonoBehaviour {
	
	public Texture2D titolPantalla;
	public Texture2D botoSeguent;
	
	public GameObject quadreEstadistiquesNormals;
	public GameObject quadreEstadistiquesAtac;
	public GameObject quadreLog;
	
	public GameObject titolNormal;
	public GameObject titolAtac;
	public GameObject titolLog;
	
	public GameObject temps;
	public GameObject cartesUtilitzades;
	public GameObject cartesNoUtilitzades;
	public GameObject personatgesUtilitzats;
	public GameObject bonificacionsUtiltizades;
	
	public GameObject atacFet;
	public GameObject atacRebut;
	
	public Vector2 scrollPosition;
    public string longString = "";
	public GUISkin skin;
	
	public int fontSize;

	// Use this for initialization
	void Start () {
		int fontSize = (int) Mathf.Ceil(15.0f * (Camera.mainCamera.pixelWidth/523.0f));
		ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
		
		titolPantalla = Resources.Load("resultats") as Texture2D;
		botoSeguent = Resources.Load("menu_principal") as Texture2D;
		
		quadreEstadistiquesNormals = new GameObject("quadreEstadistiquesNormals");
		quadreEstadistiquesNormals.AddComponent("GUITexture");
		quadreEstadistiquesNormals.guiTexture.texture = Resources.Load("BlackboardEdicio") as Texture;
		
		quadreEstadistiquesAtac = new GameObject("quadreEstadistiquesAtac");
		quadreEstadistiquesAtac.AddComponent("GUITexture");
		quadreEstadistiquesAtac.guiTexture.texture = Resources.Load("BlackboardEdicio") as Texture;
		
		quadreLog = new GameObject("quadreLog");
		quadreLog.AddComponent("GUITexture");
		quadreLog.guiTexture.texture = Resources.Load("BlackboardEdicio") as Texture;
		
		titolNormal = new GameObject("TextPerfil");
		titolNormal.AddComponent("GUIText");
		titolNormal.guiText.font = Resources.Load("ERASERDUST") as Font;
		titolNormal.guiText.fontSize = fontSize;
		titolNormal.guiText.text = "Dades de Joc";
		
		titolAtac = new GameObject("TextPerfil");
		titolAtac.AddComponent("GUIText");
		titolAtac.guiText.font = Resources.Load("ERASERDUST") as Font;
		titolAtac.guiText.fontSize = fontSize;
		titolAtac.guiText.text = "Dades d'Atac";
		
		titolLog = new GameObject("TextPerfil");
		titolLog.AddComponent("GUIText");
		titolLog.guiText.font = Resources.Load("ERASERDUST") as Font;
		titolLog.guiText.fontSize = fontSize;
		titolLog.guiText.text = "Log";
		
		temps = new GameObject("TextPerfil");
		temps.AddComponent("GUIText");
		temps.guiText.font = Resources.Load("ERASERDUST") as Font;
		temps.guiText.fontSize = fontSize;
		temps.guiText.text = "- Temps de la partida: " + cJ.getTempsPartida();
		
		cartesUtilitzades = new GameObject("TextPerfil");
		cartesUtilitzades.AddComponent("GUIText");
		cartesUtilitzades.guiText.font = Resources.Load("ERASERDUST") as Font;
		cartesUtilitzades.guiText.fontSize = fontSize;
		cartesUtilitzades.guiText.text = "- Cartes Utilitzades: " + cJ.getCartesUtilitzades();
		
		cartesNoUtilitzades = new GameObject("TextPerfil");
		cartesNoUtilitzades.AddComponent("GUIText");
		cartesNoUtilitzades.guiText.font = Resources.Load("ERASERDUST") as Font;
		cartesNoUtilitzades.guiText.fontSize = fontSize;
		cartesNoUtilitzades.guiText.text = "- Cartes No Utilitzades: " + cJ.getCartesNoUtilitzades();
		
		personatgesUtilitzats = new GameObject("TextPerfil");
		personatgesUtilitzats.AddComponent("GUIText");
		personatgesUtilitzats.guiText.font = Resources.Load("ERASERDUST") as Font;
		personatgesUtilitzats.guiText.fontSize = fontSize;
		personatgesUtilitzats.guiText.text = "- Personatges Utilitzats: " + cJ.getPersonatgesUtilitzats();
		
		bonificacionsUtiltizades = new GameObject("TextPerfil");
		bonificacionsUtiltizades.AddComponent("GUIText");
		bonificacionsUtiltizades.guiText.font = Resources.Load("ERASERDUST") as Font;
		bonificacionsUtiltizades.guiText.fontSize = fontSize;
		bonificacionsUtiltizades.guiText.text = "- Bonificacions Utilitzades: " + cJ.getBonificacionsUtilitzades();
		
		atacFet = new GameObject("TextPerfil");
		atacFet.AddComponent("GUIText");
		atacFet.guiText.font = Resources.Load("ERASERDUST") as Font;
		atacFet.guiText.fontSize = fontSize;
		atacFet.guiText.text = "- Atac Fet: " + cJ.getAtacFet();
		
		atacRebut = new GameObject("TextPerfil");
		atacRebut.AddComponent("GUIText");
		atacRebut.guiText.font = Resources.Load("ERASERDUST") as Font;
		atacRebut.guiText.fontSize = fontSize;
		atacRebut.guiText.text = "- Atac Rebut: " + cJ.getAtacRebut();
		
		List<string> llista = cJ.getLlistaLog();
		for(int i = 0; i < llista.Count; i++){
			longString += llista[i] + "\n";
		}
		
		//longString += "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI() {
		fontSize = (int) Mathf.Ceil(15.0f * (Camera.mainCamera.pixelWidth/523.0f));
		
		Rect rectTitol = new Rect(0.3f*Camera.mainCamera.pixelWidth, 
			0.0f*Camera.mainCamera.pixelHeight, 
			0.4f*Camera.mainCamera.pixelWidth, 
			0.1f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rectTitol, titolPantalla);
		
		Rect rectSeguent = new Rect(0.3f*Camera.mainCamera.pixelWidth, 
			0.9f*Camera.mainCamera.pixelHeight, 
			0.4f*Camera.mainCamera.pixelWidth, 
			0.1f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rectSeguent, botoSeguent);
		
		quadreEstadistiquesNormals.guiTexture.transform.localScale = new Vector3(0.4f, 0.7f, 1);
		quadreEstadistiquesNormals.guiTexture.transform.position = new Vector3(0.3f, 0.5f, 1);
		
		quadreEstadistiquesAtac.guiTexture.transform.localScale = new Vector3(0.4f, 0.3f, 1);
		quadreEstadistiquesAtac.guiTexture.transform.position = new Vector3(0.75f, 0.7f, 1);
		
		quadreLog.guiTexture.transform.localScale = new Vector3(0.4f, 0.35f, 1);
		quadreLog.guiTexture.transform.position = new Vector3(0.75f, 0.33f, 1);
		
		titolNormal.guiText.fontSize = fontSize;
		titolNormal.transform.position = new Vector3(0.21f, 0.82f, 2);
		
		titolAtac.guiText.fontSize = fontSize;
		titolAtac.transform.position = new Vector3(0.65f, 0.82f, 2);
		
		titolLog.guiText.fontSize = fontSize;
		titolLog.transform.position = new Vector3(0.71f, 0.49f, 2);
		
		atacFet.guiText.fontSize = fontSize-4;
		atacFet.transform.position = new Vector3(0.6f, 0.72f, 2);
		
		atacRebut.guiText.fontSize = fontSize-4;
		atacRebut.transform.position = new Vector3(0.6f, 0.62f, 2);
		
		temps.guiText.fontSize = fontSize-4;
		temps.transform.position = new Vector3(0.12f, 0.72f, 2);
		
		cartesUtilitzades.guiText.fontSize = fontSize-4;
		cartesUtilitzades.transform.position = new Vector3(0.12f, 0.60f, 2);
		
		cartesNoUtilitzades.guiText.fontSize = fontSize-4;
		cartesNoUtilitzades.transform.position = new Vector3(0.12f, 0.48f, 2);
		
		personatgesUtilitzats.guiText.fontSize = fontSize-4;
		personatgesUtilitzats.transform.position = new Vector3(0.12f, 0.36f, 2);
		
		bonificacionsUtiltizades.guiText.fontSize = fontSize-4;
		bonificacionsUtiltizades.transform.position = new Vector3(0.12f, 0.24f, 2);

		GUILayout.BeginArea(new Rect(290 * (Camera.mainCamera.pixelWidth/523.0f),
			290 * (Camera.mainCamera.pixelHeight/523.0f), 
			300 * (Camera.mainCamera.pixelWidth/523.0f), 
			150 * (Camera.mainCamera.pixelHeight/523.0f)));
      	scrollPosition = GUILayout.BeginScrollView(scrollPosition,
			GUILayout.Width(204* (Camera.mainCamera.pixelWidth/523.0f)),
			GUILayout.Height(145 * (Camera.mainCamera.pixelHeight/523.0f)));
        GUILayout.Label(longString);
        
        GUILayout.EndScrollView();

		GUILayout.EndArea();
        
		if(detectaClick(rectSeguent)){
			// Click al botó d'enrere
			Debug.Log("Click al botó d'enrere");

			Destroy(quadreEstadistiquesNormals);
			Destroy(quadreEstadistiquesAtac);
			Destroy(quadreLog);
			Destroy(titolNormal);
			Destroy(titolAtac);
			Destroy(titolLog);
			Destroy(temps);
			Destroy(cartesUtilitzades);
			Destroy(cartesNoUtilitzades);
			Destroy(personatgesUtilitzats);
			Destroy(bonificacionsUtiltizades);
			Destroy(atacFet);
			Destroy(atacRebut);
			
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
