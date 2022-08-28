using Microsoft.AspNetCore.Components;

namespace Recademy.Shared.Dtos
{
    public class GhRepositoryDto
    {
        public string RepositoryName { get; set; }
        public string RepositoryUrl { get; set; }
        public MarkupString Readme { get; set; }
        public string Language { get; set; }
    }
}