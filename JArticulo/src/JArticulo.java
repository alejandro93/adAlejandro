import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.sql.PreparedStatement;
import java.util.*;

public class JArticulo {
	static ResultSet resultSet = null;
	private static Scanner scanner = new Scanner(System.in);
	static Connection connection;
	
	public static void main(String[] args) throws ClassNotFoundException, SQLException {
		
		int eleccion;

		while(true){
			/*Menu*/
			System.out.println("\nMenu\n----------------");
			System.out.println("0 = Salir");
			System.out.println("1 = Nuevo");
			System.out.println("2 = Editar");
			System.out.println("3 = Eliminar");
			System.out.println("4 = Visualizar");
			
			eleccion = scanner.nextInt();
			
			switch(eleccion){
				case 0: {  //Salir
					System.out.println("Éxito al salir");
					return;
					}
				case 1: {  //Añadir
					System.out.println("Introduce nombre:");
					String nombre = scanner.next();
					System.out.println("Introduce precio:");
					int precio = scanner.nextInt();
					scanner.nextLine();
					System.out.println("Introduce categoría:");
					String cat = scanner.nextLine();
					anyadir(nombre, precio, cat);
					System.out.println("Artículo añadido con éxito");
					break;
				}
				case 2: {  //Editar
					visual();
					System.out.println("Escribe el id del elemento que desea modificar");
					int id = scanner.nextInt();
					scanner.nextLine();
					System.out.println("Escribe el nuevo nombre del elemento");
					String nombre = scanner.nextLine();
					System.out.println("Escribe el nuevo precio del elemento");
					int precio = scanner.nextInt();
					scanner.nextLine();
					System.out.println("Escribe la nueva categoria del elemento");
					String cat = scanner.nextLine();
					System.out.println();
					editar(id, nombre, precio, cat);
					System.out.println("\nModificado con éxito\n");
					break;
				}
				case 3: {  //Eliminar
					visual();
					System.out.println("Escribe el id del elemento que quiere eliminar: ");
					int id = scanner.nextInt();
					eliminar(id);
					System.out.println("Eliminado con éxito");
					break;
				}
				case 4: {  //Visualizar
					System.out.println("Listado:\n*****************************************************************************");
					visualizar();
					System.out.println("*********************************************************************************");
					break;
				}
			}
		}
	}
		
	
	public static void anyadir(String nombre, int precio, String cat) throws SQLException{
		connection = connection();
		PreparedStatement insert = connection.prepareStatement(
				"insert into categoria (nombre, precio, cat) values (?, ?, ?)"
				);
		insert.setObject(1, nombre);
		insert.setObject(2, precio);
		insert.setObject(3, cat);
		insert.execute();
		insert.close();
		connection.close();
	}
	public static void editar(int id, String nombre, int precio, String cat) throws SQLException{
		connection = connection();
		PreparedStatement update = connection.prepareStatement(
				"update categoria set nombre=?, precio=?, cat=? where id=?"
				);
		update.setObject(1, nombre);
		update.setObject(2, precio);
		update.setObject(3, cat);
		update.setObject(4, id);
		update.executeUpdate();
		PreparedStatement listar = connection.prepareStatement("select * from categoria where id=?");
		listar.setObject(1, id);
		resultSet = listar.executeQuery();
		while(resultSet.next()){
			System.out.printf("id=%d nombre=%s precio=%d categoria=%s\n", resultSet.getObject("id"), resultSet.getObject("nombre"), resultSet.getObject("precio"), resultSet.getObject("cat"));
		}
		listar.close();
		update.close();
		resultSet.close();
		connection.close();
	}
	public static void eliminar(int id) throws SQLException{
		connection = connection();
		PreparedStatement delete = connection.prepareStatement(
				"delete from categoria where id=?"
				);
		delete.setInt(1, id);
		delete.execute();
		delete.close();
		connection.close();
	}
	public static void visual() throws SQLException{
		connection = connection();
		Statement listar = connection.createStatement();
		resultSet = listar.executeQuery("select * from categoria");
		while(resultSet.next()){
			System.out.printf("id=%-4d    nombre=%-10s\n", resultSet.getObject("id"), resultSet.getObject("nombre"));
		}
		listar.close();
		resultSet.close();
		connection.close();
	}
	
	public static void visualizar() throws SQLException{
		connection = connection();
		Statement listar = connection.createStatement();
		resultSet = listar.executeQuery("select * from categoria");
		while(resultSet.next()){
			System.out.printf("|id=%-4d nombre=%-10s precio=%-6d categoria=%-10s|\n", resultSet.getObject("id"), resultSet.getObject("nombre"), resultSet.getObject("precio"), resultSet.getObject("cat"));
		}
		listar.close();
		connection.close();
	}
	public static Connection connection(){
		try{
		Connection connection = DriverManager.getConnection(
				"jdbc:mysql://localhost/dbprueba?user=root&password=sistemas"
				);
		return connection;
		}catch(Exception e){e.getMessage();}
		return null;
	}
}

