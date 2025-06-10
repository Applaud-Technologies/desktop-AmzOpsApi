using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonOperations.Common
{
    public class SaveResult<T>
    {
        public List<T> Inserted { get; } = new();
        public List<T> Updated { get; } = new();
        public List<T> Deleted { get; } = new();
    }
}
