using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace uchet1.Models
{
    public class UserRoles
    {

        [Display(Name ="ID пользователя")]
        public string id_user { get; set; }
        [Display(Name ="ФИО пользователя")]
        public string user_fio { get; set; }
        [Display(Name ="ID роли")]
        public string id_role { get; set; }
        [Display(Name ="Назв. роли")]
        public string role_name { get; set; }

        public List<UserRoles> UserRoleList { get; set; }
    }
}