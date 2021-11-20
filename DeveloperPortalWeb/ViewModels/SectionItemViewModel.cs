namespace InContact.DeveloperPortal.Web.ViewModels
{
    public class SectionItemViewModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public Reference Reference { get; set; }
    }

	public class Reference
	{
		public string Title { get; set; }
		public string Date { get; set; }
	}
}