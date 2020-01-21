using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlazorApp1.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WordsController : ControllerBase
    {
        private readonly ILogger<WordsController> logger;

        public WordsController(ILogger<WordsController> logger)
        {
            this.logger = logger;
        }


        [HttpGet]
        public string Get()
        {
            string txt = System.IO.File.ReadAllText("test.txt");

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            stopwatch.Start();
            var xdocument = XDocument.Parse(txt);
            stopwatch.Stop();
            this.logger.LogInformation(stopwatch.ElapsedMilliseconds.ToString());

            using MemoryStream generatedDocument = new MemoryStream();
            xdocument.Save(generatedDocument);
            generatedDocument.Position = 0;
            using XmlReader partXmlReader = XmlReader.Create(generatedDocument);

            stopwatch.Restart();
            XDocument.Load(partXmlReader);
            stopwatch.Stop();
            this.logger.LogInformation(stopwatch.ElapsedMilliseconds.ToString());

            return txt;
        }
    }
}
