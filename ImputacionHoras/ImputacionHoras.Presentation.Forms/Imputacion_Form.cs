using ImputacionHoras.Business.Logic;
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
        ImputationBL imputationBusiness;
        StringBuilder textLog;

        public Imputacion_Form()
        {
            InitializeComponent();
        }

        private void Imputacion_Form_Load(object sender, EventArgs e)
        {
            imputationBusiness = new ImputationBL();
            textLog = new StringBuilder();
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
            try
            {

                // Obtenemos los datos de imputaciones
                imputationBusiness.ImportImputations(this.tbImputaciones.Text);

                this.tbLog.Text = textLog.Append("Imporatción de Imputaciones correcta").ToString();

                // Calculamos contractors
                imputationBusiness.CalculateContractors(this.tbContracts.Text);

                this.tbLog.Text = textLog.Append("Imporatción de Contracts correcta").ToString();

                // Calculamos billing concepts
                imputationBusiness.CalculateAllBillingConcepts(this.tbUser.Text, this.tbPassword.Text); // (usuarioJira, contraseñaJira)

                this.tbLog.Text = textLog.Append("Billings calculados correctamente").ToString();

                // Calculamos assets
                imputationBusiness.CalculateAssets(this.tbAssets.Text);

                this.tbLog.Text = textLog.Append("Imporatción de Assets correcta").ToString();

                // Exportamos a CSV
                imputationBusiness.ExportImputations(this.tbExportTo.Text);

                this.tbLog.Text = textLog.Append("Llamadas a la API realizadas: ").Append(imputationBusiness.Counter).ToString();
                this.tbLog.Text = textLog.Append("Exportación finalizada.").ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
