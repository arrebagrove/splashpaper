using Newtonsoft.Json.Linq;
using splashpaper.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace splashpaper.Controllers {
    public static class RandomController {
        public static ObservableCollection<Paper> RandomPapers { get; set; }

        public static async Task<bool> FillRandomPaper() {
            RandomPapers = new ObservableCollection<Paper>();

            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", "16246a4d58baa698a0a720106aab4ecedfe241c72205586da6ab9393424894a8");
            HttpResponseMessage response = null;

            try {
                response = await http.GetAsync("https://api.unsplash.com/photos/random");
                response.EnsureSuccessStatusCode();
                string responseBodyAsText = await response.Content.ReadAsStringAsync();

                JArray jsonList = JArray.Parse(responseBodyAsText);

                foreach (JObject jsonItem in jsonList) {
                    Paper paper = new Paper();
                    paper.Id = (string)jsonItem.GetValue("id");
                    paper.URLRaw = (string)jsonItem["urls"]["raw"];
                    paper.Thumbnail = (string)jsonItem["urls"]["regular"];

                    RandomPapers.Add(paper);
                }

                return true;
            }
            catch (HttpRequestException hre) {
                return false;
            }
        }

        public static async Task<Paper> GetRandom() {
            Paper paper = new Paper();

            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Client-ID", "16246a4d58baa698a0a720106aab4ecedfe241c72205586da6ab9393424894a8");
            HttpResponseMessage response = null;

            try {
                response = await http.GetAsync("https://api.unsplash.com/photos/random");
                response.EnsureSuccessStatusCode();
                string responseBodyAsText = await response.Content.ReadAsStringAsync();

                JObject json = JObject.Parse(responseBodyAsText);

                paper.Id = (string)json.GetValue("id");
                paper.Likes = (int)json.GetValue("likes");
                paper.URLRaw = (string)json["urls"]["raw"];
                paper.Thumbnail = (string)json["urls"]["thumb"];

                return paper;
            }
            catch (HttpRequestException hre) {
                return paper;
            }
        }
    }
}
