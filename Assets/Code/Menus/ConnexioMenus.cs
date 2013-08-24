using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System;

public class ConnexioMenus : MonoBehaviour {

	public WWW www;
	public bool baseDades;
	public System.IO.StreamReader file;
	public System.Xml.Serialization.XmlSerializer reader;
	public List<InformacioCarta> llistaCartes;
	public List<InformacioCarta> cartesNoUtilitzades;
	public Players p;
	public Players seguretat;
	public int jugador = 0;
	public string pantalla = "";
	
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
		cartesNoUtilitzades = new List<InformacioCarta>();
#if UNITY_ANDROID
		//WWW www = new WWW("http://xaconi.com/database.xml");
		//www = new WWW(Application.streamingAssetsPath + "/database.xml");
		Debug.Log(Application.persistentDataPath);
		if (System.IO.File.Exists(Application.persistentDataPath + "/database.xml")){
			www = new WWW(@"file://" + Application.persistentDataPath + "/database.xml");
		}else{
			www = new WWW(Application.streamingAssetsPath + "/database.xml");
		}
			
#elif UNITY_STANDALONE_WIN
		Debug.Log(Application.persistentDataPath);
		//WWW www = new WWW("http://xaconi.com/database.xml");
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
		
		MemoryStream memoryStream = new MemoryStream(www.bytes);
       	StreamReader streamReader = new StreamReader(memoryStream, true);
		
		using (XmlReader reader = XmlReader.Create(streamReader))
        {
            reader.MoveToContent();
            switch (reader.Name)
            {
                case "players":
					p = new Players();
                    p = (Players) new XmlSerializer(typeof(Players)).Deserialize(reader);
					reader.Close();
					streamReader.Close();
					memoryStream.Close();
			   	break;
                default:
                    throw new System.NotSupportedException("Unexpected: " + reader.Name);
            }
        }
		
		Debug.Log("Fins Aqui Be");
		
		memoryStream = new MemoryStream(www.bytes);
       	streamReader = new StreamReader(memoryStream, true);
		
		using (XmlReader reader = XmlReader.Create(streamReader))
        {
            reader.MoveToContent();
            switch (reader.Name)
            {
                case "players":
					seguretat = new Players();
                    seguretat = (Players) new XmlSerializer(typeof(Players)).Deserialize(reader);
				break;
			}
			reader.Close();
		}
		Debug.Log(p.llistaPlayers[0].dU.dH.llistaNivells[0]);
		if(pantalla.Equals("Perfils")){  // Si ens trobem carregant la connexió als menus s'esta carregant a la pantalla dels perfils
			ControlGeneralPerfils cG = (ControlGeneralPerfils) Camera.mainCamera.GetComponent(typeof(ControlGeneralPerfils));
			cG.iniciarPantalla();
		}
	}
	
	// Guarda el fitxer XML de base de dades (el jugador guarda els canvis)
	public void Save(string path, Players p){
		#if UNITY_ANDROID
//				var serializer = new XmlSerializer(typeof(Players));
//				StringWriter text = new StringWriter();
//		        serializer.Serialize(text, p);
//				string uploadText = text.ToString();
//				Debug.Log(uploadText);
//				byte[] data = GetBytes(uploadText);
//				File.WriteAllBytes(Application.persistentDataPath + "/database.xml", data);
				var serializer = new XmlSerializer(typeof(Players));
		        using(var stream = new FileStream(path, FileMode.Create)){
		            serializer.Serialize(stream, p);
					stream.Close();
		        }
		#elif UNITY_STANDALONE_WIN
			 	var serializer = new XmlSerializer(typeof(Players));
		        using(var stream = new FileStream(path, FileMode.Create)){
		            serializer.Serialize(stream, p);
					stream.Close();
		        }
		#endif
    }
	
	public void activarJugador(int j){
		p.llistaPlayers[j].dU.activat = 1;
		p.llistaPlayers[j].dU.nom = "Player " + (j+1);
		#if UNITY_ANDROID
			Save(Application.persistentDataPath+ "/database.xml", p);
		#elif UNITY_STANDALONE_WIN
			Save(Application.persistentDataPath + "/database.xml", p);
		#endif
	}
	
	public void assignarJugador(int j){
		jugador = j;
	}
	
	public void assignarPantalla(string pan){
		pantalla = pan;
	}
	
	public void carregarCartes(){
		foreach(cartaUsuari carta in p.llistaPlayers[jugador].baralla.bA.llistaCartesBaralla){
			if(carta.tipus.Equals("Bonificacio")){
				llistaCartes.Add(new InformacioCartaBonificacio(carta.tipus,
					carta.nom,
					carta.nivell,
					carta.bonificacioAtacCurt,
					carta.bonificacioDefensa));
			}else if(carta.tipus.Equals("Personatge")){
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
		foreach(cartaUsuari carta in p.llistaPlayers[jugador].baralla.c.llistaCartesBaralla){
			if(carta.tipus.Equals("Bonificacio")){
				cartesNoUtilitzades.Add(new InformacioCartaBonificacio(carta.tipus,
					carta.nom,
					carta.nivell,
					carta.bonificacioAtacCurt,
					carta.bonificacioDefensa));
			}else if(carta.tipus.Equals("Personatge")){
				cartesNoUtilitzades.Add(new InformacioCartaPersonatge(carta.tipus,
					carta.nom,
					carta.nivell,
					carta.atacLlargaDistancia,
					carta.atacCurtaDistancia,
					carta.defensa,
					carta.moviment,
					carta.distanciaAtac));
			}
		}
	}
	
	public void actualitzarEstadistiques(string tipus, bool partidaGuanyada, int atacFet, int atacRebut, int cartesUtilitzades, int nombreAccions, float temps, int personatgesUtilitzats, int bonificacionsUtilitzades, int nivell){
		TimeSpan timeSpan = TimeSpan.FromSeconds(temps);
		if(tipus.Equals("Historia")){
			if(partidaGuanyada) p.llistaPlayers[jugador].e.vH++;
			else p.llistaPlayers[jugador].e.dH++;
			p.llistaPlayers[jugador].e.tempsHistoria.hores += timeSpan.Hours;
			p.llistaPlayers[jugador].e.tempsHistoria.minuts += timeSpan.Minutes;
			if(p.llistaPlayers[jugador].e.tempsHistoria.minuts > 60){
				p.llistaPlayers[jugador].e.tempsHistoria.minuts = p.llistaPlayers[jugador].e.tempsHistoria.minuts - 60;
				p.llistaPlayers[jugador].e.tempsHistoria.hores++;
			}
			p.llistaPlayers[jugador].e.tempsHistoria.segons += timeSpan.Seconds;
			if(p.llistaPlayers[jugador].e.tempsHistoria.segons > 60){
				p.llistaPlayers[jugador].e.tempsHistoria.segons = p.llistaPlayers[jugador].e.tempsHistoria.segons - 60;
				p.llistaPlayers[jugador].e.tempsHistoria.minuts++;
			}
			if(partidaGuanyada) p.llistaPlayers[jugador].dU.dH.llistaNivells[nivell-1] = 1;
		}else if(tipus.Equals("Quick")){
			if(partidaGuanyada) p.llistaPlayers[jugador].e.vQ++;
			else p.llistaPlayers[jugador].e.dQ++;
			p.llistaPlayers[jugador].e.tempsQuick.hores += timeSpan.Hours;
			p.llistaPlayers[jugador].e.tempsQuick.minuts += timeSpan.Minutes;
			if(p.llistaPlayers[jugador].e.tempsQuick.minuts > 60){
				p.llistaPlayers[jugador].e.tempsQuick.minuts = p.llistaPlayers[jugador].e.tempsQuick.minuts - 60;
				p.llistaPlayers[jugador].e.tempsQuick.hores++;
			}
			p.llistaPlayers[jugador].e.tempsQuick.segons += timeSpan.Seconds;
			if(p.llistaPlayers[jugador].e.tempsQuick.segons > 60){
				p.llistaPlayers[jugador].e.tempsQuick.segons = p.llistaPlayers[jugador].e.tempsQuick.segons - 60;
				p.llistaPlayers[jugador].e.tempsQuick.minuts++;
			}
		}
		
		if(partidaGuanyada) p.llistaPlayers[jugador].e.v++;
		else p.llistaPlayers[jugador].e.d++;
		
		p.llistaPlayers[jugador].e.temps.hores += timeSpan.Hours;
		p.llistaPlayers[jugador].e.temps.minuts += timeSpan.Minutes;
		if(p.llistaPlayers[jugador].e.temps.minuts > 60){
			p.llistaPlayers[jugador].e.temps.minuts = p.llistaPlayers[jugador].e.temps.minuts - 60;
			p.llistaPlayers[jugador].e.temps.hores++;
		}
		p.llistaPlayers[jugador].e.temps.segons += timeSpan.Seconds;
		if(p.llistaPlayers[jugador].e.temps.segons > 60){
			p.llistaPlayers[jugador].e.temps.segons = p.llistaPlayers[jugador].e.temps.segons - 60;
			p.llistaPlayers[jugador].e.temps.minuts++;
		}
		
		p.llistaPlayers[jugador].e.atacFet += atacFet;
		p.llistaPlayers[jugador].e.atacRebut += atacRebut;
		p.llistaPlayers[jugador].e.cartesUtilitzades += cartesUtilitzades;
		Save(Application.persistentDataPath+ "/database.xml", p);
	}
	
	public void esborrarJugador(){
		// Reiniciem les dades del jugador actual, i les canviem per les dades base de seguretat...
		p.llistaPlayers[jugador] = p.llistaPlayers[3];
		Save(Application.persistentDataPath+ "/database.xml", p);
	}
	
	public void assignarNovesCartes(List<InformacioCarta> llistaCartesNoves){
		for(int i = 0; i < 3; i++){
			cartaUsuari c = new cartaUsuari((InformacioCartaPersonatge) llistaCartesNoves[i]);
			p.llistaPlayers[jugador].baralla.c.llistaCartesBaralla.Add (c);
		}
		
		for(int i = 3; i < 5; i++){
			cartaUsuari c = new cartaUsuari((InformacioCartaBonificacio) llistaCartesNoves[i]);
			p.llistaPlayers[jugador].baralla.c.llistaCartesBaralla.Add (c);
		}
		Save(Application.persistentDataPath+ "/database.xml", p);
	}
}
