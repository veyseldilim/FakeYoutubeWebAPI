using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeWeb.Data.InitializeDb
{
    public static class SeedHelper
    {
        public static List<TEntity> SeedData<TEntity>(string fileName)
        {
            

            var dirPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fullPath = Path.Combine(dirPath, @$"Data/{fileName}");

            var result = new List<TEntity>();
            using (StreamReader reader = new StreamReader(fullPath))
            {
                string json = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<List<TEntity>>(json);
            }

            return result;
        }

    }
}
