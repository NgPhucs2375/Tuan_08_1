using Bai3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bai3.ViewModel
{
    public class ListPB
    {
        public IEnumerable<Deparment> Deparments { get; set; }
        public int? SelectedDeptId { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
    }
}