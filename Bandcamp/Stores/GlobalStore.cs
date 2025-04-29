using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bandcamp.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace Bandcamp.Stores
{
    public class GlobalStore
    {
        public event Action onChangeIndexDiscover;
        public event Action onChangeIndexDiscoverPlayerList;
        private ResponseIndexDiscover _ResponseIndexDiscover { get; set; } = new ResponseIndexDiscover();
        private ResponseIndexDiscover _ResponseIndexDiscoverPlayerList { get; set; } = new ResponseIndexDiscover();
        public List<string> ListGenres { get; set; } = [];
        public List<Genre> GenresList { get; set; } = new List<Genre>{
             new Genre { Id = 0, Label = "new", Slug = "new" },
             new Genre { Id = 1, Label = "acoustic", Slug = "acoustic" },
             new Genre { Id = 2, Label = "alternative", Slug = "alternative" },
             new Genre { Id = 3, Label = "ambient", Slug = "ambient" },
             new Genre { Id = 4, Label = "blues", Slug = "blues" },
             new Genre { Id = 5, Label = "classical", Slug = "classical" },
             new Genre { Id = 6, Label = "comedy", Slug = "comedy" },
             new Genre { Id = 7, Label = "country", Slug = "country" },
             new Genre { Id = 9, Label = "devotional", Slug = "devotional" },
             new Genre { Id = 10, Label = "electronic", Slug = "electronic" },
             new Genre { Id = 11, Label = "experimental", Slug = "experimental" },
             new Genre { Id = 12, Label = "folk", Slug = "folk" },
             new Genre { Id = 13, Label = "funk", Slug = "funk" },
             new Genre { Id = 14, Label = "hip-hop/rap", Slug = "hip-hop-rap" },
             new Genre { Id = 15, Label = "jazz", Slug = "jazz" },
             new Genre { Id = 16, Label = "kids", Slug = "kids" },
             new Genre { Id = 17, Label = "latin", Slug = "latin" },
             new Genre { Id = 18, Label = "metal", Slug = "metal" },
             new Genre { Id = 19, Label = "pop", Slug = "pop" },
             new Genre { Id = 20, Label = "punk", Slug = "punk" },
             new Genre { Id = 21, Label = "r&b/soul", Slug = "r-b-soul" },
             new Genre { Id = 22, Label = "reggae", Slug = "reggae" },
             new Genre { Id = 23, Label = "rock", Slug = "rock" },
             new Genre { Id = 24, Label = "soundtrack", Slug = "soundtrack" },
             new Genre { Id = 25, Label = "spoken word", Slug = "spoken-word" },
             new Genre { Id = 26, Label = "world", Slug = "world" },
             new Genre { Id = 27, Label = "podcasts", Slug = "podcasts" },
             new Genre { Id = 28, Label = "audiobooks", Slug = "audiobooks" }
        };
        public ResponseIndexDiscover GetResponseIndexDiscover() => _ResponseIndexDiscover;
        public ResponseIndexDiscover GetResponseIndexDiscoverPlayerList() => _ResponseIndexDiscoverPlayerList;

        public void AddRangeResultDiscover(ResponseIndexDiscover _responseIndexDiscover) { 
            _ResponseIndexDiscover.results.AddRange(_responseIndexDiscover.results);
            _ResponseIndexDiscover.result_count = _responseIndexDiscover.result_count;
            _ResponseIndexDiscover.cursor = _responseIndexDiscover.cursor;
            _ResponseIndexDiscover.discover_spec_id = _responseIndexDiscover.discover_spec_id;
            _ResponseIndexDiscover.batch_result_count = _responseIndexDiscover.batch_result_count;
            NotifyStateChangeIndexDiscover();
        }

        public void AddRangeResultDiscoverPlayerList(ResponseIndexDiscover _responseIndexDiscover)
        {
            _ResponseIndexDiscoverPlayerList.results.AddRange(_responseIndexDiscover.results);
            _ResponseIndexDiscoverPlayerList.result_count = _responseIndexDiscover.result_count;
            _ResponseIndexDiscoverPlayerList.cursor = _responseIndexDiscover.cursor;
            _ResponseIndexDiscoverPlayerList.discover_spec_id = _responseIndexDiscover.discover_spec_id;
            _ResponseIndexDiscoverPlayerList.batch_result_count = _responseIndexDiscover.batch_result_count;
            NotifyStateChangeIndexDiscoverPlayerList();
        }

        public void NotifyStateChangeIndexDiscover() {
            onChangeIndexDiscover?.Invoke();
        }

        public void NotifyStateChangeIndexDiscoverPlayerList()
        {
            onChangeIndexDiscoverPlayerList?.Invoke();
        }

    }
}
