using System.Security.Cryptography.X509Certificates;

void Main() {
    SettingsLoader.LoadSettings("GenSettings");
    
    MapSet mapSet;
    try
    {
        mapSet = new MapSet(SettingsLoader.settingsList?[0]);
        Console.WriteLine(mapSet);
        
    }
    catch (NullReferenceException e)
    {
        Console.WriteLine(e.Message);
    }
}



Main();