using System.Text.Json;

public class SettingsLoader
{
    public static List<Settings>? settingsList = new();

    public static void LoadSettings(string Location)
    {
        using (StreamReader r = new StreamReader($"{Location}.json"))
        {
            string json = r.ReadToEnd();
            settingsList = JsonSerializer.Deserialize<List<Settings>>(json);
        }
    }


    public class Settings
    {
        public string name { get; set; } = "Missing";
        public int mapWidth { get; set; }
        public int mapHeight { get; set; }
        public List<NoiseMapSettings> maps { get; set; } = new();

        public class NoiseMapSettings
        {
            public string name { get; set; } = "Missing";
            public float freqx { get; set; }
            public float freqy { get; set; }
            public int amplitude { get; set; }


        }
    }
}