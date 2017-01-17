
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Colg_UWP.Model
{
    public interface IIncrementalLoadable<T>
    {
        int MaxCount { get; set; }

        Func<Task<List<T>>> LoadMore { get; set; }

        void Refresh();
    }
}
