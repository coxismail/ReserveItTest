using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace ReserveItTest.Repository
{
    public class StringStore : IStringStore
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string filename = "Data.json";
        public StringStore(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public List<string> GetList()
        {
            var data = new List<string>();
            if (!File.Exists(filename))
            {
                var jsondata = JsonConvert.SerializeObject(data, Formatting.Indented);
                System.IO.File.WriteAllText(filename, jsondata);
            }
            using (StreamReader streamer = System.IO.File.OpenText(filename))
            {
                var json = streamer.ReadToEnd();
                data = JsonConvert.DeserializeObject<List<string>>(json);
                streamer.Dispose();
            }
            return data;
        }

        public void StoreData(List<string> data)
        {
            string rootPath = _hostingEnvironment.ContentRootPath;
            var newdata = new List<string>();
            using (StreamReader streamer = System.IO.File.OpenText(filename))
            {
                var json = streamer.ReadToEnd();
                newdata = JsonConvert.DeserializeObject<List<string>>(json);
                newdata.AddRange(data);
                streamer.Dispose();
            }
            var jsondata = JsonConvert.SerializeObject(newdata, Formatting.Indented);
            System.IO.File.WriteAllText(filename, jsondata);

        }
    }
}
