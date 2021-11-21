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
        int graphic = 0;
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
            if(dtpInicial.Value <= dtpFinal.Value)
            {
                switch (graphic)
                {
                    case 1:
                        RelatorioFluxo();
                        break;
                    case 2:
                        RelatotorioFinanceiro();
                        break;
                    case 3:
                        RelatorioCliente();
                        break;
                    case 4:
                        RelatorioCarroDiario();
                        break;
                    default:
                        MessageBox.Show("Selecione um tipo de Gráfico primeiro!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show("A Data final não pode ser Inferior que a data inicial!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpInicial.Focus();
            }
        }
        private void RelatorioFluxo()
        {
            if (chtRelatorio.Series.Count > 0)
            {
                chtRelatorio.Series.Clear();
            }
        }
        private void RelatotorioFinanceiro()
        {
            if (chtRelatorio.Series.Count > 0)
            {
                chtRelatorio.Series.Clear();
            }
        }
        private void RelatorioCliente()
        {
            if (chtRelatorio.Series.Count > 0)
            {
                chtRelatorio.Series.Clear();
            }
        }
        private void RelatorioCarroDiario()
        {
            DataSet ds;
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName= "@DataInicial", SqlDbType = SqlDbType.DateTime, Value = dtpInicial.Value},
                new SqlParameter(){ParameterName= "@DataFinal", SqlDbType = SqlDbType.DateTime, Value = dtpFinal.Value}
            };
            ds = banco.ExecuteProcedureWithReturnMultipleTables("dbo.Relatorio_Carro_Diario", sp);
            CriarGrafico(ds, SeriesChartType.StackedColumn);
        }

        private void CriarGrafico(DataSet ds, SeriesChartType Type, int interval = 1, int angle = 90)
        {
            if (ds.Tables.Count == 2)
            {
                DataTable TabelaGrafico = ds.Tables[0];
                DataTable TabelaGrid = ds.Tables[1];
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
                        chtRelatorio.Series[Series].ChartType = Type;

                        chtRelatorio.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = angle;
                        chtRelatorio.ChartAreas["ChartArea1"].AxisX.Interval = interval;

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
            graphic = 1;
        }

        private void btnFinanceiro_Click(object sender, EventArgs e)
        {
            FundoBotao(btnFinanceiro);
            graphic = 2;
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            FundoBotao(btnCliente);
            graphic = 3;
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
            graphic = 4;
        }
    }
}
