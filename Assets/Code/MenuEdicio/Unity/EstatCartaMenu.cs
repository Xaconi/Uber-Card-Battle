using UnityEngine;
using System.Collections;

public class EstatCartaMenu : EstatCartaEdicio {

	private CartaEdicio cartaActual;
	private Vector3 posicio;
	private TextMesh tM;
	private Color color;
	float pas = 0.1f;
	
	public EstatCartaMenu(CartaEdicio c){
		cartaActual = c;
		posicio = cartaActual.transform.position;
		cartaActual.tag = "Carta";
		cartaActual.gameObject.renderer.enabled = true;
		
		if(cartaActual.iC.GetType().ToString().Equals("InformacioCartaPersonatge")){
			string nivell = ((InformacioCartaPersonatge)cartaActual.iC).nivell.ToString();
			if(nivell.Equals("Bronze")){
				cartaActual.gameObject.renderer.material = Resources.Load("CartaBronze") as Material;
			}else if(nivell.Equals("Plata")){
				cartaActual.gameObject.renderer.material = Resources.Load("CartaPlata") as Material;
			}else if(nivell.Equals("Or")){
				cartaActual.gameObject.renderer.material = Resources.Load("CartaDaurada") as Material;
			}else if(nivell.Equals("Plati")){
				cartaActual.gameObject.renderer.material = Resources.Load("CartaPlati") as Material;
			}
			
			if(cartaActual.titol == null) cartaActual.titol = GameObject.Instantiate(Resources.Load("TextCarta")) as GameObject;
			if(cartaActual.atacLlarg == null) cartaActual.atacLlarg = GameObject.Instantiate(Resources.Load("TextCarta")) as GameObject;
			if(cartaActual.atacCurt == null) cartaActual.atacCurt = GameObject.Instantiate(Resources.Load("TextCarta")) as GameObject;
			if(cartaActual.defensa == null) cartaActual.defensa = GameObject.Instantiate(Resources.Load("TextCarta")) as GameObject;
			if(cartaActual.distanciaAtac == null) cartaActual.distanciaAtac = GameObject.Instantiate(Resources.Load("TextCarta")) as GameObject;
			if(cartaActual.moviment == null) cartaActual.moviment = GameObject.Instantiate(Resources.Load("TextCarta")) as GameObject;
			
			string nom = ((InformacioCartaPersonatge)cartaActual.iC).nom.ToString();
			if(nom.Equals("Metralleta")){
				if(cartaActual.iconCarta == null) cartaActual.iconCarta = GameObject.Instantiate(Resources.Load("CapMetralleta")) as GameObject;
			}else if(nom.Equals("Guerrero")){
				if(cartaActual.iconCarta == null) cartaActual.iconCarta = GameObject.Instantiate(Resources.Load("CapGuerrero")) as GameObject;
			}else if(nom.Equals("Franco")){
				if(cartaActual.iconCarta == null) cartaActual.iconCarta = GameObject.Instantiate(Resources.Load("CapFranco")) as GameObject;
			}else if(nom.Equals("Bazooka")){
				if(cartaActual.iconCarta == null) cartaActual.iconCarta = GameObject.Instantiate(Resources.Load("CapBazooka")) as GameObject;
			}
		}else if(cartaActual.iC.GetType().ToString().Equals("InformacioCartaBonificacio")){
			
			string nivell = ((InformacioCartaBonificacio)cartaActual.iC).nivell.ToString();
			if(nivell.Equals("Bronze")){
				cartaActual.gameObject.renderer.material = Resources.Load("CartaBronze") as Material;
			}else if(nivell.Equals("Plata")){
				cartaActual.gameObject.renderer.material = Resources.Load("CartaPlata") as Material;
			}else if(nivell.Equals("Or")){
				cartaActual.gameObject.renderer.material = Resources.Load("CartaDaurada") as Material;
			}else if(nivell.Equals("Plati")){
				cartaActual.gameObject.renderer.material = Resources.Load("CartaPlati") as Material;
			}
			
			if(cartaActual.titol == null) cartaActual.titol = GameObject.Instantiate(Resources.Load("TextCarta")) as GameObject;
			if(cartaActual.textBonificacio == null) cartaActual.textBonificacio = GameObject.Instantiate(Resources.Load("TextCarta")) as GameObject;
			
			string nom = ((InformacioCartaBonificacio)cartaActual.iC).nom.ToString();
			if(nom.Equals("Atac")){
				if(cartaActual.iconCarta == null) cartaActual.iconCarta = GameObject.Instantiate(Resources.Load("Espasa")) as GameObject;
			}else if(nom.Equals("Defensa")){
				if(cartaActual.iconCarta == null) cartaActual.iconCarta = GameObject.Instantiate(Resources.Load("Escut")) as GameObject;
			}else if(nom.Equals("Uber")){
				if(cartaActual.iconCarta == null) cartaActual.iconCarta = GameObject.Instantiate(Resources.Load("Uber")) as GameObject;
				if(cartaActual.textUber == null) cartaActual.textUber = GameObject.Instantiate(Resources.Load("TextCarta")) as GameObject;
			}
		}
		
		float ratio = Camera.mainCamera.aspect;
		float ratioOriginal = 16.0f/9.0f;
		float diferenciaRatio = ratio / ratioOriginal;
		
		if(diferenciaRatio > 0.85f){
			cartaActual.titol.transform.position = new Vector3((cartaActual.transform.position.x + 0.51f * cartaActual.posicioLlista) + 1.1f,
				cartaActual.transform.position.y + 4.1f,
				cartaActual.transform.position.z + 2);
			if(cartaActual.iC.GetType().ToString().Equals("InformacioCartaPersonatge")){
				cartaActual.atacLlarg.transform.position = new Vector3((cartaActual.transform.position.x + 0.51f * cartaActual.posicioLlista) + 1.1f,
					cartaActual.transform.position.y + 0.3f,
					cartaActual.transform.position.z + 2);
				cartaActual.atacCurt.transform.position = new Vector3((cartaActual.transform.position.x + 0.51f * cartaActual.posicioLlista) + 1.1f,
					cartaActual.transform.position.y - 0.6f,
					cartaActual.transform.position.z + 2);
				cartaActual.defensa.transform.position = new Vector3((cartaActual.transform.position.x + 0.51f * cartaActual.posicioLlista) + 1.1f,
					cartaActual.transform.position.y - 1.5f,
					cartaActual.transform.position.z + 2);
				cartaActual.moviment.transform.position = new Vector3((cartaActual.transform.position.x + 0.51f * cartaActual.posicioLlista) - 1.2f,
					cartaActual.transform.position.y - 0.15f,
					cartaActual.transform.position.z + 2);
				cartaActual.distanciaAtac.transform.position = new Vector3((cartaActual.transform.position.x + 0.51f * cartaActual.posicioLlista) - 1.2f,
					cartaActual.transform.position.y - 1.05f,
					cartaActual.transform.position.z + 2);
				cartaActual.iconCarta.transform.position = new Vector3((cartaActual.transform.position.x + 0.51f * cartaActual.posicioLlista) - 0.9f,
					cartaActual.transform.position.y + 2.2f,
					cartaActual.transform.position.z + 2);
			}else if(cartaActual.iC.GetType().ToString().Equals("InformacioCartaBonificacio")){
				cartaActual.textBonificacio.transform.position = new Vector3((cartaActual.transform.position.x + 0.57f * cartaActual.posicioLlista) - 0.15f,
					cartaActual.transform.position.y - 0.1f,
					cartaActual.transform.position.z + 2);
				string nom = ((InformacioCartaBonificacio)cartaActual.iC).nom.ToString();
				if(nom.Equals("Atac")){
					cartaActual.iconCarta.transform.position = new Vector3((cartaActual.transform.position.x + 0.51f * cartaActual.posicioLlista) - 0.9f,
						cartaActual.transform.position.y + 2.1f,
						cartaActual.transform.position.z + 2);
				}else if(nom.Equals("Defensa")){
					cartaActual.iconCarta.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
					cartaActual.iconCarta.transform.position = new Vector3((cartaActual.transform.position.x + 0.51f * cartaActual.posicioLlista) - 1.0f,
						cartaActual.transform.position.y + 2.2f,
						cartaActual.transform.position.z + 2);
				}else if(nom.Equals("Uber")){
					cartaActual.textUber.transform.position = new Vector3((cartaActual.transform.position.x + 0.57f * cartaActual.posicioLlista) - 0.15f,
						cartaActual.transform.position.y - 1.15f,
						cartaActual.transform.position.z + 2);
					cartaActual.iconCarta.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
					cartaActual.iconCarta.transform.position = new Vector3((cartaActual.transform.position.x + 0.51f * cartaActual.posicioLlista) - 0.9f,
						cartaActual.transform.position.y + 2.3f,
						cartaActual.transform.position.z + 2);
				}
			}
		}else if(ratio == 4.0f/3.0f){
			cartaActual.titol.transform.position = new Vector3((cartaActual.transform.position.x + 0.51f * cartaActual.posicioLlista) + 1.1f,
				cartaActual.transform.position.y + 4.1f,
				cartaActual.transform.position.z + 2);
		}
		
		tM = cartaActual.titol.GetComponent("TextMesh") as TextMesh;
		tM.fontSize = 70;
		if(cartaActual.iC.GetType().ToString().Equals("InformacioCartaPersonatge")){
			tM.text = ((InformacioCartaPersonatge)cartaActual.iC).nom;
			
			tM = cartaActual.atacLlarg.GetComponent("TextMesh") as TextMesh;
			tM.fontSize = 70;
			tM.text = "ATK = " + ((InformacioCartaPersonatge)cartaActual.iC).atacLlargaDistancia.ToString();
			
			tM = cartaActual.atacCurt.GetComponent("TextMesh") as TextMesh;
			tM.fontSize = 70;
			tM.text = "ATC = " + ((InformacioCartaPersonatge)cartaActual.iC).atacCurtaDistancia.ToString();
			
			tM = cartaActual.defensa.GetComponent("TextMesh") as TextMesh;
			tM.fontSize = 70;
			tM.text = "DEF = " + ((InformacioCartaPersonatge)cartaActual.iC).defensa.ToString();
			
			tM = cartaActual.moviment.GetComponent("TextMesh") as TextMesh;
			tM.fontSize = 70;
			tM.text = "MOV = " + ((InformacioCartaPersonatge)cartaActual.iC).moviment.ToString();
			
			tM = cartaActual.distanciaAtac.GetComponent("TextMesh") as TextMesh;
			tM.fontSize = 70;
			tM.text = "DAC = " + ((InformacioCartaPersonatge)cartaActual.iC).distanciaAtac.ToString();
		}else if(cartaActual.iC.GetType().ToString().Equals("InformacioCartaBonificacio")){
			tM.text = "Bonificacio";
			string nom = ((InformacioCartaBonificacio)cartaActual.iC).nom;
			
			tM = cartaActual.textBonificacio.GetComponent("TextMesh") as TextMesh;
			tM.fontSize = 70;
			if(nom.Equals("Atac")) tM.text = "ATK +" + ((InformacioCartaBonificacio)cartaActual.iC).atac.ToString();
			else if(nom.Equals("Defensa")) tM.text = "DEF +" + ((InformacioCartaBonificacio)cartaActual.iC).defensa.ToString();
			else if(nom.Equals("Uber")){
				tM.text = "ATK +" + ((InformacioCartaBonificacio)cartaActual.iC).atac.ToString();
				
				tM = cartaActual.textUber.GetComponent("TextMesh") as TextMesh;
				tM.fontSize = 70;
				tM.text = "DEF +" + ((InformacioCartaBonificacio)cartaActual.iC).defensa.ToString();
			}
		}
		color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	
		cartaActual.gameObject.renderer.material.color = Color.black;
		if(cartaActual.titol != null) cartaActual.titol.gameObject.renderer.material.color = Color.black;
		if(cartaActual.iconCarta != null) cartaActual.iconCarta.gameObject.renderer.material.color = Color.black;
		if(cartaActual.atacLlarg != null) cartaActual.atacLlarg.gameObject.renderer.material.color = Color.black;
		if(cartaActual.atacCurt != null) cartaActual.atacCurt.gameObject.renderer.material.color = Color.black;
		if(cartaActual.defensa != null) cartaActual.defensa.gameObject.renderer.material.color = Color.black;
		if(cartaActual.distanciaAtac != null) cartaActual.distanciaAtac.gameObject.renderer.material.color = Color.black;
		if(cartaActual.moviment != null) cartaActual.moviment.gameObject.renderer.material.color = Color.black;
		if(cartaActual.textBonificacio != null) cartaActual.textBonificacio.gameObject.renderer.material.color = Color.black;
		if(cartaActual.textUber != null) cartaActual.textUber.gameObject.renderer.material.color = Color.black;
	}
	
	public void pintarCarta(){
		if(pas < 0.3f){ 
			pas += 0.01f;
			cartaActual.gameObject.renderer.material.color = Color.Lerp(cartaActual.gameObject.renderer.material.color, 
				color, 
				pas);
			if(cartaActual.titol != null) cartaActual.titol.gameObject.renderer.material.color = Color.Lerp(cartaActual.titol.gameObject.renderer.material.color, 
				color, 
				pas);
			if(cartaActual.iconCarta != null) cartaActual.iconCarta.gameObject.renderer.material.color = Color.Lerp(cartaActual.iconCarta.gameObject.renderer.material.color, 
				color, 
				pas);
			if(cartaActual.atacLlarg != null) cartaActual.atacLlarg.gameObject.renderer.material.color = Color.Lerp(cartaActual.atacLlarg.gameObject.renderer.material.color, 
				color, 
				pas);
			if(cartaActual.atacCurt != null) cartaActual.atacCurt.gameObject.renderer.material.color = Color.Lerp(cartaActual.atacCurt.gameObject.renderer.material.color, 
				color, 
				pas);
			if(cartaActual.defensa != null) cartaActual.defensa.gameObject.renderer.material.color = Color.Lerp(cartaActual.defensa.gameObject.renderer.material.color, 
				color, 
				pas);
			if(cartaActual.distanciaAtac != null) cartaActual.distanciaAtac.gameObject.renderer.material.color = Color.Lerp(cartaActual.distanciaAtac.gameObject.renderer.material.color, 
				color, 
				pas);
			if(cartaActual.moviment != null) cartaActual.moviment.gameObject.renderer.material.color = Color.Lerp(cartaActual.moviment.gameObject.renderer.material.color, 
				color, 
				pas);
			if(cartaActual.textBonificacio != null) cartaActual.textBonificacio.gameObject.renderer.material.color = Color.Lerp(cartaActual.textBonificacio.gameObject.renderer.material.color, 
				color, 
				pas);
			if(cartaActual.textUber != null) cartaActual.textUber.gameObject.renderer.material.color = Color.Lerp(cartaActual.textUber.gameObject.renderer.material.color, 
				color, 
				pas);
		}
	}
}
