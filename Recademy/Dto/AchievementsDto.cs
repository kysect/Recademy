namespace Recademy.Dto
{
    public class AchievementsDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public static AchievementsDto Of(string name, string description, string icon)
        {
            return new AchievementsDto
            {
                Name = name,
                Description = description,
                Icon = icon
            };
        }
    }
}
