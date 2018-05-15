using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImputacionHoras.Presentation.Forms
{
    public partial class Imputacion_Form : Form
    {
        public Imputacion_Form()
        {
            InitializeComponent();
        }

        private void Imputacion_Form_Load(object sender, EventArgs e)
        {

        }

        private void btImputaciones_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = this.openFileDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
            }
            //this.tbImputaciones.Text = result.ToString(); // <-- For debugging use.
            this.tbImputaciones.Text = this.openFileDialog.FileName;
        }
        private void btContracts_Click(object sender, EventArgs e)
        {
            this.openFileDialog.ShowDialog();
            this.tbContracts.Text = this.openFileDialog.FileName;
        }

        private void btAssets_Click(object sender, EventArgs e)
        {
            this.openFileDialog.ShowDialog();
            this.tbAssets.Text = this.openFileDialog.FileName;
        }

        private void btExportTo_Click(object sender, EventArgs e)
        {
            this.openFileDialog.ShowDialog();
            this.tbExportTo.Text = this.openFileDialog.FileName;
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            ImputationBL imputationBusiness = new ImputationBL();
            string usuario = string.Empty;
            string contraseña = string.Empty;

            Console.WriteLine("Introduzca usuario de Jira");
            usuario = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Introduzca contraseña de Jira");
            contraseña = Console.ReadLine();
            Console.Clear();

            // Obtenemos los datos de imputaciones
            Console.WriteLine("Importando imputaciones");
            imputationBusiness.ImportImputations(PresentationResources.TimesheetPathCsvDiego);

            // Calculamos contractors
            Console.WriteLine("Calculando Contractors");
            imputationBusiness.CalculateContractors(PresentationResources.ContractorsPathCsvDiego);

            // Calculamos billing concepts
            Console.WriteLine("Calculando Billing Concepts");
            imputationBusiness.CalculateAllBillingConcepts(usuario, contraseña); // (usuarioJira, contraseñaJira)

            // Calculamos assets
            Console.WriteLine("Calculando Assets");
            imputationBusiness.CalculateAssets(PresentationResources.AssetsPathCsvDiego);

            // Exportamos a CSV
            Console.WriteLine("Exportando a Csv");
            imputationBusiness.ExportImputations();

            Console.WriteLine("Llamadas a la API realizadas: " + imputationBusiness.Counter);
            Console.WriteLine("Presiona Enter para terminar");
            Console.ReadLine();
        }
    }
}
