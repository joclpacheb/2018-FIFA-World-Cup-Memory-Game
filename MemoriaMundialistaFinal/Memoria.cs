using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

/*
 * 
 * Jose Luis Pacheco (IEC-16100388)
 Programacion Avanzada UNY
 * 
 */

namespace MemoriaMundialistaFinal
{
    public partial class Memoria : Form
    {

 //ALGUNAS DECLARACIONES PREVIAS....
     
        List<Point> puntos = new List<Point>(); //Una lista con los puntos en donde se situan las cartas, componentes "X" y "Y"
        Random posicion = new Random(); //Esto es para seleccionar un valor aleatorio tanto para la lista de x como para la de y
   
        PictureBox primera; //primera carta clickeada a comparar
        PictureBox segunda; //segunda carta clickeada a comparar

        int contseleccion = 0; //un contador que tomara el valor de 1 o 2, representa la seleccion activa de cartas pues...
        int contpares = 0; //un contador para los pares iguales de cartas ya descubiertas, para detectar el fin de juego...

 //CONTRUCTOR DE LA CLASE
        
        public Memoria()
        {
            InitializeComponent();  
         }

//METODO FORM LOAD Y FORM CLOSED
  
        private void Memoria_Load(object sender, EventArgs e)
        {
            reiniciar();
        }

        private void Memoria_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

//MEOTODOS PROPIOS REUTILIZABLES... (Se llaman en varias partes del codigo)...

        private void comprobar(PictureBox carta) {
         
                contseleccion++;
                carta.Enabled = false;

                switch (contseleccion)
                {
                    case 1:
                        primera = carta;
                        break;
                    case 2:
                        segunda = carta;

                        //Decidi trabajar con la propiedad tag de las imagenes, me pareció mucho más sencillo para agilizar la comparacion...

                        //A ambas (Las cartas y sus repetidas) les asigné el mismo número en el Tag, del 1 al 6...

                        if (primera.Tag != segunda.Tag) //Si son diferentes los Tags de las cartas seleccionadas...
                        {
                            //Se ejecuta este sonido base del sistema Windows...
                            System.Media.SystemSounds.Hand.Play();
                            timer1.Start();
                        }
                        else //si resulta que las seleccionadas tienen el mismo tag (son iguales)
                        {

                            contpares++;
                            contseleccion = 0;
                            //anulamos la seleccion de cartas, para que vengan las proximas..
                            primera = null;
                            segunda = null;

                            //chequeo si se termino el juego ya...
                            if (contpares == 6)
                            {
                                //se ejecuta este sonido base del sistema...
                                System.Media.SystemSounds.Asterisk.Play();

                                DialogResult resp = MessageBox.Show("Ha ganado! Desea volver a jugar?",
                      "FELICITACIONES", MessageBoxButtons.YesNo);
                                switch (resp)
                                {
                                    case DialogResult.Yes:

                                        contpares = 0;
                                        foreach (PictureBox pic in panel1.Controls)
                                        {
                                            pic.Image = Properties.Resources.cubierta;
                                            pic.Enabled = true;
                                        }
                                        reiniciar();
                                        break;

                                    case DialogResult.No:

                                        this.Dispose();
                                        Menu menu = new Menu();
                                        menu.Show();
                                        break;
                                }//llave del switch interno
                            }//llave del if
                        }//llave del else     
                        break; //break del case 2 del switch externo...
                }//llave del switch externo
            }//llave del metodo

        private void reiniciar() {
             //reiniciar() tenia que ser un método que sirviese para llamarlo al empezar,terminar el juego y al darle al boton reiniciar juego...

            Console.Beep();

            foreach (PictureBox carta in panel1.Controls)
            {
                carta.Image = Properties.Resources.cubierta;
                carta.Enabled = true;
                puntos.Add(carta.Location);
            }
            foreach (PictureBox carta in panel1.Controls)
            {
                int nueva = posicion.Next(puntos.Count);
                Point p = puntos[nueva];
                carta.Location = p;
                puntos.Remove(p);
            }
        }

//EVENTOS DE LOS DOS BOTONES...
        
        private void button1_Click(object sender, EventArgs e)
        {
            //se ejecuta este sonido base del sistema...
            System.Media.SystemSounds.Asterisk.Play();

            DialogResult resp = MessageBox.Show("Realmente desea volver al Menu Principal?",
                       "AVISO", MessageBoxButtons.YesNo);
            if (resp == DialogResult.Yes)
            {
                this.Dispose();
                Menu menu = new Menu();
                menu.Show();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //se ejecuta este sonido base del sistema...
            System.Media.SystemSounds.Asterisk.Play();

            DialogResult resp = MessageBox.Show("Realmente desea reiniciar la partida actual?",
                       "AVISO", MessageBoxButtons.YesNo);
            if (resp == DialogResult.Yes)
            {
                reiniciar();
            }
        }

//EL EVENTO TICK DEL TIMER SE EJECUTA UNA VEZ SE CUMPLA SU INTERVALO DE TIEMPO ESTABLECIDO...

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            primera.Image = Properties.Resources.cubierta;
            segunda.Image = Properties.Resources.cubierta;
            
            primera.Enabled = true;
            segunda.Enabled = true;

            contseleccion = 0;
            primera = null;
            segunda = null;

        }


//METODOS PARA LOS EFECTOS ADICIONALES (DECORACIONES)

        #region CODIGO GENERALIZADO PARA CAMBIAR EL BORDE DE CADA CARTA SI SE PASA EL MOUSE POR ENCIMA

   //"Generalizado" pues se llama para cada una de las cartas (revisar en la ventana de diseno de la clase los eventos asociados a las cartas)...
        
        private void PictureBox_MouseHover(object sender, EventArgs e)
        {
            PictureBox carta = sender as PictureBox;
            carta.BorderStyle = BorderStyle.Fixed3D; 
       
        }
        private void PictureBox_MouseLeave(object sender, EventArgs e)
        {
            PictureBox carta = sender as PictureBox;
            carta.BorderStyle = BorderStyle.None;

        }

       #endregion

        #region CODIGO PARA QUE CADA CARTA REVELE SU IMAGEN AL SER CLICKEADAS

//es diferente para cada carta, por eso los dejé independientes (cada método)....

        private void carta1_Click(object sender, EventArgs e)
        {
//esta validación inicial del if es necesaria, pues de no estar, si se juega clickeando rápidamente las cartas, se podría "romper" el juego....
//la hago en cada uno de los eventos de clicks y no en el metodo comprobar porque se asigna una imagen diferente para cada carta(para evitar que si el timer esta en ejecucion ni siquiera cambie la imagen de la carta al clickearla)...
           
            if (timer1.Enabled == false)
            {
                carta1.Image = Properties.Resources.argentina;
                comprobar(carta1);
            }
        }

        private void copiacarta1_Click(object sender, EventArgs e)
        {

            if (timer1.Enabled == false)
            {
                copiacarta1.Image = Properties.Resources.argentina;
                comprobar(copiacarta1);
            }
        }

        private void carta2_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
            {
                carta2.Image = Properties.Resources.germany;
                comprobar(carta2);
            }
        }

        private void copiacarta2_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
            {
                copiacarta2.Image = Properties.Resources.germany;
                comprobar(copiacarta2);
            }
        }

        private void carta3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
            {
                carta3.Image = Properties.Resources.mexico;
                comprobar(carta3);
            }
        }

        private void copiacarta3_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
            {
                copiacarta3.Image = Properties.Resources.mexico;
                comprobar(copiacarta3);
            }
        }

        private void carta4_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
            {
                carta4.Image = Properties.Resources.espana;
                comprobar(carta4);
            }
        }
        private void copiacarta4_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
            {
                copiacarta4.Image = Properties.Resources.espana;
                comprobar(copiacarta4);
            }
        }

        private void carta5_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
            {
                carta5.Image = Properties.Resources.colombia;
                comprobar(carta5);
            }
        }

        private void copiacarta5_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
            {
                copiacarta5.Image = Properties.Resources.colombia;
                comprobar(copiacarta5);
            }
        }

        private void carta6_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
            {
                carta6.Image = Properties.Resources.portugal;
                comprobar(carta6);
            }
        }

        private void copiacarta6_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == false)
            {
                copiacarta6.Image = Properties.Resources.portugal;
                comprobar(copiacarta6);
            }
        }
        #endregion 

    }
}
