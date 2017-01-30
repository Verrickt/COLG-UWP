//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************

using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Colg_UWP.Model;

namespace Colg_UWP.IncrementalLoading
{
    // This class implements IncrementalLoadingBase. 
    // To create your own Infinite List, you can create a class like this one that doesn't have 'generator' or 'maxcount', 
    //  and instead downloads items from a live data source in LoadMoreItemsOverrideAsync.
    public class IncrementalList<T1,T2> : IncrementalLoadingBase<T1>
        where T2:IIncrementalLoad<T1>
    {


        public Func<Task<(int, List<T1>)>> _loadMore;

        

        public IncrementalList(T2 source)
        {
            this._maxCount = source.MaxCount;
            this._loadMore = source.LoadMore;
        }

        private bool _isLoading { get; set; }

        public bool IsLoading { get { return _isLoading;} set {
            if (value!=_isLoading)
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        } }
        

        protected async override Task<IList<T1>> LoadMoreItemsOverrideAsync(System.Threading.CancellationToken c, int count)
        {
            IsLoading = true;

            int toGenerate = System.Math.Min(count, _maxCount - _count);

            // Wait for work 
            await Task.Delay(10);

            // This code simply generates
            try
            {
                (int newMaxCount,var items) = await _loadMore();
                _count += items.Count;
                this._maxCount = newMaxCount;
                return items;
            }
            catch (Exception)
            {
                return new List<T1>();
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected override bool HasMoreItemsOverride()
        {
            return _count < _maxCount;
        }

        #region State


        int _count = 0;
        int _maxCount;

        #endregion 
    }
}
