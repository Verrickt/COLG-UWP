using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Colg_UWP.Helper
{
    using Windows.Storage;
    public class StorageHelper
    {
        private static readonly IStorageFolder _cacheFolder = ApplicationData.Current.LocalCacheFolder;

        private static readonly SemaphoreSlim _semaphone = new SemaphoreSlim(1);

        public static async Task Save<T>(T t,string name)
        {
            await _semaphone.WaitAsync().ConfigureAwait(false);
            var file = await _cacheFolder.CreateFileAsync(name, CreationCollisionOption.OpenIfExists).AsTask(false);
            await FileIO.WriteTextAsync(file, JsonConvert.SerializeObject(t)).AsTask(false);  
            _semaphone.Release();
        }

        public static async Task<T> Read<T>(string name)
        {
            await _semaphone.WaitAsync().ConfigureAwait(false);
            var file = await _cacheFolder.CreateFileAsync(name, CreationCollisionOption.OpenIfExists).AsTask(false);
            T t = JsonConvert.DeserializeObject<T>(await FileIO.ReadTextAsync(file).AsTask(false));
            _semaphone.Release();
            return t;
        }

    }

    public class StorageNames
    {
        public const string UserData = "UserData";
        public const string LoginData = "LoginData";
    }
}
