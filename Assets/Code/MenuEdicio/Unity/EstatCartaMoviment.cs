using UnityEngine;
using System.Collections;

public class EstatCartaMoviment : EstatCartaEdicio {
	
	CartaEdicio cartaActual;
	CartaEdicio cartaExterna;
	int pL = 0;
	int pB = 0;
	int posCartaExterna;
	Vector3 posicioInicial;
	Vector3 posicioFinal;
	Vector3 posicioInicialTitol;
	Vector3 posicioFinalTitol;
	Vector3 posicioInicialIcon;
	Vector3 posicioFinalIcon;
	Vector3 posicioInicialAtacLlarg;
	Vector3 posicioFinalAtacLlarg;
	Vector3 posicioInicialAtacCurt;
	Vector3 posicioFinalAtacCurt;
	Vector3 posicioInicialDefensa;
	Vector3 posicioFinalDefensa;
	Vector3 posicioInicialMoviment;
	Vector3 posicioFinalMoviment;
	Vector3 posicioInicialDistanciaAtac;
	Vector3 posicioFinalDistanciaAtac;
	Vector3 posicioInicialTextBonificacio;
	Vector3 posicioFinalTextBonificacio;
	Vector3 posicioInicialTextUber;
	Vector3 posicioFinalTextUber;
	float temps;
	
	public EstatCartaMoviment(CartaEdicio cA, CartaEdicio cE, Vector3 pI, Vector3 pF, float t){
		cartaActual = cA;
		cartaExterna = cE;
		posicioInicial = pI;
		posicioFinal = pF;
		temps = t;
		posicioInicialTitol = cA.titol.transform.position;
		posicioFinalTitol = cE.titol.transform.position;
		posicioInicialIcon = cA.iconCarta.transform.position;
		posicioFinalIcon = cE.iconCarta.transform.position;
		if(cartaActual.tipus.GetType().ToString().Equals("TipusCartaPersonatge")){
			posicioInicialAtacLlarg = cA.atacLlarg.transform.position;
			posicioFinalAtacLlarg = cE.atacLlarg.transform.position;
			posicioInicialAtacCurt = cA.atacCurt.transform.position;
			posicioFinalAtacCurt = cE.atacCurt.transform.position;
			posicioInicialDefensa = cA.defensa.transform.position;
			posicioFinalDefensa = cE.defensa.transform.position;
			posicioInicialMoviment = cA.moviment.transform.position;
			posicioFinalMoviment = cE.moviment.transform.position;
			posicioInicialDistanciaAtac = cA.distanciaAtac.transform.position;
			posicioFinalDistanciaAtac = cE.distanciaAtac.transform.position;
		}else if(cartaActual.tipus.GetType().ToString().Equals("TipusCartaBonificacio")){
			posicioInicialTextBonificacio = cA.textBonificacio.transform.position;
			posicioFinalTextBonificacio = cE.textBonificacio.transform.position;
			if(cartaActual.textUber != null) posicioInicialTextUber = cA.textUber.transform.position;
			if(cartaActual.textUber != null) posicioFinalTextUber = cE.transform.position;
		}
		
		if(cartaExterna.estatAnterior.GetType().ToString().Equals("EstatCartaMenu")) pL = cartaExterna.posicioLlista;
		else if(cartaExterna.estatAnterior.GetType().ToString().Equals("EstatCartaBaralla")){
			pL = cartaExterna.posicioLlista;
			pB = cartaExterna.posicioLlistaBaralla;
		}
		
		ConnexioEdicio con = (ConnexioEdicio) Camera.mainCamera.GetComponent("ConnexioEdicio") as ConnexioEdicio;	
		if(cartaExterna.estatAnterior.GetType().ToString().Equals("EstatCartaMenu")){
			if(cartaExterna.tipus.GetType().ToString().Equals("TipusCartaPersonatge"))
				posCartaExterna = con.searchCartaUsuariMenu((InformacioCartaPersonatge)cartaExterna.iC);
			else if(cartaExterna.tipus.GetType().ToString().Equals("TipusCartaBonificacio"))
				posCartaExterna = con.searchCartaUsuariMenu((InformacioCartaBonificacio)cartaExterna.iC);
		}else if(cartaExterna.estatAnterior.GetType().ToString().Equals("EstatCartaBaralla")){
			if(cartaExterna.tipus.GetType().ToString().Equals("TipusCartaBonificacio"))
				posCartaExterna = con.searchCartaUsuariBaralla((InformacioCartaBonificacio)cartaExterna.iC);
			else if(cartaExterna.tipus.GetType().ToString().Equals("TipusCartaPersonatge"))
				posCartaExterna = con.searchCartaUsuariBaralla((InformacioCartaPersonatge)cartaExterna.iC);
		}
	}
	
	public void pintarCarta(){
		cartaActual.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.titol != null) cartaActual.titol.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.iconCarta != null) cartaActual.iconCarta.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.atacLlarg != null) cartaActual.atacLlarg.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.atacCurt != null) cartaActual.atacCurt.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.defensa != null) cartaActual.defensa.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.distanciaAtac != null) cartaActual.distanciaAtac.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.moviment != null) cartaActual.moviment.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.textBonificacio != null) cartaActual.textBonificacio.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.textUber != null) cartaActual.textUber.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		
		cartaActual.transform.position = Vector3.Lerp (posicioInicial, posicioFinal, Time.time - temps);
		cartaActual.titol.transform.position = Vector3.Lerp (posicioInicialTitol, posicioFinalTitol, Time.time - temps);
		cartaActual.iconCarta.transform.position = Vector3.Lerp (posicioInicialIcon, posicioFinalIcon, Time.time - temps);
		int e = 0;
		if(cartaActual.tipus.GetType().ToString().Equals("TipusCartaPersonatge")){
			cartaActual.atacLlarg.transform.position = Vector3.Lerp (posicioInicialDistanciaAtac, posicioFinalAtacLlarg, Time.time - temps);
			cartaActual.atacCurt.transform.position = Vector3.Lerp (posicioInicialAtacCurt, posicioFinalAtacCurt, Time.time - temps);
			cartaActual.defensa.transform.position = Vector3.Lerp (posicioInicialDefensa, posicioFinalDefensa, Time.time - temps);
			cartaActual.moviment.transform.position = Vector3.Lerp (posicioInicialMoviment, posicioFinalMoviment, Time.time - temps);
			cartaActual.distanciaAtac.transform.position = Vector3.Lerp (posicioInicialDistanciaAtac, posicioFinalDistanciaAtac, Time.time - temps);
		}else if(cartaActual.tipus.GetType().ToString().Equals("TipusCartaBonificacio")){
			cartaActual.textBonificacio.transform.position = Vector3.Lerp (posicioInicialTextBonificacio, posicioFinalTextBonificacio, Time.time - temps);
			if(cartaActual.textUber != null) cartaActual.textUber.transform.position = Vector3.Lerp (posicioInicialTextUber, posicioFinalTextUber, Time.time - temps);
		}
		
		if(cartaActual.gameObject.transform.position == posicioFinal){
			//Debug.Log("FINAL MOVIMENT CARTA");
			cartaActual.desactivarCarta();
			ConnexioEdicio con = (ConnexioEdicio) Camera.mainCamera.GetComponent("ConnexioEdicio") as ConnexioEdicio;
			ConnexioMenus conMenu = (ConnexioMenus) Camera.mainCamera.GetComponent("ConnexioMenus") as ConnexioMenus;
			if(cartaActual.estatAnterior.GetType().ToString().Equals("EstatCartaMenu")){
				cartaActual.donarPosicioLlistaBaralla(pL, pB);
				cartaActual.estat = new EstatCartaBaralla(cartaActual);
				cartaActual.estatAnterior = cartaActual.estat;
				MenuCartesDisponiblesEdicio mE =  (MenuCartesDisponiblesEdicio) GameObject.FindGameObjectWithTag("MenuCartesDisponiblesEdicio").GetComponent("MenuCartesDisponiblesEdicio");
				mE.cartesDisponibles.Remove(cartaActual);
				MenuBaralla mB =  (MenuBaralla) GameObject.FindGameObjectWithTag("MenuBaralla").GetComponent("MenuBaralla");
				mB.cartesDisponibles.Add(cartaActual);
				
				if(cartaActual.tipus.GetType().ToString().Equals("TipusCartaPersonatge")){
					e = con.searchCartaUsuariMenu((InformacioCartaPersonatge)cartaActual.iC);
					con.p.llistaPlayers[conMenu.jugador].baralla.c.llistaCartesBaralla.RemoveAt(e);
					con.cartesNoUtilitzades.RemoveAt(e);
				}
				else if(cartaActual.tipus.GetType().ToString().Equals("TipusCartaBonificacio")){
					e = con.searchCartaUsuariMenu((InformacioCartaBonificacio)cartaActual.iC);
					con.p.llistaPlayers[conMenu.jugador].baralla.c.llistaCartesBaralla.RemoveAt(e);
					con.cartesNoUtilitzades.RemoveAt(e);
				}
				
				Debug.Log(posCartaExterna);
				if(cartaActual.tipus.GetType().ToString().Equals("TipusCartaPersonatge")){
					con.p.llistaPlayers[conMenu.jugador].baralla.bA.llistaCartesBaralla.Insert(posCartaExterna, new cartaUsuari((InformacioCartaPersonatge)cartaActual.iC));
					con.llistaCartes.Insert(posCartaExterna, (InformacioCartaPersonatge)cartaActual.iC);
				}
				else if(cartaActual.tipus.GetType().ToString().Equals("TipusCartaBonificacio")){
					con.p.llistaPlayers[conMenu.jugador].baralla.bA.llistaCartesBaralla.Insert(posCartaExterna, new cartaUsuari((InformacioCartaBonificacio)cartaActual.iC));			
					con.llistaCartes.Insert(posCartaExterna, (InformacioCartaBonificacio)cartaActual.iC);
				}
			
				cartaActual.activarCartaBaralla();
			}else if(cartaActual.estatAnterior.GetType().ToString().Equals("EstatCartaBaralla")){
				cartaActual.donarPosicioLlista(pL);
				cartaActual.estat = new EstatCartaMenu(cartaActual);
				cartaActual.estatAnterior = cartaActual.estat;
				MenuBaralla mB =  (MenuBaralla) GameObject.FindGameObjectWithTag("MenuBaralla").GetComponent("MenuBaralla");
				mB.cartesDisponibles.Remove(cartaActual);
				MenuCartesDisponiblesEdicio mE =  (MenuCartesDisponiblesEdicio) GameObject.FindGameObjectWithTag("MenuCartesDisponiblesEdicio").GetComponent("MenuCartesDisponiblesEdicio");
				mE.cartesDisponibles.Add(cartaActual);
				
				if(cartaActual.tipus.GetType().ToString().Equals("TipusCartaPersonatge")){
					e = con.searchCartaUsuariBaralla((InformacioCartaPersonatge)cartaActual.iC);
					con.p.llistaPlayers[conMenu.jugador].baralla.bA.llistaCartesBaralla.RemoveAt(e);
					con.llistaCartes.RemoveAt(e);
				}
				else if(cartaActual.tipus.GetType().ToString().Equals("TipusCartaBonificacio")){
					e = con.searchCartaUsuariBaralla((InformacioCartaBonificacio)cartaActual.iC);
					con.p.llistaPlayers[conMenu.jugador].baralla.bA.llistaCartesBaralla.RemoveAt(e);
					con.llistaCartes.RemoveAt(e);
				}
				
				Debug.Log(posCartaExterna);
				if(cartaActual.tipus.GetType().ToString().Equals("TipusCartaPersonatge")){
					con.p.llistaPlayers[conMenu.jugador].baralla.c.llistaCartesBaralla.Insert(posCartaExterna, new cartaUsuari((InformacioCartaPersonatge)cartaActual.iC));
					con.cartesNoUtilitzades.Insert(posCartaExterna, (InformacioCartaPersonatge)cartaActual.iC);
				}
				else if(cartaActual.tipus.GetType().ToString().Equals("TipusCartaBonificacio")){
					con.p.llistaPlayers[conMenu.jugador].baralla.c.llistaCartesBaralla.Insert(posCartaExterna, new cartaUsuari((InformacioCartaBonificacio)cartaActual.iC));
					con.cartesNoUtilitzades.Insert(posCartaExterna, (InformacioCartaBonificacio)cartaActual.iC);
				}
			
				cartaActual.activarCarta();
			}
		}
	}
}
