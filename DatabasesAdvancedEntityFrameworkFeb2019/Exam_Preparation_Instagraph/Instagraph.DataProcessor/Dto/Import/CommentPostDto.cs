using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Instagraph.DataProcessor.Dto.Import
{
    [XmlType("post")]
    public class CommentPostDto
    {
        [Required]
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
