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
            dtpFinal.Value = DateTime.Now;
            dtpInicial.Value = DateTime.Now.AddDays(-7);
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
            if (dtpInicial.Value <= dtpFinal.Value)
            {
                switch (graphic)
                {
                    case 1:
                        RelatorioTickets();
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
        private void RelatorioTickets()
        {
            if (chtRelatorio.Series.Count > 0)
            {
                chtRelatorio.Series.Clear();
            }
            if (chtRelatorio.Series.Count > 0)
            {
                chtRelatorio.Series.Clear();
            }
            DataSet ds;
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName= "@DataInicial", SqlDbType = SqlDbType.DateTime, Value = dtpInicial.Value},
                new SqlParameter(){ParameterName= "@DataFinal", SqlDbType = SqlDbType.DateTime, Value = dtpFinal.Value}
            };
            ds = banco.ExecuteProcedureWithReturnMultipleTables("dbo.Relatorio_Ticket_Diario", sp);
            DataTable dt = ds.Tables[0];
            CriarGraficoTicket(dt);

        }
        private void RelatotorioFinanceiro()
        {
            if (chtRelatorio.Series.Count > 0)
            {
                chtRelatorio.Series.Clear();
            }
            DataSet ds;
            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter(){ParameterName= "@DataInicial", SqlDbType = SqlDbType.DateTime, Value = dtpInicial.Value},
                new SqlParameter(){ParameterName= "@DataFinal", SqlDbType = SqlDbType.DateTime, Value = dtpFinal.Value}
            };
            ds = banco.ExecuteProcedureWithReturnMultipleTables("dbo.Relatorio_Financeiro_Diario", sp);
            CriaGraficoPie(ds.Tables[0], "Financeiro");
            PopulaGrid(ds.Tables[1]);
        }
        private void RelatorioCliente()
        {
            DataTable dt = CriaDataTable();
            DataSet ds;
            try
            {
                if (chtRelatorio.Series.Count > 0)
                {
                    chtRelatorio.Series.Clear();
                }

                List<SqlParameter> sp = new List<SqlParameter>()
                {
                new SqlParameter(){ParameterName= "@DataInicial", SqlDbType = SqlDbType.DateTime, Value = dtpInicial.Value},
                new SqlParameter(){ParameterName= "@DataFinal", SqlDbType = SqlDbType.DateTime, Value = dtpFinal.Value}
                };
                ds = banco.ExecuteProcedureWithReturnMultipleTables("dbo.Relatorio_Cliente", sp);
                if (ds.Tables.Count == 3)
                {
                    int total = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[0]);
                    int ident = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[1]);
                    int Nid = Convert.ToInt32(ds.Tables[0].Rows[0].ItemArray[2]);
                    int voltaram = 0;
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        if (Convert.ToInt32(ds.Tables[1].Rows[i]["qtd"]) != 1)
                        {
                            voltaram += Convert.ToInt32(ds.Tables[1].Rows[i]["qtd"]);
                        }
                        else
                        {
                            break;
                        }
                    }
                    dt.Rows.Add(
                        total,
                        ident,
                        Nid,
                        total - voltaram,
                        voltaram
                        );
                    CriaGraficoCliente(dt, "Relatório Clientes");
                    PopulaGrid(ds.Tables[2]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable CriaDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Total", typeof(int));
            dt.Columns.Add("Cadastrados", typeof(int));
            dt.Columns.Add("Não Cadastrados", typeof(int));
            dt.Columns.Add("Visita Única", typeof(int));
            dt.Columns.Add("Retornaram", typeof(int));
            return dt;
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
            CriarGraficoStackedColumn(ds.Tables[0], "Tipos de Veículos");
            PopulaGrid(ds.Tables[1]);
        }

        private void PopulaGrid(DataTable TabelaGrid)
        {
            try
            {
                if (TabelaGrid.Columns.Count > 1)
                {
                    SrcGrafico.DataSource = null;
                    SrcGrafico.DataSource = TabelaGrid;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CriarGraficoStackedColumn(DataTable TabelaGrafico, string legendTitle, bool labels = false, SeriesChartType Style = SeriesChartType.StackedColumn, int interval = 1, int angle = 90, bool Style3d = true, int border = 5)
        {
            try
            {
                if (TabelaGrafico.Columns.Count > 1)
                {
                    lblNada.Visible = false;
                    if (chtRelatorio.Series.Count > 0)
                    {
                        chtRelatorio.Series.Clear();
                    }

                    for (int l = 0; l < TabelaGrafico.Rows.Count; l++)
                    {
                        string Series = TabelaGrafico.Rows[l].ItemArray[0].ToString();
                        if (Series == "0")
                        {
                            Series = "Encerrados";
                        }
                        if (Series == "1")
                        {
                            Series = "Iniciados";
                        }
                        chtRelatorio.Series.Add(Series);
                        chtRelatorio.Series[Series].ChartType = Style;
                        chtRelatorio.Legends[0].Title = legendTitle;
                        chtRelatorio.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = angle;
                        chtRelatorio.ChartAreas["ChartArea1"].AxisX.Interval = interval;
                        chtRelatorio.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = Style3d;
                        chtRelatorio.Series[Series].IsValueShownAsLabel = labels;
                        chtRelatorio.Series[Series].BorderWidth = border;

                        for (int c = 1; c < TabelaGrafico.Columns.Count; c++)
                        {
                            chtRelatorio.Series[Series].Points.AddXY(TabelaGrafico.Columns[c].ColumnName, TabelaGrafico.Rows[l].ItemArray[c].ToString());
                        }
                    }
                }
                else
                {
                    if (chtRelatorio.Series.Count > 0)
                    {
                        chtRelatorio.Series.Clear();
                    }
                    lblTicket.Visible = true;
                    lblNada.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void CriarGraficoTicket(DataTable TabelaGrafico)
        {
            try
            {
                if (TabelaGrafico.Columns.Count > 1)
                {
                    lblNada.Visible = false;
                    if (chtRelatorio.Series.Count > 0)
                    {
                        chtRelatorio.Series.Clear();
                    }
                    for (int i = 1; i < TabelaGrafico.Columns.Count; i++)
                    {
                        string Series = TabelaGrafico.Columns[i].ColumnName;
                        chtRelatorio.Series.Add(Series);
                        chtRelatorio.Series[Series].ChartType = SeriesChartType.StackedColumn;
                        chtRelatorio.Legends[0].Title = "Tickets";
                        chtRelatorio.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 90;
                        chtRelatorio.ChartAreas["ChartArea1"].AxisX.Interval = 1;
                        chtRelatorio.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true; ;
                        chtRelatorio.Series[Series].IsValueShownAsLabel = false;
                        chtRelatorio.Series[Series].BorderWidth = 5;
                        for (int l = 0; l < TabelaGrafico.Rows.Count; l++)
                        {
                            chtRelatorio.Series[Series].Points.AddXY(TabelaGrafico.Rows[l].ItemArray[0], TabelaGrafico.Rows[l].ItemArray[i]);
                            
                        }
                    }
                }
                else
                {
                    if (chtRelatorio.Series.Count > 0)
                    {
                        chtRelatorio.Series.Clear();
                    }
                    lblTicket.Visible = true;
                    lblNada.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void CriaGraficoPie(DataTable TabelaGrafico, string name)
        {
            try
            {
                if (TabelaGrafico.Columns.Count > 1)
                {
                    if (chtRelatorio.Series.Count > 0)
                    {
                        chtRelatorio.Series.Clear();
                    }
                    decimal soma = 0;
                    chtRelatorio.Series.Add(name);
                    chtRelatorio.Legends[0].Title = "Forma Pagamento";
                    chtRelatorio.Series[name].ChartType = SeriesChartType.Doughnut;
                    for (int l = 0; l < TabelaGrafico.Rows.Count; l++)
                    {
                        string PointsName = TabelaGrafico.Rows[l].ItemArray[0].ToString();
                        for (int c = 1; c < TabelaGrafico.Columns.Count; c++)
                        {
                            soma += Convert.ToDecimal(TabelaGrafico.Rows[l].ItemArray[c]);
                        }
                        chtRelatorio.Series[name].Points.AddXY(PointsName, soma);
                        chtRelatorio.Series[name].Points[l].LegendText = PointsName + " - R$" + soma.ToString();
                        chtRelatorio.Series[name].Points[l].Label = "#PERCENT{P2}";
                        chtRelatorio.Series[name].Points[l].LabelForeColor = Color.White;
                        soma = 0;
                    }
                }
                else
                {
                    if (chtRelatorio.Series.Count > 0)
                    {
                        chtRelatorio.Series.Clear();
                    }
                    lblTicket.Visible = true;
                    lblNada.Visible = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void CriaGraficoCliente(DataTable TabelaGrafico, string name)
        {
            try
            {
                if (TabelaGrafico.Columns.Count > 1)
                {
                    if (chtRelatorio.Series.Count > 0)
                    {
                        chtRelatorio.Series.Clear();
                    }
                    chtRelatorio.Series.Add(name);
                    chtRelatorio.Legends[0].Title = "Legenda";
                    chtRelatorio.Series[name].ChartType = SeriesChartType.Pie;
                    for (int l = 1; l < TabelaGrafico.Columns.Count; l++)
                    {
                        string PointsName = TabelaGrafico.Columns[l].ColumnName.ToString();
                        chtRelatorio.Series[name].Points.AddXY(PointsName, TabelaGrafico.Rows[0].ItemArray[l]);
                        chtRelatorio.Series[name].Points[l - 1].LegendText = PointsName + " - " + TabelaGrafico.Rows[0].ItemArray[l].ToString();
                        chtRelatorio.Series[name].Points[l - 1].Label = "#PERCENT{P2}";
                        chtRelatorio.Series[name].Points[l - 1].LabelForeColor = Color.White;
                    }
                }
                else
                {
                    if (chtRelatorio.Series.Count > 0)
                    {
                        chtRelatorio.Series.Clear();
                    }
                    lblTicket.Visible = true;
                    lblNada.Visible = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Legends()
        {
            chtRelatorio.Legends.Clear();
            chtRelatorio.Legends.Add("Legenda");
            chtRelatorio.Legends[0].LegendStyle = LegendStyle.Table;
            chtRelatorio.Legends[0].Docking = Docking.Right;
            chtRelatorio.Legends[0].Alignment = StringAlignment.Center;
            chtRelatorio.Legends[0].BorderColor = Color.Black;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnFluxo_Click(object sender, EventArgs e)
        {
            FundoBotao(btnFluxo);
            graphic = 1;
            AtiveFuncoes();
        }

        private void btnFinanceiro_Click(object sender, EventArgs e)
        {
            FundoBotao(btnFinanceiro);
            graphic = 2;
            AtiveFuncoes();
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            FundoBotao(btnCliente);
            graphic = 3;
            AtiveFuncoes();
        }

        private void LimparGrafico()
        {
            chtRelatorio.ChartAreas["ChartArea1"].BackColor = Color.Transparent;
            chtRelatorio.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true;
            Legends();
            if (chtRelatorio.Series.Count > 0)
            {
                chtRelatorio.Series.Clear();
            }
        }

        private void btnVeiculo_Click(object sender, EventArgs e)
        {
            FundoBotao(btnVeiculo);
            graphic = 4;
            AtiveFuncoes();
        }

        private void AtiveFuncoes()
        {
            btnGerar.Enabled = true;
            dtpFinal.Enabled = true;
            dtpInicial.Enabled = true;
        }
    }
}
