using UnityEngine;
using System.Collections;

public class TitolJoc : MonoBehaviour {
	
	public Texture2D titol;
	public Texture2D creator;
	public Rect pantalla;
	
	void Awake(){
		titol = Resources.Load("title") as Texture2D;
		creator = Resources.Load("creator") as Texture2D;
		pantalla = new Rect(0.0f, 0.0f, Camera.mainCamera.pixelWidth, Camera.mainCamera.pixelHeight);
		AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
		if(a == null) GameObject.Instantiate(Resources.Load("AudioObject"));
	}

	// Use this for initialization
	void Start () {
		//Debug.Log(Dades.bronze.dadaPersonatge.guerrero.atacCBaix);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		Rect rect = new Rect(0.12f*Camera.mainCamera.pixelWidth, 
			0.1f*Camera.mainCamera.pixelHeight, 
			0.8f*Camera.mainCamera.pixelWidth, 
			0.6f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rect, titol);
		
		rect = new Rect(0.35f*Camera.mainCamera.pixelWidth, 
			0.8f*Camera.mainCamera.pixelHeight, 
			0.3f*Camera.mainCamera.pixelWidth, 
			0.1f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rect, creator);
		
		
		if(detectaClick(pantalla)){
			Debug.Log("SortidaPantalla");
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			a.playClick();
			
			ControlGeneralTitol cG = (ControlGeneralTitol) Camera.mainCamera.GetComponent("ControlGeneralTitol");
			Destroy (cG);
			
			ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
			cJ.carregarPantallaPerfils();
			
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
