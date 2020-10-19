using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Newtonsoft.Json;
using Microsoft.TeamFoundation.TestClient.PublishTestResults;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

namespace TestProjectConsoleApp
{
    class Program
    {

        const string accessToken = "3rsrxikx3y4n2sxb2rlpu25z3ew4ixx3t65eiskgq54oivyyyqba";
        const  string projectUrl = "https://dev.azure.com/interests";
        private const string projectName = "TestProject";
        private const string repoName = "TestProject";


        static async Task Main(string[] args)
        {
            await GetProjectData();
        }
        

        public static async Task GetProjectData()
        {
            try
            {
                var credentials = new VssBasicCredential(string.Empty, accessToken);

                var connection = new VssConnection(new Uri(projectUrl), credentials);

                using var gitClient = connection.GetClient<GitHttpClient>();

                var repo = await gitClient.GetRepositoryAsync(projectName, repoName);
                 var projectData = repo.ProjectReference;

                Console.WriteLine($"Project Name : {projectData.Name}\nProject Url : {projectData.Url}" +
                                  $"\nProject Description : {projectData.Description}\nProject Id : {projectData.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
