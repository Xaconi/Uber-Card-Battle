using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using System.Xml;

public class Connexio : MonoBehaviour {
	public WWW www;
	public bool baseDades;
	public Baralla barallaActual;
	public System.IO.StreamReader file;
	public System.Xml.Serialization.XmlSerializer reader;
	public List<InformacioCarta> llistaCartes;
	public Players p;
	
	public void assignarBaralla(Baralla b){
		Debug.Log(llistaCartes.Count);
		barallaActual = b;
		barallaActual.llistaCartes = llistaCartes;
		barallaActual.nCartes = llistaCartes.Count;
	}
	
	void Awake () {
		StartCoroutine(ferYield());
	}
	
	// Use this for initialization
	void Start () {
		//StartCoroutine(ferYield());
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	public IEnumerator ferYield(){
		llistaCartes = new List<InformacioCarta>();
#if UNITY_ANDROID
		//WWW www = new WWW("http://xaconi.com/database.xml");
		if (System.IO.File.Exists(Application.persistentDataPath + "/database.xml")){
			www = new WWW(@"file://" + Application.persistentDataPath + "/database.xml");
		}else{
			www = new WWW(Application.streamingAssetsPath + "/database.xml");
		}
#elif UNITY_STANDALONE_WIN
		Debug.Log(Application.streamingAssetsPath + "/database.xml");
		if (System.IO.File.Exists(Application.persistentDataPath + "/database.xml")){
			www = new WWW(@"file://" + Application.persistentDataPath + "/database.xml");
			Debug.Log(Application.persistentDataPath + "/database.xml");
		}else{
			www = new WWW(@"file://" + Application.streamingAssetsPath + "/database.xml");
		}
#endif
		while(!www.isDone)
		{
			yield return new WaitForSeconds(1);
		}
		yield return www;
		Debug.Log("Estic aqui");
		
		using (XmlReader reader = XmlReader.Create(new System.IO.StringReader(www.text)))
        {
            reader.MoveToContent();
            switch (reader.Name)
            {
                case "players":
					p = new Players();
                    p = (Players) new XmlSerializer(typeof(Players)).Deserialize(reader);
					ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
					Debug.Log(p.llistaPlayers.Count);
					foreach(cartaUsuari carta in p.llistaPlayers[conMenu.jugador].baralla.bA.llistaCartesBaralla){
					//foreach(cartaUsuari carta in p.llistaPlayers[0].baralla.bA.llistaCartesBaralla){ // <- Canviar el l'index de jugadors
						if(carta.tipus.Equals("Bonificacio")){
							Debug.Log("Tinc una bonificacio");
							llistaCartes.Add(new InformacioCartaBonificacio(carta.tipus,
								carta.nom,
								carta.nivell,
								carta.bonificacioAtacCurt,
								carta.bonificacioDefensa));
						}else if(carta.tipus.Equals("Personatge")){
							Debug.Log("Tinc un personatge");
							llistaCartes.Add(new InformacioCartaPersonatge(carta.tipus,
								carta.nom,
								carta.nivell,
								carta.atacLlargaDistancia,
								carta.atacCurtaDistancia,
								carta.defensa,
								carta.moviment,
								carta.distanciaAtac));
						}
					}
					ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
					cG.iniciarPartida();
                    break;
                default:
                    throw new System.NotSupportedException("Unexpected: " + reader.Name);
            }
        }  
	}
	
	public void llegirBaseDades(){

	}
}
