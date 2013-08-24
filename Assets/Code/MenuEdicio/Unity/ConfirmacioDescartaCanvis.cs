using UnityEngine;
using System.Collections;

public class ConfirmacioDescartaCanvis : MonoBehaviour {

	Texture2D missatge;
	Texture2D botoSi;
	Texture2D botoNo;
	Material material;
	Color color;
	float pas = 0.1f;
	public delegate void Missatge();
  	public static event Missatge ferMissatge;

	// Use this for initialization
	void Start () {
		ferMissatge();
		missatge = Resources.Load("missatge_descartar_canvis") as Texture2D;
		botoSi = Resources.Load("missatge_si") as Texture2D;
		botoNo = Resources.Load("missatge_no") as Texture2D;
		color = new Color(1.0f, 1.0f, 1.0f, pas);
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnGUI(){
		if(pas < 1.0f) pas += 0.01f;
		GUI.color = new Color(1.0f, 1.0f, 1.0f, pas);
		Rect rect = new Rect(0.2f*Camera.mainCamera.pixelWidth, 
			0.2f*Camera.mainCamera.pixelHeight, 
			0.6f*Camera.mainCamera.pixelWidth, 
			0.6f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rect, missatge);
		
		Rect rectSi = new Rect(0.3f*Camera.mainCamera.pixelWidth, 
			0.6f*Camera.mainCamera.pixelHeight, 
			0.1f*Camera.mainCamera.pixelWidth, 
			0.1f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rectSi, botoSi);
		
		Rect rectNo = new Rect(0.6f*Camera.mainCamera.pixelWidth, 
			0.6f*Camera.mainCamera.pixelHeight, 
			0.1f*Camera.mainCamera.pixelWidth, 
			0.1f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rectNo, botoNo);
		
		if(detectaClick(rectNo)){
			Debug.Log("NO");
			AudioObject aO = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			aO.playClick();
			ferMissatge();
			Destroy(this);
		}else if(detectaClick(rectSi)){
			Debug.Log("SI");
			AudioObject aO = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			aO.playClick();
			AccioEdicio a = new AccioConfirmarNoDesa();
			a.executarAccio();
		}
	}
	
	public void activarBoto(){
		color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}
	
	public void desactivarBoto(){
		color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
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
