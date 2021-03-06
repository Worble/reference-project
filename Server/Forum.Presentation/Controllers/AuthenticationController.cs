﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Forum.Application.Abstractions.Dates;
using Forum.Application.Forum.Queries.Login;
using Forum.Presentation.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Presentation.Controllers
{
	public class AuthenticationController : ForumControllerBase
	{
		private readonly IDateTimeService _dateTimeService;
		private readonly IMediator _mediator;

		public AuthenticationController(IMediator mediator, IDateTimeService dateTimeService)
		{
			_mediator = mediator;
			_dateTimeService = dateTimeService;
		}

		[HttpPost]
		public async Task<ActionResult<LoginUserViewModel>> Login([FromQuery] bool? isPersistent, LoginQuery query)
		{
			try
			{
				var user = await _mediator.Send(query);
				await LoginUser(isPersistent ?? false, user);
				return user;
			}
			catch (LoginFailedException)
			{
				return BadRequest();
			}
		}

		private Task LoginUser(bool isPersistent, LoginUserViewModel userView)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, userView.EmailAddress),
				new Claim(ClaimTypes.Name, userView.Username),
				new Claim(ClaimTypes.NameIdentifier, userView.Id.ToString()),
			};

			var claimsIdentity = new ClaimsIdentity(
				claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var authProperties = new AuthenticationProperties
			{
				//AllowRefresh = <bool>,
				// Refreshing the authentication session should be allowed.

				//ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
				// The time at which the authentication ticket expires. A
				// value set here overrides the ExpireTimeSpan option of
				// CookieAuthenticationOptions set with AddCookie.

				IsPersistent = isPersistent,
				// Whether the authentication session is persisted across
				// multiple requests. When used with cookies, controls
				// whether the cookie's lifetime is absolute (matching the
				// lifetime of the authentication ticket) or session-based.

				IssuedUtc = _dateTimeService.UtcNow
				// The time at which the authentication ticket was issued.

				//RedirectUri = <string>
				// The full path or absolute URI to be used as an http
				// redirect response value.
			};

			return HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authProperties);
		}
	}
}
