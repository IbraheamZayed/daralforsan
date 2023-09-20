namespace Models
{
    public class Branch
    {
        public long BranchID { get; set; }
        public string LocalName { get; set; }
        public string LatinName { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}