﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta_de_discos.Clases;

namespace Venta_de_discos.Repositorios
{
    class ClientesRepositorio
    {
        private acceso_BD _BD;
        public ClientesRepositorio()
        {
            _BD = new acceso_BD();
        }

        public DataTable ObtenerCliente()
        {
            string sqltxt = "SELECT Id,Nombre,Apellido FROM Cliente";
            return _BD.consulta(sqltxt);
        }

        public Cliente ObtenerCliente(string clienteId)
        {
            string sqltxt = $"SELECT * FROM [dbo].[Cliente] WHERE id = {clienteId}";
            var tablaTemporal = _BD.consulta(sqltxt);

            if (tablaTemporal.Rows.Count == 0)
                return null;

            var cliente = new Cliente();

            foreach (DataRow fila in tablaTemporal.Rows)
            {
                if (fila.HasErrors)
                    continue; // no corto el ciclo

                cliente.Id = fila.ItemArray[0].ToString();
                cliente.Nombre = fila.ItemArray[1].ToString();
                cliente.Apellido = fila.ItemArray[2].ToString(); 
            }

            return cliente;
        }

        public bool Guardar(Cliente cliente)
        {//De aca sale el error. Como generar id diferentes? en base a que los genero
            string sqltxt = $"INSERT[dbo].[Cliente]([Nombre],[Apellido])" +
                $"VALUES " +
                $"('{cliente.Nombre}', " +
                $"'{cliente.Apellido}')";
            return _BD.EjecutarSQL(sqltxt);
        }

        public bool Eliminar(string clienteId)
        {
            string sqltxt = $"DELETE FROM [dbo].[Cliente] WHERE id = {clienteId}";
            return _BD.EjecutarSQL(sqltxt);
        }

        public bool Editar(Cliente cliente)
        {
            string sqltxt = $"UPDATE [dbo].[cliente] SET nombre ='{ cliente.Nombre }'" +
                $", apellido='{ cliente.Apellido}' " +
                $"WHERE id= {cliente.Id}";
            return _BD.EjecutarSQL(sqltxt);

        }
    }
}