using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TinyCMS.Models.Data;
using TinyCMS.Models.Data.DTO;
using TinyCMS.Models.ViewModels.Pages;

namespace TinyCMS.Areas.admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: admin/Pages
        public ActionResult Index()
        {
            //Declare list of Page View Model
            List<PageViewModel> pages;
            using (TinyCMSDB db = new TinyCMSDB())
            {
                pages = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x => new PageViewModel(x)).ToList();
            }

            return View(pages);
        }

        // GET: admin/Pages/addPage
        [HttpGet]
        public ActionResult addPage()
        {
            return View();
        }

        // POST: admin/Pages/addPage
        [HttpPost]
        public ActionResult addPage(PageViewModel pageVm)
        {
            if (!ModelState.IsValid) {
                return View(pageVm);
            }

            using (TinyCMSDB db = new TinyCMSDB())
            {
                String slug;
                Page page = new Page();
                page.Title = pageVm.Title;
                if (String.IsNullOrWhiteSpace(pageVm.Slug))
                {
                    slug = pageVm.Title.Replace(" ", "-").ToLower();
                }
                else
                {
                    slug = pageVm.Slug.Replace(" ", "-").ToLower();
                }

                if (db.Pages.Any(x => x.Title == pageVm.Title) || db.Pages.Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "This title or slug already exists.");
                    return View(pageVm);
                }

                page.Slug = slug;
                page.Body = pageVm.Body;
                page.HasSidebar = pageVm.HasSidebar;
                page.Sorting = 100;

                db.Pages.Add(page);
                db.SaveChanges();

                TempData["successMessage"] = "The new page has been added successfully.";

            }
            return RedirectToAction("Index");
        }

        // GET: admin/Pages/editPage/id
        public ActionResult editPage(int id)
        {
            PageViewModel pageVM;
            using (TinyCMSDB db = new TinyCMSDB())
            {
                Page page = db.Pages.Find(id);
                if(page == null)
                {
                    return Content("The page does not exist.");
                }
                else
                {
                    pageVM = new PageViewModel(page);
                }
            }
            return View(pageVM);
        }
        // POST: admin/Pages/editPage
        [HttpPost]
        public ActionResult editPage(PageViewModel pageVm)
        {
            if (!ModelState.IsValid)
            {
                return View(pageVm);
            }

            using (TinyCMSDB db = new TinyCMSDB())
            {
                int id = pageVm.Id;
                String slug = "home";
                Page page = db.Pages.Find(id);
                page.Title = pageVm.Title;
                if(pageVm.Slug != "home")
                {
                    if (String.IsNullOrWhiteSpace(pageVm.Slug))
                    {
                        slug = pageVm.Title.Replace(" ", "-").ToLower();
                    }
                    else
                    {
                        slug = pageVm.Slug.Replace(" ", "-").ToLower();
                    }
                }
                if (db.Pages.Where(x => x.Id != id).Any(x => x.Title == pageVm.Title) 
                    || db.Pages.Where(x => x.Id != id).Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "This title or slug already exists.");
                    return View(pageVm);
                }

                page.Slug = slug;
                page.Body = pageVm.Body;
                page.HasSidebar = pageVm.HasSidebar;

                db.SaveChanges();

                TempData["successMessage"] = "Page has been edited successfully.";

            }
            return View(pageVm);
        }

        // GET: admin/Pages/deletePage/id
        [HttpGet]
        public ActionResult deletePage(int id)
        {
            using(TinyCMSDB db = new TinyCMSDB())
            {
                Page page = db.Pages.Find(id);
                db.Pages.Remove(page);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: admin/Pages/viewDetails/id
        [HttpGet]
        public ActionResult viewDetails(int id)
        {
            PageViewModel pageVM;
            using (TinyCMSDB db = new TinyCMSDB())
            {
                pageVM = new PageViewModel(db.Pages.Find(id));
            }
            return View(pageVM);
        }
    }
}