namespace SaveMW.Models
{
    public abstract class FileDetails
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Extention { get; set; }
    }
}
