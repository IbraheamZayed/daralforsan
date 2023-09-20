using Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DarAlforsan.Areas.Basic.Controllers
{
    [Authorize]
    [Area("Basic")]
    public class SocialMediaController : Core.UI.BaseController
    {
        public SocialMediaController(IConfiguration Config) : base(Config)
        {
            
        }
        //-----------------------------------------------------------------------------------------
        public IActionResult Index() 
        { 
            return View();
        }
        //-----------------------------------------------------------------------------------------
        public ActionResult GetList()
        {
            var service = new Services.SocialMedia();

            #region JQuery posted Data Table params

            //current page index
            int start = Convert.ToInt32(Request.Form["start"]);
            //page size
            int length = Convert.ToInt32(Request.Form["length"]);
            //search value
            string searchValue = Request.Form["search[value]"];

            //order column index
            string index = Request.Form["order[0][column]"];

            //order column name (data field name)
            string colName = Request.Form["columns[" + index + "][data]"];
            //order direction 
            string dir = Request.Form["order[0][dir]"];

            #endregion

            var plist = service.GetList(searchValue, start / length, length, colName, dir);

            #region IQuery datat table return params

            string pdraw = Request.Form["draw"];

            #endregion

            #region Ordering

            //use(system.linq.dynamic nuget package) dynamic linq to sort and page data
            //Order  ascending
            if (Core.Data.OrderDirection.Asc == dir)
            {
                plist.Data = plist.Data.OrderBy(x => x.GetType().GetProperty(colName).GetValue(x, null)).ToList<ViewModels.Form.SocialMedia>();
            }
            else //Order Descending
            {
                plist.Data = plist.Data.OrderByDescending(x => x.GetType().GetProperty(colName).GetValue(x, null)).ToList<ViewModels.Form.SocialMedia>();
            }

            #endregion Ordering

            var result = JsonSerializer.Serialize<object>(new { data = plist.Data, draw = pdraw, recordsTotal = plist.TotalRecords, recordsFiltered = plist.FilteredRecords });

            return Ok(result);
        }
        //-----------------------------------------------------------------------------------------
        public IActionResult New()
        {
            return PartialView("_NewOrEdit", new ViewModels.Form.SocialMedia());
        }
        //-----------------------------------------------------------------------------------------
        [HttpPost]
        public IActionResult Save(ViewModels.Form.SocialMedia model)
        {
            try
            {
                var service = new Services.SocialMedia();
                if (model.ID > 0)
                    return Ok(service.Update(model));
                else
                {
                    var result = service.Add(model);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return Ok(ExceptionHandler.GlobalExceptionResult(ex));
            }
        }
        //-----------------------------------------------------------------------------------------
        public IActionResult Edit(long ID)
        {
            var service = new Services.SocialMedia();
            return PartialView("_NewOrEdit", service.InitEdit(ID));
        }
        //-----------------------------------------------------------------------------------------
        [HttpPost]
        public EXResult Remove(Core.Data.PARAM Param)
        {
            var service = new Services.SocialMedia();
            return service.Delete(Convert.ToInt64(Param.Value));
        }
        //-----------------------------------------------------------------------------------------
    }
}