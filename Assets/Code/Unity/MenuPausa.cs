using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuPausa : MonoBehaviour {
	
	public Texture2D menuPausa;
	public Texture2D botoAbandona;
	public Texture2D botoContinua;
	float pas = 0.1f;
	public bool confirmAbandona;
	Color color;
	
	void Awake(){
		menuPausa = Resources.Load("menu_pausa") as Texture2D;
		botoAbandona = Resources.Load("boto_abandonar") as Texture2D;
		botoContinua = Resources.Load("boto_continuar") as Texture2D;
		color = new Color(1.0f, 1.0f, 1.0f, pas);
		confirmAbandona = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		if(pas < 1.0f) pas += 0.01f;
		GUI.color = new Color(1.0f, 1.0f, 1.0f, pas);
		if(!confirmAbandona){
			// Menu de pausa normal
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
				Debug.Log("Continuem la partida");
				BotoPausa p = GameObject.FindGameObjectWithTag("BotoPausa").GetComponent("BotoPausa") as BotoPausa;
				p.desactivarPausa();
				Destroy(this);
			}else if(detectaClick(rectAbandona)){
				Debug.Log("Abandonem la partida");
				confirmAbandona = true;
				menuPausa = Resources.Load("menu_confirm_abandona") as Texture2D;
				botoAbandona = Resources.Load("menu_confirm_si") as Texture2D;
				botoContinua = Resources.Load("menu_confirm_no") as Texture2D;
			}
		}else{
			// Confirmació d'abandonar la partida
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
				Debug.Log("Continuem la partida");
				confirmAbandona = false;
				menuPausa = Resources.Load("menu_pausa") as Texture2D;
				botoAbandona = Resources.Load("boto_abandonar") as Texture2D;
				botoContinua = Resources.Load("boto_continuar") as Texture2D;
			}else if(detectaClick(rectAbandona)){
				Debug.Log("Abandonem la partida");
				abandonaPartida();
			}
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
	
	private void abandonaPartida(){
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
		
		g = new List<GameObject>(GameObject.FindGameObjectsWithTag("Personatge"));
		for(int i = 0; i < g.Count; i++){
			Destroy (g[i]);
		}
		
		g = new List<GameObject>(GameObject.FindGameObjectsWithTag("PersonatgeInvisible"));
		for(int i = 0; i < g.Count; i++){
			Destroy (g[i]);
		}
		
		g = new List<GameObject>(GameObject.FindGameObjectsWithTag("Base"));
		for(int i = 0; i < g.Count; i++){
			Destroy (g[i]);
		}
		
		Destroy (GameObject.FindGameObjectWithTag("LifeBar"));
		Destroy (GameObject.FindGameObjectWithTag("LifeBarP1"));
		Destroy (GameObject.FindGameObjectWithTag("LifeBarP2"));
		Destroy (GameObject.FindGameObjectWithTag("BotoPausa"));
		Destroy (GameObject.FindGameObjectWithTag("BotoEsborrarMoviment"));
		Destroy (GameObject.FindGameObjectWithTag("BotoConfirmarMoviment"));
		Destroy (GameObject.FindGameObjectWithTag("MenuCartesDisponibles"));
		Destroy (GameObject.FindGameObjectWithTag("Tauler"));
		Destroy (GameObject.FindGameObjectWithTag("TextLifeBar"));
		
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		cG.partidaAbandonada = true;
		
		PartGrafica p = (PartGrafica) Camera.mainCamera.GetComponent("PartGrafica");
		Destroy (p);
		
		Connexio c = (Connexio) Camera.mainCamera.GetComponent("Connexio");
		Destroy (c);
		
		IA ia = (IA) Camera.mainCamera.GetComponent("IA");
		Destroy (ia);
		
		Destroy(this);
		
		Camera.mainCamera.gameObject.AddComponent("AnimacioPantallesBatallaFinal");
	}
}
