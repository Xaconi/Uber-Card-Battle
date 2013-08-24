using UnityEngine;
using System.Collections;

public class ControlGeneralHowToPlay : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
		conMenu.assignarPantalla("HowToPlay");
		Camera.mainCamera.gameObject.AddComponent("HowToPlay");
		AnimacioFlashMenu aF = (AnimacioFlashMenu) GUITexture.FindObjectOfType(typeof(AnimacioFlashMenu));
		aF.invertirAnimacio();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
