using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Domain.ViewModels
{
    public class SelectableSectionsViewModel : IEnumerable
    {
        public IEnumerable<SectionViewModel> Sections { get; set; }

        public int? SectionId { get; set; }

        public int? ParentSectionId { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
