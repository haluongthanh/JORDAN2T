using Microsoft.AspNetCore.Mvc.Rendering;
using JORDAN_2T.ApplicationCore.Models;
using System.Collections.Generic;

namespace JORDAN_2T.ApplicationCore.ViewModels.Admin;

    public class CategoryDetailsVM
    {
        private Category category1;

        public CategoryDetailsVM(Category category)
        {
            category1 = category;
           
        }
        public int Id
        {
            get
            {
                return (int)category1.Id;
            }
        }

        public string Name
        {
            get { return category1.Name; }
            set { category1.Name = value; }
        }

    }
