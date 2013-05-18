using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Globalization;

namespace PesoIdeal
{
    public class Genero
    {
        public String genero { get; set; }
    }
    public class Altura
    {
        public Double altura { get; set; }
    }
    public class Edad
    {
        public int edad { get; set; }

    }
    public class Peso
    {
        public int peso { get; set; }

    }
	public class GramosCentenas
	{
	    public int Centenas {get; set;}
	}
	public class GramosDecenas
	{
	    public int Decenas {get; set;}
	}
    
	public class GramosUnidades
	{
	    public int Unidades {get; set;}
	}

    public  class  Datos
    {
        public  String  genero  { get; set; }
        public  Double  altura  { get; set; }
        public  int     edad    { get; set; }
        public  Double  peso    { get; set; }
		public  Double  gramos	{ get; set; }
        public double   indice  { get; set; }


        public Double imc { 
            get {

            double _altura = 0.0;

            
            _altura = altura / 100;

            double alturas = _altura * _altura;
            return peso / alturas;                    } 
        }
        public double pesoideal {
            get {
                double pesoI = 0;
                if (genero == "HOMBRE")
					pesoI = (((altura - 100)) - (((altura - 100) - 52) * 0.2));
                    //pesoI = ((altura - 152.4) / 2.54) * 2.7 + 47.75;
                if (genero == "MUJER")
					pesoI = (((altura - 100)) - (((altura - 100) - 52) * 0.4));
                    //pesoI = ((altura - 152.4) / 2.54) * 2.25 + 45.40;


                if (App.IsMetric)
                    return pesoI;
                else
                    return Conversion.ToLibras(pesoI);
            }
        }
        public double porcentajeGrasa {

            get {

                double grasa = 0;

                if (genero == "HOMBRE")
                    grasa = 1.2 * ICM() + 0.23 * edad - 10.8 * 1 - 5.4;
                if (genero == "MUJER")
                    grasa = 1.2 * ICM() + 0.23 * edad - 10.8 * 0 - 5.4;

                return grasa;
            }
        }
        public String tipoPeso {
            get {
                double icm = Math.Round(ICM(), 1);
                string tipoPeso = "";
                if (icm < 18.5)
                    tipoPeso = Resource.A_MenorNormal;
                if (icm >= 18.5 && icm <= 24.99)
                    tipoPeso = Resource.B_Normal;
                if (icm >= 25 && icm <= 29.99)
                    tipoPeso = Resource.C_Sobrepeso;
                if (icm >= 30 && icm <= 34.99)
                    tipoPeso = Resource.D_ObesidadTipo1;
                if (icm >= 35 && icm <= 39.99)
                    tipoPeso = Resource.E_ObesidadTipo2;
                if (icm >= 40)
                    tipoPeso = Resource.F_ObesidadTipo3;

                return tipoPeso;
            }
        }

        public Datos()
        { }
        public Datos(String Genero, double Altura, int Edad, double Peso,double Indice)
        {
            this.genero = Genero;
            this.peso   = Peso;
            this.altura = Altura;
            this.edad   = Edad;
            this.indice = Indice;
                 
        }
        public  Double ICM()
        {
            double _altura = 0.0;
            _altura = altura / 100;
            double alturas = _altura * _altura;
            return peso / alturas;
        }
        public  double PESOIDEAL()
        {
            double pesoI = 0;
            //if (!App.IsMetric)
            //{
            //    if (genero == "HOMBRE")
            //        pesoI = ((altura  - 152.4) / 2.54) * 2.7 + 47.75;
            //    if (genero == "MUJER")
            //        pesoI = ((altura - 152.4) / 2.54) * 2.25 + 45.40;
            //}
            //else
            //{
            //if (genero == "HOMBRE")
            //    pesoI = (((altura / 100) - 152.4) / 2.54) * 2.7 + 47.75;
            //if (genero == "MUJER")
            //    pesoI = (((altura / 100) - 152.4) / 2.54) * 2.25 + 45.40;
            //}

            if (genero == "HOMBRE")
				pesoI = (((altura - 100)) - (((altura - 100) - 52) * 0.2));
                //pesoI = ((altura - 152.4) / 2.54) * 2.7 + 47.75;
            if (genero == "MUJER")
				pesoI = (((altura - 100)) - (((altura - 100) - 52) * 0.4));
                //pesoI = ((altura - 152.4) / 2.54) * 2.25 + 45.40;
           

            if (App.IsMetric)
                return pesoI;
            else
                return Conversion.ToLibras(pesoI);

            /*
            form.PIMD.value = 45.5 + 2.3 * ((parseInt(form.A.value) - 152.4) / 2.54);

            form.PIMD.value = Math.round(parseFloat(form.PIMD.value) * 1000) / 1000;

            form.PIHD.value = 50 + 2.3 * ((parseInt(form.A.value) - 152.4) / 2.54);

            form.PIHD.value = Math.round(parseFloat(form.PIHD.value) * 1000) / 1000;

            form.PIMR.value = 49 + 1.7 * ((parseInt(form.A.value) - 152.4) / 2.54);

            form.PIMR.value = Math.round(parseFloat(form.PIMR.value) * 1000) / 1000;

            form.PIHR.value = 52 + 1.9 * ((parseInt(form.A.value) - 152.4) / 2.54);

            form.PIHR.value = Math.round(parseFloat(form.PIHR.value) * 1000) / 1000;

            form.PIMM.value = 53.1 + 1.36 * ((parseInt(form.A.value) - 152.4) / 2.54);

            form.PIMM.value = Math.round(parseFloat(form.PIMM.value) * 1000) / 1000;

            form.PIHM.value = 56.2 + 1.41 * ((parseInt(form.A.value) - 152.4) / 2.54);

            form.PIHM.value = Math.round(parseFloat(form.PIHM.value) * 1000) / 1000;

            form.PIMH.value = 45.5 + 2.2 * ((parseInt(form.A.value) - 152.4) / 2.54);

            form.PIMH.value = Math.round(parseFloat(form.PIMH.value) * 1000) / 1000;

            form.PIHH.value = 48 + 2.7 * ((parseInt(form.A.value) - 152.4) / 2.54);

            form.PIHH.value = Math.round(parseFloat(form.PIHH.value) * 1000) / 1000;
            */
        }

        public  double PORCENTAJEGRASA()
        { 
            double grasa = 0;

             if(genero == "HOMBRE")
                grasa = 1.2 * ICM() + 0.23 * edad - 10.8 * 1 - 5.4;
             if (genero == "MUJER")
                grasa = 1.2 * ICM() + 0.23 * edad - 10.8 * 0 - 5.4;

             return grasa;
        
        }

        public String TIPOPESO()
        { 
            double icm = Math.Round(ICM(), 1);
            string tipoPeso = "";
            if (icm < 18.5)
                tipoPeso = Resource.A_MenorNormal;
            if (icm >= 18.5 && icm <= 24.99)
                tipoPeso = Resource.B_Normal;
            if (icm >= 25 && icm <= 29.99)
                tipoPeso = Resource.C_Sobrepeso;
            if (icm >= 30 && icm <= 34.99)
                tipoPeso = Resource.D_ObesidadTipo1;
            if (icm >= 35 && icm <= 39.99)
                tipoPeso = Resource.E_ObesidadTipo2;
            if (icm >= 40)
                tipoPeso = Resource.F_ObesidadTipo3;

            return tipoPeso;

           
        }
		public double PesoMinimoIdeal()
		{
			 double p = 0.0;
			 double _altura = 0.0;
            
            _altura = altura / 100;

             p = 18.5 * _altura * _altura;

			if (App.IsMetric)
                return p;
            else
                return Conversion.ToLibras(p);
    
		}
		public double PesoMaximoIdeal()
		{
			 double p = 0.0;
			 double _altura = 0.0;
            
            _altura = altura / 100;

            p = 25 * _altura * _altura;
			
			if (App.IsMetric)
                return p;
            else
                return Conversion.ToLibras(p);
		}

        public double TASAMETABOLICA()
        {
           
            double tasa = 0.0 ;
           

            if(genero == "HOMBRE")
            tasa = 66 + (13.75 * peso) + (5.003 * altura) - (6.775 * edad);
            if(genero == "MUJER")
            tasa = 655 + (9.6 * peso) + (1.850*  altura) - (4.676 * edad );

            return tasa;
          //  * P = peso (kg); T = talla (cm); E = edad (años)
        }

        public double TASAMETABOLICAOMS()
        {
            double _altura = 0.0;
            double tasa = 0.0;

            if (App.IsMetric)
                _altura = (altura * 100);
            else
                _altura = altura;

            if (genero == "HOMBRE")
            {
                if (edad >= 10 && edad <= 18)
                    tasa = 16.6 * peso + 77 * altura + 572;
                if (edad > 18 && edad <= 30)
                    tasa = 15.5 * peso + 27 * altura + 717;
                if (edad > 30 && edad <= 60)
                    tasa = 11.3 * peso + 16 * altura + 901;
                if (edad > 60)
                    tasa = 8.8 * peso + 25 * altura + 865;
            }

            if (genero == "MUJER")
            {
                if (edad >= 10 && edad <= 18)
                    tasa = 7.4 * peso + 482 * altura + 217;
                if (edad > 18 && edad <= 30)
                    tasa = 13.3 * peso + 334 * altura + 35;
                if (edad > 30 && edad <= 60)
                    tasa = 8.7 * peso + 25 * altura + 865;
                if (edad > 60)
                    tasa = 9.2 * peso + 673 * altura - 302;
            }
            

            return tasa;
            //  * P = peso (kg); T = talla (cm); E = edad (años)

           // kcal/día= (TM*Indice Actividad1*Horas Actividad1)+(TM*Indice Actividad2*Horas Actividad2)+ ...+(TM*Indice Actividadn*Horas Actividadn)
        }
        public double KILOCALORIASDIA()
        {
            double kcaldia = 0.0;

            kcaldia = TASAMETABOLICA() * this.indice;

            return kcaldia;
        
        }
      

    }

    public class ProveedorResources
    {
        private readonly Resource recursos = new Resource();

        public Resource Resources
        {
            get { return recursos; }
        }
    }

    public class IndiceActividad
    {
        public double indice { get; set; }
        public String descripcion { get; set; }
    }

    public static class Conversion
    { 
        public static double ConverDouble(string data)
        {
            var cultura = CultureInfo.CurrentCulture;
            RegionInfo regionInfo = new RegionInfo(cultura.Name);
            bool isMetric = regionInfo.IsMetric;
            double x = 0.0;
            if (double.TryParse(data, NumberStyles.Any,cultura.NumberFormat, out x))
            {
               
            }

            return x;
        
                
        }

        public static double ToCentimetros(double pies , double pulgadas)
        {
            //return (pulgadas * 2.54) + ((pies *100)/3.2808);

            return Math.Round((pies * 30.48) + (pulgadas * 2.54),2);
        }

        public static double ToPulgadas(double centimetros)
        {
            if (centimetros == 0.0) return 0;

            return centimetros /  2.6;
        }
        public static void ToPies(double centimetros,out double Pies, out double Pulgadas)
        {
            //return centimetros * (3.28083989501312 / 100);

            double pies                 =   Math.Floor(centimetros / 30.48);
            double totalPulgadas        =   centimetros/2.54;
            double pulgadasRestantes    =   Math.Round(totalPulgadas - (pies * 12),2);

            Pies = pies;
            Pulgadas =  Math.Round(Math.Floor(pulgadasRestantes < 1 ? 0 : pulgadasRestantes));
            //return Math.Round( pies + (pulgadasRestantes/100),2);

        }
        
        public static double ToLibras(double kilogramos)
        {   
            double libra = 0.4535924;

            return kilogramos / libra;
        }
        public static double ToKilogramos(double libras)
        {
            return libras * 0.4535924;
        }
    }
}
