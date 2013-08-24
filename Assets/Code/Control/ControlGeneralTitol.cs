using UnityEngine;
using System.Collections;

public class ControlGeneralTitol : ControlGeneral {
	
	void Awake(){
		// Carrega de tots els elements de la pantalla
		GameObject.Instantiate(Resources.Load("BackgroundEdicio"));
		
		// Assignació de Scripts necessaris a la càmara principal
		Camera.mainCamera.gameObject.AddComponent("TitolJoc");
		//Camera.mainCamera.gameObject.AddComponent("MenuResultats");
	}
	
	// Use this for initialization
	void Start () {
		AnimacioFlashMenu aF = (AnimacioFlashMenu) GUITexture.FindObjectOfType(typeof(AnimacioFlashMenu));
		aF.invertirAnimacio();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
