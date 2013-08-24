using UnityEngine;
using System.Collections;

public class Bonificacio : MonoBehaviour {
	
	private Personatge personatgeActual;
	private string nom;
	private InformacioCartaBonificacio informacio;
	
	// Use this for initialization
	void Awake () {
		// Assignar la posició tenint en compte el personatge
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(personatgeActual != null){
			// Actualitzar posició del simbol amb la del personatge
			gameObject.transform.position = personatgeActual.transform.position;
		}
	}
	
	public void iniciarBonificacio(Personatge p, string n, InformacioCartaBonificacio i){
		personatgeActual = p;
		gameObject.tag = "Bonificacio";
		informacio = i;
		nom = n;
	}
	
	public string getNom(){
		return nom;
	}
	
	public InformacioCartaBonificacio getInformacio(){
		return informacio;
	}
}
