﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Venta_de_discos
{

    class acceso_BD
    {
        //instancia objeto <conexion> de tipo <OleDbConnection>
        OleDbConnection conexion = new OleDbConnection();
        //instancia objeto <cmd> del tipo <OleDbCommand>
        OleDbCommand cmd = new OleDbCommand();
        //se crea uan variable local a la clase <cadena_conexio> para alojar la cadena de conexión
        //con la base de datos que se desea conecta.
        //Esta cadena de conexion se obtiene del explorador de servidores.
        //El explorador de servidores brinda la cande de conexion, pero recordar que es incompleta,
        //pues no suministra el componete <Password> que debe agregado manualmente por el programador

        //string cadena_conexion = "Provider=SQLNCLI11;Data Source=maquis;Persist Security Info=True;User ID=avisuales1;Initial Catalog=pav1-db-peliculas;password=avisuales1"; //workstation id=pav1-db.mssql.somee.com;packet size=4096;user id=milizc_SQLLogin_1;pwd=2s9o1yeexo;data source=pav1-db.mssql.somee.com;persist security info=False;initial catalog=pav1-db
        string cadena_conexion = "Provider=SQLNCLI11;workstation id = GerardoDB.mssql.somee.com; packet size = 4096; user id = geraCrossfit_SQLLogin_1; pwd=otyvkmvxvm;data source = GerardoDB.mssql.somee.com; persist security info=False;initial catalog = GerardoDB";
        //procedimiento privado <conectar> que prepara la conexión con la base de dato
        public void conectar()
        {
            //asigan al objeto <conexion> la cadena de conexion
            conexion.ConnectionString = cadena_conexion;
            //agrega la conexion (se crea el pipe entre la aplicación y la base de datos)
            conexion.Open();
            //se comunica al objeto <cmd> sobre que conexion debe trabajar
            cmd.Connection = conexion;
            //se establece el tipo de comando que va ha ejecutar
            cmd.CommandType = CommandType.Text;
        }
        //procedimiento privado <cerrar> que finaliza la conexión con la base de datos
        public void cerrar()
        {
            //cierra la conexión con la base de datos
            conexion.Close();
        }
        //función pública <consulta> que permite a través de parámetro de entra recibir
        //un comando SQL del tipo SELECT para ser ejecutado en la base de datos.
        //Este comando SELECT selecciona un conjunto de datos en la base de datos, que
        //es devuelto un cursor a travéz de <cmd>.
        //Por esto la función tiene como valor de devolución una DataTable.
        public DataTable consulta(string comando)
        {
            //ejecuta el procedimiento local <conectar>
            conectar();
            //asigna a <cmd> el comando que se debe ejecutar, que viene por parámetro
            //de entrada <comando>
            cmd.CommandText = comando;
            //instancia un objeto <tabla> del tipo DataTable
            DataTable tabla = new DataTable();
            //aquí dos acciones. 1) Ejecuta el comando SQL que ingreso por parámetro de entrada
            //en el pedazo de comando <cmd.ExecuteReader()>
            //2) Carga la tabla con el valor de resultado del comando SQL en el pedazo de texto
            //<tabla.Load(. . . )>
            tabla.Load(cmd.ExecuteReader());
            //ejecuta el procedimiento <cerrar>
            cerrar();
            //devuelve el valor calculado a través de la función
            return tabla;
        }
        public bool EjecutarSQL(string comando)
        {
            conectar();

            cmd.CommandText = comando;

            var filasAfectadas = cmd.ExecuteNonQuery(); //Cantidad de filas afectadas

            //ejecuta el procedimiento <cerrar>
            cerrar();

            return filasAfectadas > 0;
        }
        public bool EjecutarSQLEnTransaccion(string comando)
        {

            cmd.CommandText = comando;

            var filasAfectadas = cmd.ExecuteNonQuery(); //Cantidad de filas afectadas


            return filasAfectadas > 0;
        }

        public int EjecutarTransaccion(string comando)
        {
            var id = 0;
            cmd.CommandText = comando;

            if (cmd.ExecuteNonQuery() > 0)
            {
                string consultaGetId = "Select @@Identity";
                cmd.CommandText = consultaGetId;
                id = int.Parse(cmd.ExecuteScalar()?.ToString());
            }
            return id;
        }

        public DataTable ConsultaDuranteTransaccion(string comando)
        {

            cmd.CommandText = comando;
            //instancia un objeto <tabla> del tipo DataTable
            DataTable tabla = new DataTable();

            tabla.Load(cmd.ExecuteReader());

            //devuelve el valor calculado a través de la función
            return tabla;
        }

        public bool EjecutarSentenciaPreparadaSQL(string comando)
        {
            conectar();

            cmd.CommandText = comando;

            var filasAfectadas = cmd.ExecuteNonQuery(); //Cantidad de filas afectadas

            //ejecuta el procedimiento <cerrar>
            cerrar();

            return filasAfectadas > 0;
        }

        public OleDbTransaction IniciarTransaccion()
        {
            conectar();
            var transaccion = conexion.BeginTransaction();
            cmd.Transaction = transaccion;

            return transaccion;

        }
    }
}
