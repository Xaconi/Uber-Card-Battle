using UnityEngine;
using System.Collections;

public class BotoEsborrarCarta : MonoBehaviour {
	
	Texture2D textura;
	Material material;
	Color color;

	// Use this for initialization
	void Start () {
		textura = Resources.Load("boto_esborrar_carta") as Texture2D;
		color = new Color(1.0f, 1.0f, 1.0f, 0.4f);
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnGUI(){
		GUI.color = color;
		Rect rect = new Rect(0.1f*Camera.mainCamera.pixelWidth, 
			0.01f*Camera.mainCamera.pixelHeight, 
			0.2f*Camera.mainCamera.pixelWidth, 
			0.03f*Camera.mainCamera.pixelHeight);
		GUI.DrawTexture(rect,
			textura);
		if(detectaClick(rect)){
			if(GUI.color.a == 1.0f){
				// Carta seleccionada
				AudioObject aO = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
				aO.playClick();
				
				CartaEdicio carta = (CartaEdicio) GameObject.FindGameObjectWithTag("CartaSeleccionada").GetComponent("CartaEdicio");
				AccioEdicio a = new AccioEsborrarCarta(carta);
				a.executarAccio();
			}
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
