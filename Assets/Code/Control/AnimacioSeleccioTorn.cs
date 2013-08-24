using UnityEngine;
using System.Collections;

public class AnimacioSeleccioTorn : MonoBehaviour {
	
	public Texture2D menu{
		get;
		set;
	}
	
	public bool moviment;
	private bool players;
	private bool fiSeleccioTorn;
	private float pPantalla = 0.0f;
	private float posPantalla = 0.0f;
	private Texture2D player1;
	private Texture2D player2;
	private Texture2D player;
	private int variacioTornPlayer = 1;
	private int margeTornPlayer = 1;
	private int limitSorteigTorn = 35;
	private int playerTorn;
	public delegate void Partida(int pT);
  	public event Partida carregaBaralles;
	
	// Singleton
	private static AnimacioSeleccioTorn instance;
	
	public static AnimacioSeleccioTorn Instance{
		get
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType(typeof(AnimacioSeleccioTorn)) as AnimacioSeleccioTorn;
			
			return instance;
		}
	}
	
	public void Awake(){
		//menu = (GUITexture) GUITexture.Instantiate(Resources.Load("MenuSeleccioTorn"));
		menu = (Texture2D) Resources.Load("pantallaSeleccioTorn");
		player1 = (Texture2D) Resources.Load("Player1");
		player2 = (Texture2D) Resources.Load("Player2");
		player = player1;
		moviment = false;
		players = false;
		fiSeleccioTorn = false;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnGUI(){
		if(moviment){
			if(pPantalla <= 1.0f){
				pPantalla += 0.01f;
				Rect pantalla = new Rect(0.0f, Camera.mainCamera.pixelHeight*0.2f, Camera.mainCamera.pixelWidth*pPantalla*0.6f, Camera.mainCamera.pixelHeight*0.6f);
				Rect percentatge1 = new Rect(1.0f-pPantalla, 0.0f, pPantalla, 1.0f);
				Graphics.DrawTexture(pantalla, menu, percentatge1,0,0,0,0,null);
			}else{
				if(posPantalla <= Camera.mainCamera.pixelWidth*0.17f) posPantalla += 4.9f;
				else if(posPantalla <= Camera.mainCamera.pixelWidth*0.2f) posPantalla += 1.5f;
				else{
					moviment = false;
					players = true;
				}
				Rect pantalla2 = new Rect(posPantalla,
					Camera.mainCamera.pixelHeight*0.2f,
					Camera.mainCamera.pixelWidth*0.6f,
					Camera.mainCamera.pixelHeight*0.6f);
				Graphics.DrawTexture(pantalla2, menu);
			}
		}else if(players){
			if(variacioTornPlayer > margeTornPlayer){
				if(player == player1) player = player2;
				else{
					player = player1;
					margeTornPlayer += 2;
				}
				variacioTornPlayer = 0;
			}
			
			Rect screen = new Rect(Camera.mainCamera.pixelWidth*0.2f,
											Camera.mainCamera.pixelHeight*0.2f,
											Camera.mainCamera.pixelWidth*0.6f,
											Camera.mainCamera.pixelHeight*0.6f);
			Graphics.DrawTexture(screen, menu);
			
			Rect playerScreen = new Rect(Camera.mainCamera.pixelWidth*0.3f,
											Camera.mainCamera.pixelHeight*0.3f,
											Camera.mainCamera.pixelWidth*0.4f,
											Camera.mainCamera.pixelHeight*0.4f);
			Graphics.DrawTexture(playerScreen, player);
			
			variacioTornPlayer++;
			if(margeTornPlayer >= limitSorteigTorn){
				players = false;
				fiSeleccioTorn = true;
				if(player == player1) playerTorn = 1;
				else playerTorn = 2;
			}
		}else if(fiSeleccioTorn){
			if(posPantalla > 0.0f){
				if(posPantalla >= Camera.mainCamera.pixelWidth*0.03f) posPantalla -= 4.9f;
				else if(posPantalla >= Camera.mainCamera.pixelWidth*0.0f) posPantalla -= 1.5f;

				Rect pantalla2 = new Rect(posPantalla,
					Camera.mainCamera.pixelHeight*0.2f,
					Camera.mainCamera.pixelWidth*0.6f,
					Camera.mainCamera.pixelHeight*0.6f);
				Graphics.DrawTexture(pantalla2, menu);
			}else{
				pPantalla -= 0.01f;
				Rect pantalla = new Rect(0.0f, Camera.mainCamera.pixelHeight*0.2f, Camera.mainCamera.pixelWidth*pPantalla*0.6f, Camera.mainCamera.pixelHeight*0.6f);
				Rect percentatge1 = new Rect(1.0f-pPantalla, 0.0f, pPantalla, 1.0f);
				Graphics.DrawTexture(pantalla, menu, percentatge1,0,0,0,0,null);
				if(pPantalla < 0.0f){
					carregaBaralles(playerTorn);
				}
			}
		}
	}
}
