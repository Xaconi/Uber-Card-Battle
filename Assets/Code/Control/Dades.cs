using UnityEngine;
using System.Collections;

public class Dades {

	// Dades de tots els personatges
	
	public static Bronze bronze = new Bronze();
	public static Plata plata = new Plata();
	public static Or or = new Or();
	public static Plati plati = new Plati();
}

public class Bronze {
	public DadaPersonatge dadaPersonatge;
	public DadaBonificacio dadaBonificacio;
	
	public Bronze(){
		dadaPersonatge = new DadaPersonatge();
		dadaBonificacio = new DadaBonificacio();
		
		dadaPersonatge.guerrero = new Guerrero		(0, 9, 34, 0, 11, 36, 3, 1);
		dadaPersonatge.metralleta = new Metralleta	(7, 10, 38, 9, 11, 40, 3, 3);
		dadaPersonatge.bazooka = new Bazooka		(13, 0, 44, 15, 0, 46, 3, 4);
		dadaPersonatge.franco = new Franco			(7, 3, 24, 9, 5, 26, 5, 4);
		
		dadaBonificacio.atac = new Atac(2,3);
		dadaBonificacio.defensa = new Defensa(2,3);
		dadaBonificacio.uber = new Uber(2,3,2,3);
	}
}

public class Plata {
	public DadaPersonatge dadaPersonatge;
	public DadaBonificacio dadaBonificacio;
	
	public Plata(){
		dadaPersonatge = new DadaPersonatge();
		dadaBonificacio = new DadaBonificacio();
		dadaPersonatge.guerrero = new Guerrero		(0, 12, 37, 0, 13, 40, 3, 1);
		dadaPersonatge.metralleta = new Metralleta	(10, 14, 41, 11, 16, 44, 3, 3);
		dadaPersonatge.bazooka = new Bazooka		(16, 0, 47, 18, 0, 50, 3, 4);
		dadaPersonatge.franco = new Franco			(10, 6, 27, 11, 7, 28, 5, 4);
		
		dadaBonificacio.atac = new Atac(4,5);
		dadaBonificacio.defensa = new Defensa(4,5);
		dadaBonificacio.uber = new Uber(4,5,4,5);
	}
}

public class Or {
	public DadaPersonatge dadaPersonatge;
	public DadaBonificacio dadaBonificacio;
	
	public Or(){
		dadaPersonatge = new DadaPersonatge();
		dadaBonificacio = new DadaBonificacio();
		dadaPersonatge.guerrero = new Guerrero		(0, 14, 41, 0, 15, 43, 3, 1);
		dadaPersonatge.metralleta = new Metralleta	(12, 16, 44, 14, 18, 45, 3, 3);
		dadaPersonatge.bazooka = new Bazooka		(19, 0, 51, 22, 0, 53, 3, 4);
		dadaPersonatge.franco = new Franco			(12, 8, 29, 14, 9, 31, 5, 4);
		
		dadaBonificacio.atac = new Atac(6,7);
		dadaBonificacio.defensa = new Defensa(6,7);
		dadaBonificacio.uber = new Uber(6,7,6,7);
	}
}

public class Plati {
	public DadaPersonatge dadaPersonatge;
	public DadaBonificacio dadaBonificacio;
	
	public Plati(){
		dadaPersonatge = new DadaPersonatge();
		dadaBonificacio = new DadaBonificacio();
		dadaPersonatge.guerrero = new Guerrero		(0, 14, 41, 0, 15, 43, 3, 1);
		dadaPersonatge.metralleta = new Metralleta	(16, 19, 49, 19, 22, 52, 3, 3);
		dadaPersonatge.bazooka = new Bazooka		(23, 0, 54, 28, 0, 56, 3, 4);
		dadaPersonatge.franco = new Franco			(16, 10, 32, 19, 12, 34, 5, 4);
		
		dadaBonificacio.atac = new Atac(8,9);
		dadaBonificacio.defensa = new Defensa(8,9);
		dadaBonificacio.uber = new Uber(8,9,8,9);
	}
}

public class DadaPersonatge{
	public Guerrero guerrero;
	public Metralleta metralleta;
	public Bazooka bazooka;
	public Franco franco;
	
	public DadaPersonatge(){
		
	}
}

public class Guerrero{
	public int atacLBaix;
	public int atacCBaix;
	public int defensaBaix;
	public int atacLAlt;
	public int atacCAlt;
	public int defensaAlt;
	public int moviment;
	public int distanciaA;
	
	public Guerrero(int a, int b, int c, int d, int e, int f, int g, int h){
		atacLBaix = a;
		atacCBaix = b;
		defensaBaix = c;
		atacLAlt = d;
		atacCAlt = e;
		defensaAlt = f;
		moviment = g;
		distanciaA = h;
	}
}

public class Metralleta{
	public int atacLBaix;
	public int atacCBaix;
	public int defensaBaix;
	public int atacLAlt;
	public int atacCAlt;
	public int defensaAlt;
	public int moviment;
	public int distanciaA;
	
	public Metralleta(int a, int b, int c, int d, int e, int f, int g, int h){
		atacLBaix = a;
		atacCBaix = b;
		defensaBaix = c;
		atacLAlt = d;
		atacCAlt = e;
		defensaAlt = f;
		moviment = g;
		distanciaA = h;
	}
}

public class Bazooka{
	public int atacLBaix;
	public int atacCBaix;
	public int defensaBaix;
	public int atacLAlt;
	public int atacCAlt;
	public int defensaAlt;
	public int moviment;
	public int distanciaA;
	
	public Bazooka(int a, int b, int c, int d, int e, int f, int g, int h){
		atacLBaix = a;
		atacCBaix = b;
		defensaBaix = c;
		atacLAlt = d;
		atacCAlt = e;
		defensaAlt = f;
		moviment = g;
		distanciaA = h;
	}
}

public class Franco{
	public int atacLBaix;
	public int atacCBaix;
	public int defensaBaix;
	public int atacLAlt;
	public int atacCAlt;
	public int defensaAlt;
	public int moviment;
	public int distanciaA;
	
	public Franco(int a, int b, int c, int d, int e, int f, int g, int h){
		atacLBaix = a;
		atacCBaix = b;
		defensaBaix = c;
		atacLAlt = d;
		atacCAlt = e;
		defensaAlt = f;
		moviment = g;
		distanciaA = h;
	}
}
	
public class DadaBonificacio{
	public Atac atac;
	public Defensa defensa;
	public Uber uber;
		
	public DadaBonificacio(){
	}
}
	
public class Atac{
	public int atacAlt;
	public int atacBaix;
		
	public Atac(int a, int b){
		atacBaix = a;
		atacAlt = b;
	}
}
	
public class Defensa{
	public int defensaAlt;
	public int defensaBaix;
		
	public Defensa(int c, int d){
		defensaBaix = c;
		defensaAlt = d;
	}
}
	
public class Uber{
	public int atacAlt;
	public int atacBaix;
	public int defensaAlt;
	public int defensaBaix;
		
	public Uber(int a, int b, int c, int d){
		atacBaix = a;
		atacAlt = b;
		defensaBaix = c;
		defensaAlt = d;
	}
}
