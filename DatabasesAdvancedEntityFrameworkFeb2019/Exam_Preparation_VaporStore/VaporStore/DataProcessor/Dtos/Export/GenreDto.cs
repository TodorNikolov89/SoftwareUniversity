using System;
using System.Collections.Generic;
using System.Text;

namespace VaporStore.DataProcessor.Dtos.Export
{
    public class GenreDto
    {
        public int Id { get; set; }

        public string Genre { get; set; }

        public List<GameExportDto> Games { get; set; }

        public int TotalPlayers { get; set; }
    }
}
