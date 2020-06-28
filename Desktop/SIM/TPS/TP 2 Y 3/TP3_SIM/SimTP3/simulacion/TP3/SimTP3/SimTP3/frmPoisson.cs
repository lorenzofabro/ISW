using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimTP3.Generadores;
using SimTP3.Utiles;

namespace SimTP3
{
    public partial class frmPoisson : Form
    {
        DataTable tablaNumeros = new DataTable();
        List<Intervalo> intervalos;
        int contador;
        Poisson generador;
        int maximoNumGenerado = 0;
        int minimoNumGenerado = 0;

        public frmPoisson()
        {
            InitializeComponent();
        }

        private void frmPoisson_Load(object sender, EventArgs e)
        {
            tablaNumeros.Columns.Add("#", typeof(int));
            tablaNumeros.Columns.Add("Número Aleatorio", typeof(double));
            dgvNumeros.DataSource = tablaNumeros;

            btnGrafico.Enabled = false;
            btn_ji.Enabled = false;
        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            // Limpiar grilla
            tablaNumeros.Clear();
            dgvNumeros.DataSource = tablaNumeros;

            // Reacomodar botones
            btnGenerarNumeros.Enabled = true;
            btnGrafico.Enabled = false;
            btn_ji.Enabled = false;

            nudLambda.Enabled = true;
            nudTamMuestra.Enabled = true;

            txt_chicalculado.Text = "";
            txt_chitabulado.Text = "";
            
        }

        private void btnGenerarNumeros_Click(object sender, EventArgs e)
        {
            contador = 0;
            int lambda = (int)nudLambda.Value;
            int N = (int)nudTamMuestra.Value;
            int numeroGenerado; // Sirve para poder determinar el maximo y el minimo

            generador = new Poisson(lambda, N);

            for (int i = 0; i < N; i++)
            {
                contador++;

                // Generacion del numero aleatorio
                numeroGenerado = generador.generarNumero();

                // Agregar el primer numero generado como maximo y minimo
                if (contador == 1)
                {
                    maximoNumGenerado = numeroGenerado;
                    minimoNumGenerado = numeroGenerado;
                }
                else
                {
                    // Par de ifs para actualizar el maximo y minimo numero generado
                    if (maximoNumGenerado < numeroGenerado)
                    {
                        maximoNumGenerado = numeroGenerado;
                    }
                    if (minimoNumGenerado > numeroGenerado)
                    {
                        minimoNumGenerado = numeroGenerado;
                    }
                }

                tablaNumeros.Rows.Add(contador, numeroGenerado);
            }

            generarIntervalos();

            dgvNumeros.DataSource = tablaNumeros;
            btnGenerarNumeros.Enabled = false;
            btnGrafico.Enabled = true;
            btn_ji.Enabled = true;
        }

        private void btnGrafico_Click(object sender, EventArgs e)
        {
            // Crear el formulario del grafico.
            frmGraficos grafico = new frmGraficos();

            grafico.intervalos = intervalos.ToArray();

            grafico.ShowDialog();

            nudLambda.Enabled = false;
            nudTamMuestra.Enabled = false;

        }

        // ===================================================================
        // FUNCIONES EXTRAS
        // ===================================================================

        // Funcion que recorre la tabla y genera los intervalos (numeros) necesarios para Poisson
        void generarIntervalos()
        {
            intervalos = new List<Intervalo>();
            int indiceIntervalo; // Variable de soporte para saber donde insertar intervalo
            int numeroDeTabla = Convert.ToInt32(tablaNumeros.Rows[0][1]); // Variable de soporte para hacer mas cortas las operaciones
            bool insertar; // Variable de soporte para saber si debe insertar o no

            // Insertar primer numero de la tabla
            Intervalo nuevo = new Intervalo(numeroDeTabla, numeroDeTabla, 1, calcularFrecuenciaEsperada(numeroDeTabla));
            intervalos.Add(nuevo);

            for (int i = 1; i < tablaNumeros.Rows.Count; i++)
            {
                numeroDeTabla = Convert.ToInt32(tablaNumeros.Rows[i][1]);

                insertar = true;
                indiceIntervalo = 0;
                for (int j = 0; j < intervalos.Count; j++)
                {
                    if (intervalos[j].pertenecePoisson(numeroDeTabla))
                    {
                        intervalos[j].fo++;
                        insertar = false;
                        break;
                    }
                    else
                    {
                        if (intervalos[j].limiteInferior < numeroDeTabla)
                        {
                            if (j == intervalos.Count - 1)
                            {
                                indiceIntervalo = j;

                            }
                            else
                            {
                                indiceIntervalo = j + 1;
                            }
                        }
                    }
                }
                if (insertar)
                {
                    nuevo = new Intervalo(numeroDeTabla, numeroDeTabla, 1, calcularFrecuenciaEsperada(numeroDeTabla));
                    intervalos.Insert(indiceIntervalo, nuevo);
                }
            }
        }

        int calcularFrecuenciaEsperada(int numero)
        {
            // Valores necesarios para calcular
            double lambda = (double)nudLambda.Value;
            int N = (int)nudTamMuestra.Value;

            double Prob = Math.Round((Math.Pow(lambda, numero) * Math.Exp(-lambda) / factorial(numero))*N, 2);

            return (int)Math.Truncate(Prob);
        }

        
        double factorial (double n)
        {
            double fact = 1;
            for (int i = 1; i <= n; i++)
            {
                fact *= i;
            }
            return fact;
        }

        private void btn_ji_Click(object sender, EventArgs e)
        {
            List<Intervalo> intervalosAjustados = ChiCuadrado.ajustarIntervalos(intervalos.ToArray());
            double valorChi = ChiCuadrado.calcularChi(intervalosAjustados);

            int cantIntervalos = intervalosAjustados.Count;

            if (cantIntervalos < 3)
            {
                lbl_hipotesis.Text = "No se puede calcular el Chi debido a\nque la cantidad de intervalos es 2 o menos";
            }
            else
            {
                int gradosLibertad = cantIntervalos - 2;

                txt_chicalculado.Text = valorChi.ToString();
                txt_chitabulado.Text = ChiCuadrado.obtenerChiTabulado(gradosLibertad).ToString();

                lbl_hipotesis.Text = ChiCuadrado.determinarDecision(valorChi, gradosLibertad);
            }
        }
    }
}
