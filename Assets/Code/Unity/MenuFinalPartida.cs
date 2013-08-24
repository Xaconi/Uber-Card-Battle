using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuFinalPartida : MonoBehaviour {
	
	public Texture2D missatge;
	float pas = 0.1f;
	Color color;
	public delegate void FinalPartida();
  	public static event FinalPartida fiPartida;
	
	void Awake(){
		ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
		if(cJ.getGuanyador() == 1){
			missatge = Resources.Load("missatge_final_partida_victoria") as Texture2D;
		}else if(cJ.getGuanyador() == 2){
			missatge = Resources.Load("missatge_final_partida_derrota") as Texture2D;
		}
		color = new Color(1.0f, 1.0f, 1.0f, pas);
	}

	// Use this for initialization
	void Start () {
		fiPartida();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		if(pas < 1.0f) pas += 0.01f;
		GUI.color = new Color(1.0f, 1.0f, 1.0f, pas);
		Rect rectMissatge = new Rect(0.2f*Camera.mainCamera.pixelWidth, 
			0.2f*Camera.mainCamera.pixelHeight, 
			0.6f*Camera.mainCamera.pixelWidth, 
			0.6f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rectMissatge, missatge);
		
		if(detectaClick(rectMissatge)){
			Debug.Log("NO");
			carregarPantallaResultats();
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
	
	private void carregarPantallaResultats(){
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
		
		g = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bonificacio"));
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
		
		Camera.mainCamera.gameObject.AddComponent("AnimacioPantallesBatallaFinal");
		
		Destroy(this);
	}
}
