
using System.Globalization;

namespace LifeHub.Models.MoodJournal;

// Models/MoodJournal/MoodGroup.cs
public class MoodGroup : List<MoodEntry>
{
    public string DateHeader { get; set; }
    public MoodGroup(string dateHeader, List<MoodEntry> entries) : base(entries)
    {
        DateHeader = dateHeader;
    }
}