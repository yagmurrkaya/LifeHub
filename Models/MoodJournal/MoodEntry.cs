using System.Globalization;

namespace LifeHub.Models.MoodJournal;

public class MoodEntry
{
    public string Mood { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    // Ekran görüntülerindeki "Tarih | Mood \n Not" formatı
    public string DisplayText => $"{Date.ToString("dd MMM HH:mm", new CultureInfo("tr-TR"))} | {Mood}\n{Note}";

    // Ekran görüntülerindeki renk kodlaması
    public Color MoodColor => Mood switch
    {
        "Happy" => Color.FromArgb("#FFF4B1"),   // Sarı
        "Neutral" => Color.FromArgb("#EAEAEA"), // Gri
        "Sad" => Color.FromArgb("#D6E4F0"),     // Mavi
        "Angry" => Color.FromArgb("#F7C8C8"),   // Kırmızı
        "Tired" => Color.FromArgb("#EADCF8"),   // Mor
        _ => Colors.White
    };
}