using UnityEngine;
using System.Collections;

public class BotoDesaCanvis : MonoBehaviour {

	Texture2D textura;
	Material material;

	// Use this for initialization
	void Start () {
		textura = Resources.Load("boto_guardar_canvis") as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnGUI(){
		Rect rect = new Rect(0.7f*Camera.mainCamera.pixelWidth, 
			0.01f*Camera.mainCamera.pixelHeight, 
			0.2f*Camera.mainCamera.pixelWidth, 
			0.03f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rect,
			textura);
		if(detectaClick(rect)){
			AudioObject aO = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			aO.playClick();
			AccioEdicio a = new AccioDesaCanvis();
			a.executarAccio();
		}
	}
	
	private bool detectaClick(Rect rect){
		bool click = false;
		Event e = Event.current;
		if(e.type == EventType.MouseDown && rect.Contains(e.mousePosition)){
			click = true;
		}
		return click;
	}
}
