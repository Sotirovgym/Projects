namespace HTTPServer.ByTheCake.Data
{
    using HTTPServer.ByTheCake.Models.Cakes;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CakesData
    {
        private const string dataPath = @"ByTheCake\Data\Database.csv";

        public void Add(string name, string price)
        {
            int id;

            using (var streamReader = new StreamReader(dataPath))
            {
                id = streamReader.ReadToEnd().Split(Environment.NewLine).Length - 1 + 1;
            }

            using (var streamWriter = new StreamWriter(dataPath, true))
            {
                streamWriter.WriteLine($"{id}, {name}, {price}");
            }
        }

        public IEnumerable<Cake> All()
        {
            var cakes = File
                    .ReadAllLines(dataPath)
                    .Where(l => l.Contains(','))
                    .Select(l => l.Split(','))
                    .Select(l => new Cake
                    {
                        Id = int.Parse(l[0]),
                        Name = l[1],
                        Price = decimal.Parse(l[2])
                    });

            return cakes;
        }

        public Cake FindById(int id)
        {
            var cake = this.All()
                .SingleOrDefault(c => c.Id == id);

            return cake;
        }
    }
}
