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


        }

        private void btnVentaDeDiscos_Click(object sender, EventArgs e)
        {
            GenerarVenta frmVentas = new GenerarVenta();
            frmVentas.ShowDialog();

        }

        private void frmMenu_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscarDisco_Click_1(object sender, EventArgs e)
        {
            BuscarDisco frm = new BuscarDisco();
            frm.ShowDialog();
        }

        private void btnInformes_Click(object sender, EventArgs e)
        {
            MenuMantenimiento frm = new MenuMantenimiento();
            frm.ShowDialog();
        }
    }
}
