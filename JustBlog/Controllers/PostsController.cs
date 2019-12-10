using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JustBlog.Models;
using PagedList;

namespace JustBlog.Controllers
{
    public class PostsController : Controller
    {
        private PostContext db = new PostContext();

        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "PostDate" ? "date_desc" : "PostDate";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var posts1 = from s in db.Posts
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                posts1 = posts1.Where(s => s.Name.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "name_desc":
                    posts1 = posts1.OrderByDescending(s => s.Name);
                    break;
                case "PostDate":
                    posts1 = posts1.OrderBy(s => s.PostDate);
                    break;
                case "date_desc":
                    posts1 = posts1.OrderByDescending(s => s.PostDate);
                    break;
                default:
                    posts1 = posts1.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var posts = db.Posts.Include(p => p.Cat);
            return View(posts1.ToPagedList(pageNumber, pageSize));
        }
        // GET: Posts1/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts1/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Categories, "Id", "Categorys");
            return View();
        }

        // POST: Posts1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Short_Description,Description,Hero_image,PostDate")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Categories, "Id", "Categorys", post.Id);
            return View(post);
        }

        // GET: Posts1/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Categories, "Id", "Categorys", post.Id);
            return View(post);
        }

        // POST: Posts1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Short_Description,Description,Hero_image,PostDate")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Categories, "Id", "Categorys", post.Id);
            return View(post);
        }

        // GET: Posts1/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await db.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Post post = await db.Posts.FindAsync(id);
            db.Posts.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
