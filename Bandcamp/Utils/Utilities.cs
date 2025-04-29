using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bandcamp.Stores;
using Bandcamp.Models;
using Bandcamp.Services;
using System.Diagnostics;

namespace Bandcamp.Utils
{
    public class Utilities
    {

        private GlobalStore _GlobalStore;
        private StreamStore _StreamStore;
        private ApplicationService applicationService;
        public Utilities(GlobalStore globalstore, StreamStore streamstore, ApplicationService applicationservice)
        {
            _GlobalStore = globalstore;
            _StreamStore = streamstore;
            applicationService = applicationservice;
        }
        private int GetRandomIndex(int totalitems) {
            Random random = new Random();
            return random.Next(0, totalitems-1) ;
        }
        public ItemSong GetRandomItemSong()
        {
            Debug.WriteLine("ejecutando GetRandomItemSong");

            if (_GlobalStore.GetResponseIndexDiscoverPlayerList().results.Count() >= 5)
            {
                int IndexRandom = GetRandomIndex(_GlobalStore.GetResponseIndexDiscoverPlayerList().results.Count());
                ItemSong itemSong = _GlobalStore.GetResponseIndexDiscoverPlayerList().results[IndexRandom];

                if (itemSong.result_type == "s")
                {
                    return GetRandomItemSong();
                }
                else {
                    _StreamStore.IndexItemSongPlayList = IndexRandom;
                    return itemSong;
                }
            }
            else { 
                return new ItemSong();
            }
            
        }
        public ItemSong GetPrevItemSong()
        {
            Debug.WriteLine("ejecutando GetPrevItemSong");
            if (_StreamStore.IndexItemSongPlayList - 1 < 0)
            {
                return new ItemSong();
            }

            ItemSong itemSong = _GlobalStore.GetResponseIndexDiscoverPlayerList().results[_StreamStore.IndexItemSongPlayList-1];
            _StreamStore.IndexItemSongPlayList -= 1;

            if (itemSong.result_type == "s")
            {
                return GetPrevItemSong();
            }

            return itemSong;
        }

        public ItemSong GetNextItemSong()
        {
            Debug.WriteLine("ejecutando GetNextItemSong");
            if (_StreamStore.IndexItemSongPlayList + 1 > (_GlobalStore.GetResponseIndexDiscoverPlayerList().results.Count()-1))
            {
                return new ItemSong();
            }

            ItemSong itemSong = _GlobalStore.GetResponseIndexDiscoverPlayerList().results[_StreamStore.IndexItemSongPlayList+1];
            _StreamStore.IndexItemSongPlayList += 1;

            if (itemSong.result_type == "s")
            {
                return GetNextItemSong();
            }

            return itemSong;
        }

        public void SetNextItemPlayList(bool RepeatStream = false, bool shuffleStream = false)
        {
            Debug.WriteLine("ejecutando SetNextItemPlayList"+shuffleStream);
            if (_GlobalStore.GetResponseIndexDiscoverPlayerList().results.Count() == 0)
            {
                return;
            }

            // si el total de pistas es mayor o igual que contador de indices mas 1
            ItemSong itemSong = shuffleStream ? GetRandomItemSong() : GetNextItemSong();
            
            if (itemSong.id == 0)
            {
                _StreamStore.IndexItemSongPlayList = 0;

                if (RepeatStream) {
                    SetNextItemPlayList(RepeatStream,shuffleStream);
                }
            }
            else { 
                _StreamStore.SetItemSongPlayList(itemSong);
            }
        }

        public void SetPrevItemPlayList()
        {
            // sino hay elementos
            if (_GlobalStore.GetResponseIndexDiscoverPlayerList().results.Count() == 0)
            {
                return;
            }
            // si el total de pistas es mayor o igual que contador de indices mas 1
            ItemSong itemSong = GetPrevItemSong();
        
            if (itemSong.id == 0)
            {
                _StreamStore.IndexItemSongPlayList = 0;
                _StreamStore.SetItemSongPlayList(_GlobalStore.GetResponseIndexDiscoverPlayerList().results[0]);
            }
            else
            {
                _StreamStore.SetItemSongPlayList(itemSong);
            }
        }

        public void StartPlayList(int startIndex = 0) {
            if (_GlobalStore.GetResponseIndexDiscoverPlayerList().results.Count() >= 3) {
                _StreamStore.IndexItemSongPlayList = startIndex;
                ItemSong itemSong = _GlobalStore.GetResponseIndexDiscoverPlayerList().results[startIndex];

                if (itemSong.result_type == "s")
                {
                    StartPlayList(startIndex+1);
                }
                else {
                    _StreamStore.SetItemSongPlayList(itemSong);
                }

            }
        }

        public void SetItemSongCustom(ItemSong itemsong)
        {
            int index = _GlobalStore.GetResponseIndexDiscoverPlayerList().results.FindIndex(item => item.id == itemsong.id);
            StartPlayList(index);            
        }

        public string ConvertSecondsStandar(double seconds = 0.0)
        {
            int minutos = (int)(seconds / 60);
            int segundos = (int)(seconds % 60);

            string TextMinutos = minutos < 10 ? $"0{minutos}" : minutos.ToString();
            string TextSegundos = segundos < 10 ? $"0{segundos}" : segundos.ToString();

            return $"{TextMinutos}:{TextSegundos}";
        }
    }
}
