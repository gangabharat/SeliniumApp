using HtmlAgilityPack;

namespace SeliniumApp.Services
{
    public class ChristianlyriczService
    {
        private readonly HtmlDocument _htmlDocument;
        public ChristianlyriczService(HtmlDocument htmlDocument)
        {

            _htmlDocument = htmlDocument;
        }

        public async Task Run()
        {
            // Read a text file line by line.
            string[] lines = File.ReadAllLines(@"C:\\Users\\ganga\\OneDrive\\Desktop\\christianlyricz.csv");
            int count = 0;
            int total = lines.Skip(1).Count();
            foreach (string line in lines.Skip(1))
            {
                count++;
                //Thread.Sleep(1000 * 5);
                var model = line.Split(',');
                Console.WriteLine("executing {0}/{1}", count, total);
                HttpClient Client = new HttpClient();
                var res = await Client.GetAsync(model[1]);

                if (res.IsSuccessStatusCode)
                {
                    _htmlDocument.LoadHtml(res.Content.ReadAsStringAsync().Result);
                    var tabPanels = _htmlDocument.DocumentNode.Descendants("div")
                        .Where(x => x.GetAttributeValue("class", "").StartsWith("tabcontent")).Take(2).ToList();


                    File.WriteAllText($"output/{model[0]}.htm", string.Join(Environment.NewLine, tabPanels.Select(x => x.InnerHtml)));
                }
            }

            await Task.CompletedTask;
        }
    }
}
