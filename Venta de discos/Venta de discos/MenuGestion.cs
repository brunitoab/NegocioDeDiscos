﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Venta_de_discos
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscarDisco_Click(object sender, EventArgs e)
        {
            BuscarDisco frmBuscar = new BuscarDisco();
            frmBuscar.ShowDialog();
        }

        private void btnVentaDeDiscos_Click(object sender, EventArgs e)
        {
            VentaDeDiscos frmVentas = new VentaDeDiscos();
            frmVentas.ShowDialog();

        }

        private void frmMenu_Load(object sender, EventArgs e)
        {

        }
    }
}