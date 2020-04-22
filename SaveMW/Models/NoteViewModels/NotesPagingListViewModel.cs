namespace SaveMW.Models.NoteViewModels
{
    public class NotesPagingListViewModel : NotesListViewModel
    {
        public Paging Paging { get; set; }

        public FetchOptions FetchOptions { get; set; }
    }
}