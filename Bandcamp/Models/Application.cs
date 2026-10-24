using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bandcamp.Models
{
    public class ResponseIndexDiscover {

        public int result_count { get; set; } = 0;
        public int batch_result_count { get; set; } = 0;

        public string cursor { get; set; } = "*";

        public int discover_spec_id { get; set; } = 0;

        public List<ItemSong> results { get; set; } = new List<ItemSong>();
    }
    
    public class FeaturedTrack
    {
        public long id { get; set; } = 0;
        public string title { get; set; } = "";
        public string band_name { get; set; } = "";
        public long band_id { get; set; } = 0;
        public string stream_url { get; set; } = "";
        public double duration { get; set; } = 0;
    }

    public class PackageInfo
    {
        public long id { get; set; } = 0;
        public string title { get; set; } = "";
        public string format { get; set; } = "";
        public bool is_digital_included { get; set; } = false;
        public bool is_set_price { get; set; } = false;
        public bool is_preorder { get; set; } = false;
        public int image_id { get; set; } = 0;
        public Price price { get; set; } = new Price();
        public int type_id { get; set; } = 0;
    }

    public class Price
    {
        public int amount { get; set; } = 0;
        public string currency { get; set; } = "";
        public bool is_money { get; set; } = false;
    }

    public class ItemSong
    {
        public long item_id { get; set; } = 0;
        public string item_type { get; set; } = "";
        public string result_type { get; set; } = "";
        public string title { get; set; } = "";
        public string item_url { get; set; } = "";
        public PrimaryImage primary_image {  get; set; } = new PrimaryImage();
        public object addl_images { get; set; } = new object();
        public long band_id { get; set; } = 0;
        public object album_artist { get; set; } = new object();
        public string band_name { get; set; } = "";
        public string band_url { get; set; } = "";
        public bool is_free_download { get; set; } = false;
        public bool is_set_price { get; set; } = false;
        public Price price { get; set; } = new Price();
        public int band_genre_id { get; set; } = 0;
        public string band_location { get; set; } = "";
        public FeaturedTrack featured_track { get; set; }= new FeaturedTrack();
        public bool is_album_preorder { get; set; } = false;
        public string release_date { get; set; } = "";
        public int track_count { get; set; } = 0;
        public double duration { get; set; } = 0;
        public object is_wishlisted { get; set; } = new object();
        public object wishlisted { get; set; } = new object();
        public List<PackageInfo> package_info { get; set; } = new List<PackageInfo>();
        public PrimaryImage band_image { get; set; } = new PrimaryImage(); 
        public object is_following_band { get; set; } = new object();

        //public bool is_purchasable { get; set; } = false;
        //public double item_price { get; set; } = 0;
        //public string item_currency { get; set; } = "";
        //public long item_image_id { get; set; } = 0;
        //public string result_type { get; set; } = "";
        //public long band_bio_image_id { get; set; } = 0;
        //public long band_latest_art_id { get; set; } =0;
        //public int total_package_count { get; set; } = 0;
        //public object item_tags { get; set; } = new object();
    }

    public class Genre 
    {
        public int Id { get; set; } = 0;
        public string Label { get; set; } = "";
        public string Slug { get; set; } = "";
    }

    public class PrimaryImage { 
    
        public long image_id { get; set; }
        public Boolean is_art { get; set; }
    }
}
    