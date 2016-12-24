using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace uchet1.Models
{
    public class Statement
    {
        [Display(Name ="ID вед.")]
        public int id_statement { get; set; }
        [Display(Name ="ID предм.")]
        public int id_subject { get; set; }
        [Display(Name ="Преподаватель")]
        public string prepname { get; set; }
        [Display(Name = "Предмет")]
        public string subjtitle { get; set; }
        [Display(Name ="ID препод.")]
        public String id_user { get; set; }
        [Display(Name ="Статус")]
        public string status { get; set; }
        [Display(Name ="Название")]
        public string title { get; set; }
        [Display(Name = "ID группы")]
        public int idstudgroup { get; set; }
        [Display(Name = "Группа студ.")]
        public string studgroupname { get; set; }
    }
}