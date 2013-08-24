using UnityEngine;
using System.Collections;

public class DadesHeuristiques {

	public static treurePersonatge sortidaPersonatge = new treurePersonatge(8, 6, 4, 1, -1);
	public static mourePersonatge movimentPersonatge = new mourePersonatge(7, 6, 2, 1, -1);
	public static atacarPersonatge atacPersonatge = new atacarPersonatge(14, 11, 10, 7, 3, 2);
	public static atacarBase atacBase = new atacarBase(20, 16, 13, 12, 11, 4, -1);
	public static aplicarBonificacio aplicacioBonificacio = new aplicarBonificacio(15, 13, 12, 11, 10, 4, -1);
}

public class aplicarBonificacio{
	public int mataBase;
	public int mataPersonatge;
	public int personatgeFerit;
	public int atacaBase;
	public int atacaPersonatge;
	public int personatgeNormal;
	public int personatgePerill;
	
	public aplicarBonificacio(int a, int b, int c, int d, int e, int f, int g){
		mataBase = a;
		mataPersonatge = b;
		personatgeFerit = c;
		atacaBase = d;
		atacaPersonatge = e;
		personatgeNormal = f;
		personatgePerill = g;
	}
}

public class atacarBase{
	public int mataBase;
	public int atacaBaseMataBase;
	public int atacaBaseAtacaBase;
	public int atacaBaseMataPersonatge;
	public int atacaBaseAtacaPersonatge;
	public int atacaBasePersonatgeNormal;
	public int atacaBasePersonatgePerill;
	
	public atacarBase(int a, int b, int c, int d, int e, int f, int g){
		mataBase = a;
		atacaBaseMataBase = b;
		atacaBaseMataPersonatge = c;
		atacaBaseAtacaPersonatge = d;
		atacaBaseAtacaBase = e;
		atacaBasePersonatgeNormal = f;
		atacaBasePersonatgePerill = g;
	}
}

public class treurePersonatge{
	public int mataPersonatge;
	public int atacaPersonatge;
	public int personatgeSol;
	public int personatgeNormal;
	public int personatgePerill;
	
	public treurePersonatge(int a, int b, int c, int d, int e){
		mataPersonatge = a;
		atacaPersonatge = b;
		personatgeSol = c;
		personatgeNormal = d;
		personatgePerill = e;
	}
}

public class mourePersonatge{
	public int mataPersonatge;
	public int atacaPersonatge;
	public int personatgeSol;
	public int personatgeNormal;
	public int personatgePerill;
	
	public mourePersonatge(int a, int b, int c, int d, int e){
		mataPersonatge = a;
		atacaPersonatge = b;
		personatgeSol = c;
		personatgeNormal = d;
		personatgePerill = e;
	}
}

public class atacarPersonatge{
	public int personatgeMortMataPersonatge;
	public int personatgeMortAtacaPersonatge;
	public int personatgeAtacatMataPersonatge;
	public int personatgeAtacatAtacaPersonatge;
	public int personatgeNormal;
	public int personatgePerill;
	
	public atacarPersonatge(int a, int c, int d, int e, int f, int g){
		personatgeMortMataPersonatge = a;
		personatgeMortAtacaPersonatge = c;
		personatgeAtacatMataPersonatge = d;
		personatgeAtacatAtacaPersonatge = e;
		personatgeNormal = f;
		personatgePerill = g;
	}
}
