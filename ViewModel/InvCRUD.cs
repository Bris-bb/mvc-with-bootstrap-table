using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AT.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AT.ViewModel
{
    public class InvCRUD
    {
        public inv createInv { get; set; }
        public List<inv> readInv { get; set; }
    }
}