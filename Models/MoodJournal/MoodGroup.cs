
using System.Globalization;

namespace LifeHub.Models.MoodJournal;

public class MoodGroup : List<MoodEntry>
{
    public string DateHeader { get; set; }
    public MoodGroup(string dateHeader, List<MoodEntry> entries) : base(entries)
    {
        DateHeader = dateHeader;
    }
}