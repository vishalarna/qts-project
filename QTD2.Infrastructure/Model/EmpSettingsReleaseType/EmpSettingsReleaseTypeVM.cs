using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTD2.Infrastructure.Model.EmpSettingsReleaseType
{
    public class EmpSettingsReleaseTypeVM
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }

        public EmpSettingsReleaseTypeVM() { }
        public EmpSettingsReleaseTypeVM(int typeId, string typeName)
        {
            TypeId = typeId;
            TypeName = typeName;
        }
    }
}
