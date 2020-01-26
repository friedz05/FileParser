using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FileParserAPI.Controllers
{
    /// <summary>
    /// Controller for Web API for File Parser
    /// Assumptions:
    /// Get will return an empty if there are no objects
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FileParserController : ControllerBase
    {        
        /// <summary>
        /// Simple method that returns name of api
        /// </summary>
        /// <returns>name of api</returns>
        [HttpGet]
        public string Get()
        {
            return "FileParser";
        }

        /// <summary>
        /// Get method returns the record data sorted by gender
        /// </summary>
        /// <returns>Ok Json response with data sorted</returns>
        [HttpGet("/records/gender")]
        public ActionResult GetByGender()
        {
            return Ok(RecordContainer.ApiRecords.OrderBy(x => x.Gender));
        }
        /// <summary>
        /// Get method returns the record data sorted by Date of Birth
        /// </summary>
        /// <returns>Ok Json response with data sorted</returns>
        [HttpGet("/records/birthdate")]
        public ActionResult GetByBirthDate()
        {
            return Ok(RecordContainer.ApiRecords.OrderBy(x => x.DOB));
        }
        /// <summary>
        /// Get method returns the record data sorted by Last Name
        /// </summary>
        /// <returns>Ok Json response with data sorted</returns>
        [HttpGet("/records/name")]
        public ActionResult GetByName()
        {
            return Ok(RecordContainer.ApiRecords.OrderBy(x => x.LastName));
        }

        /// <summary>
        /// Post method that grabs the data from the body of the request, attempts to parse it
        /// If parsed successful, data is added to singleton
        /// </summary>
        /// <returns>String "Content Posted" if parsed successfully, "Content Not Parsed" otherwise</returns>
        [HttpPost("/records")]
        public async Task<ActionResult> Post()
        {
            if (Request == null)
            {
                return BadRequest();
            }
            var content = new string[] { await new StreamReader(Request.Body).ReadToEndAsync() };
            var initCount = RecordContainer.ApiRecords.Count;
            RecordContainer.ApiRecords.AddRange(FileParser.FileParser.ParseLines(content));
            if (initCount < RecordContainer.ApiRecords.Count)
                return Ok(RecordContainer.ApiRecords);
            else
                return BadRequest();
        }
    }
}
