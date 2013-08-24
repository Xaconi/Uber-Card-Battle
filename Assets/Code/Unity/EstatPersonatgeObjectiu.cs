using UnityEngine;

public class EstatPersonatgeObjectiu : EstatPersonatge {
	
	Personatge personatgeUsuari;
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
	
  	//Maybe this should be a private var
    private Vector2 offset;
	
	public EstatPersonatgeObjectiu(Personatge p){
		personatgeUsuari = p;
		color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		posicio = p.transform.position;
	}

	public void pintarPersonatge(){
		//throw new System.NotImplementedException();
		if(pas < 1.0f) pas += 0.001f;
		personatgeUsuari.gameObject.renderer.material.color = Color.Lerp(personatgeUsuari.gameObject.renderer.material.color, color, pas);
	
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
		personatgeUsuari.gameObject.renderer.enabled = true;
	}
}
