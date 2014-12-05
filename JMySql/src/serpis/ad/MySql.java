package serpis.ad;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
//import java.sql.Statement;
import java.sql.PreparedStatement;
import java.util.*;

/* java -cp .:/usr/share/java/mysql.jar serpis.ad.MySql
 * Introducir esa orden en la terminal para poder ejecutarlo introduciendo las librerías
 * Si no se pone esta orden no importa y no funciona correctamente (En terminal)*/
public class MySql {

	private static Scanner scanner = new Scanner(System.in);
	
	public static void main(String[] args) throws ClassNotFoundException, SQLException {
		//Class.forName("com.mysql.jdbc.Driver"); Necesario en tipo 3 o inferior
		System.out.println("Hola MySql desde eclipse");
		System.out.println("Dime tu nombre");
		String nombre = scanner.nextLine();
		System.out.println("Hola "+nombre);
		Connection connection = DriverManager.getConnection(
				"jdbc:mysql://localhost/dbprueba?user=root&password=sistemas"
				);
//		Statement statement = connection.createStatement();
//		ResultSet resultSet = statement.executeQuery("select * from categoria");
		
		PreparedStatement preparedStatement = connection.prepareStatement(
				"select * from categoria where nombre like ?"
				);
		preparedStatement.setObject(1, "%on%");
		ResultSet resultSet = preparedStatement.executeQuery();
		
		while(resultSet.next()){
			System.out.printf("id=%4s nombre=%s\n", resultSet.getObject("id"), resultSet.getObject("nombre"));
		}
		resultSet.close();
		//statement.close();
		preparedStatement.close();
		connection.close();
		//No es necesario cerrar el scanner, pero es conveniente.
		scanner.close();
	}

}
