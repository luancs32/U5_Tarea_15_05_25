using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace U5_Tarea_15_05_25
{
    
    public partial class Form1 : Form
    {
        string NombreArchivo;
        public Form1()
        {
            InitializeComponent();
        }
        ////Crea un programa que pida al usuario el nombre
        ///de un fichero de texto y busque los archivos que
        ///inicien con lo que tecleo el usuario, muestre la
        ///información en una lista de todos los archivos que 
        ///se llamen similar lo selecciones y te muestre el contenido
        private void Form1_Load(object sender, EventArgs e)
        {
            lstResultados.View = View.List;
            lstResultados.LabelEdit = true;
            lstResultados.GridLines = true;
            lstResultados.FullRowSelect = true;
            lstResultados.Columns.Add("Nombre", -2, HorizontalAlignment.Left);
        }
        //HOLA 
        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            string nombreFichero = txtNombre.Text.Trim();
            lstResultados.Items.Clear();

            string ruta = Directory.GetCurrentDirectory();
            DirectoryInfo dir = new DirectoryInfo(ruta);
            var archivos = dir.GetFiles("*.txt")
                .Where(f => f.Name.StartsWith(nombreFichero, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var fil in archivos)
            {
                lstResultados.Items.Add(fil.Name);
            }

            if (lstResultados.Items.Count == 0)
            {
                MessageBox.Show("No se encontraron archivos que coincidan con la búsqueda.");
            }
            else
            {
                MessageBox.Show("Se encontraron " + lstResultados.Items.Count + " archivos que coinciden con la búsqueda.");
            }
        }

        private void lstResultados_DoubleClick(object sender, EventArgs e)
        {
            if (lstResultados.SelectedItems.Count > 0)
            {
                NombreArchivo = lstResultados.SelectedItems[0].Text;
                txtSeleccion.Clear();

                string ruta = Path.Combine(Directory.GetCurrentDirectory(), NombreArchivo);
                using (StreamReader archivo = new StreamReader(ruta))
                {
                    while (!archivo.EndOfStream)
                    {
                        txtSeleccion.AppendText(archivo.ReadLine() + Environment.NewLine);
                    
                    }
                }
            }
            
        }
    }
}
