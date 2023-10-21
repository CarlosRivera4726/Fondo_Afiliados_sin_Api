public class Empleado {

    public int id {get; set;} = 0;
    public string nombres {get; set;} = "";
    public string apellidos {get; set;}= "";
    public string identificacion {get; set;}="";
    public string telefono {get; set;}="";
    public string correo {get; set;}=" ";

    public Afiliado Afiliado { get; set;}

    public Empleado(string nombres, string apellidos, string identificacion, string telefono, string correo)
    {
        this.nombres = nombres;
        this.apellidos = apellidos;
        this.identificacion = identificacion;
        this.telefono = telefono;
        this.correo = correo;
    }
    public Empleado(){
        
    }
    public override string ToString()
    {
        return $"Empleado:\nNombre y Apellido: {nombres} {apellidos}\nIdentificación: {identificacion}\nTelefono: {telefono}\nCorreo: {correo}\n\n";
    }
}