namespace Recademy.Dto
{
    public class GhRepositoryDto
    {
        public string RepositoryName { get; set; }
        public string RepositoryUrl { get; set; }
        public string Readme { get; set; }

        public override string ToString()
        {
            return RepositoryName;
        }
    }
}
