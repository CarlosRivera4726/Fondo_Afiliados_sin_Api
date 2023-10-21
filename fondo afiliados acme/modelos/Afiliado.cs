public class Afiliado {
    int id {get; set;} = 0;
    string estado {get; set;} = "";
    string fechaRetiro {get; set;}= "";
    string fechaIngreso {get; set;}="";
    
    public ICollection <Empleado> Empleados {get;set;}

    public override string ToString()
    {
        return $"ID: {id}\nEstado: {estado}\nFecha Retiro: {fechaRetiro}\nFecha Ingreso: {fechaIngreso}\n";
    }


}