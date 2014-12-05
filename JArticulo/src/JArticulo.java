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
		
		/*Menu*/
		System.out.println("Menu\n----------------");
		System.out.println("0 = Salir");
		System.out.println("1 = Nuevo");
		System.out.println("2 = Editar");
		System.out.println("3 = Eliminar");
		System.out.println("4 = Visualizar");
		
		final int eleccion = scanner.nextInt();
		
			switch(eleccion){
			case 0: {
				System.out.println("Éxito al salir");
				return;
				}
			case 1: {
				System.out.println("Introduzca los datos del nuevo elemento");
				int id = scanner.nextInt();
				String nombre = scanner.nextLine();
				int precio = scanner.nextInt();
				String cat = scanner.nextLine();
				anyadir(id, nombre, precio, cat);
				break;
			}
			case 2: {
				visual();
				System.out.println("Escribe el id del elemento que desea modificar");
				int id = scanner.nextInt();
				System.out.println("Escribe el nuevo nombre del elemento");
				String nombre = scanner.nextLine();
				System.out.println("Escribe el nuevo precio del elemento");
				int precio = scanner.nextInt();
				System.out.println("Escribe la nueva categoria del elemento");
				String cat = scanner.nextLine();
				editar(id, nombre, precio, cat);
				break;
			}
			case 3: {
				visual();
				System.out.println("Escribe el id del elemento que quiere eliminar: ");
				int id = scanner.nextInt();
				eliminar(id);
				System.out.println("Elemento eliminado con éxito");
				break;
			}
			case 4: {
				System.out.println("Listado:\n******************************************");
				visualizar();
				System.out.println("*********************************************");
				break;
			}
		
			
		}
	

	}
	
	public static void anyadir(int id, String nombre, int precio, String cat) throws SQLException{
		connection = connection();
		PreparedStatement insert = connection.prepareStatement(
				"insert into categoria (id, nombre, precio, cat) values (?, ?, ?, ?)"
				);
		insert.setObject(1, id);
		insert.setObject(2, nombre);
		insert.setObject(3, precio);
		insert.setObject(4, cat);
		resultSet = insert.executeQuery();
		insert.close();
		resultSet.close();
		connection.close();
	}
	public static void editar(int id, String nombre, int precio, String cat) throws SQLException{
		connection = connection();
		PreparedStatement update = connection.prepareStatement(
				"update categoria set id=?, nombre=?, precio=?, cat=? "
				);
		update.setObject(1, id);
		update.setObject(2, nombre);
		update.setObject(3, precio);
		update.setObject(4, cat);
		resultSet = update.executeQuery();
		PreparedStatement listar = connection.prepareStatement("select * from categoria where id=?");
		listar.setObject(1, id);
		resultSet = listar.executeQuery();
		while(resultSet.next()){
			System.out.printf("id=%4s    nombre=%s\n", resultSet.getObject("id"), resultSet.getObject("nombre"));
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
		delete.setObject(1, id);
		resultSet = delete.executeQuery();
		delete.close();
		resultSet.close();
		connection.close();
	}
	public static void visual() throws SQLException{
		connection = connection();
		Statement listar = connection.createStatement();
		resultSet = listar.executeQuery("select * from categoria");
		while(resultSet.next()){
			System.out.printf("id=%4s    nombre=%s\n", resultSet.getObject("id"), resultSet.getObject("nombre"));
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
			System.out.printf("id=%4s    nombre=%s    precio=%s    categoria=%s\n", resultSet.getObject("id"), resultSet.getObject("nombre"), resultSet.getObject("precio"), resultSet.getObject("cat"));
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

