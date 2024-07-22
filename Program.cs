using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Quiz2Calendario

{
    class Evento
    {
        public Evento(DateTime miFecha, String miDescripcion) {
            this.descripcion = miDescripcion;
            this.fecha = miFecha;
        }

        public DateTime fecha { set; get; }

        public String descripcion;
    }

    internal class Program
    {
        static Evento[] eventosAgendados = new Evento[100];

        static int contadorAgendar = 0;


        static void Main(string[] args)
        {
            Menu();





        }

        static void Menu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("     Bienvenidos al calendario ");
                Console.WriteLine("            UNED.NET ");
                Console.WriteLine("************************************");
                Console.WriteLine("   ");
                Console.WriteLine("Elige una de las siguentes opciones");
                Console.WriteLine("1 - Mostrar el calendario por el año");
                Console.WriteLine("2 - Mostrar el mes que desea del año");
                Console.WriteLine("3 - Mostrar el dia que desea del mes y el año");
                Console.WriteLine("4 - Agendar un evento");
                Console.WriteLine("5 - Ver eventos agendados");
                Console.WriteLine("6 - Salir del calendario");


                    string opcion = Console.ReadLine();

                    switch (opcion)
                    {
                        case "1":
                            MostrarPorAnio();
                            break;
                        case "2":
                            MostarPorMes();
                            break;
                        case "3":
                            MostarPorSemana();
                            break;
                        case "4":
                            AgendarEventos();
                            break;
                        case "5":
                            MostrarEventos();
                            break;
                        case "6":
                            return;
                        default:
                            Console.WriteLine("Ingreso una opcion invalidad");
                            break;
                    }



             
            }



        }
        static string ObtenerNombreMes(int month)
        {
            string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Setiembre", "Octubre", "Noviembre", "Diciembre" };
            return meses[month - 1];
        }

        static void MostrarMes(int year, int month)
        {
            Console.WriteLine("\n" + ObtenerNombreMes(month) + "" + year);
            Console.WriteLine("Do Lu Ma Mi Ju Vi Sa");
            DateTime primerDiaMes = new DateTime(year, month, 1);
            int diasEnMes = DateTime.DaysInMonth(year, month);
            int diaDeLaSemana = (int)primerDiaMes.DayOfWeek;
            
            for (int i = 0; i < diaDeLaSemana; i++)
            {
                Console.Write("   ");
            }

            for (int dia = 1; dia <= diasEnMes; dia++)
            {
                Console.Write($"{dia,2} ");

                if ((dia + diaDeLaSemana) % 7 == 0)
                {
                    Console.Write("\n");
                }
            }


            

            Console.WriteLine( "\n");
        }

        static void MostarPorMes()
        {
            Console.Clear();
            Console.WriteLine("Introduce el año");
            int year;

            while (!int.TryParse(Console.ReadLine(), out year) || year < 999)
            {
                Console.WriteLine("Itroduce un valor valido del 1 al 999");
            }

            Console.WriteLine("Introduce el mes (1-12):");
            int month;
            while (!int.TryParse(Console.ReadLine(), out month) || month > 12)
            {
                Console.WriteLine("Introduce un mes valido entre 1 y 12 :");
            }
            MostrarMes(year, month);
            Console.WriteLine("Presiona cualquier tecla para continuar");
            Console.ReadKey();





        }


        static void MostrarPorAnio()
        
        {
          
            Console.Clear();
           
                Console.WriteLine("Introduce el año");
            


            int year = 0;
            bool anioEsValido = false;

           
           
            
                while (!int.TryParse(Console.ReadLine(), out year) || year < 1 || year > 9999)
                {
                    Console.WriteLine("Introduce un numero valido entre 1 a 9999");
                }

                for (int month = 1; month <= 12; month++)
                {
                    MostrarMes(year, month);
                }
                Console.WriteLine("Presiona cual tecla para continuar");
                Console.ReadKey();


            


        }

        static void MostarPorSemana()
        {
            Console.WriteLine("Introduce el año");

            int year;
            while (!int.TryParse(Console.ReadLine(), out year) || year < 1 || year > 9999)
            {

                Console.WriteLine("Introduce un año valido entre 1 a 9999:");



            }
            Console.WriteLine("Introduce un numero de mes del 1 al 12 )");

            int month;
            while (!int.TryParse(Console.ReadLine(), out month) || month < 1 || month > 12)
            {
                Console.WriteLine("Introduce un mes valido entre del 1 al 12 ");

            }
            Console.WriteLine("Introduce un numero de la semana del 1 al 5 ");
            int week;
            while (!int.TryParse(Console.ReadLine(), out week) || week < 1 || week > 5)
            {
                Console.WriteLine("Introduce un numero valido del 1 al 5 ");

            }
            mostrarSemana(year, month, week);
            Console.WriteLine("Presiona cualquier tecla para continuar");
            Console.ReadKey();





        }
        static void mostrarSemana(int year, int month, int week)
        {
            Console.WriteLine("\n" + ObtenerNombreMes(month) + " " + year);
            Console.WriteLine("Do Lu Ma Mi Ju Vi Sa");
            DateTime primerDiaMes = new DateTime(year,month,1);
            int DiaEnMes = DateTime.DaysInMonth(year, month);
            int DiaDeLaSemana = (int)primerDiaMes.DayOfWeek;
            int DiaInicioSemana = (week - 1) * 7 - DiaDeLaSemana + 1;
            if (DiaInicioSemana < 1)
            {

                DiaInicioSemana = 1;



            }
            for (int i = 0; i < 7; i++)
            {
                int DiaActual = DiaInicioSemana + i;
                if (DiaActual > DiaEnMes || DiaActual < 1)
                {

                    Console.Write(" ");


                }
                else
                {
                    Console.Write($"{DiaActual,2} ");

                }





            }

            Console.WriteLine("\n Presiona cualquier tecla para seguir");
            Console.ReadLine();
        }

       

        static void AgendarEventos()
        {
            Console.WriteLine("Agendar un evento");
            if (contadorAgendar < eventosAgendados.Length)
            {
                Console.WriteLine("Introduce una fecha del evento ");
                DateTime fechaEvento;

                while (!DateTime.TryParse(Console.ReadLine(), out fechaEvento))
                {
                    Console.WriteLine("Introduce una fecha valida");
                }

                Console.WriteLine("Introduzca la descripcion del evento");
                string descripcionEvento = Console.ReadLine();

                while (descripcionEvento.Length < 1)
                {
                    Console.WriteLine("Introduce descripcion valida");
                    descripcionEvento = Console.ReadLine();
                }

                eventosAgendados[contadorAgendar] = new Evento(fechaEvento, descripcionEvento);
                contadorAgendar++;
                
                
                Console.WriteLine("Evento guardado con exito" );
            }
            else
            {
                Console.WriteLine("(ERROR) No se logro guardar con exito");


            }

            Console.WriteLine("Presiona cualquier tecla para seguir");
            Console.ReadLine();



        }

        static void MostrarEventos()
        {
            Console.Clear();
            Console.WriteLine("Tus eventos agendados");

            for(int i = 0; i < contadorAgendar; i++) 
            {
                Console.WriteLine(eventosAgendados[i].fecha + " - " + eventosAgendados[i].descripcion);
             

            }
            if(contadorAgendar == 0) 
            {

                Console.WriteLine("No tienes eventos agendados");

            }
            Console.WriteLine("Presiona cualquier tecla para seguir");
            Console.WriteLine("CALENDARIO UNED.NET");
            Console.ReadLine();

        }
    }
}
