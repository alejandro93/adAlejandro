package serpis.ad;

public class Vector {

	public static void main(String[] args) {
		int [] vector = {7, 8, 1, 4, 9};
		System.out.println(min(vector));
	}
	public static int min(int[] v){
		int guarda = v[0];
		for(int i=1; i<v.length; i++){
			if(v[i] < guarda){
				guarda = v[i];
			}
		}
//		for (int item : v)
//			if(item < guarda)
//				guarda = item;
		
		return guarda;
	}

}
