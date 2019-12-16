﻿using System.Threading.Tasks;

namespace Simplify.Web.Responses
{
	/// <summary>
	/// Provides controller response with exact status code and optional string data (send only specified string to response or empty body)
	/// </summary>
	public class StatusCode : ControllerResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StatusCode" /> class.
		/// </summary>
		/// <param name="statusCode">The HTTP response status code.</param>
		/// <param name="responseData">The response data.</param>
		/// <param name="contentType">Type of the content.</param>
		public StatusCode(int statusCode, string? responseData = null, string? contentType = null)
		{
			Code = statusCode;
			ResponseData = responseData;
			ContentType = contentType;
		}

		/// <summary>
		/// Gets the response data.
		/// </summary>
		/// <value>
		/// The response data.
		/// </value>
		public string? ResponseData { get; }

		/// <summary>
		/// Gets the type of the content.
		/// </summary>
		/// <value>
		/// The type of the content.
		/// </value>
		public string? ContentType { get; }

		/// <summary>
		/// Gets the HTTP response status code.
		/// </summary>
		/// <value>
		/// The HTTP response status code.
		/// </value>
		public int Code { get; }

		/// <summary>
		/// Processes this response
		/// </summary>
		public override async Task<ControllerResponseResult> ProcessAsync()
		{
			Context.Response.StatusCode = Code;

			if (ContentType != null)
				Context.Response.ContentType = ContentType;

			if (ResponseData != null)
				await ResponseWriter.WriteAsync(ResponseData, Context.Response);

			return ControllerResponseResult.RawOutput;
		}
	}
}