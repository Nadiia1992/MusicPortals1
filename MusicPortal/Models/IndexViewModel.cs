namespace MusicPortal.Models;

public class IndexViewModel
(
    IEnumerable<Singers> singer,
    PageViewModel pageViewModel,
    FilterViewModel filterViewModel,
    SortViewModel sortViewModel
)
{
    public IEnumerable<Singers> Singer { get; } = singer;
    public PageViewModel PageViewModel { get; } = pageViewModel;
    public FilterViewModel FilterViewModel { get; } = filterViewModel;
    public SortViewModel SortViewModel { get; } = sortViewModel;
}
