using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


namespace ConsoleAppCore
{
    public class Program
    {
        public static IConfiguration Configuration;
        private static IContext context;

        static void Main(string[] args)
        {
            try
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
                Configuration = builder.Build();

                context = new Model(Configuration.GetConnectionString("LocalDB"));
                StaticMethodsClass.SetContext(context);

                String n; ConsoleKeyInfo aux;
                do
                {
                    Console.WriteLine("\n\nElige una opcion:");
                    Console.WriteLine("\t1. Añadir.");
                    Console.WriteLine("\t2. Seleccionar.");
                    Console.WriteLine("\t3. Renombrar.");
                    Console.WriteLine("\t4. Eliminar.");
                    Console.WriteLine("\t5. Mostrar BD.");
                    Console.WriteLine("\t6. Añadir hijos a un padre");
                    Console.WriteLine("\tESC. Salir\n");

                    aux = Console.ReadKey();

                    switch (aux.Key)
                    {
                        default:
                            Console.WriteLine("Opcion incorrecta");
                            break;
                        case ConsoleKey.Escape:
                            break;
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            Console.WriteLine("\nEscribir nombre: ");
                            n = Console.ReadLine();
                            StaticMethodsClass.Add<master>(new master() { Name = n });
                            break;
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            Console.WriteLine("\nEscribir nombre: ");
                            n = Console.ReadLine();
                            var m1 = StaticMethodsClass.Get<master>(x => x.Name.ToUpper().Equals(n.ToUpper()));
                            Console.WriteLine(String.Format("Encontradas {0} coincidencias.", m1.Count()));
                            break;
                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            Console.WriteLine("\nEscribir nombre: ");
                            n = Console.ReadLine();
                            var m3 = StaticMethodsClass.Get<master>(x => x.Name.ToUpper().Equals(n.ToUpper()));
                            if (m3 != null && m3.Any())
                            {
                                foreach (var m in m3.ToList())
                                {
                                    m.Name = "Cambiado";
                                    StaticMethodsClass.Update(m);
                                }
                            }
                            else Console.WriteLine("No encontrado");

                            break;
                        case ConsoleKey.D4:
                        case ConsoleKey.NumPad4:
                            Console.WriteLine("\nEscribir nombre: ");
                            n = Console.ReadLine();
                            var m4 = StaticMethodsClass.Get<master>(x => x.Name.ToUpper().Equals(n.ToUpper()));
                            if (m4 != null && m4.Any())
                            {
                                foreach (master m in m4.ToList())
                                {
                                    //foreach (detalle d in m.detalle.ToList())
                                    //{
                                    //    ClaseEstaticaMetodos.Delete(d);
                                    //}
                                    StaticMethodsClass.Delete(m); //elimina tambien los hijos (cascade)
                                }

                            }
                            else Console.WriteLine("No encontrado");
                            var m5 = StaticMethodsClass.Get<detail>(x => x.IdMaster.Equals(n.ToUpper()));

                            break;
                        case ConsoleKey.D5:
                        case ConsoleKey.NumPad5:

                            Console.Write("\n...................................................................");
                            foreach (master m in context.Set<master>().ToList())
                            {
                                Console.Write("\nID Maestro: " + m.Id + ", Nombre: " + m.Name);
                            }
                            Console.Write("\n...................................................................");
                            foreach (detail d in context.Set<detail>().ToList())
                            {
                                Console.Write("\nID Hijo: " + d.Id + ",    Nombre: " + d.Name + ", ID Maestro: " + d.IdMaster);
                            }
                            Console.Write("\n...................................................................\n");

                            break;
                        case ConsoleKey.D6:
                        case ConsoleKey.NumPad6:
                            String nombreHijo = null;
                            Console.WriteLine("\nEscribir nombre del padre: ");
                            n = Console.ReadLine();
                            var m6 = StaticMethodsClass.GetFirst<master>(x => x.Name.ToUpper().Equals(n.ToUpper()));
                            if (m6 != null)
                            {
                                Console.WriteLine("Escribir nombre del hijo: ");
                                nombreHijo = Console.ReadLine();
                                StaticMethodsClass.Add(new detail() { Name = nombreHijo, IdMaster = m6.Id });
                            }
                            else
                            {
                                Console.WriteLine("Padre no encontrado");
                            }
                            break;


                    }
                    Console.ReadKey();
                    Console.Clear();
                } while (aux.Key != ConsoleKey.Escape);

                Console.ReadLine();
            }
            catch (Exception exc)
            {
                Console.WriteLine(String.Format(@"Excepcion controlada con mensaje: {exc.Message}"));
            }
        }
    }
}