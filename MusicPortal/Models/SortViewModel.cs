namespace MusicPortal.Models
{
    public class SortViewModel
    {
        public SortState SingerSort { get; private set; }
        public SortState GenreSort { get; private set; }
        public SortState NameOfSongSort { get; private set; }
        public SortState Current { get; private set; }

        public SortViewModel(SortState sortOrder)
        {
            SingerSort = sortOrder == SortState.SingerAsc ? SortState.SingerDesc : SortState.SingerAsc;
            GenreSort = sortOrder == SortState.GenreAsc ? SortState.GenreDesc : SortState.GenreAsc;
            NameOfSongSort = sortOrder == SortState.NameOfSongAsc ? SortState.NameOfSongDesc : SortState.NameOfSongAsc;
            Current = sortOrder;
        }
    }
}
