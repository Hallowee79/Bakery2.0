using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bakery.DB;

namespace Bakery.ClassHelper
{
    public class EFClass
    {
       public static DB.BakeryGribovPolypanEntities Context { get; } = new DB.BakeryGribovPolypanEntities();

    }
}

/////Подключение бд
