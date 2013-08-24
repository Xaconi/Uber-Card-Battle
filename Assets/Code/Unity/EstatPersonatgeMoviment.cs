using UnityEngine;

public class EstatPersonatgeMoviment : EstatPersonatge {
	
	private Personatge personatgeUsuari;
	private Fitxa fitxaOrigen;
	private Fitxa fitxaDesti;
	private Vector3 startPoint;
	private Vector3 endPoint;
	private float startTime;
	
	//vars for the whole sheet
	public int colCount =  5;
	public int rowCount =  25;
	
	//vars for animation
	public int  rowNumber  =  0; //Zero Indexed
	public int colNumber = 0; //Zero Indexed
	public int totalCells = 125;
	public int  fps     = 60;

	public EstatPersonatgeMoviment(Personatge p, Fitxa o, Fitxa d){
		personatgeUsuari = p;
		fitxaOrigen = o;
		fitxaDesti = d;
		startPoint = new Vector3(o.gameObject.transform.position.x - 0.8f + 0.13f*o.columna, 
							o.gameObject.transform.position.y + 0.65f - o.fila*0.08f, o.gameObject.transform.position.z + 1);
		endPoint = new Vector3(d.gameObject.transform.position.x - 0.8f + 0.13f*d.columna, 
							d.gameObject.transform.position.y + 0.65f - d.fila*0.08f, d.gameObject.transform.position.z + 1);
		startTime = Time.time;
		if(personatgeUsuari.nom.ToString().Equals("Metralleta")) personatgeUsuari.renderer.material = Resources.Load("PersonatgeMoviment") as Material;
		else if(personatgeUsuari.nom.Equals("Franco")) personatgeUsuari.renderer.material = Resources.Load("MovimentFranco") as Material;
		else if(personatgeUsuari.nom.Equals("Guerrero")) personatgeUsuari.renderer.material = Resources.Load("GuerreroMoviment") as Material;
		else if(personatgeUsuari.nom.Equals("Bazooka")) personatgeUsuari.renderer.material = Resources.Load("BazookaMoviment") as Material;
		//personatgeUsuari.renderer.material = Resources.Load("PersonatgeMoviment") as Material;
		personatgeUsuari.gameObject.renderer.enabled = true;
		personatgeUsuari.blockUsuariPublic();
	}

	public void pintarPersonatge(){
		//throw new System.NotImplementedException();
		personatgeUsuari.transform.position = Vector3.Lerp(startPoint, endPoint, Time.time - startTime);
		// Si arribem al punt de destí...
		if(personatgeUsuari.gameObject.transform.position == endPoint){
			fitxaOrigen.traspassarPersonatge(fitxaDesti, personatgeUsuari);
			personatgeUsuari.estat = personatgeUsuari.estatAnterior;
			personatgeUsuari.gameObject.renderer.enabled = false;
			if(personatgeUsuari.nom.ToString().Equals("Metralleta")) personatgeUsuari.renderer.material = Resources.Load("PersonatgeNormal") as Material;
			else if(personatgeUsuari.nom.Equals("Franco")) personatgeUsuari.renderer.material = Resources.Load("FrancoIdle") as Material;
			else if(personatgeUsuari.nom.Equals("Guerrero")) personatgeUsuari.renderer.material = Resources.Load("GuerreroIdle") as Material;
			else if(personatgeUsuari.nom.Equals("Bazooka")) personatgeUsuari.renderer.material = Resources.Load("BazookaIdle") as Material;
			personatgeUsuari.gameObject.tag = "Personatge";
			ControlGeneralBatalla cG = (ControlGeneralBatalla) Camera.mainCamera.GetComponent("ControlGeneralBatalla");
			if(cG.IA && cG.usuariTorn == 2){
				// Si es tracta d'un moviment de la IA
				cG.avisarIA();
			}
			personatgeUsuari.blockUsuariPublic();
		}else{
			// An atlas is a single texture containing several smaller textures.
			// It's used for GUI to have not power of two textures and gain space, for example.
			// Here, we have an atlas with 16 faces
		    // Calculate index
		    int index  = (int)(Time.time * fps);
			
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
		}
	}
}
