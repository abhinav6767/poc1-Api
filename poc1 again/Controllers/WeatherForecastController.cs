using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Octokit;
using poc1_again.Model;
using System.Collections.ObjectModel;

namespace poc1_again.Controllers
{
    
    [ApiController]
    [Route("[controller]")]
    public class GetRepoDataController : ControllerBase
    {
        static string name1;
        static string name ;
        static string orgname ;

        private readonly ILogger<GetRepoDataController> _logger;

        public GetRepoDataController(ILogger<GetRepoDataController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet(Name = "GetRepoData")]
        public async Task<IReadOnlyCollection<object>> GetAsync()
        {
            
            var client = new GitHubClient(new Octokit.ProductHeaderValue("MyAmazingApp"));
            var user = await client.User.Get( string.IsNullOrEmpty(name)? "abhinav6767":name);
            if (user == null) return null;

            var tokenAuth = new Credentials(string.IsNullOrEmpty(name1) ? "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" : name1); // NOTE: not real token
            client.Credentials = tokenAuth;


            /*var  data=await client.Repository.Collaborator.GetAll(331587235);
            Console.WriteLine(data.Count);*/

            try
            {
                var dat1 = await client.Repository.GetAllForOrg(string.IsNullOrEmpty(orgname) ? "Dl-Abhi-Enterprise" : orgname);
                foreach (var dinosaur in dat1)
                {
                    Console.WriteLine(dinosaur.Name);
                    Console.WriteLine(dinosaur.Description);
                    Console.WriteLine(dinosaur.Id);
                    Console.WriteLine(dinosaur.Url);

                }
                name1 = null;
                name = null;
                orgname = null;

                return dat1;
            }
            catch (Exception ex)
            {
                var dat = new List<infomodel>(); 
                 dat.Add(   new infomodel{ Name="Null", id="Null", fullName = "Null" , updatedAt = "Null"});
                return dat;
            }



            /*  if (dat1 == null) { Console.WriteLine("i am called"); }*/
            return null; 
           


        }

        [HttpPost]
        public async Task<ActionResult> postdata(string token,string profile_name ,string organisation_name)
        {
            name1 = token;
            name=profile_name;
            orgname=organisation_name;
            Console.WriteLine(name1);
            Console.WriteLine(name);
            Console.WriteLine(orgname);
            return Ok();
        }



    }
}