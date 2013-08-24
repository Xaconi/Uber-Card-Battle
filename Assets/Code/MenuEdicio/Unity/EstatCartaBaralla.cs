using UnityEngine;
using System.Collections;

public class EstatCartaBaralla : EstatCartaEdicio {
	
	private CartaEdicio cartaActual;
	private Vector3 posicio;
	private TextMesh tM;
	private Color color;
	float pas = 0.1f;
	
	public EstatCartaBaralla(CartaEdicio c){
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
		
		if(diferenciaRatio > 0.95f){
			cartaActual.titol.transform.position = new Vector3((cartaActual.transform.position.x + 0.5f * cartaActual.posicioLlistaBaralla) + 0.8f,
				cartaActual.transform.position.y + 2.5f + 0.5f * cartaActual.posicioLlista,
				cartaActual.transform.position.z + 2);
			if(cartaActual.iC.GetType().ToString().Equals("InformacioCartaPersonatge")){
				cartaActual.atacLlarg.transform.position = new Vector3((cartaActual.transform.position.x + 0.5f * cartaActual.posicioLlistaBaralla) + 0.7f,
					cartaActual.transform.position.y - 1.3f + 0.5f * cartaActual.posicioLlista,
					cartaActual.transform.position.z + 2);
				cartaActual.atacCurt.transform.position = new Vector3((cartaActual.transform.position.x + 0.5f * cartaActual.posicioLlistaBaralla) + 0.7f,
					cartaActual.transform.position.y - 2.2f + 0.5f * cartaActual.posicioLlista,
					cartaActual.transform.position.z + 2);
				cartaActual.defensa.transform.position = new Vector3((cartaActual.transform.position.x + 0.5f * cartaActual.posicioLlistaBaralla) + 0.7f,
					cartaActual.transform.position.y - 3.1f + 0.5f * cartaActual.posicioLlista,
					cartaActual.transform.position.z + 2);
				cartaActual.moviment.transform.position = new Vector3((cartaActual.transform.position.x + 0.5f * cartaActual.posicioLlistaBaralla) - 1.4f,
					cartaActual.transform.position.y - 1.65f + 0.5f * cartaActual.posicioLlista,
					cartaActual.transform.position.z + 2);
				cartaActual.distanciaAtac.transform.position = new Vector3((cartaActual.transform.position.x + 0.5f * cartaActual.posicioLlistaBaralla) - 1.4f,
					cartaActual.transform.position.y - 2.55f + 0.5f * cartaActual.posicioLlista,
					cartaActual.transform.position.z + 2);
				cartaActual.iconCarta.transform.position = new Vector3((cartaActual.transform.position.x + 0.5f * cartaActual.posicioLlistaBaralla) - 1.3f,
					cartaActual.transform.position.y + 0.55f + 0.5f * cartaActual.posicioLlista,
					cartaActual.transform.position.z + 2);
			}else if(cartaActual.iC.GetType().ToString().Equals("InformacioCartaBonificacio")){
				cartaActual.textBonificacio.transform.position = new Vector3((cartaActual.transform.position.x + 0.5f * cartaActual.posicioLlistaBaralla) - 0.4f,
					cartaActual.transform.position.y - 1.55f + 0.5f * cartaActual.posicioLlista,
					cartaActual.transform.position.z + 2);
				string nom = ((InformacioCartaBonificacio)cartaActual.iC).nom.ToString();
				if(nom.Equals("Atac")){
					cartaActual.iconCarta.transform.position = new Vector3((cartaActual.transform.position.x + 0.49f * cartaActual.posicioLlistaBaralla) - 1.35f,
						cartaActual.transform.position.y + 0.55f + 0.5f * cartaActual.posicioLlista,
						cartaActual.transform.position.z + 2);
				}else if(nom.Equals("Defensa")){
					cartaActual.iconCarta.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
					cartaActual.iconCarta.transform.position = new Vector3((cartaActual.transform.position.x + 0.49f * cartaActual.posicioLlistaBaralla) - 1.35f,
						cartaActual.transform.position.y + 0.55f + 0.5f * cartaActual.posicioLlista,
						cartaActual.transform.position.z + 2);
				}else if(nom.Equals("Uber")){
					cartaActual.textUber.transform.position = new Vector3((cartaActual.transform.position.x + 0.5f * cartaActual.posicioLlistaBaralla) - 0.4f,
						cartaActual.transform.position.y - 2.55f + 0.5f * cartaActual.posicioLlista,
						cartaActual.transform.position.z + 2);
					cartaActual.iconCarta.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
					cartaActual.iconCarta.transform.position = new Vector3((cartaActual.transform.position.x + 0.49f * cartaActual.posicioLlistaBaralla) - 1.35f,
						cartaActual.transform.position.y + 0.55f + 0.5f * cartaActual.posicioLlista,
						cartaActual.transform.position.z + 2);
				}
			}
		}else if(diferenciaRatio > 0.85f){
			cartaActual.titol.transform.position = new Vector3((cartaActual.transform.position.x + 0.45f * cartaActual.posicioLlistaBaralla) + 0.8f,
				cartaActual.transform.position.y + 2.5f + 0.5f * cartaActual.posicioLlista,
				cartaActual.transform.position.z + 2);
			if(cartaActual.iC.GetType().ToString().Equals("InformacioCartaPersonatge")){
				cartaActual.atacLlarg.transform.position = new Vector3((cartaActual.transform.position.x + 0.45f * cartaActual.posicioLlistaBaralla) + 0.7f,
					cartaActual.transform.position.y - 1.3f + 0.5f * cartaActual.posicioLlista,
					cartaActual.transform.position.z + 2);
				cartaActual.atacCurt.transform.position = new Vector3((cartaActual.transform.position.x + 0.45f * cartaActual.posicioLlistaBaralla) + 0.7f,
					cartaActual.transform.position.y - 2.2f + 0.5f * cartaActual.posicioLlista,
					cartaActual.transform.position.z + 2);
				cartaActual.defensa.transform.position = new Vector3((cartaActual.transform.position.x + 0.45f * cartaActual.posicioLlistaBaralla) + 0.7f,
					cartaActual.transform.position.y - 3.1f + 0.5f * cartaActual.posicioLlista,
					cartaActual.transform.position.z + 2);
				cartaActual.moviment.transform.position = new Vector3((cartaActual.transform.position.x + 0.45f * cartaActual.posicioLlistaBaralla) - 1.4f,
					cartaActual.transform.position.y - 1.65f + 0.5f * cartaActual.posicioLlista,
					cartaActual.transform.position.z + 2);
				cartaActual.distanciaAtac.transform.position = new Vector3((cartaActual.transform.position.x + 0.45f * cartaActual.posicioLlistaBaralla) - 1.4f,
					cartaActual.transform.position.y - 2.55f + 0.5f * cartaActual.posicioLlista,
					cartaActual.transform.position.z + 2);
				cartaActual.iconCarta.transform.position = new Vector3((cartaActual.transform.position.x + 0.45f * cartaActual.posicioLlistaBaralla) - 1.3f,
					cartaActual.transform.position.y + 0.55f + 0.5f * cartaActual.posicioLlista,
					cartaActual.transform.position.z + 2);
			}else if(cartaActual.iC.GetType().ToString().Equals("InformacioCartaBonificacio")){
				cartaActual.textBonificacio.transform.position = new Vector3((cartaActual.transform.position.x + 0.45f * cartaActual.posicioLlistaBaralla) - 0.4f,
					cartaActual.transform.position.y - 1.55f + 0.5f * cartaActual.posicioLlista,
					cartaActual.transform.position.z + 2);
				string nom = ((InformacioCartaBonificacio)cartaActual.iC).nom.ToString();
				if(nom.Equals("Atac")){
					cartaActual.iconCarta.transform.position = new Vector3((cartaActual.transform.position.x + 0.49f * cartaActual.posicioLlistaBaralla) - 1.35f,
						cartaActual.transform.position.y + 0.55f + 0.5f * cartaActual.posicioLlista,
						cartaActual.transform.position.z + 2);
				}else if(nom.Equals("Defensa")){
					cartaActual.iconCarta.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
					cartaActual.iconCarta.transform.position = new Vector3((cartaActual.transform.position.x + 0.49f * cartaActual.posicioLlistaBaralla) - 1.35f,
						cartaActual.transform.position.y + 0.55f + 0.5f * cartaActual.posicioLlista,
						cartaActual.transform.position.z + 2);
				}else if(nom.Equals("Uber")){
					cartaActual.textUber.transform.position = new Vector3((cartaActual.transform.position.x + 0.45f * cartaActual.posicioLlistaBaralla) - 0.4f,
						cartaActual.transform.position.y - 2.55f + 0.5f * cartaActual.posicioLlista,
						cartaActual.transform.position.z + 2);
					cartaActual.iconCarta.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
					cartaActual.iconCarta.transform.position = new Vector3((cartaActual.transform.position.x + 0.49f * cartaActual.posicioLlistaBaralla) - 1.35f,
						cartaActual.transform.position.y + 0.55f + 0.5f * cartaActual.posicioLlista,
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
	}
	
	public void pintarCarta(){
		if(pas < 1.0f) pas += 0.001f;
		cartaActual.gameObject.renderer.material.color = Color.Lerp(cartaActual.gameObject.renderer.material.color, 
			color, 
			pas);
	}
}
