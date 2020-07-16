using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCheck.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public bool Active { get; set; }
        public DateTime DateOfLaunch { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool FreeDelivery { get; set; }

    }
}
