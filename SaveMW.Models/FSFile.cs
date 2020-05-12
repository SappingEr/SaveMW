namespace SaveMW.Models
{
    public class FSFile : FileDetails
    {
        public virtual Note Note { get; set; } 
        
        public virtual int Key { get; set; }

        public virtual string Extention { get; set; }
    }
}
