﻿using Blogifier.Core.Common;
using Blogifier.Core.Data.Interfaces;
using Blogifier.Core.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blogifier.Core.Controllers
{
    [Route("author")]
	public class AuthorController : Controller
	{
		IUnitOfWork _db;
        ILogger _logger;
        private readonly string _themePattern = "~/Views/Blogifier/Blog/{0}/";
        string _theme;

		public AuthorController(IUnitOfWork db, ILogger<AuthorController> logger)
		{
			_db = db;
            _logger = logger;
			_theme = string.Format(_themePattern, ApplicationSettings.BlogTheme);
        }

        [Route("{slug}")]
        public IActionResult Index(string slug)
        {
            var pager = new Pager(1);
            var profile = _db.Profiles.Single(p => p.Slug == slug);
            var posts = _db.BlogPosts.Find(p => p.ProfileId == profile.Id && p.Published > System.DateTime.MinValue, pager);
            return View(_theme + "Author.cshtml", new BlogAuthorModel { Profile = profile, Posts = posts, Pager = pager });
        }

        [Route("{slug}/{page:int}")]
        public IActionResult Index(string slug, int page)
        {
            var pager = new Pager(page);
            var profile = _db.Profiles.Single(p => p.Slug == slug);
            var posts = _db.BlogPosts.Find(p => p.ProfileId == profile.Id && p.Published > System.DateTime.MinValue, pager);
            return View(_theme + "Author.cshtml", new BlogAuthorModel { Profile = profile, Posts = posts, Pager = pager });
        }
    }
}