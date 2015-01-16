package serpis.ad;


import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Temporal;
import javax.persistence.TemporalType;

@Entity
public class Categoria {
	private Long id;
	private String nombre;
	
	public Categoria(){
		
	}
	
	@Id
	public Long getId() {
		return id;
    }

    private void setId(Long id) {
		this.id = id;
    }


    public String getNombre() {
		return nombre;
    }

    public void setNombre(String nombre) {
		this.nombre = nombre;
    }
}
