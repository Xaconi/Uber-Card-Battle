using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;

public class Baralla{
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------

	public List<InformacioCarta> llistaCartes;
	public int nCartes;
	public string nivellCartes;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	public Baralla(string tipus){
		nCartes = 0;
		llistaCartes = new List<InformacioCarta>();
		if(tipus.Equals("Usuari")) llegirBaseDades();
		else if(tipus.Equals("RivalLocal"))generarBaralla();
		else if(tipus.Equals("RivalOnline")){
			// Part multijugador, en teoria sincronitzar 
			// baralla per temes de seguretat...	
		}
	}
	
	private void llegirBaseDades(){
		
#if UNITY_ANDROID
		Connexio con = (Connexio) Camera.mainCamera.gameObject.GetComponent("Connexio");
		// En asignar la baralla, obtenim la llista de cartes del XML i la seva quantitat...
		con.assignarBaralla(this);
#elif UNITY_STANDALONE
		Connexio con = (Connexio) Camera.mainCamera.gameObject.GetComponent("Connexio");
		// En asignar la baralla, obtenim la llista de cartes del XML i la seva quantitat...
		con.assignarBaralla(this);
#endif
	}
	
	private void generarBaralla(){
		// Llegim la baralla activa de l'usuari a base de dades
		// De moment generem tot de manera fixa
		
		ControlGeneralJoc cJ = (ControlGeneralJoc) Camera.mainCamera.GetComponent("ControlGeneralJoc");
		nivellCartes = cJ.getNivellCartes();	// <--- nivellCarta
		Debug.Log(nivellCartes);
		System.Random rnd = new System.Random((int) Time.time);
		for(int i=0;i<15;i++){
			int sort = rnd.Next(1,5);	// Mirem quin personatge aleatori generem
			Debug.Log(sort);
			switch(sort){
				case 1:
					llistaCartes.Add(generadorCartaPersonatge("Metralleta", nivellCartes)); // <--- nivellCarta
				break;
				case 2:
					llistaCartes.Add(generadorCartaPersonatge("Franco", nivellCartes)); // <--- nivellCarta
				break;
				case 3:
					llistaCartes.Add(generadorCartaPersonatge("Bazooka", nivellCartes)); // <--- nivellCarta
				break;
				case 4:
					llistaCartes.Add(generadorCartaPersonatge("Guerrero", nivellCartes)); // <--- nivellCarta
				break;
			}
			nCartes++;
		}
		
		System.Random rnd2 = new System.Random((int) Time.time);
		for(int i=0;i<6;i++){
			int sort2 = rnd2.Next(1,4);	// Mirem quin personatge aleatori generem
			switch(sort2){
				case 1:
					llistaCartes.Add(generadorCartaBonificacio("Atac", nivellCartes)); // <--- nivellCarta
				break;
				case 2:
					llistaCartes.Add(generadorCartaBonificacio("Defensa", nivellCartes)); // <--- nivellCarta
				break;
				case 3:
					llistaCartes.Add(generadorCartaBonificacio("Uber", nivellCartes)); // <--- nivellCarta
				break;
			}
			nCartes++;
		}
	}
	
	public InformacioCartaPersonatge generadorCartaPersonatge(string personatge, string nivell){
		System.Random rnd = new System.Random();
		Debug.Log(nivell);
		switch(nivell){
			case "Bronze":
				switch(personatge){
					case "Metralleta":
						Debug.Log ("PASO");
						return new InformacioCartaPersonatge("Personatge",
							"Metralleta",
							 "Bronze", 
							 rnd.Next(Dades.bronze.dadaPersonatge.metralleta.atacLBaix, Dades.bronze.dadaPersonatge.metralleta.atacLAlt+1),
							 rnd.Next(Dades.bronze.dadaPersonatge.metralleta.atacCBaix, Dades.bronze.dadaPersonatge.metralleta.atacCAlt+1),
							 rnd.Next(Dades.bronze.dadaPersonatge.metralleta.defensaBaix, Dades.bronze.dadaPersonatge.metralleta.defensaBaix+1),
							Dades.bronze.dadaPersonatge.metralleta.moviment, Dades.bronze.dadaPersonatge.metralleta.distanciaA);
					break;
					case "Franco":
						Debug.Log ("PASO");
						return new InformacioCartaPersonatge("Personatge",
							"Franco",
							 "Bronze", 
							 rnd.Next(Dades.bronze.dadaPersonatge.franco.atacLBaix, Dades.bronze.dadaPersonatge.franco.atacLAlt+1),
							 rnd.Next(Dades.bronze.dadaPersonatge.franco.atacCBaix, Dades.bronze.dadaPersonatge.franco.atacCAlt+1),
							 rnd.Next(Dades.bronze.dadaPersonatge.franco.defensaBaix, Dades.bronze.dadaPersonatge.franco.defensaBaix+1),
							Dades.bronze.dadaPersonatge.franco.moviment, Dades.bronze.dadaPersonatge.franco.distanciaA);
					break;
					case "Bazooka":
						Debug.Log ("PASO");
						return new InformacioCartaPersonatge("Personatge",
							"Bazooka",
							 "Bronze", 
							 rnd.Next(Dades.bronze.dadaPersonatge.bazooka.atacLBaix, Dades.bronze.dadaPersonatge.bazooka.atacLAlt+1),
							 rnd.Next(Dades.bronze.dadaPersonatge.bazooka.atacCBaix, Dades.bronze.dadaPersonatge.bazooka.atacCAlt+1),
							 rnd.Next(Dades.bronze.dadaPersonatge.bazooka.defensaBaix, Dades.bronze.dadaPersonatge.bazooka.defensaBaix+1),
							Dades.bronze.dadaPersonatge.bazooka.moviment, Dades.bronze.dadaPersonatge.bazooka.distanciaA);
					break;
					case "Guerrero":
						Debug.Log ("PASO");
						return new InformacioCartaPersonatge("Personatge",
							"Guerrero",
							 "Bronze", 
							 rnd.Next(Dades.bronze.dadaPersonatge.guerrero.atacLBaix, Dades.bronze.dadaPersonatge.guerrero.atacLAlt+1),
							 rnd.Next(Dades.bronze.dadaPersonatge.guerrero.atacCBaix, Dades.bronze.dadaPersonatge.guerrero.atacCAlt+1),
							 rnd.Next(Dades.bronze.dadaPersonatge.guerrero.defensaBaix, Dades.bronze.dadaPersonatge.guerrero.defensaBaix+1),
							Dades.bronze.dadaPersonatge.guerrero.moviment, Dades.bronze.dadaPersonatge.guerrero.distanciaA);
					break;
				}
			break;
			case "Plata":
				switch(personatge){
					case "Metralleta":
						return new InformacioCartaPersonatge("Personatge",
							"Metralleta",
							 "Plata", 
							 rnd.Next(Dades.plata.dadaPersonatge.metralleta.atacLBaix, Dades.plata.dadaPersonatge.metralleta.atacLAlt+1),
							 rnd.Next(Dades.plata.dadaPersonatge.metralleta.atacCBaix, Dades.plata.dadaPersonatge.metralleta.atacCAlt+1),
							 rnd.Next(Dades.plata.dadaPersonatge.metralleta.defensaBaix, Dades.plata.dadaPersonatge.metralleta.defensaBaix+1),
							Dades.plata.dadaPersonatge.metralleta.moviment, Dades.plata.dadaPersonatge.metralleta.distanciaA);
					break;
					case "Franco":
						return new InformacioCartaPersonatge("Personatge",
							"Franco",
							 "Plata", 
							 rnd.Next(Dades.plata.dadaPersonatge.franco.atacLBaix, Dades.plata.dadaPersonatge.franco.atacLAlt+1),
							 rnd.Next(Dades.plata.dadaPersonatge.franco.atacCBaix, Dades.plata.dadaPersonatge.franco.atacCAlt+1),
							 rnd.Next(Dades.plata.dadaPersonatge.franco.defensaBaix, Dades.plata.dadaPersonatge.franco.defensaBaix+1),
							Dades.plata.dadaPersonatge.franco.moviment, Dades.plata.dadaPersonatge.franco.distanciaA);
					break;
					case "Bazooka":
						return new InformacioCartaPersonatge("Personatge",
							"Bazooka",
							 "Plata", 
							 rnd.Next(Dades.plata.dadaPersonatge.bazooka.atacLBaix, Dades.plata.dadaPersonatge.bazooka.atacLAlt+1),
							 rnd.Next(Dades.plata.dadaPersonatge.bazooka.atacCBaix, Dades.plata.dadaPersonatge.bazooka.atacCAlt+1),
							 rnd.Next(Dades.plata.dadaPersonatge.bazooka.defensaBaix, Dades.plata.dadaPersonatge.bazooka.defensaBaix+1),
							Dades.plata.dadaPersonatge.bazooka.moviment, Dades.plata.dadaPersonatge.bazooka.distanciaA);
					break;
					case "Guerrero":
						return new InformacioCartaPersonatge("Personatge",
							"Guerrero",
							 "Plata", 
							 rnd.Next(Dades.plata.dadaPersonatge.guerrero.atacLBaix, Dades.plata.dadaPersonatge.guerrero.atacLAlt+1),
							 rnd.Next(Dades.plata.dadaPersonatge.guerrero.atacCBaix, Dades.plata.dadaPersonatge.guerrero.atacCAlt+1),
							 rnd.Next(Dades.plata.dadaPersonatge.guerrero.defensaBaix, Dades.plata.dadaPersonatge.guerrero.defensaBaix+1),
							Dades.plata.dadaPersonatge.guerrero.moviment, Dades.plata.dadaPersonatge.guerrero.distanciaA);
					break;
				}
			break;
			case "Or":
				switch(personatge){
					case "Metralleta":
						return new InformacioCartaPersonatge("Personatge",
							"Metralleta",
							 "Or", 
							 rnd.Next(Dades.or.dadaPersonatge.metralleta.atacLBaix, Dades.or.dadaPersonatge.metralleta.atacLAlt+1),
							 rnd.Next(Dades.or.dadaPersonatge.metralleta.atacCBaix, Dades.or.dadaPersonatge.metralleta.atacCAlt+1),
							 rnd.Next(Dades.or.dadaPersonatge.metralleta.defensaBaix, Dades.or.dadaPersonatge.metralleta.defensaBaix+1),
							Dades.or.dadaPersonatge.metralleta.moviment, Dades.or.dadaPersonatge.metralleta.distanciaA);
					break;
					case "Franco":
						return new InformacioCartaPersonatge("Personatge",
							"Franco",
							 "Or", 
							 rnd.Next(Dades.or.dadaPersonatge.franco.atacLBaix, Dades.or.dadaPersonatge.franco.atacLAlt+1),
							 rnd.Next(Dades.or.dadaPersonatge.franco.atacCBaix, Dades.or.dadaPersonatge.franco.atacCAlt+1),
							 rnd.Next(Dades.or.dadaPersonatge.franco.defensaBaix, Dades.or.dadaPersonatge.franco.defensaBaix+1),
							Dades.or.dadaPersonatge.franco.moviment, Dades.or.dadaPersonatge.franco.distanciaA);
					break;
					case "Bazooka":
						return new InformacioCartaPersonatge("Personatge",
							"Bazooka",
							 "Or", 
							 rnd.Next(Dades.or.dadaPersonatge.bazooka.atacLBaix, Dades.or.dadaPersonatge.bazooka.atacLAlt+1),
							 rnd.Next(Dades.or.dadaPersonatge.bazooka.atacCBaix, Dades.or.dadaPersonatge.bazooka.atacCAlt+1),
							 rnd.Next(Dades.or.dadaPersonatge.bazooka.defensaBaix, Dades.or.dadaPersonatge.bazooka.defensaBaix+1),
							Dades.or.dadaPersonatge.bazooka.moviment, Dades.or.dadaPersonatge.bazooka.distanciaA);
					break;
					case "Guerrero":
						return new InformacioCartaPersonatge("Personatge",
							"Guerrero",
							 "Or", 
							 rnd.Next(Dades.or.dadaPersonatge.guerrero.atacLBaix, Dades.or.dadaPersonatge.guerrero.atacLAlt+1),
							 rnd.Next(Dades.or.dadaPersonatge.guerrero.atacCBaix, Dades.or.dadaPersonatge.guerrero.atacCAlt+1),
							 rnd.Next(Dades.or.dadaPersonatge.guerrero.defensaBaix, Dades.or.dadaPersonatge.guerrero.defensaBaix+1),
							Dades.or.dadaPersonatge.guerrero.moviment, Dades.or.dadaPersonatge.guerrero.distanciaA);
					break;
				}
			break;
			case "Plati":
				switch(personatge){
					case "Metralleta":
						return new InformacioCartaPersonatge("Personatge",
							"Metralleta",
							 "Plati", 
							 rnd.Next(Dades.plati.dadaPersonatge.metralleta.atacLBaix, Dades.plati.dadaPersonatge.metralleta.atacLAlt+1),
							 rnd.Next(Dades.plati.dadaPersonatge.metralleta.atacCBaix, Dades.plati.dadaPersonatge.metralleta.atacCAlt+1),
							 rnd.Next(Dades.plati.dadaPersonatge.metralleta.defensaBaix, Dades.plati.dadaPersonatge.metralleta.defensaBaix+1),
							Dades.plati.dadaPersonatge.metralleta.moviment, Dades.plati.dadaPersonatge.metralleta.distanciaA);
					break;
					case "Franco":
						return new InformacioCartaPersonatge("Personatge",
							"Franco",
							 "Plati", 
							 rnd.Next(Dades.plati.dadaPersonatge.franco.atacLBaix, Dades.plati.dadaPersonatge.franco.atacLAlt+1),
							 rnd.Next(Dades.plati.dadaPersonatge.franco.atacCBaix, Dades.plati.dadaPersonatge.franco.atacCAlt+1),
							 rnd.Next(Dades.plati.dadaPersonatge.franco.defensaBaix, Dades.plati.dadaPersonatge.franco.defensaBaix+1),
							Dades.plati.dadaPersonatge.franco.moviment, Dades.plati.dadaPersonatge.franco.distanciaA);
					break;
					case "Bazooka":
						return new InformacioCartaPersonatge("Personatge",
							"Bazooka",
							 "Plati", 
							 rnd.Next(Dades.plati.dadaPersonatge.bazooka.atacLBaix, Dades.plati.dadaPersonatge.bazooka.atacLAlt+1),
							 rnd.Next(Dades.plati.dadaPersonatge.bazooka.atacCBaix, Dades.plati.dadaPersonatge.bazooka.atacCAlt+1),
							 rnd.Next(Dades.plati.dadaPersonatge.bazooka.defensaBaix, Dades.plati.dadaPersonatge.bazooka.defensaBaix+1),
							Dades.plati.dadaPersonatge.bazooka.moviment, Dades.plati.dadaPersonatge.bazooka.distanciaA);
					break;
					case "Guerrero":
						return new InformacioCartaPersonatge("Personatge",
							"Guerrero",
							 "Plati", 
							 rnd.Next(Dades.plati.dadaPersonatge.guerrero.atacLBaix, Dades.plati.dadaPersonatge.guerrero.atacLAlt+1),
							 rnd.Next(Dades.plati.dadaPersonatge.guerrero.atacCBaix, Dades.plati.dadaPersonatge.guerrero.atacCAlt+1),
							 rnd.Next(Dades.plati.dadaPersonatge.guerrero.defensaBaix, Dades.plati.dadaPersonatge.guerrero.defensaBaix+1),
							Dades.plati.dadaPersonatge.guerrero.moviment, Dades.plati.dadaPersonatge.guerrero.distanciaA);
					break;
				}
			break;
		}
		return null;
	}
	
	public InformacioCartaBonificacio generadorCartaBonificacio(string bonificacio, string nivell){
		System.Random rnd = new System.Random();
		switch(nivell){
			case "Bronze":
				switch(bonificacio){
					case "Atac":
						return new InformacioCartaBonificacio("Bonificacio",
							"Atac",
							 "Bronze",
							 rnd.Next(Dades.bronze.dadaBonificacio.atac.atacBaix, Dades.bronze.dadaBonificacio.atac.atacAlt+1),
							 0);
					break;	
					case "Defensa":
						return new InformacioCartaBonificacio("Bonificacio",
							"Defensa",
							 "Bronze",
							 0,
							 rnd.Next(Dades.bronze.dadaBonificacio.defensa.defensaBaix, Dades.bronze.dadaBonificacio.defensa.defensaBaix+1));
					break;
					case "Uber":
						return new InformacioCartaBonificacio("Bonificacio",
							"Uber",
							 "Bronze",
							  rnd.Next(Dades.bronze.dadaBonificacio.uber.atacBaix, Dades.bronze.dadaBonificacio.uber.atacAlt+1),
							  rnd.Next(Dades.bronze.dadaBonificacio.uber.defensaBaix, Dades.bronze.dadaBonificacio.uber.defensaBaix+1));
					break;
				}
			break;	
			case "Plata":
				switch(bonificacio){
					case "Atac":
						return new InformacioCartaBonificacio("Bonificacio",
							"Atac",
							 "Plata",
							 rnd.Next(Dades.plata.dadaBonificacio.atac.atacBaix, Dades.plata.dadaBonificacio.atac.atacAlt+1),
							 0);
					break;	
					case "Defensa":
						return new InformacioCartaBonificacio("Bonificacio",
							"Defensa",
							 "Plata",
							 0,
							 rnd.Next(Dades.plata.dadaBonificacio.defensa.defensaBaix, Dades.plata.dadaBonificacio.defensa.defensaBaix+1));
					break;
					case "Uber":
						return new InformacioCartaBonificacio("Bonificacio",
							"Uber",
							 "Plata",
							  rnd.Next(Dades.plata.dadaBonificacio.uber.atacBaix, Dades.plata.dadaBonificacio.uber.atacAlt+1),
							  rnd.Next(Dades.plata.dadaBonificacio.uber.defensaBaix, Dades.plata.dadaBonificacio.uber.defensaBaix+1));
					break;
				}
			break;
			case "Or":
				switch(bonificacio){
					case "Atac":
						return new InformacioCartaBonificacio("Bonificacio",
							"Atac",
							 "Or",
							 rnd.Next(Dades.or.dadaBonificacio.atac.atacBaix, Dades.or.dadaBonificacio.atac.atacAlt+1),
							 0);
					break;	
					case "Defensa":
						return new InformacioCartaBonificacio("Bonificacio",
							"Defensa",
							 "Or",
							 0,
							 rnd.Next(Dades.or.dadaBonificacio.defensa.defensaBaix, Dades.or.dadaBonificacio.defensa.defensaBaix+1));
					break;
					case "Uber":
						return new InformacioCartaBonificacio("Bonificacio",
							"Uber",
							 "Or",
							  rnd.Next(Dades.or.dadaBonificacio.uber.atacBaix, Dades.or.dadaBonificacio.uber.atacAlt+1),
							  rnd.Next(Dades.or.dadaBonificacio.uber.defensaBaix, Dades.or.dadaBonificacio.uber.defensaBaix+1));
					break;
				}
			break;
			case "Plati":
				switch(bonificacio){
					case "Atac":
						return new InformacioCartaBonificacio("Bonificacio",
							"Atac",
							 "Plati",
							 rnd.Next(Dades.plati.dadaBonificacio.atac.atacBaix, Dades.plati.dadaBonificacio.atac.atacAlt+1),
							 0);
					break;	
					case "Defensa":
						return new InformacioCartaBonificacio("Bonificacio",
							"Defensa",
							 "Plati",
							 0,
							 rnd.Next(Dades.plati.dadaBonificacio.defensa.defensaBaix, Dades.plati.dadaBonificacio.defensa.defensaBaix+1));
					break;
					case "Uber":
						return new InformacioCartaBonificacio("Bonificacio",
							"Uber",
							 "Plati",
							  rnd.Next(Dades.plati.dadaBonificacio.uber.atacBaix, Dades.plati.dadaBonificacio.uber.atacAlt+1),
							  rnd.Next(Dades.plati.dadaBonificacio.uber.defensaBaix, Dades.plati.dadaBonificacio.uber.defensaBaix+1));
					break;
				}
			break;
		}
		return null;
	}
	
	public List<InformacioCarta> donaCartesNoves(int n){
		List<InformacioCarta> iC = new List<InformacioCarta>();
		int limit = n;
		if(n <= nCartes){
			// Donar les n cartes que demanen, de manera aleatòria, esborrant-les de la llista...
			// Si no son les últimes cartes, sempre ha de tornar un personatge com a mínim...
			System.Random rnd = new System.Random((int) Time.time);
			for(int i=0;i<n;i++){
				int index = rnd.Next(0, llistaCartes.Count);
				Debug.Log(index);
				while(i == 0 && !llistaCartes[index].GetType().ToString().Equals("InformacioCartaPersonatge"))
					index = rnd.Next(0, llistaCartes.Count);
				iC.Add(llistaCartes[index]);
				llistaCartes.RemoveAt(index);
				nCartes--;
				limit--;
			}
		}else{
			Debug.Log("Han demanat " + n + " cartes i la baralla no te tantes. En te " + nCartes);
			// Donar totes les cartes restants de la baralla, si en te...
			int limit2 = llistaCartes.Count-1;
			int final = llistaCartes.Count;
			for(int i=0;i<final;i++){
				int index = UnityEngine.Random.Range(0, limit2);
				Debug.Log (index);
				iC.Add(llistaCartes[index]);
				llistaCartes.RemoveAt(index);
				nCartes--;
				limit2--;
			}
		}
		return iC;
	}
	
	public bool comprovarPersonatges(){
		int i = 0;
		bool personatgeTrobat = false;
		while(i < llistaCartes.Count && !personatgeTrobat){
			if(llistaCartes[i].GetType().ToString() == "InformacioCartaPersonatge"){
				personatgeTrobat = true;
			}
			i++;
		}
		return personatgeTrobat;
	}
}

[XmlRoot("players")]
public class Players{
	
	public Players(){
		llistaPlayers = new List<Player>();
	}
	
	[XmlElement("player")]
	public List<Player> llistaPlayers;
}

[XmlRoot("player")]
public class Player{
	
	public Player(){
		dU = new dadesUsuari();
		e = new estadistiques();
		baralla = new barallaUsuari();
	}
	
	[XmlElement("dadesUsuari")]
	public dadesUsuari dU;
	[XmlElement("estadistiques")]
	public estadistiques e;
	[XmlElement("barallaUsuari")]
	public barallaUsuari baralla;
}

[XmlRoot("dadesUsuari")]
public class dadesUsuari{
	
	public dadesUsuari(){
		nom = "";
		activat = 0;
		dH = new dadesHistoria();
	}
	
	public dadesUsuari(string n, int a){
		nom = n;
		activat = a;
	}
	
	[XmlElement("nom")]
	public string nom;
	
	[XmlElement("activat")]
	public int activat;
	
	[XmlElement("dadesHistoria")]
	public dadesHistoria dH;
}

[XmlRoot("dadesHistoria")]
public class dadesHistoria{
	
	public dadesHistoria(){
		llistaNivells = new List<int>();
	}
	
	[XmlElement("nivell")]
	public List<int> llistaNivells;
}

[XmlRoot("estadistiques")]
public class estadistiques{
	
	public estadistiques(){
		v = 0;
		d = 0;
	}
	
	public estadistiques(int x, int y, int a, int b, int c, int d, Temps t, TempsHistoria th, TempsQuick tq, int e, int f){
		v = x;
		d = y;
		vH = a;
		dH = b;
		vQ = c;
		dQ = d;
		temps = t;
		tempsHistoria = th;
		tempsQuick = tq;
		atacFet = e;
		atacRebut = f;
	}
	
	[XmlElement("victories")]
	public int v;
	
	[XmlElement("derrotes")]
	public int d;
	
	[XmlElement("victoriesHistoria")]
	public int vH;
	
	[XmlElement("derrotesHistoria")]
	public int dH;
	
	[XmlElement("victoriesQuick")]
	public int vQ;
	
	[XmlElement("derrotesQuick")]
	public int dQ;
	
	[XmlElement("temps")]
	public Temps temps;
	
	[XmlElement("tempsHistoria")]
	public TempsHistoria tempsHistoria;
	
	[XmlElement("tempsQuick")]
	public TempsQuick tempsQuick;
	
	[XmlElement("atacFet")]
	public int atacFet;
	
	[XmlElement("atacRebut")]
	public int atacRebut;
	
	[XmlElement("cartesUtilitzades")]
	public int cartesUtilitzades;
}

[XmlRoot("temps")]
public class Temps{
	
	public Temps(){
		hores = 0;
		minuts = 0;
		segons = 0;
	}
	
	[XmlElement("hores")]
	public int hores;
	[XmlElement("minuts")]
	public int minuts;
	[XmlElement("segons")]
	public int segons;
}

[XmlRoot("tempsHistoria")]
public class TempsHistoria{
	
	public TempsHistoria(){
		hores = 0;
		minuts = 0;
		segons = 0;
	}
	
	[XmlElement("hores")]
	public int hores;
	[XmlElement("minuts")]
	public int minuts;
	[XmlElement("segons")]
	public int segons;
}

[XmlRoot("tempsQuick")]
public class TempsQuick{
	
	public TempsQuick(){
		hores = 0;
		minuts = 0;
		segons = 0;
	}
	
	[XmlElement("hores")]
	public int hores;
	[XmlElement("minuts")]
	public int minuts;
	[XmlElement("segons")]
	public int segons;
}

[XmlRoot("barallaUsuari")]
public class barallaUsuari{
	
	public barallaUsuari(){
		formacio = "";
		bA = new barallaActual();
		c = new cartes();
	}
	
	[XmlElement("formacio")]
	public string formacio;
	[XmlElement("barallaActual")]
	public barallaActual bA;
	[XmlElement("cartes")]
	public cartes c;
}

[XmlRoot("barallaActual")]
public class barallaActual{
	
	public barallaActual(){
		llistaCartesBaralla = new List<cartaUsuari>();
	}
	
	[XmlElement("cartaUsuari")]
	public List<cartaUsuari> llistaCartesBaralla;
}

[XmlRoot("cartes")]
public class cartes{
	public cartes(){
		llistaCartesBaralla = new List<cartaUsuari>();
	}
	
	[XmlElement("cartaUsuari")]
	public List<cartaUsuari> llistaCartesBaralla;
}

[XmlRoot("cartaUsuari")]
public class cartaUsuari{
	
	public cartaUsuari(){
	}
	
	public cartaUsuari(string n, string t, string niv, int aL, int aC, int d, int m, int dA){
		nom = n;
		tipus = t;
		nivell = niv;
		atacLlargaDistancia = aL;
		atacCurtaDistancia = aC;
		defensa = d;
		moviment = m;
		distanciaAtac = dA;
	}
	
	public cartaUsuari(InformacioCartaPersonatge iC){
		nom = iC.nom;
		tipus = iC.tipus;
		nivell = iC.nivell;
		atacLlargaDistancia = iC.atacLlargaDistancia;
		atacCurtaDistancia = iC.atacCurtaDistancia;
		defensa = iC.defensa;
		moviment = iC.moviment;
		distanciaAtac = iC.distanciaAtac;
	}
	
	public cartaUsuari(InformacioCartaBonificacio iC){
		nom = iC.nom;
		tipus = iC.tipus;
		nivell = iC.nivell;
		bonificacioAtacCurt = iC.atac;
		bonificacioAtacLlarg = iC.atac;
		bonificacioDefensa = iC.defensa;
	}
	
	public cartaUsuari(string n, string t, string niv, int bAC, int bAL, int bD){
		nom = n;
		nivell = niv;
		bonificacioAtacCurt = bAC;
		bonificacioAtacLlarg = bAL;
		bonificacioDefensa = bD;
	}
	
	[XmlElement("nom")]
	public string nom;
	[XmlElement("tipus")]
	public string tipus;
	[XmlElement("nivell")]
	public string nivell;
	[XmlElement("atacLlargaDistancia")]
	public int atacLlargaDistancia;
	[XmlElement("atacCurtaDistancia")]
	public int atacCurtaDistancia;
	[XmlElement("defensa")]
	public int defensa;
	[XmlElement("moviment")]
	public int moviment;
	[XmlElement("distanciaAtac")]
	public int distanciaAtac;
	[XmlElement("bonificacioAtacCurt")]
	public int bonificacioAtacCurt;
	[XmlElement("bonificacioAtacLlarg")]
	public int bonificacioAtacLlarg;
	[XmlElement("bonificacioDefensa")]
	public int bonificacioDefensa;
}
