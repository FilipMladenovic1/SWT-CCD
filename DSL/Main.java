package flowerDSL;

public class Main {

	public static void main(String[] args) {
		Bouquet bouquet1 = (new Bouquet("rose","red",5));
		Bouquet bouquet2 = (new Bouquet("lilly","purple",7));
		Bouquet bouquet3 = (new Bouquet("marigold","yellow",9));
		Bouquet bouquet4 = (new Bouquet("azalea","pink",10));
		Bouquet bouquet5 = (new Bouquet("tulip","white",6));
		
		System.out.println(bouquet1);
		System.out.println(bouquet2);
		System.out.println(bouquet3);
		System.out.println(bouquet4);
		System.out.println(bouquet5);
	}

}
