package serpis.ad;

import static org.junit.Assert.*;

import org.junit.Test;

public class VectorTest {

	@Test
	public void testMin() {
		assertEquals(7, Vector.min(new int[] {7,90,15,37}));
		assertEquals(15, Vector.min(new int[] {30,90,15,37}));
		assertEquals(4, Vector.min(new int[] {30,90,15,4}));
	}

	@Test(expected = java.lang.ArrayIndexOutOfBoundsException.class)
	public void testMinEmpty(){
		Vector.min(new int[]{});
	}
}
