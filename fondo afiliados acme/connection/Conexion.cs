using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Data;
using Dapper;

public class Conexion
{
    private IDbConnection connection;
    private const string DATABASE_NAME = "db_afiliados_Acme.db3";

    public Conexion()
    {
        connection = new SqliteConnection($"Data Source = {Environment.CurrentDirectory + $"/Data/{DATABASE_NAME};"}");
    }
    // obtener todos los empleados sin relacion
    public async Task<IEnumerable<Empleado>> GetDataEmpleado(string identificacion)
    {
        var result = await connection.QueryAsync<Empleado>($"SELECT * FROM EMPLEADOS WHERE identificacion = {identificacion}");
        return result;
    }

    
    // ingresar un empleado
    public async Task<bool> PostEmpleado(Empleado empleado)
    {
        var sql = "INSERT INTO Empleados (nombres, apellidos, identificacion, telefono, correo) values (@nombres, @apellidos, @identificacion, @telefono, @correo)";

        var count = await connection.ExecuteAsync(sql, empleado);
        if(count !=1) return false;
        return true;
    }
    // mostrar todos los datos con afiliacion
    public async Task<IEnumerable<Empleado>> GetEmpleadosAfiliados(){
        string sql = @"
        SELECT * 
        FROM EMPLEADOS EM 
        INNER JOIN AFILIADOS AF ON AF.EMPLEADO = EM.ID;";

        var empleados = await connection.QueryAsync<Empleado, Afiliado, Empleado>(sql, (empleado, afiliado) => {
            empleado.Afiliado = afiliado;
            return empleado;
        }, 
        splitOn: "ID");

        return empleados;
    }
    // buscar por identificacion

    public async Task<IEnumerable<Empleado>> GetEmpleadoAfiliado(int identificacion){
        string sql = @"
        SELECT * 
        FROM EMPLEADOS EM 
        INNER JOIN AFILIADOS AF ON AF.EMPLEADO = EM.ID
        "+$"WHERE EM.identificacion = {identificacion};";

        var empleados = await connection.QueryAsync<Empleado, Afiliado, Empleado>(sql, (empleado, afiliado) => {
            empleado.Afiliado = afiliado;
            return empleado;
        }, 
        splitOn: "ID");

        return empleados;
    }

    //actualizar
    public async Task<int> UpdateEmpleadoAfiliado(int identificacion, Empleado empleado){
        string sql = @"UPDATE Empleados SET nombres = @nombres, apellidos = @apellidos, identificacion=@identificacion, telefono=@telefono, correo = @correo WHERE identificacion = '"+identificacion+"';";

        var empleados = await connection.ExecuteAsync(sql, empleado);
        return empleados;
    }
     //eliminar
    public async Task<int> DeleteEmpleadoAfiliado(string identificacion){
        string sql = @"DELETE FROM Empleados WHERE identificacion = '"+identificacion+"';";

        var empleados = await connection.ExecuteAsync(sql);
        return empleados;
    }

    // afiliar
    // ingresar un empleado
    public async Task<bool> PostAfiliacion(Empleado empleado)
    {
        var sql = "INSERT INTO Afiliados (estado, fecharetiro, fechaingreso, empleado) values (@estado, @fecharetiro, @fechaingreso, @empleado)";

        var count = await connection.ExecuteAsync(sql, empleado.Afiliado);
        if(count !=1) return false;
        return true;
    }

}