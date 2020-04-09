namespace SaveMW.Models.Filters
{
    public abstract class Range<T>
    {
        public T From { get; set; }

        public T To { get; set; }
    }
}
