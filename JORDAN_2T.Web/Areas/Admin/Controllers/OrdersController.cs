using JORDAN_2T.Infrastructure.Interfaces;
using JORDAN_2T.ApplicationCore.Models;
using JORDAN_2T.ApplicationCore.ViewModels;
using JORDAN_2T.ApplicationCore.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc.Rendering;
using JORDAN_2T.Infrastructure.Data;
using JORDAN_2T.ApplicationCore.ViewModels.Admin;
using JORDAN_2T.Web.Controllers;


namespace JORDAN_2T.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// Controller for administrative functions. Enable authorization on this controller to restrict who can modify website content.
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = WebsiteRole.Admin)]
    public class OrdersController : BaseController
    {
        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uow"></param>
        public OrderHistoryVM orderHistoryVM {get;set;}
        public OrdersController(IUnitOfWork uow) : base(uow)
        {
        }
        #endregion
        public IActionResult Index(int pg =1)
        {

            const int pageSize=8;
            if(pg<1)
                pg=1;
                

            var querry =_uow.Orders.GetAll(p=>p.Id!=null);
            int recsCount =querry.Count();

            var pager=new Pager(recsCount,pg,pageSize);

            int recSkip=(pg-1)*pageSize;
            
            this.ViewBag.pager=pager;
            
            orderHistoryVM = new OrderHistoryVM()
            {
                Order = ((OrderRepository)_uow.Orders).GetOrders(pg)
                
            };
            
         
            return View(orderHistoryVM);
        }
       
        
    }
    
}
