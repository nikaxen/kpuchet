using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace uchet1.Models
{
    public class Mark
    {
        [Display(Name ="ID оценки")]
        public int id_mark { get; set; }
        [Display(Name ="ID вед.")]
        public int id_statement { get; set; }
        [Display(Name ="Название вед.")]
        public string statement_title { get; set; }
        [Display(Name = "ID предм.")]
        public int subject_id { get; set; }
        [Display(Name ="Название предм.")]
        public string subject_title { get; set; }
        [Display(Name ="ID студ.")]
        public string id_user { get; set; }
        [Display(Name ="Оценка")]
        public string themark { get; set; }
        [Display(Name = "ФИО студ.")]
        public string user_fio { get; set; }
        public List<Mark> MarkList { get; set; }
       
    }
}