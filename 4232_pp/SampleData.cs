using _4232_pp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace _4232_pp
{
    public class SampleData
    {
        public static void Initialize(AptekaContext context, IWebHostEnvironment env)
        {
            if (!context.Products.Any())
            {
                try
                {
                    context.Products.AddRange(
                    new Product
                    {
                        Name = "Парацетомол",
                        Category = "Обезболивающее, жарапонижающее",
                        Price = 100,
                        Img="/img/товаркартинка.jpg"
                    },

                    new Product
                    {
                        Name = "Амоксицилин",
                        Category = "Антибиотик",
                        Price = 102,
                        Img = "/img/товаркартинка.jpg"
                    },

                    new Product
                    {
                        Name = "Бинт",
                        Category = "Перевязочные материалы",
                        Price = 35,
                        Img = "/img/товаркартинка.jpg"
                    },

                    new Product
                    {
                        Name = "Ибупрофен",
                        Category = "Обезболивающее, противовоспалительное средство",
                        Price = 199,
                        Img = "/img/товаркартинка.jpg"
                    },

                    new Product
                    {
                        Name = "Каптоприл",
                        Category = "Препараты для сердечно-сосудистой системы",
                        Price = 100,
                        Img = "/img/товаркартинка.jpg"
                    },

                    new Product
                    {
                        Name = "Нурофен",
                        Category = "Обезболивающее, противовоспалительное средство",
                        Price = 150,
                        Img = "/img/товаркартинка.jpg"
                    },

                    new Product
                    {
                        Name = "Эргоферон",
                        Category = "Иммуномодулятор",
                        Price = 300,
                        Img = "/img/товаркартинка.jpg"
                    },

                    new Product
                    {
                        Name = "Гемотаген",
                        Category = "Витамины и добавки",
                        Price = 45,
                        Img = "/img/товаркартинка.jpg"
                    },

                      new Product
                      {
                          Name = "Уголь",
                          Category = "Препараты для желудочно-кишечного тракта",
                          Price = 75,
                          Img = "/img/товаркартинка.jpg"
                      },

                       new Product
                       {
                           Name = "Нош-па",
                           Category = "Спазмолитик",
                           Price = 200,
                           Img = "/img/товаркартинка.jpg"
                       },

                        new Product
                        {
                            Name = "Градусник",
                            Category = "Медицинский прибор",
                            Price = 150,
                            Img = "/img/товаркартинка.jpg"
                        },

                         new Product
                         {
                             Name = "Пластырь",
                             Category = "Перевязочные материалы",
                             Price = 50,
                             Img = "/img/товаркартинка.jpg"
                         },

                          new Product
                          {
                              Name = "Танометр",
                              Category = "Медицинский прибор",
                              Price = 2000,
                              Img = "/img/товаркартинка.jpg"
                          },

                           new Product
                           {
                               Name = "Бахилы",
                               Category = "Защитные средства",
                               Price = 20,
                               Img = "/img/товаркартинка.jpg"
                           },

                            new Product
                            {
                                Name = "Аскорбинка",
                                Category = "Витамины и добавки",
                                Price = 25,
                                Img = "/img/товаркартинка.jpg"
                            },

                             new Product
                             {
                                 Name = "Каптоприл",
                                 Category = "Препараты для сердечно-сосудистой системы",
                                 Price = 100,
                                 Img = "/img/товаркартинка.jpg"
                             },

                              new Product
                              {
                                  Name = "Супрастин",
                                  Category = "Антигистаминное средство",
                                  Price = 135,
                                  Img = "/img/товаркартинка.jpg"
                              }
                   );
                    context.SaveChanges();
                }
                catch
                {
                    
                }
                }

        }
    }
}
