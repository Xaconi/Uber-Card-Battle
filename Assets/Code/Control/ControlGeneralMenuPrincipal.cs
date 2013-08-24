using UnityEngine;
using System.Collections;

public class ControlGeneralMenuPrincipal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
		conMenu.assignarPantalla("MenuPrincipal");
		Camera.mainCamera.gameObject.AddComponent("MenuPrincipal");
		AnimacioFlashMenu aF = (AnimacioFlashMenu) GUITexture.FindObjectOfType(typeof(AnimacioFlashMenu));
		aF.invertirAnimacio();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
