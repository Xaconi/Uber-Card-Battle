using UnityEngine;
using System.Collections;

public class ControlGeneralMenuEstadistiques : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
		conMenu.assignarPantalla("MenuEstadistiques");
		Camera.mainCamera.gameObject.AddComponent("MenuEstadistiques");
		AnimacioFlashMenu aF = (AnimacioFlashMenu) GUITexture.FindObjectOfType(typeof(AnimacioFlashMenu));
		aF.invertirAnimacio();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
