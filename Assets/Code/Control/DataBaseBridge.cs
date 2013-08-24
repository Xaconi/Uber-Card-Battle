using UnityEngine;
using System.IO;

public class DataBaseBridge : MonoBehaviour{
	
	//--------------------------
	// Variables, gets and sets
	//--------------------------
	
	public string connection{
		get;
		set;
	}

	public int comanda{
		get{return this.comanda;}
		set{this.comanda = value;}
	}
	
	//-------------------------------
	// Methods, functions and actions
	//-------------------------------

	public void guardarCartesNoves(){
		//throw new System.NotImplementedException();
	}
	
	public void Awake(){
//		Debug.Log(Application.dataPath+"/Text.db");
//		db.Open(Application.dataPath+"/Text.db");
//		#if UNITY_EDITOR
//			var db = new SqliteDatabase();
//    		db.Open(Application.dataPath+"/Text.db");
//			var dataTable = db.ExecuteQuery("SELECT * FROM Card");
//			GameObject.FindGameObjectWithTag("Finish").guiText.text = (string) (dataTable.Rows[0][dataTable.Columns[0]]);
//			db.Close();
//  		#endif
//		#if UNITY_ANDROID
//			string filepath = Application.persistentDataPath + "/Text.db";
//			if(!File.Exists(filepath)){
//    			WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/Text.db");  // this is the path to your StreamingAssets in android
//    			while(!loadDB.isDone) {}  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
//    			File.WriteAllBytes(filepath, loadDB.bytes);
//			}
//			var dbCon = new SqliteDatabase();
//    		dbCon.Open(Application.dataPath+"/Text.db");
//			var dT = dbCon.ExecuteQuery("SELECT * FROM Card");
//			GameObject.FindGameObjectWithTag("Finish").guiText.text = (string) (dT.Rows[0][dT.Columns[0]]);
//			dbCon.Close();
////			var dT = dbcon.("SELECT * FROM Card");
////			GameObject.FindGameObjectWithTag("Finish").guiText.text = (string) (dT.Rows[0][dT.Columns[0]]);
////			dbcon.Close();
//		#endif
	}
//
//	public void guardarEstadistiques(){
//		throw new System.NotImplementedException();
//	}
}
