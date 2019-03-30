using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.Dtos.Export
{
    [XmlType("Users")]
    public class UserProductsInfoDto
    {
        [XmlElement("count")]
        public int Count { get; set; }
            
        [XmlArray("users")]
        public List<UsersDto> Users { get; set; }
    }
}
