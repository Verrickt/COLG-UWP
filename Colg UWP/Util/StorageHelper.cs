using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Colg_UWP.Util
{
    using Windows.Storage;

    public class StorageHelper
    {
        private static readonly IStorageFolder _cacheFolder = ApplicationData.Current.LocalCacheFolder;

        private static readonly SemaphoreSlim _semaphone = new SemaphoreSlim(1);

        public static async Task SaveAsync<T>(T t, string name)
        {
            await _semaphone.WaitAsync().ConfigureAwait(false);
            var file = await _cacheFolder.CreateFileAsync(name, CreationCollisionOption.OpenIfExists).AsTask(false);
            await FileIO.WriteTextAsync(file, JsonConvert.SerializeObject(t)).AsTask(false);
            _semaphone.Release();
        }

        public static async Task<T> ReadAsync<T>(string name)
        {
            await _semaphone.WaitAsync().ConfigureAwait(false);
            var file = await _cacheFolder.CreateFileAsync(name, CreationCollisionOption.OpenIfExists).AsTask(false);
            T t = JsonConvert.DeserializeObject<T>(await FileIO.ReadTextAsync(file).AsTask(false));
            _semaphone.Release();
            return t;
        }
    }
}