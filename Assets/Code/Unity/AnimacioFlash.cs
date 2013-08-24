using UnityEngine;
using System.Collections;

public class AnimacioFlash : MonoBehaviour {
	
	private GUITexture textura;
	private Color color;
	private float pas;
	private Moviment movimentEsborrar;
	private bool animacioInvertida = false;
	private bool acabat = false;
	public delegate void Flash(Moviment m);
  	public static event Flash esborrarMoviment;
	
	void Awake(){
		textura = guiTexture;
		textura.color = new Color(1.0f, 1.0f, 1.0f, 0.001f);
		color = new Color(1.0f, 1.0f, 1.0f, 1.1f);
		pas = 0.1f;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		if(!animacioInvertida && !acabat){
			textura.color = Color.Lerp(textura.color, color, pas);
			pas += 0.001f;
			if(textura.color.a >= 1.0f){
				esborrarMoviment(movimentEsborrar);
				acabat = true;
			}
		}
		else if(animacioInvertida){
			textura.color = Color.Lerp(textura.color, color, pas);
			pas -= 0.001f;
			if(pas <= 0.1f){
				Destroy(gameObject);
			}
		}
	}
	
	public void assignarMoviment(Moviment m){
		movimentEsborrar = m;
	}
	
	public void invertirAnimacio(){
		animacioInvertida = true;
		color = new Color(1.0f, 1.0f, 1.0f, 0.001f);
	}
}
