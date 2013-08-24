//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó mediante una herramienta.
//     Los cambios del archivo se perderán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

using UnityEngine;
using System.Collections.Generic;

public class ControlGeneralEdicio : ControlGeneral {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	public delegate void Fi();
  	public static event Fi fiPantalla;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	void Awake(){
		
		// Carrega de tots els elements de la pantalla
		Debug.Log(Application.persistentDataPath);
		GameObject.Instantiate(Resources.Load("MenuCartesDisponiblesEdicio"));
		GameObject.Instantiate(Resources.Load("MenuBaralla"));
		GameObject.Instantiate(Resources.Load("BotoDreta"));
		GameObject.Instantiate(Resources.Load("BotoEsquerre"));
		//GameObject.Instantiate(Resources.Load("BackgroundEdicio"));
		
		// Assignació de Scripts necessaris a la càmara principal
		Camera.mainCamera.gameObject.AddComponent("PartGraficaEdicio");
		Camera.mainCamera.gameObject.AddComponent("ConnexioEdicio");
		Camera.mainCamera.gameObject.AddComponent("BotoEsborrarCarta");
		Camera.mainCamera.gameObject.AddComponent("BotoDescartaCanvis");
		Camera.mainCamera.gameObject.AddComponent("BotoDesaCanvis");
	}
	
	// Use this for initialization
	void Start () {
		Debug.Log("START del ControlGeneralEdicio");
		AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
		a.FadeSoundLow();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void iniciarEdicio(){
		// Donar les ordres per emplenar la pantalla de cartes
		MenuCartesDisponiblesEdicio mD = (MenuCartesDisponiblesEdicio) GameObject.FindGameObjectWithTag("MenuCartesDisponiblesEdicio").GetComponent("MenuCartesDisponiblesEdicio");
		mD.crearCartes();
		mD.demanarInformacioCartesNoves(0);
		MenuBaralla mB = (MenuBaralla) GameObject.FindGameObjectWithTag("MenuBaralla").GetComponent("MenuBaralla");
		mB.emplenarTauler();
		AnimacioPantallesEdicio a = (AnimacioPantallesEdicio) Camera.mainCamera.GetComponent("AnimacioPantallesEdicio");
		Debug.Log(a);
		if(a == null){
			AnimacioPantallesEdicioFinal b = (AnimacioPantallesEdicioFinal) Camera.mainCamera.GetComponent("AnimacioPantallesEdicioFinal");
			b.obertura = false;
		}else a.obertura = false;
	}
	
}


