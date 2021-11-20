using System;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;

namespace Teste
{
    public partial class FrmTelaRelatorios : Form
    {
        Banco banco = new Banco();
        public FrmTelaRelatorios()
        {
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmTelaRelatorios_Load(object sender, EventArgs e)
        {
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
                if (chart1.Series.Count > 0)
                {
                    chart1.Series.Clear();
                }

                if (TabelaGrafico.Columns.Count > 1)
                {
                    for (int l = 0; l < TabelaGrafico.Rows.Count; l++)
                    {
                        string Series = TabelaGrafico.Rows[l].ItemArray[0].ToString();
                        chart1.Series.Add(Series);
                        SeriesChartType Type = SeriesChartType.StackedColumn;
                        chart1.Series[Series].ChartType = Type;

                        chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
                        chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                        for (int c = 1; c < TabelaGrafico.Columns.Count; c++)
                        {
                            chart1.Series[Series].Points.AddXY(TabelaGrafico.Columns[c].ColumnName, TabelaGrafico.Rows[l].ItemArray[c].ToString());
                        }
                    }
                    dataGridView1.DataSource = TabelaGrid;
                }
                else
                {
                    if (chart1.Series.Count > 0)
                    {
                        chart1.Series.Clear();
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
    }
}
