using Colg_UWP.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace Colg_UWP.ViewModel
{
    public class SettingVM : VMBase
    {
        public string CacheSize
        {
            get { return _cacheSize; }
            set { SetProperty(ref _cacheSize, value); }
        }

        private StorageFolder temporaryFolder;

        private string _cacheSize;

        public RelayCommand ClearCacheCommand { get; set; }

        private bool _isProcessing;

        public bool IsProcessing
        {
            get { return _isProcessing; }
            set { SetProperty(ref _isProcessing, value); }
        }

        public SettingVM()
        {
            temporaryFolder = ApplicationData.Current.TemporaryFolder;
            ClearCacheCommand = new RelayCommand(
                async () =>
                {
                    IsProcessing = true;
                    var items = await GetFilesAsync();
                    var deleteTasks = items.Select(async x => await x.DeleteAsync());
                    await Task.WhenAll(deleteTasks);
                    await UpdateCacheSize();
                    IsProcessing = false;
                });
        }

        public async Task UpdateCacheSize()
        {
            CacheSize = "计算中......";

            var files = await GetFilesAsync();

            var fileTasks = files.Select(async x => (await x.GetBasicPropertiesAsync()).Size);

            var sizes = await Task.WhenAll(fileTasks);

            var size = sizes.Sum(l => (double)l);

            CacheSize = $"{(size / Math.Pow(1024, 2)).ToString("F1")}MB";
        }

        private async Task<IEnumerable<StorageFile>> GetFilesAsync()
        {
            var option = new QueryOptions() { FolderDepth = FolderDepth.Deep };

            var query = temporaryFolder.CreateFileQueryWithOptions(option);

            return await query.GetFilesAsync();
        }
    }
}