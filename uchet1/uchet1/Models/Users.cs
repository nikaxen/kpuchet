using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace uchet1.Models
{
    public class Users
    {
        [Display(Name ="ID")]
        public string Id { get; set; }
        [Display(Name ="EMail")]
        public string Email { get; set; }
        [Display(Name ="ФИО")]
        public string fio { get; set; }
        [Display(Name = "Дата рождения")]
        public DateTime birthdate { get; set; }
        [Display(Name = "ID группы студ.")]
        public int studgroupid { get; set; }
        [Display(Name = "Группа студ.")]
        public string studgroupname { get; set; }
        public List<Users> UsersList { get; set; }
    }
}