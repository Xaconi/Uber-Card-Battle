using UnityEngine;
using System.Collections;

public class ControlGeneralPerfils : MonoBehaviour {
	
	// Use this for initialization
	void Awake () {
		// Assignació de Scripts necessaris a la càmara principal
		Camera.mainCamera.gameObject.AddComponent("ConnexioMenus");
		ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
		conMenu.assignarPantalla("Perfils");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void iniciarPantalla(){
		Camera.mainCamera.gameObject.AddComponent("SeleccioPerfil");
		AnimacioFlashMenu aF = (AnimacioFlashMenu) GUITexture.FindObjectOfType(typeof(AnimacioFlashMenu));
		aF.invertirAnimacio();
	}
}
