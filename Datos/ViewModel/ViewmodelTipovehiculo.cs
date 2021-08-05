using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.ViewModel
{
   public class ViewmodelTipovehiculo
    {
        public int CodigoVehiculo { get; set; }
        public string DescripcionVehiculo { get; set; }
        public int Codigo { get; set; }
        public string Nombres { get; set; }
        public int Estado { get; set; }

        public object Tolist()
        {
            throw new NotImplementedException();
        }
    }
}

