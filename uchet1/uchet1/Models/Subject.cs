using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace uchet1.Models
{
    public class Subject
    {
        [Display(Name ="ID")]
        public int id_subject { get; set; }
        [Display(Name ="Название предмета")]
        public String subject_title { get; set; }
        public List<Subject> SubjectList { get; set; }
    }
}