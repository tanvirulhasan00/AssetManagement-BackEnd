using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AssetManagement.Models.Request.Generic
{
    public class GenericRequest<T>
    {
        public Expression<Func<T, bool>>? Expression { get; set; } = null;
        public string? IncludeProperties { get; set; } = null;
        public bool NoTracking { get; set; } = false;
        public CancellationToken CancellationToken { get; set; } = CancellationToken.None;
    }
}