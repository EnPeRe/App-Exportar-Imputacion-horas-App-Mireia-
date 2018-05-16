using ImputacionHoras.Business.Logic;
using ImputacionHoras.Presentation.Forms.Resources;
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
            this.openFileDialog.ShowDialog();
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
            this.folderBrowserDialog.ShowDialog();
            this.tbExportTo.Text = this.folderBrowserDialog.SelectedPath;
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            try
            {
                // Llamada api para comprobar autenticacion y status de la conexion
                imputationBusiness.CheckAndStartConnection(this.tbUser.Text, this.tbPassword.Text);
                this.tbLog.Text = textLog.AppendLine(WinFormResources.ConnectionOk).ToString();

                // Obtenemos los datos de imputaciones
                imputationBusiness.ImportImputations(this.tbImputaciones.Text);

                this.tbLog.Text = textLog.AppendLine(WinFormResources.ImputacionesImport).ToString();

                // Calculamos contractors
                imputationBusiness.CalculateContractors(this.tbContracts.Text);

                this.tbLog.Text = textLog.AppendLine(WinFormResources.ContractsImport).ToString();

                // Calculamos billing concepts
                imputationBusiness.CalculateAllBillingConcepts(this.tbUser.Text, this.tbPassword.Text); // (usuarioJira, contraseñaJira)

                this.tbLog.Text = textLog.AppendLine(WinFormResources.BillingCalculate).ToString();

                // Calculamos assets
                imputationBusiness.CalculateAssets(this.tbAssets.Text);

                this.tbLog.Text = textLog.AppendLine(WinFormResources.AssetsImport).ToString();

                // Exportamos a CSV
                imputationBusiness.ExportImputations(this.tbExportTo.Text);

                this.tbLog.Text = textLog.Append(WinFormResources.ApiCalls).AppendLine(imputationBusiness.Counter.ToString()).ToString();
                this.tbLog.Text = textLog.AppendLine(WinFormResources.ExportEnded).ToString();
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals(WinFormResources.ExceptionDaoNoAutorizado) || 
                    ex.Message.Equals(WinFormResources.ExceptionDaoProhibido))
                {
                    this.tbLog.Text = textLog.AppendLine(WinFormResources.ConnectionFail).ToString();
                }
                else
                {
                    this.tbLog.Text = textLog.AppendLine(ex.Message).ToString();
                }
            }

        }
    }
}
