using Diabetes.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diabetes.Web.Logic
{
    public static class Constants
    {
        public const string ApiRoute = "api/[controller]";

        public const EntityModificationType Deleted = EntityModificationType.Deleted;

        public const EntityModificationType Updated = EntityModificationType.Updated;
    }
}
