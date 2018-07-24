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
                    Console.WriteLine("\n\nChoose an option:");
                    Console.WriteLine("\t1. Add.");
                    Console.WriteLine("\t2. Select.");
                    Console.WriteLine("\t3. Rename.");
                    Console.WriteLine("\t4. Delete.");
                    Console.WriteLine("\t5. Show DB.");
                    Console.WriteLine("\t6. Add childs to father");
                    Console.WriteLine("\tESC. Salir\n");

                    aux = Console.ReadKey();

                    switch (aux.Key)
                    {
                        default:
                            Console.WriteLine("Incorrect option!");
                            break;
                        case ConsoleKey.Escape:
                            break;
                        case ConsoleKey.D1:
                        case ConsoleKey.NumPad1:
                            Console.WriteLine("\nType name: ");
                            n = Console.ReadLine();
                            StaticMethodsClass.Add<master>(new master() { Name = n });
                            break;
                        case ConsoleKey.D2:
                        case ConsoleKey.NumPad2:
                            Console.WriteLine("\nType name: ");
                            n = Console.ReadLine();
                            var m1 = StaticMethodsClass.Get<master>(x => x.Name.ToUpper().Equals(n.ToUpper()));
                            Console.WriteLine(String.Format("Found {0} coincidences.", m1.Count()));
                            break;
                        case ConsoleKey.D3:
                        case ConsoleKey.NumPad3:
                            Console.WriteLine("\nType name: ");
                            n = Console.ReadLine();
                            var m3 = StaticMethodsClass.Get<master>(x => x.Name.ToUpper().Equals(n.ToUpper()));
                            if (m3 != null && m3.Any())
                            {
                                foreach (var m in m3.ToList())
                                {
                                    m.Name = "Changed";
                                    StaticMethodsClass.Update(m);
                                }
                            }
                            else Console.WriteLine("Not found!");

                            break;
                        case ConsoleKey.D4:
                        case ConsoleKey.NumPad4:
                            Console.WriteLine("\nType name: ");
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
                            else Console.WriteLine("Not found!");
                            var m5 = StaticMethodsClass.Get<detail>(x => x.IdMaster.Equals(n.ToUpper()));

                            break;
                        case ConsoleKey.D5:
                        case ConsoleKey.NumPad5:

                            Console.Write("\n...................................................................");
                            foreach (master m in context.Set<master>().ToList())
                            {
                                Console.Write("\nID Master: " + m.Id + ", Name: " + m.Name);
                            }
                            Console.Write("\n...................................................................");
                            foreach (detail d in context.Set<detail>().ToList())
                            {
                                Console.Write("\nID Detail: " + d.Id + ",    Name: " + d.Name + ", ID Master: " + d.IdMaster);
                            }
                            Console.Write("\n...................................................................\n");

                            break;
                        case ConsoleKey.D6:
                        case ConsoleKey.NumPad6:
                            String childName = null;
                            Console.WriteLine("\nType father's name: ");
                            n = Console.ReadLine();
                            var m6 = StaticMethodsClass.GetFirst<master>(x => x.Name.ToUpper().Equals(n.ToUpper()));
                            if (m6 != null)
                            {
                                Console.WriteLine("Type child's name: ");
                                childName = Console.ReadLine();
                                StaticMethodsClass.Add(new detail() { Name = childName, IdMaster = m6.Id });
                            }
                            else
                            {
                                Console.WriteLine("Father not found!");
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
                Console.WriteLine(String.Format(@"Exception with message: {exc.Message}"));
            }
        }
    }
}