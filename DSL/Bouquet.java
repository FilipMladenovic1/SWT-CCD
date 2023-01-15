package flowerDSL;

public class Bouquet {
	private final String flower;
	private final String color;
	private final int amount;
	
	
	public Bouquet (String flower, String color, int amount) {
		this.flower = flower;
		this.color = color;
		this.amount = amount;
		
	}
	
	public String toString(){
		return "Your bouquet of flowers consists of: " + this.flower + ",(color: " +this.color + ")," + "(amount: " +this.amount + ")";
	}
}
