using Assignment.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.ViewModels
{
    public class SearchBooksViewModel
    {
        [Required]
        [DisplayName("Serach")]
        public string SearchText { get; set; }

        //public IEnumerable<string> SearchListExamples { get; set; }

        public IEnumerable<Book> BookList { get; set; }

    }
}
