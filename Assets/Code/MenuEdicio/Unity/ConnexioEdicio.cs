using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

public class ConnexioEdicio : MonoBehaviour {
	public WWW www;
	public bool baseDades;
	public System.IO.StreamReader file;
	public System.Xml.Serialization.XmlSerializer reader;
	public List<InformacioCarta> llistaCartes;
	public List<InformacioCarta> cartesNoUtilitzades;
	public Players p;
	public Players seguretat;
	
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
		if (System.IO.File.Exists(Application.persistentDataPath + "/database.xml")){
			www = new WWW(@"file://" + Application.persistentDataPath + "/database.xml");
		}else{
			www = new WWW(Application.streamingAssetsPath + "/database.xml");
		}
			
#elif UNITY_STANDALONE_WIN
		Debug.Log("/database.xml");
		//WWW www = new WWW("http://xaconi.com/database.xml");
		if (System.IO.File.Exists(Application.persistentDataPath + "/database.xml")){
			www = new WWW(@"file://" + Application.persistentDataPath + "/database.xml");
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
					ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
					foreach(cartaUsuari carta in p.llistaPlayers[conMenu.jugador].baralla.bA.llistaCartesBaralla){
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
					foreach(cartaUsuari carta in p.llistaPlayers[conMenu.jugador].baralla.c.llistaCartesBaralla){
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
					//p.llistaPlayers[conMenu.jugador].baralla.bA.llistaCartesBaralla.RemoveAt(0);
					reader.Close();
					streamReader.Close();
					memoryStream.Close();
					
                    break;
                default:
                    throw new System.NotSupportedException("Unexpected: " + reader.Name);
            }
        }
		
		ControlGeneralEdicio cE = (ControlGeneralEdicio) Camera.mainCamera.GetComponent("ControlGeneralEdicio");
		cE.iniciarEdicio();
		
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
		//Save(Application.streamingAssetsPath+ "/database.xml", p);
	}
	
	public int searchCartaUsuariMenu(InformacioCartaBonificacio iC){
		int index = 0;
		ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
		for(int i = 0; i < p.llistaPlayers[conMenu.jugador].baralla.c.llistaCartesBaralla.Count; i++){
			cartaUsuari c = p.llistaPlayers[conMenu.jugador].baralla.c.llistaCartesBaralla[i];
			if(c.nom.ToString().Equals(iC.nom) && c.nivell.ToString().Equals(iC.nivell) && c.bonificacioAtacCurt == iC.atac
				&& c.bonificacioAtacLlarg == iC.atac && c.bonificacioDefensa == iC.defensa) index = i;
		}
		return index;
	}
	
	public int searchCartaUsuariMenu(InformacioCartaPersonatge iC){
		int index = 0;
		ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
		for(int i = 0; i < p.llistaPlayers[conMenu.jugador].baralla.c.llistaCartesBaralla.Count; i++){
			cartaUsuari c = p.llistaPlayers[conMenu.jugador].baralla.c.llistaCartesBaralla[i];
			if(c.nom.ToString().Equals(iC.nom) && c.nivell.ToString().Equals(iC.nivell) && c.atacCurtaDistancia == iC.atacCurtaDistancia
				&& c.atacLlargaDistancia == iC.atacLlargaDistancia && c.defensa == iC.defensa && c.moviment == iC.moviment
				&& c.distanciaAtac == iC.distanciaAtac) index = i;
		}
		return index;
	}
	
	public int searchCartaUsuariBaralla(InformacioCartaBonificacio iC){
		int index = 0;
		ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
		for(int i = 0; i < p.llistaPlayers[conMenu.jugador].baralla.bA.llistaCartesBaralla.Count; i++){
			cartaUsuari c = p.llistaPlayers[conMenu.jugador].baralla.bA.llistaCartesBaralla[i];
			if(c.nom.ToString().Equals(iC.nom) && c.nivell.ToString().Equals(iC.nivell) && c.bonificacioAtacCurt == iC.atac
				&& c.bonificacioAtacLlarg == iC.atac && c.bonificacioDefensa == iC.defensa) index = i;
		}
		return index;
	}
	
	public int searchCartaUsuariBaralla(InformacioCartaPersonatge iC){
		int index = 0;
		ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
		for(int i = 0; i < p.llistaPlayers[conMenu.jugador].baralla.bA.llistaCartesBaralla.Count; i++){
			cartaUsuari c = p.llistaPlayers[conMenu.jugador].baralla.bA.llistaCartesBaralla[i];
			if(c.nom.ToString().Equals(iC.nom) && c.nivell.ToString().Equals(iC.nivell) && c.atacCurtaDistancia == iC.atacCurtaDistancia
				&& c.atacLlargaDistancia == iC.atacLlargaDistancia && c.defensa == iC.defensa && c.moviment == iC.moviment
				&& c.distanciaAtac == iC.distanciaAtac) index = i;
		}
		return index;
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
	
	public void UploadFile()
    {
        StartCoroutine(UploadFileCo());
		//UploadFileCo();
    }
	
	public IEnumerator UploadFileCo()
    {
		var serializer = new XmlSerializer(typeof(Players));
		StringWriter text = new StringWriter();
        serializer.Serialize(text, p);
		string uploadText = text.ToString();
		Debug.Log(uploadText);
		byte[] data = GetBytes(uploadText);

        WWWForm postForm = new WWWForm();
        postForm.AddField("Data", uploadText);

        WWW upload = new WWW(Application.streamingAssetsPath + "/database.xml",postForm);        
        while(!upload.isDone)
		{
			yield return new WaitForSeconds(1);
		}
		yield return upload;
        if (upload.error == null)
            Debug.Log("upload done :" + upload.text);
        else
            Debug.Log("Error during upload: " + upload.error);
    }
	
	public byte[] GetBytes(string str)
	{
	    byte[] bytes = new byte[str.Length * sizeof(char)];
	    System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
	    return bytes;
	}

    public static Players Load(string path){
        var serializer = new XmlSerializer(typeof(Players));
        using(var stream = new FileStream(path, FileMode.Open)){
			return serializer.Deserialize(stream) as Players;
        }
    }
	
	public List<InformacioCarta> donaCartesNoves(int index){
		int indexInicial = index*5;
		List<InformacioCarta> cartesReturn = new List<InformacioCarta>();
		if(index <= cartesNoUtilitzades.Count){
			int indexFinal = indexInicial+5;
			if(indexFinal > cartesNoUtilitzades.Count) indexFinal = cartesNoUtilitzades.Count;
			for(int i=indexInicial; i<indexFinal; i++){
				cartesReturn.Add(cartesNoUtilitzades[i]);
			}
		}
		return cartesReturn;
	}
	
	public List<InformacioCarta> donaCartesBaralla(){
		return llistaCartes;
	}
}
