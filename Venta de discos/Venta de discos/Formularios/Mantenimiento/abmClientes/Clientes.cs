﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Venta_de_discos.Formularios.Mantenimiento.abmCliente;
using Venta_de_discos.Repositorios;

namespace Venta_de_discos.Formularios.Mantenimiento.abmCliente
{
    public partial class Clientes : Form
    {

        ClientesRepositorio clientesRepositorio = new ClientesRepositorio();

        public Clientes()
        {
            InitializeComponent();
        }

        private void actualizarClientes()
        {
            var barrios = clientesRepositorio.ObtenerCliente();
            dgvClientes.DataSource = barrios;
            this.dgvClientes.Columns["id"].Visible = false;
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            actualizarClientes();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            NuevoCliente frm = new NuevoCliente();
            frm.ShowDialog();
            actualizarClientes();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            var seleccionados = dgvClientes.SelectedRows;
            if (seleccionados.Count == 0 || seleccionados.Count > 1)
            {
                MessageBox.Show("Debe seleccionar una fila!");
                return;

            }
            foreach (DataGridViewRow fila in seleccionados)
            {
                var id = fila.Cells[0].Value;
                var ventana = new ModificarCliente(id.ToString());
                ventana.ShowDialog();
                actualizarClientes();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            var seleccionados = dgvClientes.SelectedRows;
            if (seleccionados.Count == 0 || seleccionados.Count > 1)
            {
                MessageBox.Show("Deberia seleccionar una fila");
                return;
            }
            foreach (DataGridViewRow fila in seleccionados)
            {
                var nombre = fila.Cells[1].Value;
                var id = fila.Cells[0].Value;

                var confirmacion = MessageBox.Show($"¿Esta seguro/a de eliminar a {nombre}?",
                    "Confirmar operacion",
                    MessageBoxButtons.YesNo);

                if (confirmacion.Equals(DialogResult.No))
                    return;

                if (clientesRepositorio.Eliminar(id.ToString()))
                {
                    MessageBox.Show($"Usted Elimino a {nombre}");
                    actualizarClientes();
                }

            }
        }
    }
}