using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Drawing;

namespace Teste
{
    public partial class FrmTelaRelatorios : Form
    {
        Banco banco = new Banco();
        public FrmTelaRelatorios()
        {
            InitializeComponent();
        }

        private void FundoBotao(Button Botao)
        {

            btnFluxo.BackColor = Color.Silver;
            btnFinanceiro.BackColor = Color.Silver;
            btnCliente.BackColor = Color.Silver;
            btnVeiculo.BackColor = Color.Silver;
            Botao.BackColor = Color.WhiteSmoke;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmTelaRelatorios_Load(object sender, EventArgs e)
        {
            LimparGrafico();
            if (Properties.Settings.Default.StringBanco != null)
            {
                DataSet ds = new DataSet();
                ds = banco.ExecuteProcedureWithReturnMultipleTables("dbo.Top_Relatorio");
                if (ds.Tables.Count > 1)
                {
                    lblMarca.Text = ds.Tables[0].Rows[0]["Marca"].ToString();
                    lblCountMarca.Text = ds.Tables[0].Rows[0]["Qtd"].ToString();

                    lblMarca1.Text = ds.Tables[1].Rows[0]["automovel"].ToString();
                    lblMarca2.Text = ds.Tables[1].Rows[1]["automovel"].ToString();
                    lblMarca3.Text = ds.Tables[1].Rows[2]["automovel"].ToString();
                    lblMarca4.Text = ds.Tables[1].Rows[3]["automovel"].ToString();
                    lblTop1.Text = ds.Tables[1].Rows[0]["QTD"].ToString();
                    lblTop2.Text = ds.Tables[1].Rows[1]["QTD"].ToString();
                    lblTop3.Text = ds.Tables[1].Rows[2]["QTD"].ToString();
                    lblTop4.Text = ds.Tables[1].Rows[3]["QTD"].ToString();
                    int soma = Convert.ToInt32(lblTop1.Text) + Convert.ToInt32(lblTop2.Text) + Convert.ToInt32(lblTop3.Text) + Convert.ToInt32(lblTop4.Text);
                    lblTotal.Text = soma.ToString();
                }
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void lblMarca_Click(object sender, EventArgs e)
        {

        }

        private void lblCountMarca_Click(object sender, EventArgs e)
        {

        }

        private void lblTop2_Click(object sender, EventArgs e)
        {

        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            DataTable TabelaGrafico = new DataTable();
            DataTable TabelaGrid = new DataTable();
            DataSet ds = new DataSet();
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName= "@DataInicial", SqlDbType = SqlDbType.DateTime, Value = DataInicial.Value},
                new SqlParameter(){ParameterName= "@DataFinal", SqlDbType = SqlDbType.DateTime, Value = DataFinal.Value}
            };
            ds = banco.ExecuteProcedureWithReturnMultipleTables("Relatorio_Carro_Diario", sp);
            if(ds.Tables.Count == 2)
            {
                TabelaGrafico = ds.Tables[0];
                TabelaGrid = ds.Tables[1];
                lblNada.Visible = false;
                if (chtRelatorio.Series.Count > 0)
                {
                    chtRelatorio.Series.Clear();
                }

                if (TabelaGrafico.Columns.Count > 1)
                {
                    for (int l = 0; l < TabelaGrafico.Rows.Count; l++)
                    {
                        string Series = TabelaGrafico.Rows[l].ItemArray[0].ToString();
                        chtRelatorio.Series.Add(Series);
                        chtRelatorio.Series[Series].ChartType = SeriesChartType.StackedColumn;

                        chtRelatorio.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
                        chtRelatorio.ChartAreas["ChartArea1"].AxisX.Interval = 1;

                        for (int c = 1; c < TabelaGrafico.Columns.Count; c++)
                        {
                            chtRelatorio.Series[Series].Points.AddXY(TabelaGrafico.Columns[c].ColumnName, TabelaGrafico.Rows[l].ItemArray[c].ToString());
                        }
                    }
                    dataGridView1.DataSource = TabelaGrid;
                }
                else
                {
                    if (chtRelatorio.Series.Count > 0)
                    {
                        chtRelatorio.Series.Clear();
                    }
                    lblTicket.Visible = true;
                }
            }
            else
            {
                lblNada.Visible = true;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnFluxo_Click(object sender, EventArgs e)
        {
            FundoBotao(btnFluxo);
        }

        private void btnFinanceiro_Click(object sender, EventArgs e)
        {
            FundoBotao(btnFinanceiro);
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            FundoBotao(btnCliente);

           

        }

        private void LimparGrafico()
        {
            if (chtRelatorio.Series.Count > 0)
            {
                chtRelatorio.Series.Clear();
            }
        }

        private void btnVeiculo_Click(object sender, EventArgs e)
        {
            FundoBotao(btnVeiculo);
        }
    }
}
