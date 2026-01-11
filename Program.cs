using System.Security.Cryptography.X509Certificates;

void Main() {
    SettingsLoader.LoadSettings("GenSettings");

    try
    {
        MapSet mapSet = new MapSet(SettingsLoader.settingsList?[0]);
        Draw.BMPDraw(mapSet[0]);
    }
    catch (NullReferenceException e)
    {
        Console.WriteLine(e.Message);
    }
}



Main();