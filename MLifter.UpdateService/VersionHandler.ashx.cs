using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;

namespace MLifterUpdateService
{
	/// <summary>
	/// Summary description for $codebehindclassname$
	/// </summary>
	public class VersionHandler : IHttpHandler
	{
		/// <summary>
		/// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"/> interface.
		/// </summary>
		/// <param name="context">An <see cref="T:System.Web.HttpContext"/> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
		/// <remarks>Documented by Dev05, 2011-10-06</remarks>
		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			context.Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
			context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			bool beta, onstick;
			string baseVersion;
			GetParameters(context, out baseVersion, out beta, out onstick);
			if (beta)
			{
				//check for beta and release updates
				Version betaVersion = new Version(ConfigurationManager.AppSettings["BetaVersionFor" + baseVersion].ToString());
				Version releaseVersion = new Version(ConfigurationManager.AppSettings["StableVersionFor" + baseVersion].ToString());
				//check which version is higher
				if (betaVersion > releaseVersion)
				{
					context.Response.Write(ConfigurationManager.AppSettings[(onstick) ? "Stick" : String.Empty + "BetaVersionFor" + baseVersion]);
				}
				else
				{
					context.Response.Write(ConfigurationManager.AppSettings[(onstick) ? "Stick" : String.Empty + "StableVersionFor" + baseVersion]);
				}
			}
			else
				context.Response.Write(ConfigurationManager.AppSettings[(onstick) ? "Stick" : String.Empty + "StableVersionFor" + baseVersion]);
		}

		/// <summary>
		/// Gets the parameters.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="baseVersion">The base version.</param>
		/// <param name="beta">if set to <c>true</c> [beta].</param>
		/// <param name="onstick">if set to <c>true</c> [onstick].</param>
		/// <remarks>Documented by Dev05, 2011-10-06</remarks>
		private void GetParameters(HttpContext context, out string baseVersion, out bool beta, out bool onstick)
		{
			baseVersion = string.Empty;
			beta = onstick = false;

			try
			{
				baseVersion = Convert.ToString(context.Request.Params["base"]);
				if ((baseVersion == null || baseVersion.Length < 2) && context.Request.Browser.Win32)
					baseVersion = "2.30";
			}
			catch (FormatException) { }
			try
			{
				beta = Convert.ToBoolean(context.Request.Params["beta"]);
			}
			catch (FormatException) { }
			try
			{
				onstick = Convert.ToBoolean(context.Request.Params["onstick"]);
			}
			catch (FormatException) { }
		}

		/// <summary>
		/// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"/> instance.
		/// </summary>
		/// <returns>true if the <see cref="T:System.Web.IHttpHandler"/> instance is reusable; otherwise, false.
		///   </returns>
		/// <remarks>Documented by Dev05, 2011-10-06</remarks>
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}
