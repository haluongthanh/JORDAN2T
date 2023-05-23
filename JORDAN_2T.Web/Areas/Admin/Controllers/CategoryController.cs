using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using JORDAN_2T.Infrastructure.Data;
using JORDAN_2T.Infrastructure.Interfaces;
using JORDAN_2T.ApplicationCore.Models;
using JORDAN_2T.ApplicationCore.ViewModels;
using JORDAN_2T.ApplicationCore.ViewModels.Admin;
using JORDAN_2T.ApplicationCore.Utilities;
using JORDAN_2T.Web.Controllers;


namespace JORDAN_2T.Web.Areas.Admin.Controllers{
    [Area("Admin")]
    [Authorize(Roles = WebsiteRole.Admin)]
    public class CategoryController : BaseController
    {
        public CategoryController(IUnitOfWork uow) : base(uow)
        {
        }

         public IActionResult Index(string SearchString="",int pg=1)
        {
           
            CategoryVM vM= new CategoryVM();

            const int pageSize=8;
            if(pg<1)
                pg=1;
                

            var querry =_uow.Orders.GetAll(p=>p.Id!=null);
            int recsCount =querry.Count();

            var pager=new Pager(recsCount,pg,pageSize);

            int recSkip=(pg-1)*pageSize;
            
            this.ViewBag.pager=pager;
            vM.search=SearchString;

            vM.category=((CategoryRepository)_uow.Categorys).GetCategories(SearchString,pg);
           
            return View(vM);
        }
        
        public ActionResult Create()
        {
            CategoryVM categoryVM = new CategoryVM();
          
            return View(categoryVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( IFormCollection collection)
        {
            Category category = new Category();
            
            try
            {
                if (ModelState.IsValid)
                {
                    
                    category.Name = collection["Name"];
                    _uow.Categorys.Add(category);
                    _uow.Save();
                   
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
            }
            return View("Index");
        }
        
        public ActionResult Edit(int id, bool isPhoto = false)
        {
            // Get the movie along with its photo collection.
            Category category = ((CategoryRepository)_uow.Categorys).GetCategory(id);
            if (category == null)
            {
                category = new Category();
            }
            CategoryDetailsVM vm = new CategoryDetailsVM(category);
           
            return View("Details", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            Category category = new Category();
            
            try
            {
                if (ModelState.IsValid)
                {
                    category = ((CategoryRepository)_uow.Categorys).GetCategory(id);
                    category.Name = collection["Name"];
                    
                    _uow.Save();
                   
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
            }
            return View("Edit",new CategoryDetailsVM(category) );
        }
         public ActionResult Delete(int id){
            
            Category category=((CategoryRepository)_uow.Categorys).GetCategory(id);
            
                ((CategoryRepository)_uow.Categorys).Remove(category);
            
                _uow.Save();
                
            
            return RedirectToAction("Index");
            
        }
    }
}
