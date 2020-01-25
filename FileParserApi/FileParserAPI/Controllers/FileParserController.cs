using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FileParserAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileParserController : ControllerBase
    {
        private readonly ILogger<FileParserController> _logger;
        

        public FileParserController(ILogger<FileParserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "FileParser";
        }


        [HttpGet("/records/gender")]
        public string[] GetByGender()
        {
            
            var result = new List<string>();
            foreach (var record in RecordContainer.ApiRecords.OrderBy(x => x.Gender))
            {
                result.Add(Newtonsoft.Json.JsonConvert.SerializeObject(record, Formatting.None));
            }
            return result.ToArray();

        }

        [HttpGet("/records/birthdate")]
        public string[] GetByBirthDate()
        {
            var result = new List<string>();
            foreach (var record in RecordContainer.ApiRecords.OrderBy(x => x.DOB))
            {
                result.Add(Newtonsoft.Json.JsonConvert.SerializeObject(record, Formatting.None));
            }
            return result.ToArray();
        }

        [HttpGet("/records/name")]
        public string[] GetByName()
        {
            var result = new List<string>();
            foreach (var record in RecordContainer.ApiRecords.OrderBy(x => x.LastName))
            {
                result.Add(Newtonsoft.Json.JsonConvert.SerializeObject(record, Formatting.None));
            }
            return result.ToArray();
        }

        [HttpPost("/records")]
        public async Task<string> Post()
        {
            var content = new string[] { await new StreamReader(Request.Body).ReadToEndAsync() };
            RecordContainer.ApiRecords.AddRange(FileParser.FileParser.ParseLines(content));
            return "Success";
        }
    }
}
