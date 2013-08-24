using UnityEngine;

public class EstatPersonatgeAtacat : EstatPersonatge {
	
	Personatge personatgeActual;
	Fitxa fitxaActual;
	
	//vars for the whole sheet
	public int colCount =  7;
	public int rowCount =  9;
	
	//vars for animation
	public int  rowNumber  =  0; //Zero Indexed
	public int colNumber = 0; //Zero Indexed
	public int totalCells = 63;
	public int  fps     = 30;
	
  	//Maybe this should be a private var
    private Vector2 offset;
	int index = 0;
	int indexG = 0;

	public EstatPersonatgeAtacat(Personatge p){
		personatgeActual = p;
		fitxaActual = personatgeActual.fitxaActual;
		if(personatgeActual.nom.ToString().Equals("Metralleta")){
			if(personatgeActual.transform.localScale.x < 0){
				personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x - 0.12f,
					personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z + 0.12f);
				personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 1.1f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 1.05f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}else{
				personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x + 0.12f,
					personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z + 0.12f);
				personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.5f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 1.05f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}
		}else if(personatgeActual.nom.Equals("Franco")){
			if(personatgeActual.transform.localScale.x < 0){
				personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x - 0.03f,
					personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z + 0.03f);
				personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 1.0f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 0.75f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}else{
				personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x + 0.03f,
					personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z + 0.03f);
				personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.6f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 0.70f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}
		}else if(personatgeActual.nom.Equals("Guerrero")){
			if(personatgeActual.transform.localScale.x < 0){
				personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 1.05f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 0.75f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}else{
				personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.6f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 0.70f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}
		}else if(personatgeActual.nom.Equals("Bazooka")){
			if(personatgeActual.transform.localScale.x < 0){
				personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.75f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}else{
				personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.85f + 0.13f*fitxaActual.columna, 
					fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
					fitxaActual.gameObject.transform.position.z + 1);
			}
		}
	}
	
	public void pintarPersonatge(){
		//throw new System.NotImplementedException();
		// Quan s'acabi l'animació, passem a l'estat anterior i mirem si el personatge ha mort...
		if(fiAnimacio()){
			personatgeActual.estat = personatgeActual.estatAnterior;
			personatgeActual.gameObject.renderer.enabled = false;
			if(personatgeActual.nom.ToString().Equals("Metralleta")){
				if(personatgeActual.transform.localScale.x < 0){
					personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x + 0.12f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z - 0.12f);
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}else{
					personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x - 0.12f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z - 0.12f);
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}
			 	personatgeActual.renderer.material = Resources.Load("PersonatgeNormal") as Material;
			}
			else if(personatgeActual.nom.Equals("Franco")){
				if(personatgeActual.transform.localScale.x < 0){
					personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x + 0.03f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z - 0.03f);
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}else{
					personatgeActual.transform.localScale = new Vector3(personatgeActual.transform.localScale.x - 0.03f,
						personatgeActual.transform.localScale.y, personatgeActual.transform.localScale.z - 0.03f);
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}
				personatgeActual.renderer.material = Resources.Load("FrancoIdle") as Material;
			}else if(personatgeActual.nom.ToString().Equals("Guerrero")){
				if(personatgeActual.transform.localScale.x < 0){
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}else{
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}
				personatgeActual.renderer.material = Resources.Load("GuerreroIdle") as Material;
			}else if(personatgeActual.nom.ToString().Equals("Bazooka")){
				if(personatgeActual.transform.localScale.x < 0){
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}else{
					personatgeActual.transform.position = new Vector3(fitxaActual.gameObject.transform.position.x - 0.8f + 0.13f*fitxaActual.columna, 
						fitxaActual.gameObject.transform.position.y + 0.65f - fitxaActual.fila*0.08f, 
						fitxaActual.gameObject.transform.position.z + 1);
				}
				personatgeActual.renderer.material = Resources.Load("BazookaIdle") as Material;
			}
			personatgeActual.mirarMort();
			if(personatgeActual.defensa > 0){
				ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
				if(cG.IA && cG.usuariTorn == 2){
					// Si es tracta d'un moviment de la IA
					cG.avisarIA();
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
	
	private bool fiAnimacio(){
		return index == 62;			// Comprovar segons tipus personatges
	}
}
