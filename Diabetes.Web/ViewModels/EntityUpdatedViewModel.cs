using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetes.Web.ViewModels
{
    public class EntityUpdatedViewModel<T>
    {
        public T Data { get; set; }

        /// <summary>
        ///     The type of action that has been performed on the entity, either UPDATED or DELETED
        /// </summary>
        public EntityModificationType Type { get; set; }
    }

    public enum EntityModificationType
    {
        Updated,
        Deleted
    }
}
