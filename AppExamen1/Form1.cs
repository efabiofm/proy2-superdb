using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AppExamen1
{
    public partial class Form1 : Form
    {
        string comandoPrincipal = "superDB";
        string[] comandos = { "init", "show", "select", "delete", "clear", "exit", "open" };
        string bd = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtLineaComandos.KeyDown += new KeyEventHandler(txtLineaComandos_KeyDown);
            try
            {
                bd = System.IO.File.ReadAllText(@"C:\superdb.txt");
            }
            catch
            {

            }
        }

        private void txtLineaComandos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int cantLineas = txtLineaComandos.Lines.Length;
                if (cantLineas > 0)
                {
                    string ultimaLinea = txtLineaComandos.Lines[cantLineas - 1]; //es el comando ingresado
                    string[] cmd = esComando(ultimaLinea);

                    if (cmd != null)
                    {
                        if (cmd[0].Equals(comandos[0]))
                        {
                            crearBD(cmd[1]);
                        }
                        else if (cmd[0].Equals(comandos[3]))
                        {
                            eliminarBD(cmd[1]);
                        }
                        else if (cmd[0].Equals(comandos[4]))
                        {
                            e.SuppressKeyPress = true; //elimina la linea creada con el enter
                            txtLineaComandos.Clear();
                        }
                        else if (cmd[0].Equals(comandos[5]))
                        {
                            System.Windows.Forms.Application.Exit();
                        }
                        else //comandos que requieren una base de datos
                        {
                            if (!bd.Equals(""))
                            {
                                if (cmd[0].Equals(comandos[1]))
                                {
                                    consultar("select * from " + cmd[1]);
                                }
                                else if (cmd[0].Equals(comandos[2]))
                                {
                                    txtLineaComandos.Text += "\r\n\r\nUsuario:";
                                    consultar("select cedula, nombre, edad from usuarios where id_usuario = " + cmd[1]);
                                    txtLineaComandos.Text += "\r\nPuntos de contacto:";
                                    consultar("select contacto from ptos_contacto where id_usuario = " + cmd[1]);
                                    txtLineaComandos.Text += "\r\nMascotas:";
                                    consultar("select raza, color, edad from mascotas where id_mascota in (select id_mascota from usuarios_x_mascotas where id_usuario = " + cmd[1] + ")");
                                }
                            }
                            else 
                            {
                                txtLineaComandos.Text += "\r\nNo se ha creado ninguna base de datos.";
                            }
                        }
                    }
                    else
                    {
                        if (!txtLineaComandos.Text.Equals(""))
                        {
                            txtLineaComandos.Text += "\r\n'" + ultimaLinea + "' no se reconoce como un comando válido.";
                        }
                    }
                }
                txtLineaComandos.SelectionStart = txtLineaComandos.Text.Length;
            }
        }

        private void crearBD(string pbase)
        {
            try
            {
                SqlConnection conexion = new SqlConnection("Data Source=(local);Initial Catalog=" +bd+ ";Integrated Security=True");
                conexion.Open();
                string queryBD = "CREATE DATABASE " + pbase;
                string queryTablas = @"CREATE TABLE [dbo].[mascotas](
                                        [id_mascota] [int] IDENTITY(1,1) NOT NULL,
                                        [raza] [varchar](50) NOT NULL,
                                        [color] [varchar](50) NOT NULL,
                                        [edad] [int] NOT NULL,
                                    CONSTRAINT [PK_mascotas] PRIMARY KEY CLUSTERED ([id_mascota]));

                                    CREATE TABLE [dbo].[ptos_contacto](
	                                    [id_pto_contacto] [int] IDENTITY(1,1) NOT NULL,
	                                    [contacto] [varchar](50) NOT NULL,
	                                    [id_usuario] [int] NOT NULL,
                                    CONSTRAINT [PK_ptos_contacto] PRIMARY KEY ([id_pto_contacto]));

                                    CREATE TABLE [dbo].[usuarios](
	                                    [id_usuario] [int] IDENTITY(1,1) NOT NULL,
	                                    [cedula] [varchar](50) NOT NULL,
	                                    [nombre] [varchar](50) NOT NULL,
	                                    [edad] [int] NOT NULL,
                                    CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED ([id_usuario]));

                                    CREATE TABLE [dbo].[usuarios_x_mascotas](
	                                    [id_usuario] [int] NOT NULL,
	                                    [id_mascota] [int] NOT NULL,
                                    CONSTRAINT [PK_usuarios_x_mascotas] PRIMARY KEY CLUSTERED ([id_usuario], [id_mascota]));

                                    ALTER TABLE [dbo].[ptos_contacto] ADD CONSTRAINT [FK_ptos_contacto_usuarios] 
                                    FOREIGN KEY([id_usuario]) REFERENCES [dbo].[usuarios] ([id_usuario]);

                                    ALTER TABLE [dbo].[usuarios_x_mascotas] ADD CONSTRAINT [FK_usuarios_x_mascotas_mascotas] 
                                    FOREIGN KEY([id_mascota]) REFERENCES [dbo].[mascotas] ([id_mascota]);

                                    ALTER TABLE [dbo].[usuarios_x_mascotas]  WITH CHECK ADD  CONSTRAINT [FK_usuarios_x_mascotas_usuarios] 
                                    FOREIGN KEY([id_usuario]) REFERENCES [dbo].[usuarios] ([id_usuario]);";

                string queryDatos = @"INSERT INTO usuarios VALUES('102340567', 'Pedro Perez', 23);
                                    INSERT INTO usuarios VALUES('203450678', 'María Mora', 25);
                                    INSERT INTO usuarios VALUES('104560789', 'Jose Jimenez', 27);
                                    INSERT INTO usuarios VALUES('254365475', 'Mario Machado', 31);
                                    INSERT INTO usuarios VALUES('142235465', 'Luis López', 26);
                                    INSERT INTO usuarios VALUES('142344545', 'Carlos Castro', 30);
                                    INSERT INTO usuarios VALUES('123432433', 'Sergio Segura', 24);
                                    INSERT INTO usuarios VALUES('234554456', 'Melissa Madrigal', 27);
                                    INSERT INTO usuarios VALUES('243543434', 'Bryan Barrantes', 32);
                                    INSERT INTO usuarios VALUES('243543322', 'Ezequiel Echandi', 33);

                                    INSERT INTO ptos_contacto VALUES('2345-6789', 1);
                                    INSERT INTO ptos_contacto VALUES('pepe23@mail.com', 1);
                                    INSERT INTO ptos_contacto VALUES('7654-8210', 2);
                                    INSERT INTO ptos_contacto VALUES('mama31@mail.com', 4);
                                    INSERT INTO ptos_contacto VALUES('8765-4321', 3);
                                    INSERT INTO ptos_contacto VALUES('sese24@mail.com', 7);
                                    INSERT INTO ptos_contacto VALUES('2554-6650', 8);
                                    INSERT INTO ptos_contacto VALUES('mema27@gmail.com', 8);
                                    INSERT INTO ptos_contacto VALUES('7254-3334', 9);
                                    INSERT INTO ptos_contacto VALUES('ezecha33@mail.com', 10);

                                    INSERT INTO mascotas VALUES('Pitbull', 'Gris', 5);
                                    INSERT INTO mascotas VALUES('Huskey', 'Negro', 10);
                                    INSERT INTO mascotas VALUES('Angora', 'Blanco', 7);
                                    INSERT INTO mascotas VALUES('Schnauzer', 'Gris', 12);
                                    INSERT INTO mascotas VALUES('Persa', 'Naranja', 10);
                                    INSERT INTO mascotas VALUES('Doberman', 'Negro', 4);
                                    INSERT INTO mascotas VALUES('Rottweiler','Negro', 7);
                                    INSERT INTO mascotas VALUES('Poodle','Blanco', 12);
                                    INSERT INTO mascotas VALUES('Dachshund','Café', 10);
                                    INSERT INTO mascotas VALUES('Siamés','Gris', 6);
                                    INSERT INTO mascotas VALUES('Beagle','Café', 3);
                                    INSERT INTO mascotas VALUES('Boxer','Gris', 2);
                                    INSERT INTO mascotas VALUES('Pug','Blanco', 7);
                                    INSERT INTO mascotas VALUES('Cocker','Café', 8);
                                    INSERT INTO mascotas VALUES('Galgo','Café', 5);

                                    INSERT INTO usuarios_x_mascotas VALUES(1, 1);
                                    INSERT INTO usuarios_x_mascotas VALUES(1, 2);
                                    INSERT INTO usuarios_x_mascotas VALUES(2, 1);
                                    INSERT INTO usuarios_x_mascotas VALUES(2, 4);
                                    INSERT INTO usuarios_x_mascotas VALUES(3, 3);
                                    INSERT INTO usuarios_x_mascotas VALUES(3, 4);
                                    INSERT INTO usuarios_x_mascotas VALUES(4, 8);
                                    INSERT INTO usuarios_x_mascotas VALUES(5, 9);
                                    INSERT INTO usuarios_x_mascotas VALUES(5, 5);
                                    INSERT INTO usuarios_x_mascotas VALUES(6, 7);
                                    INSERT INTO usuarios_x_mascotas VALUES(7, 3);
                                    INSERT INTO usuarios_x_mascotas VALUES(7, 6);
                                    INSERT INTO usuarios_x_mascotas VALUES(8, 10);
                                    INSERT INTO usuarios_x_mascotas VALUES(9, 10);
                                    INSERT INTO usuarios_x_mascotas VALUES(10, 2);";

                SqlCommand sc1 = new SqlCommand(queryBD, conexion);
                sc1.ExecuteNonQuery();
                bd = pbase;
                System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\superdb.txt");
                file.WriteLine(pbase);
                file.Close();

                conexion.Close();
                conexion = new SqlConnection("Data Source=(local);Initial Catalog=" +bd+ ";Integrated Security=True");
                conexion.Open();
                SqlCommand sc2 = new SqlCommand(queryTablas, conexion);
                SqlCommand sc3 = new SqlCommand(queryDatos, conexion);
                sc2.ExecuteNonQuery();
                sc3.ExecuteNonQuery();
                txtLineaComandos.Text += "\r\n" +pbase+ " fue creado con éxito.";
            }
            catch
            {
                txtLineaComandos.Text += "\r\nLa base de datos '"+pbase+"' ya existe.";
            }
        }

        private void consultar(string query)
        {
            try
            {
                SqlConnection conexion = new SqlConnection("Data Source=(local);Initial Catalog=" + bd + ";Integrated Security=True");
                conexion.Open();
                SqlCommand sc = new SqlCommand(query, conexion);
                SqlDataReader lector = sc.ExecuteReader();

                string consulta = "\r\n\r\n";

                for (int i = 0; i < lector.FieldCount; i++)
                {
                    consulta += lector.GetName(i) + "\t";
                }

                consulta += "\r\n-----------------------------------------------\r\n";

                while (lector.Read())
                {
                    for (int j = 0; j < lector.FieldCount; j++)
                    {
                        consulta += lector[j] + "\t";
                    }
                    consulta += "\r\n";
                }
                txtLineaComandos.Text += consulta;
            }
            catch
            {
                txtLineaComandos.Text += "\r\nEl parámetro ingresado es incorrecto.";
            }
        }

        private void eliminarBD(string pbase)
        {
            try
            {
                SqlConnection conexion = new SqlConnection("Data Source=(local);Initial Catalog=" + bd + ";Integrated Security=True");
                conexion.Open();
                string query = "drop database " + pbase;
                SqlCommand sc = new SqlCommand(query, conexion);
                sc.ExecuteNonQuery();
                bd = "";
                txtLineaComandos.Text += "\r\n" +pbase+ " fue eliminado con éxito.";
            }
            catch
            {
                txtLineaComandos.Text += "\r\nLa base de datos '" +pbase+ "' no existe.";
            }
        }

        private string[] esComando(string texto) 
        {
            int pos1erEspacio = texto.IndexOf(" ");
            if (pos1erEspacio != -1)
            {
                if (!texto.Substring(0, pos1erEspacio).Equals(comandoPrincipal))
                {
                    return null;
                }
                texto = texto.Substring(pos1erEspacio + 1); //evalua si se esta usando el comando principal
            }
            else 
            {
                return null;
            }
            int pos2doEspacio = texto.IndexOf(" ");
            if (pos2doEspacio != -1)
            {
                string cmd = texto.Substring(0, pos2doEspacio);
                string[] resul = { cmd, texto.Substring(pos2doEspacio + 1)};
                return resul; //regresa el comando y el parametro ingresado
            }
            else
            {
                return null;
            }
        }
    }
}
