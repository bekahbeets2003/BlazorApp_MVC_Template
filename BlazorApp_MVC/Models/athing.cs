namespace BlazorApp_MVC.Models
{
    //change this to book once db schema fully in place
    public class athing
    {
        public int file_id { get; set; }
        public int user_id { get; set; }
        public int vendor_id { get; set; }
        public string upload_datetime { get; set; } = string.Empty;
        public string network_file_location { get; set; } = string.Empty;
        public string temp_guid { get; set; } = string.Empty;
    }
}
