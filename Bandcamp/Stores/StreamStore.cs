using Bandcamp.Models;
using Bandcamp.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bandcamp.Stores
{
    public class StreamStore
    {
        public event Action onAbortAudioStream;
        public event Action onChangeAudioStream;
        public event Action onChangeItemSong;
        public event Action onChangeLengthStream;
        public event Action onChangeProgressStream;
        private ItemSong _ItemSong { get; set; } = new ItemSong();
        public MemoryStream AudioStreamContainer { get; set; } = new MemoryStream();
        public long TotalLengthStream { get; set; } = 0;
        public int ProgressStream { get; set; } = 0;
        public bool RunningPlayList { get; set; } = false;
        public int IndexItemSongPlayList { get; set; } = 0;
        public List<ItemSong> HistoryPlayList { get; set; } = [];
        public ItemSong GetItemSong() => _ItemSong;
        public void ClearStreamVariables() {
            AudioStreamContainer = new MemoryStream();
            _ItemSong = new ItemSong();
            TotalLengthStream = 0;
            ProgressStream = 0;
            RunningPlayList = false;
            NotifyStateChangeProgressStream();

        }
        public void SetItemSong(ItemSong itemsong) {
            AudioStreamContainer = new MemoryStream();
            _ItemSong = itemsong;
            TotalLengthStream = 0;
            ProgressStream = 0;
            NotifyStateChangeItemSong();
        }
        public void SetItemSongPlayList(ItemSong itemsong)
        {
            if (!RunningPlayList) {
                RunningPlayList = true;
            }
            AudioStreamContainer = new MemoryStream();
            _ItemSong = itemsong;
            TotalLengthStream = 0;
            NotifyStateChangeItemSong();
        }
        public void ClearAudioStream() {
            AudioStreamContainer = new MemoryStream();
        }
        public void SetLengthStream(long lengthstream) { 
            TotalLengthStream = lengthstream;
            NotifyStateChangeLengthStream();
        }
        public void SetProgressStream(int progress) {
            if (progress != ProgressStream) { 
                ProgressStream = progress;
                NotifyStateChangeProgressStream();
                Debug.WriteLine("seteando progreso en store: "+ progress);
            }

        }
        public void NotifyStateChangeStream()
        {
            onChangeAudioStream?.Invoke();
        }
        public void NotifyStateChangeItemSong()
        {
            onChangeItemSong?.Invoke();
        }
        public void NotifyStateChangeLengthStream() {
            onChangeLengthStream?.Invoke();
        }
        public void NotifyStateChangeProgressStream()
        {
            onChangeProgressStream?.Invoke();
        }
        public void NotifyChangeAbortStream()
        {
            onAbortAudioStream?.Invoke();
        }
        public void SetRunningPlayList() {
            RunningPlayList = true;
        }
                        
    }
}
