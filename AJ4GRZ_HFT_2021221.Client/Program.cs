using AJ4GRZ_HFT_2021221.Models;
using ConsoleTools;
using System;
using System.Collections.Generic;
using System.Reflection;
using static AJ4GRZ_HFT_2021221.Models.StatReturnTypes;

namespace AJ4GRZ_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(8000);

            RestService rest = new("http://localhost:32747");

            Menu(rest);
        }

        private static void Menu(RestService rest)
        {
            #region Menu items
            var gpuMenu = new ConsoleMenu()
                .Add("List GPUs", () => GetAll<Gpu>("gpu"))
                .Add("Select GPU by Id", () => GetOne<Gpu>("gpu"))
                .Add("Create new GPU", () => Create("gpu", new Gpu()))
                .Add("Update GPU", () => Update("gpu", new Gpu()))
                .Add("Delete GPU by Id", () => Delete<Gpu>("gpu"))
                .Add("Close GPUs", ConsoleMenu.Close)
                .Add("Exit", () => Environment.Exit(0))
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = true;
                    config.Title = "GPU Menu";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });

            var cpuMenu = new ConsoleMenu()
                .Add("List CPUs", () => GetAll<Cpu>("cpu"))
                .Add("Select CPU by Id", () => GetOne<Cpu>("cpu"))
                .Add("Create new CPU", () => Create("cpu", new Cpu()))
                .Add("Update CPU", () => Update("cpu", new Cpu()))
                .Add("Delete CPU by Id", () => Delete<Cpu>("cpu"))
                .Add("Close CPUs", ConsoleMenu.Close)
                .Add("Exit", () => Environment.Exit(0))
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = true;
                    config.Title = "CPU Menu";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });

            var brandMenu = new ConsoleMenu()
                .Add("List brands", () => GetAll<Brand>("brand"))
                .Add("Select brand by Id", () => GetOne<Brand>("brand"))
                .Add("Create new brand", () => Create("brand", new Brand()))
                .Add("Update brand", () => Update("brand", new Brand()))
                .Add("Delete brand by Id", () => Delete<Brand>("brand"))
                .Add("Close brands", ConsoleMenu.Close)
                .Add("Exit", () => Environment.Exit(0))
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = true;
                    config.Title = "Brand Menu";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });

            var notebookMenu = new ConsoleMenu()
                .Add("List notebooks", () => GetAll<Notebook>("notebook"))
                .Add("Select notebook by Id", () => GetOne<Notebook>("notebook"))
                .Add("Create new notebook", () => Create("notebook", new Notebook()))
                .Add("Update notebook", () => Update("notebook", new Notebook()))
                .Add("Delete notebook by Id", () => Delete<Notebook>("notebook"))
                .Add("Close notebooks", ConsoleMenu.Close)
                .Add("Exit", () => Environment.Exit(0))
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = true;
                    config.Title = "Notebook Menu";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });

            var statMenu = new ConsoleMenu()
                .Add("Best GPU", () => GetAll<string>("stat/bestgpu"))
                .Add("Brands' average prices", () => GetAll<BrandsAvgPrice>("stat/brandsavgprices"))
                .Add("New generation notebooks", () => GetAll<KeyValuePair<string, string>>("stat/newgennotebooks"))
                .Add("Most popular CPUs", () => GetAll<KeyValuePair<string, int>>("stat/threemostpopularcpus"))
                .Add("High-End notebooks", () => GetAll<HighEndNotebook>("stat/highendnotebooks"))
                .Add("Min and max prices by brands", () => GetAll<MaxMinPrice>("stat/maxminprices"))
                .Add("Close statistics", ConsoleMenu.Close)
                .Add("Exit", () => Environment.Exit(0))
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = true;
                    config.Title = "Stat Menu";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });

            var menu = new ConsoleMenu()
              .Add("Stats", statMenu.Show)
              .Add("Notebooks", notebookMenu.Show)
              .Add("Brands", brandMenu.Show)
              .Add("CPUs", cpuMenu.Show)
              .Add("GPUs", gpuMenu.Show)
              .Add("Exit", () => Environment.Exit(0))
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.EnableFilter = true;
                  config.Title = "Main menu";
                  config.EnableWriteTitle = true;
                  config.EnableBreadcrumb = true;
              });

            menu.Show();
            #endregion

            #region Extension methods
            void GetAll<T>(string endpoint)
            {
                rest.Get<T>(endpoint).ForEach(z => Console.WriteLine(z.ToString())); Console.ReadLine();
            }

            void GetOne<T>(string endpoint)
            {
                Console.WriteLine("Type in Id of item to view!");
                string id = Console.ReadLine();
                Console.WriteLine(rest.Get<T>(int.Parse(id), endpoint));
                Console.ReadLine();
            }

            void Create<T>(string endpoint, T entity)
            {
                var newEntity = entity;
                Type t = typeof(T);
                var props = t.GetProperties();
                foreach (var prop in props)
                {
                    bool isNumber = prop.GetCustomAttribute<NumberAttribute>() != null;
                    bool isText = prop.GetCustomAttribute<TextAttribute>() != null;
                    if (isNumber)
                    {
                        Console.WriteLine($"Type in {prop.Name}!");
                        prop.SetValue(newEntity, int.Parse(Console.ReadLine()));
                    }
                    else if (isText)
                    {
                    Console.WriteLine($"Type in {prop.Name}!");
                        prop.SetValue(newEntity, Console.ReadLine());
                    }
                }
                rest.Post<T>(newEntity, endpoint);
            }

            void Update<T>(string endpoint, T entity)
            {
                var newEntity = entity;
                Type t = typeof(T);
                var props = t.GetProperties();
                foreach (var prop in props)
                {
                    bool isId = prop.GetCustomAttribute<IdAttribute>() != null;
                    bool isNumber = prop.GetCustomAttribute<NumberAttribute>() != null;
                    bool isText = prop.GetCustomAttribute<TextAttribute>() != null;
                    if (isNumber || isId)
                    {
                        Console.WriteLine($"Type in {prop.Name}!");
                        prop.SetValue(newEntity, int.Parse(Console.ReadLine()));
                    }
                    else if (isText)
                    {
                        Console.WriteLine($"Type in {prop.Name}!");
                        prop.SetValue(newEntity, Console.ReadLine());
                    }
                }
                rest.Put<T>(newEntity, endpoint);
            }

            void Delete<T>(string endpoint)
            {
                Console.WriteLine("Type in Id!");
                int id = int.Parse(Console.ReadLine());
                rest.Delete(id, endpoint);
            }
            #endregion
        }
    }
}
