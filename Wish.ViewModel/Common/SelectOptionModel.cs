using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wish.ViewModel.Common
{
    [Serializable]
    public class ListResultDto<T>
    {
        /// <inheritdoc />
        public IReadOnlyList<T> items
        {
            get { return _items ?? (_items = new List<T>()); }
            set { _items = value; }
        }
        private IReadOnlyList<T> _items;

        /// <summary>
        /// Creates a new <see cref="ListResultDto{T}"/> object.
        /// </summary>
        public ListResultDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="ListResultDto{T}"/> object.
        /// </summary>
        /// <param name="items">List of items</param>
        public ListResultDto(IReadOnlyList<T> _items)
        {
            items = _items;
        }
    }

    public class SelectOptionModel
    {
        /// <summary>
        /// 绑定值
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 显示值
        /// </summary>
        public string Text { get; set; }

    }

    /// <summary>
    /// Implements <see cref="IPagedResult{T}"/>.
    /// </summary>
    /// <typeparam name="T">Type of the items in the <see cref="ListResultDto{T}.Items"/> list</typeparam>
    [Serializable]
    public class PagedResultDto<T> : ListResultDto<T>
    {
        /// <inheritdoc />
        public long total { get; set; } = 0; //TODO: Can be a long value..?

        /// <summary>
        /// Creates a new <see cref="PagedResultDto{T}"/> object.
        /// </summary>
        public PagedResultDto()
        {

        }

        /// <summary>
        /// Creates a new <see cref="PagedResultDto{T}"/> object.
        /// </summary>
        /// <param name="totalCount">Total count of Items</param>
        /// <param name="items">List of items in current page</param>
        public PagedResultDto(long totalCount, IReadOnlyList<T> _items)
            : base(_items)
        {
            total = totalCount;
        }
    }
}
