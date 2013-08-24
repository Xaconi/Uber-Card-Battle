using UnityEngine;

public class AnimacioPerdedor : MonoBehaviour {
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	public struct Posicio{
		public float positionX;
		public float positionY;
		public float positionZ;
		public float rotationX;
		public float rotationY;
		public float rotationZ;
		public float scaleX;
		public float scaleY;
		public float scaleZ;
	}
	
	public Posicio posicio{
		get{return this.posicio;}
		set{this.posicio = value;}
	}

	public Material textura{
		get{return this.textura;}
		set{this.textura = value;}
	}
	
	public GameObject figura{
		get{return this.figura;}
		set{this.figura = value;}
	}
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public void Start(){
		throw new System.NotImplementedException();
	}

	public void Update(){
		throw new System.NotImplementedException();
	}
}
