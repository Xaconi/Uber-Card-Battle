using UnityEngine;
using System.Collections;

public class EstatCartaObjectiuCanvi : EstatCartaEdicio {
	
	CartaEdicio cartaActual;
	CartaEdicio cartaCanvi;
	private Color color;
	float pas = 0.1f;

	public EstatCartaObjectiuCanvi(CartaEdicio c, CartaEdicio canvi){
		cartaActual = c;
		cartaCanvi = canvi;
		string tipus = cartaActual.tipus.GetType().ToString();
		string nivellCartaActual = "";
		string nivellCartaExterna = "";
		if(tipus.Equals("TipusCartaPersonatge")){
			nivellCartaActual = ((InformacioCartaPersonatge)cartaActual.iC).nivell.ToString();
			nivellCartaExterna = ((InformacioCartaPersonatge)cartaCanvi.iC).nivell.ToString();
		}else if(tipus.Equals("TipusCartaBonificacio")){
			nivellCartaActual = ((InformacioCartaBonificacio)cartaActual.iC).nivell.ToString();
			nivellCartaExterna = ((InformacioCartaBonificacio)cartaCanvi.iC).nivell.ToString();
			
		}
		int nivellCA = 0;
		int nivellCE = 0;
		if(nivellCartaActual.Equals("Plati")) nivellCA = 4;
		else if(nivellCartaActual.Equals("Or")) nivellCA = 3;
		else if(nivellCartaActual.Equals("Plata")) nivellCA = 2;
		else if(nivellCartaActual.Equals("Bronze")) nivellCA = 1;
		
		if(nivellCartaExterna.Equals("Plati")) nivellCE = 4;
		else if(nivellCartaExterna.Equals("Or")) nivellCE = 3;
		else if(nivellCartaExterna.Equals("Plata")) nivellCE = 2;
		else if(nivellCartaExterna.Equals("Bronze")) nivellCE = 1;
		
		if(nivellCA > nivellCE) color = Color.red;
		else if(nivellCA < nivellCE) color = Color.green;
		else if(nivellCA == nivellCE) color = Color.grey;
	}
	
	public void pintarCarta(){
		if(pas < 1.0f) pas += 0.001f;
		cartaActual.gameObject.renderer.material.color = Color.Lerp(cartaActual.gameObject.renderer.material.color, 
			color, 
			pas);
		if(cartaActual.titol != null) cartaActual.titol.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.iconCarta != null) cartaActual.iconCarta.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.atacLlarg != null) cartaActual.atacLlarg.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.atacCurt != null) cartaActual.atacCurt.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.defensa != null) cartaActual.defensa.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.distanciaAtac != null) cartaActual.distanciaAtac.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.moviment != null) cartaActual.moviment.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.textBonificacio != null) cartaActual.textBonificacio.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		if(cartaActual.textUber != null) cartaActual.textUber.gameObject.renderer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}
}
