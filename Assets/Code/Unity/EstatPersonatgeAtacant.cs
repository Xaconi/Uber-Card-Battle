using UnityEngine;
using System.Collections.Generic;

public class EstatPersonatgeAtacant : EstatPersonatge {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------

	private List<Personatge> llistaPersonatges;
	private Base baseAtacar;
	private Personatge personatgeActual;
	private Fitxa fitxaActual;
	private bool personatgesAvisats;
	private bool atacBase;
	bool enrere;
	private string tipus;
	
	//vars for the whole sheet
	public int colCount =  7;
	public int rowCount =  9;
	
	//vars for animation
	public int  rowNumber  =  0; //Zero Indexed
	public int colNumber = 0; //Zero Indexed
	public int totalCells = 63;
	public int  fps     = 30;
	Vector2 offset = new Vector2(0.0f, 0.0f);
	int indexG = 0;
	int index = 0;
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------
	
	public EstatPersonatgeAtacant(Personatge p, string t){
		personatgeActual = p;
		fitxaActual = personatgeActual.fitxaActual;
		llistaPersonatges = p.getLlistaObservers();
		personatgesAvisats = false;
		tipus = t;
		atacBase = false;
		enrere = false;
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		foreach(Personatge per in llistaPersonatges){
			per.estat = new EstatPersonatgeExpectant(per);
			if(per.fitxaActual.columna < personatgeActual.fitxaActual.columna && cG.usuariTorn == 1) enrere = true;
			else if(per.fitxaActual.columna > personatgeActual.fitxaActual.columna && cG.usuariTorn == 2) enrere = true;
		}
		if(enrere) personatgeActual.gameObject.transform.localScale = new Vector3(-personatgeActual.gameObject.transform.localScale.x,
			personatgeActual.gameObject.transform.localScale.y, personatgeActual.gameObject.transform.localScale.z);
		if(tipus.Equals("AtacLlargaDistancia")){
			if(personatgeActual.nom.ToString().Equals("Metralleta")) personatgeActual.renderer.material = Resources.Load("PersonatgeAtac") as Material;
			else if(personatgeActual.nom.Equals("Franco")){
				personatgeActual.renderer.material = Resources.Load("FrancoAtac") as Material;
				if(personatgeActual.transform.localScale.x < 0){
					personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x - 0.15f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z + 0.15f);
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.2f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 1.3f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}else{
					personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x + 0.15f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z + 0.15f);
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 1.4f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 1.25f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}
			}else if(personatgeActual.nom.Equals("Bazooka")){
				personatgeActual.renderer.material = Resources.Load("BazookaAtac") as Material;
				if(personatgeActual.transform.localScale.x < 0){
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.85f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}else{
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.77f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.62f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}
			}
		}
		else if(tipus.Equals("AtacCurtaDistancia")){
			if(personatgeActual.nom.ToString().Equals("Metralleta")){
				personatgeActual.renderer.material = Resources.Load("PersonatgeAtacCurt") as Material;
				if(personatgeActual.transform.localScale.x < 0){
					personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x - 0.08f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z + 0.08f);
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.2f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.6f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}else{
					personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x + 0.08f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z + 0.08f);
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 1.4f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}
			}else if(personatgeActual.nom.Equals("Franco")){
				personatgeActual.renderer.material = Resources.Load("FrancoAtacCurt") as Material;
				if(personatgeActual.transform.localScale.x < 0){
					personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x - 0.1f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z + 0.1f);
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.3f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.6f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}else{
					personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x + 0.1f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z + 0.1f);
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 1.3f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}
			}else if(personatgeActual.nom.Equals("Guerrero")){
				personatgeActual.renderer.material = Resources.Load("GuerreroAtacCurt") as Material;
				if(personatgeActual.transform.localScale.x < 0){
					personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x - 0.05f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z + 0.05f);
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.3f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.55f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}else{
					personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x + 0.05f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z + 0.05f);
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 1.3f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.6f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}
			}else if(personatgeActual.nom.Equals("Bazooka")){
				personatgeActual.renderer.material = Resources.Load("BazookaAtac") as Material;
				if(personatgeActual.transform.localScale.x < 0){
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.85f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}else{
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.77f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.62f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}
			}
		}
	}
	
	public EstatPersonatgeAtacant(Personatge p, Base b, string t){
		personatgeActual = p;
		llistaPersonatges = p.getLlistaObservers();
		fitxaActual = personatgeActual.fitxaActual;
		personatgesAvisats = false;
		tipus = t;
		baseAtacar = b;
		atacBase = true;
		if(personatgeActual.nom.ToString().Equals("Metralleta")) personatgeActual.renderer.material = Resources.Load("PersonatgeAtac") as Material;
		else if(personatgeActual.nom.Equals("Franco")){
			personatgeActual.renderer.material = Resources.Load("FrancoAtac") as Material;
			if(personatgeActual.transform.localScale.x < 0){
				personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x - 0.15f,
					personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z + 0.15f);
				personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.2f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 1.3f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}else{
				personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x + 0.15f,
					personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z + 0.15f);
				personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 1.4f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 1.25f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}
		}else if(personatgeActual.nom.Equals("Bazooka")){
			personatgeActual.renderer.material = Resources.Load("BazookaAtac") as Material;
			if(personatgeActual.transform.localScale.x < 0){
				personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.85f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
			}else{
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.77f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.62f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
			}
		}
	}
	
	public void pintarPersonatge(){
		//throw new System.NotImplementedException();
		// Animacio d'atac, quan arribi a un punt X de l'animació, fer l'actualizació dels observers
		bool animacioAtac = false;
		if(tipus.Equals("AtacLlargaDistancia")) animacioAtac = fiAnimacioAtacLlarg();
		else if(tipus.Equals("AtacCurtaDistancia")) animacioAtac = fiAnimacioAtacCurt();
		if(!atacBase && fiAnimacio()){
			personatgeActual.estat = personatgeActual.estatAnterior;
			if(tipus.Equals("AtacLlargaDistancia")){
				if(personatgeActual.nom.Equals("Franco")){
					if(personatgeActual.transform.localScale.x < 0){
						personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x + 0.15f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z - 0.15f);
					}else{
						personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x - 0.15f,
							personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z - 0.15f);
					}
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}if(personatgeActual.nom.Equals("Bazooka")){
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}
			}
			else if(tipus.Equals("AtacCurtaDistancia")){
				if(personatgeActual.nom.ToString().Equals("Metralleta")){
					if(personatgeActual.transform.localScale.x < 0){
						personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x + 0.08f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z - 0.08f);
					}else{
						personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x - 0.08f,
							personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z - 0.08f);
					}
				}else if(personatgeActual.nom.Equals("Franco")){
					if(personatgeActual.transform.localScale.x < 0){
						personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x + 0.1f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z - 0.1f);
					}else{
						personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x - 0.1f,
							personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z - 0.1f);
					}
				}else if(personatgeActual.nom.Equals("Guerrero")){
					if(personatgeActual.transform.localScale.x < 0){
						personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x + 0.05f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z - 0.05f);
					}else{
						personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x - 0.05f,
							personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z - 0.05f);
					}
				}else if(personatgeActual.nom.Equals("Bazooka")){

				}
				personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
			}
			personatgeActual.gameObject.renderer.enabled = false;
			if(enrere) personatgeActual.gameObject.transform.localScale = new Vector3(-personatgeActual.gameObject.transform.localScale.x,
			personatgeActual.gameObject.transform.localScale.y, personatgeActual.gameObject.transform.localScale.z);
			if(personatgeActual.nom.ToString().Equals("Metralleta")) personatgeActual.renderer.material = Resources.Load("PersonatgeNormal") as Material;
			else if(personatgeActual.nom.Equals("Franco")) personatgeActual.renderer.material = Resources.Load("FrancoIdle") as Material;
			else if(personatgeActual.nom.Equals("Guerrero")) personatgeActual.renderer.material = Resources.Load("GuerreroIdle") as Material;
			else if(personatgeActual.nom.Equals("Bazooka")) personatgeActual.renderer.material = Resources.Load("BazookaIdle") as Material;
		}else if(atacBase && fiAnimacio()){
			actualitzarObserverBase();
			personatgesAvisats = true;
			// Quan s'arribi al final de l'animació, passem a l'estat anterior
			llistaPersonatges.Clear();
			personatgeActual.estat = personatgeActual.estatAnterior;
			personatgeActual.gameObject.renderer.enabled = false;
			if(personatgeActual.nom.ToString().Equals("Metralleta")) personatgeActual.renderer.material = Resources.Load("PersonatgeNormal") as Material;
			else if(personatgeActual.nom.ToString().Equals("Franco")){
				if(tipus.Equals("AtacLlargaDistancia")){
					if(personatgeActual.transform.localScale.x < 0){
						personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x + 0.15f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z - 0.15f);
					}else{
						personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x - 0.15f,
							personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z - 0.15f);
					}
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}
				personatgeActual.renderer.material = Resources.Load("FrancoIdle") as Material;
			}else if(personatgeActual.nom.ToString().Equals("Guerrero")){
				personatgeActual.renderer.material = Resources.Load("GuerreroIdle") as Material;
			}else if(personatgeActual.nom.ToString().Equals("Bazooka")){
				personatgeActual.renderer.material = Resources.Load("BazookaIdle") as Material;
			}
		}else if(!personatgesAvisats && animacioAtac){
			AudioObject a = (AudioObject) GameObject.FindObjectOfType(typeof(AudioObject));
			actualitzarObservers();
			personatgesAvisats = true;
			llistaPersonatges.Clear();
			if(personatgeActual.nom.ToString().Equals("Franco")){
				if(tipus.Equals("AtacLlargaDistancia")){
					a.playSniperShot();
				}else if(tipus.Equals("AtacCurtaDistancia")){
				}
			}
		}
		// An atlas is a single texture containing several smaller textures.
		// It's used for GUI to have not power of two textures and gain space, for example.
		// Here, we have an atlas with 16 faces
	    // Calculate index
	    if(indexG == 0){
			index  = 0;
			indexG = (int)(Time.time * fps);
		}else{
			index  = (int)(Time.time * fps) - indexG;
		}
	    // Repeat when exhausting all cells
	    index = index % totalCells; // => 0 1 2 3 / 0 1 2 3 / 0 1 2 3 ...
	    // Size of every cell
	    float sizeX = 1.0f / colCount; // We split the texture in 4 rows and 4 cols
	    float sizeY = 1.0f / rowCount;
	    Vector2 size =  new Vector2(sizeX,sizeY);
	    
	    // split into horizontal and vertical index
	    var uIndex = index % colCount;
	    var vIndex = index / colCount;
	 
	    // build offset
	    // v coordinate is the bottom of the image in opengl so we need to invert.
	    float offsetX = (uIndex + colNumber) * size.x;
	    float offsetY = (1.0f - size.y) - (vIndex + rowNumber) * size.y;
	    offset = new Vector2(offsetX,offsetY);
		// We give the change to the material
		// This has the same effect as changing the offset value of the material in the editor.
	    personatgeActual.renderer.material.SetTextureOffset ("_MainTex", offset); // Which face should be displayed
	    personatgeActual.renderer.material.SetTextureScale  ("_MainTex", size); // The size of a single face
		personatgeActual.renderer.enabled = true;
	}

	public void actualitzarObservers(){
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		foreach(Personatge per in llistaPersonatges){
			if(tipus.Equals("AtacCurtaDistancia")){
				if(!personatgeActual.nom.ToString().Equals("Bazooka")) per.rebrePuntsAtac(personatgeActual.atacCurtaDistancia);
				else per.rebrePuntsAtac(personatgeActual.atacLlargaDistancia);
				if(personatgeActual.propietari == 1) cG.augmentarAtacFet(personatgeActual.atacCurtaDistancia);
				else cG.augmentarAtacRebut(personatgeActual.atacCurtaDistancia);
			}
			else if(tipus.Equals("AtacLlargaDistancia")){
				per.rebrePuntsAtac(personatgeActual.atacLlargaDistancia);
				if(personatgeActual.propietari == 1) cG.augmentarAtacFet(personatgeActual.atacLlargaDistancia);
				else cG.augmentarAtacRebut(personatgeActual.atacLlargaDistancia);
			}
			per.renderer.enabled = false;
			per.estat = new EstatPersonatgeAtacat(per);
			if(per.nom.ToString().Equals("Metralleta")) per.renderer.material = Resources.Load("PersonatgeAtacat") as Material;
			else if(per.nom.ToString().Equals("Franco")) per.renderer.material = Resources.Load("FrancoAtacat") as Material;
			else if(per.nom.ToString().Equals("Guerrero")) per.renderer.material = Resources.Load("GuerreroAtacat") as Material;
			else if(per.nom.ToString().Equals("Bazooka")) per.renderer.material = Resources.Load("BazookaAtacat") as Material;
		}
	}

	public void actualitzarObserverBase(){
		ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
		if(tipus.Equals("AtacCurtaDistancia")) baseAtacar.rebrePuntsAtac(personatgeActual.atacCurtaDistancia);
		else if(tipus.Equals("AtacLlargaDistancia")) baseAtacar.rebrePuntsAtac(personatgeActual.atacLlargaDistancia);
		baseAtacar.setEstat(new EstatBaseAtacat(baseAtacar));
		if(personatgeActual.propietari == 1) cG.augmentarAtacFet(personatgeActual.atacLlargaDistancia);
		else cG.augmentarAtacRebut(personatgeActual.atacLlargaDistancia);
	}
	
	private bool fiAnimacio(){
		return index == 62;			// Comprovar segons tipus personatges
	}
	
	private bool fiAnimacioAtacLlarg(){
		if(personatgeActual.nom.ToString().Equals("Metralleta")) return index == 40;			// Comprovar segons tipus personatges
		else if(personatgeActual.nom.ToString().Equals("Franco")) return index == 33;
		else if(personatgeActual.nom.ToString().Equals("Bazooka")) return index == 33;	
		else return index == 40;	
	}
	
	private bool fiAnimacioAtacCurt(){
		if(personatgeActual.nom.ToString().Equals("Bazooka")) return index == 33;
		return index == 50;			// Comprovar segons tipus personatges
	}

}
