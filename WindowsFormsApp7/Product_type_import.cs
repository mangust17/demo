//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApp7
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product_type_import
    {
        public Product_type_import()
        {
            this.Products_import = new HashSet<Products_import>();
        }
    
        public byte id { get; set; }
        public string prod_type { get; set; }
        public decimal prode_type_coef { get; set; }
    
        public virtual ICollection<Products_import> Products_import { get; set; }
    }
}
