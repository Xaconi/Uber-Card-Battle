using UnityEngine;
using System.Collections;

public class ControlGeneralMenuHistoria : MonoBehaviour {
	
	void Awake(){
		ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
		conMenu.assignarPantalla("MenuHistoria");
		Camera.mainCamera.gameObject.AddComponent("MenuHistoria");
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
