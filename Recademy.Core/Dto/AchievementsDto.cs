using System.Text.Json.Serialization;

namespace Recademy.Core.Dto
{
    public class AchievementsDto
    {
        [JsonConstructor]
        public AchievementsDto(string name, string description, string icon)
        {
            Name = name;
            Description = description;
            Icon = icon;
        }

        public string Name { get; }
        public string Description { get; }
        public string Icon { get; }
    }
}
