namespace Recademy.Dto
{
    public class GhRepositoryDto
    {
        public string RepositoryName { get; set; }
        public string RepositoryUrl { get; set; }
        public string Readme { get; set; }
        public string Language { get; set; }

        public static GhRepositoryDto Of(string repositoryName, string respositoryUrl, string readme, string language)
        {
            
            return new GhRepositoryDto
            {
                RepositoryName = repositoryName,
                RepositoryUrl = respositoryUrl,
                Readme = readme,
                Language = language
            };
        }

        public override string ToString()
        {
            return RepositoryName;
        }
    }
}
