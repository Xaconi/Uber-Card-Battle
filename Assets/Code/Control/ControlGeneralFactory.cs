using UnityEngine;

public class ControlGeneralFactory {
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	public ControlGeneralFactory(){
	}
	
	public void crearObjecteControl(string tipus){
		GameObject camara;
		AnimacioFlashMenu aF;
		switch (tipus){
			case "PantallaTitol":
				camara = GameObject.FindGameObjectsWithTag ("MainCamera")[0];
				camara.AddComponent("ControlGeneralTitol");
			break;
			case "Batalla":
				Debug.Log("ASDASDASDASSADSAD");
				camara = GameObject.FindGameObjectsWithTag ("MainCamera")[0];
				camara.AddComponent("AnimacioPantalles");
			break;
			case "Edicio":
				camara = GameObject.FindGameObjectsWithTag ("MainCamera")[0];
				camara.AddComponent("AnimacioPantallesEdicio");
			break;
			case "BatallaSensePantalles":
				camara = GameObject.FindGameObjectsWithTag ("MainCamera")[0];
				camara.AddComponent("ControlGeneralBatalla");
			break;
			case "EdicioSensePantalles":
				camara = GameObject.FindGameObjectsWithTag ("MainCamera")[0];
				camara.AddComponent("ControlGeneralEdicio");
			break;
			case "Titol":
				GameObject.Instantiate(Resources.Load("flash_screen_menu"));
				aF = (AnimacioFlashMenu) GUITexture.FindObjectOfType(typeof(AnimacioFlashMenu));
				aF.assignarPantalla("Titol");
			break;
			case "Perfils":
				GameObject.Instantiate(Resources.Load("flash_screen_menu"));
				aF = (AnimacioFlashMenu) GUITexture.FindObjectOfType(typeof(AnimacioFlashMenu));
				aF.assignarPantalla("Perfils");
			break;
			case "MenuPrincipal":
				GameObject.Instantiate(Resources.Load("flash_screen_menu"));
				aF = (AnimacioFlashMenu) GUITexture.FindObjectOfType(typeof(AnimacioFlashMenu));
				aF.assignarPantalla("MenuPrincipal");
			break;
			case "MenuQuick":
				GameObject.Instantiate(Resources.Load("flash_screen_menu"));
				aF = (AnimacioFlashMenu) GUITexture.FindObjectOfType(typeof(AnimacioFlashMenu));
				aF.assignarPantalla("MenuQuick");
			break;
			case "MenuHistoria":
				GameObject.Instantiate(Resources.Load("flash_screen_menu"));
				aF = (AnimacioFlashMenu) GUITexture.FindObjectOfType(typeof(AnimacioFlashMenu));
				aF.assignarPantalla("MenuHistoria");
			break;
			case "MenuEstadistiques":
				GameObject.Instantiate(Resources.Load("flash_screen_menu"));
				aF = (AnimacioFlashMenu) GUITexture.FindObjectOfType(typeof(AnimacioFlashMenu));
				aF.assignarPantalla("MenuEstadistiques");
			break;
			case "HowTo":
				GameObject.Instantiate(Resources.Load("flash_screen_menu"));
				aF = (AnimacioFlashMenu) GUITexture.FindObjectOfType(typeof(AnimacioFlashMenu));
				aF.assignarPantalla("HowTo");
			break;
		}
	}
	
}
