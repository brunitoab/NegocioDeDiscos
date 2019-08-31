﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Venta_de_discos.Clases;
using Venta_de_discos.Repositorios;

namespace Venta_de_discos.Formularios.Mantenimiento
{
    public partial class NuevoInterprete : Form
    {
        PaisRepositorio paisRepositorio;
        InterpretesRepositorio interpretesRepositorio;
        public NuevoInterprete()
        {
            InitializeComponent();
            paisRepositorio = new PaisRepositorio();
            interpretesRepositorio = new InterpretesRepositorio();
        }

        private void NuevoInterprete_Load(object sender, EventArgs e)
        {
            txtNombre.Focus();
            ActualizarComboPais();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ActualizarComboPais()
        {
            var paises = paisRepositorio.ObtenerPais();
            cmbPais.ValueMember = "Id";
            cmbPais.DisplayMember = "nombre";
            cmbPais.DataSource = paises;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var interprete = new Interprete();
            interprete.Nombre = txtNombre.Text;
            interprete.Id_Pais = cmbPais.SelectedValue.ToString();

            if (!interprete.NombreValido())
            {
                MessageBox.Show("Nombre Invalido!");
                txtNombre.Text = " ";
                txtNombre.Focus();
                return;
            }

            if(interpretesRepositorio.Guardar(interprete))
            {
                MessageBox.Show("Se agrego director con exito!");
                this.Dispose();
            }






        }
    }
}