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
    public partial class ModificarInterprete : Form
    {
        InterpretesRepositorio interpretesRepositorio;
        Interprete interprete;
        PaisRepositorio paisRepositorio;
        string _id;

        public ModificarInterprete(string interpreteId)
        {
            InitializeComponent();
            interpretesRepositorio = new InterpretesRepositorio();
            paisRepositorio = new PaisRepositorio();
            interprete = interpretesRepositorio.ObtenerInterprete(interpreteId);

        }

        private void ModificarInterprete_Load(object sender, EventArgs e)
        {
            txtNombre.Text = interprete.Nombre;
            cmbPais.SelectedValue = interprete.Id_Pais;
            _id = interprete.Id;
            ActualizarComboPais();


        }
        private void ActualizarComboPais()
        {
            var paises = paisRepositorio.ObtenerPais();
            cmbPais.ValueMember = "Id";
            cmbPais.DisplayMember = "nombre";
            cmbPais.DataSource = paises;

            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            foreach (DataRow row in paises.Rows)
            {
                collection.Add(Convert.ToString(row["nombre"]));
            }

            cmbPais.AutoCompleteCustomSource = collection;
            cmbPais.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbPais.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            var datosInterprete = new Interprete();
            datosInterprete.Nombre = txtNombre.Text.Trim();
            datosInterprete.Id = _id;

            if (cmbPais.SelectedValue is null)
            {
                MessageBox.Show("Nombre de pais no existe!");
                cmbPais.Focus();
                return;
            }
            else
            {
                datosInterprete.Id_Pais = cmbPais.SelectedValue.ToString();
            }




            if (!datosInterprete.NombreValido())
            {
                MessageBox.Show("Nombre Invalido!");
                txtNombre.Text = "";
                txtNombre.Focus();
                return;
            }
            if (datosInterprete.Nombre != interprete.Nombre)
            {
                if (datosInterprete.NombreRepetido(datosInterprete.Nombre))
                {
                    MessageBox.Show("Nombre ya existe!");
                    txtNombre.Text = "";
                    txtNombre.Focus();
                    return;
                }
            }

            if (!datosInterprete.IdValido())
            {

                MessageBox.Show("Pais Invalido!");
                cmbPais.Focus();
                return;

            }

            if (interpretesRepositorio.Editar(datosInterprete))
            {
                MessageBox.Show("La edicion ha finalizado correctamente");
                this.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NuevoPais frm = new NuevoPais();
            frm.ShowDialog();
            ActualizarComboPais();
        }

        private void btnSalirMenu_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
