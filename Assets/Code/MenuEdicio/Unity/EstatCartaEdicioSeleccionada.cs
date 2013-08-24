using UnityEngine;
using System.Collections;

public class EstatCartaEdicioSeleccionada : EstatCartaEdicio {
	
	CartaEdicio cartaActual;
	float pas = 0.1f;
	Color colorSeleccionat;
	
	public EstatCartaEdicioSeleccionada(CartaEdicio c){
		cartaActual = c;
		colorSeleccionat = Color.blue;
	}
	
	public void pintarCarta(){
		if(pas < 1.0f) pas += 0.001f;
		cartaActual.gameObject.renderer.material.color = Color.Lerp(cartaActual.gameObject.renderer.material.color, 
			colorSeleccionat, 
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
