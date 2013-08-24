using UnityEngine;
using System.Collections;

public class EstatPersonatgeMort : EstatPersonatge {

	Personatge personatgeUsuari;
	Fitxa fitxaActual;
	Vector3 posicio;
	Color color;
	float pas = 0.1f;
	
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
	
	public EstatPersonatgeMort(Personatge p){
		personatgeUsuari = p;
		fitxaActual = personatgeUsuari.fitxaActual;
		if(personatgeUsuari.nom.ToString().Equals("Metralleta")){
			personatgeUsuari.renderer.material = Resources.Load("PersonatgeMort") as Material;
			if(personatgeUsuari.transform.localScale.x < 0){
				personatgeUsuari.transform.localScale = new Vector3(personatgeUsuari.transform.localScale.x - 0.08f,
					personatgeUsuari.transform.localScale.y, personatgeUsuari.transform.localScale.z + 0.08f);
				personatgeUsuari.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.9f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 0.6f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}else{
				personatgeUsuari.transform.localScale = new Vector3(personatgeUsuari.transform.localScale.x + 0.08f,
					personatgeUsuari.transform.localScale.y, personatgeUsuari.transform.localScale.z + 0.08f);
				personatgeUsuari.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.7f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 0.4f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}
		}else if(personatgeUsuari.nom.ToString().Equals("Franco")){
			personatgeUsuari.renderer.material = Resources.Load("MortFranco") as Material;
			if(personatgeUsuari.transform.localScale.x < 0){
				personatgeUsuari.transform.localScale = new Vector3(personatgeUsuari.transform.localScale.x - 0.27f,
					personatgeUsuari.transform.localScale.y, personatgeUsuari.transform.localScale.z + 0.27f);
				personatgeUsuari.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x + 0.1f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 0.6f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}else{
				personatgeUsuari.transform.localScale = new Vector3(personatgeUsuari.transform.localScale.x + 0.27f,
					personatgeUsuari.transform.localScale.y, personatgeUsuari.transform.localScale.z + 0.27f);
				personatgeUsuari.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 1.7f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 0.6f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}
		}else if(personatgeUsuari.nom.ToString().Equals("Guerrero")){
			personatgeUsuari.renderer.material = Resources.Load("GuerreroMort") as Material;
			if(personatgeUsuari.transform.localScale.x < 0){
				personatgeUsuari.transform.localScale = new Vector3(personatgeUsuari.transform.localScale.x - 0.14f,
					personatgeUsuari.transform.localScale.y, personatgeUsuari.transform.localScale.z + 0.14f);
				personatgeUsuari.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 1.2f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 0.5f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}else{
				personatgeUsuari.transform.localScale = new Vector3(personatgeUsuari.transform.localScale.x + 0.14f,
					personatgeUsuari.transform.localScale.y, personatgeUsuari.transform.localScale.z + 0.14f);
				personatgeUsuari.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.4f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 0.4f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}
		}else if(personatgeUsuari.nom.ToString().Equals("Bazooka")){
			personatgeUsuari.renderer.material = Resources.Load("BazookaMort") as Material;
			if(personatgeUsuari.transform.localScale.x < 0){
				personatgeUsuari.transform.localScale = new Vector3(personatgeUsuari.transform.localScale.x - 0.25f,
					personatgeUsuari.transform.localScale.y, personatgeUsuari.transform.localScale.z + 0.25f);
				personatgeUsuari.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x + 0.5f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 0.5f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}else{
				personatgeUsuari.transform.localScale = new Vector3(personatgeUsuari.transform.localScale.x + 0.25f,
					personatgeUsuari.transform.localScale.y, personatgeUsuari.transform.localScale.z + 0.25f);
				personatgeUsuari.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 1.9f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 0.5f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}
		}
	}

	public void pintarPersonatge(){
		if(fiAnimacio()){
			if(personatgeUsuari.nom.ToString().Equals("Metralleta")){
				if(personatgeUsuari.transform.localScale.x < 0){
					personatgeUsuari.transform.localScale = new Vector3(personatgeUsuari.transform.localScale.x + 0.27f,
					personatgeUsuari.transform.localScale.y, personatgeUsuari.transform.localScale.z - 0.27f);
				}else{
					personatgeUsuari.transform.localScale = new Vector3(personatgeUsuari.transform.localScale.x - 0.27f,
						personatgeUsuari.transform.localScale.y, personatgeUsuari.transform.localScale.z - 0.27f);
				}
			}else if(personatgeUsuari.nom.Equals("Franco")){
				if(personatgeUsuari.transform.localScale.x < 0){
					personatgeUsuari.transform.localScale = new Vector3(personatgeUsuari.transform.localScale.x + 0.08f,
					personatgeUsuari.transform.localScale.y, personatgeUsuari.transform.localScale.z - 0.08f);
				}else{
					personatgeUsuari.transform.localScale = new Vector3(personatgeUsuari.transform.localScale.x - 0.08f,
						personatgeUsuari.transform.localScale.y, personatgeUsuari.transform.localScale.z - 0.08f);
				}
			}else if(personatgeUsuari.nom.Equals("Guerrero")){
			}
			else if(personatgeUsuari.nom.Equals("Bazooka")){
				if(personatgeUsuari.transform.localScale.x < 0){
					personatgeUsuari.transform.localScale = new Vector3(personatgeUsuari.transform.localScale.x + 0.08f,
					personatgeUsuari.transform.localScale.y, personatgeUsuari.transform.localScale.z - 0.08f);
				}else{
					personatgeUsuari.transform.localScale = new Vector3(personatgeUsuari.transform.localScale.x - 0.08f,
						personatgeUsuari.transform.localScale.y, personatgeUsuari.transform.localScale.z - 0.08f);
				}
			}
			personatgeUsuari.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			
			personatgeUsuari.acabar();
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
	    Vector2 offset = new Vector2(offsetX,offsetY);
	    
		// We give the change to the material
		// This has the same effect as changing the offset value of the material in the editor.
	    personatgeUsuari.renderer.material.SetTextureOffset ("_MainTex", offset); // Which face should be displayed
	    personatgeUsuari.renderer.material.SetTextureScale  ("_MainTex", size); // The size of a single face
		personatgeUsuari.gameObject.renderer.enabled = true;
	}
	
	private bool fiAnimacio(){
		return index == 62;			// Comprovar segons tipus personatges
	}
	
	private void ferMort(){
		personatgeUsuari.estat = new EstatPersonatgeNoVisible(personatgeUsuari);
		personatgeUsuari.fitxaActual.treurePersonatge();
	}
}
