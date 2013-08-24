using UnityEngine;
using System.Collections;

public class AnimacioFlashMenu : MonoBehaviour {
	
	private GUITexture textura;
	private Color color;
	private float pas;
	private bool animacioInvertida = false;
	private string pantalla;
	private bool acabat;
	
	void Awake(){
		textura = guiTexture;
		textura.color = new Color(1.0f, 1.0f, 1.0f, 0.001f);
		color = new Color(0.0f, 0.0f, 0.0f, 1.1f);
		pas = 0.1f;
		pantalla = "";
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		if(!animacioInvertida && !acabat){
			textura.color = Color.Lerp(textura.color, color, pas);
			pas += 0.001f;
			if(textura.color.a >= 1.0f){
				carregarPantalla();
				acabat = true;
			}
		}
		else if(animacioInvertida){
			textura.color = Color.Lerp(textura.color, color, pas);
			pas -= 0.001f;
			if(pas <= 0.1f){
				Destroy(gameObject);
			}
		}
	}
	
	public void assignarPantalla(string p){
		pantalla = p;
	}
	
	public void carregarPantalla(){
		switch(pantalla){
			case "Titol":
				Camera.mainCamera.gameObject.AddComponent("ControlGeneralTitol");
			break;
			case "Perfils":
				Camera.mainCamera.gameObject.AddComponent("ControlGeneralPerfils");
			break;
			case "MenuPrincipal":
				Camera.mainCamera.gameObject.AddComponent("ControlGeneralMenuPrincipal");
			break;
			case "MenuQuick":
				Camera.mainCamera.gameObject.AddComponent("ControlGeneralMenuQuick");
			break;
			case "MenuHistoria":
				Camera.mainCamera.gameObject.AddComponent("ControlGeneralMenuHistoria");
			break;
			case "MenuEstadistiques":
				Camera.mainCamera.gameObject.AddComponent("ControlGeneralMenuEstadistiques");
			break;
			case "HowTo":
				Camera.mainCamera.gameObject.AddComponent("ControlGeneralHowToPlay");
			break;
		}
	}
	
	public void invertirAnimacio(){
		animacioInvertida = true;
		color = new Color(1.0f, 1.0f, 1.0f, 0.001f);
	}
}
