using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
/*
 * 
 * Jose Luis Pacheco (IEC-16100388)
 Programacion Avanzada UNY
 * 
 */

namespace MemoriaMundialistaFinal
{
    public partial class Creditos : Form
    {
        public Creditos()
        {
            InitializeComponent();

           //controles para hacer transparente el fondo del logo de la UNY...
           
            pictureBox1.Controls.Add(pictureBox2);

            pictureBox1.BackColor = Color.Transparent;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menu men = new Menu();
            men.Show();
            this.Hide();
        }

        private void Creditos_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    

        
      
    }
}
