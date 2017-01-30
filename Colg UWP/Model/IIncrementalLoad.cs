
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Colg_UWP.Model
{
    public interface IIncrementalLoad<T>
    {
        int MaxCount { get;  }

        Func<Task<(int,List<T>)>> LoadMore { get; set; }

        void Refresh();
    }
}
