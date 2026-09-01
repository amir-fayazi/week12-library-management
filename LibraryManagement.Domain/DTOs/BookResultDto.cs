using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.DTOs
{
    public class BookResultDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string CatergoryName { get; set; }
    }
}
