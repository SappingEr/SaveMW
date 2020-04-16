namespace SaveMW.Models
{
    public class FSFile : File
    {
        public virtual Note Note { get; set; } 
        
        public virtual int Key { get; set; }

        public virtual string Extention { get; set; }
    }
}
