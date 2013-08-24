using UnityEngine;
using System.Collections;

public class CartaDreta : MonoBehaviour {
	
	public int ID;

	// Use this for initialization
	void Start () {
		gameObject.transform.position = Camera.mainCamera.ScreenToWorldPoint(new Vector3(Camera.mainCamera.pixelWidth*0.93f,
			Camera.mainCamera.pixelHeight*0.15f, 
			-5-gameObject.transform.position.z));
		ID = gameObject.GetInstanceID();
	}
	
	// Update is called once per frame
	void Update () {
		if(detectaClick()){
			AccioEdicio a = new AccioCartesDreta(1);
			a.executarAccio();
		}
	}
	
	public bool detectaClick(){
		bool click = false;
		if(Input.GetMouseButtonDown(0)){
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float marge = 100;
 			if (Physics.Raycast (ray, out hit, marge)){
				if(hit.collider.gameObject.transform.tag.Equals("BotoDreta")){
					CartaDreta c = (CartaDreta) hit.collider.gameObject.GetComponent(typeof(CartaDreta));
					if(c.ID.Equals(this.ID)){
						gameObject.renderer.material = Resources.Load("BotoDretaPolsat") as Material;
					}
				}
			}
		}
		if(Input.GetMouseButtonUp(0)){
			RaycastHit hit = new RaycastHit();
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			float marge = 100;
 			if (Physics.Raycast (ray, out hit, marge)){
				if(hit.collider.gameObject.transform.tag.Equals("BotoDreta")){
					CartaDreta c = (CartaDreta) hit.collider.gameObject.GetComponent(typeof(CartaDreta));
					if(c.ID.Equals(this.ID)){
						if(gameObject.renderer.material.name.Equals("BotoDretaPolsat (Instance)")){
							click = true;
						}
					}
				}
			}
			if(gameObject.renderer.material.name.Equals("BotoDretaPolsat (Instance)")){
				gameObject.renderer.material = Resources.Load("BotoDretaNoPolsat") as Material;
			}
		}
		return click;
	}
}
