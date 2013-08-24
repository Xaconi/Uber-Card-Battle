using UnityEngine;
using System.Collections;

public class ControlGeneralMenuQuick : MonoBehaviour {
	
	void Awake(){
		// Assignació de Scripts necessaris a la càmara principal
		Camera.mainCamera.gameObject.AddComponent("MenuQuick");
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
