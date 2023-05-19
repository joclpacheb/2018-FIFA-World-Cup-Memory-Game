using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//system.media es para sonidos 
using System.Media;  

/*
 * 
 * Jose Luis Pacheco (IEC-16100388)
 Programacion Avanzada UNY
 * 
 */


namespace MemoriaMundialistaFinal
{
    public partial class Menu : Form
    {
       //CONSTRUCTOR DE LA CLASE MENU

        public Menu()
        {
            InitializeComponent();
            
            //se agregan controles del fondo al panel para las imagenes png con fondos transparentes... 
            pictureBox2.Controls.Add(pictureBox3);
            pictureBox2.Controls.Add(pictureBox1);

           //se hacen sus fondos transparentes....
            pictureBox3.BackColor = Color.Transparent;
            pictureBox1.BackColor = Color.Transparent;
  
           //se crea un objeto cancion para reproducir la musica de fondo...

           SoundPlayer cancion = new SoundPlayer(Properties.Resources.Live_It_Up_INSTRUMENTAL____2018_FIFA_World_Cup_Russia_);
           cancion.PlayLooping();//el looping es para repetirla una y otra vez...
          }

//PROGRAMACION DE LOS BOTONES

        private void button2_Click(object sender, EventArgs e)
        {
           Creditos cred = new Creditos();
            cred.Show();    
            this.Hide();
        }   



        private void button1_Click(object sender, EventArgs e)
        {
            Memoria memo= new Memoria();
            memo.Show();
            this.Hide();
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }




    }
}
