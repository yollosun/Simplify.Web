﻿using System;

namespace Simplify.Web.Responses
{
	/// <summary>
	/// Provides tempate response (puts data to DataCollector specified variable)
	/// </summary>
	public class InlineTpl : ControllerResponse
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="InlineTpl"/> class.
		/// </summary>
		/// <param name="dataCollectorVariableName">Name of the data collector variable.</param>
		/// <param name="template">The template.</param>
		public InlineTpl(string dataCollectorVariableName, ITemplate template)
		{
			if (string.IsNullOrEmpty(dataCollectorVariableName))
				throw new ArgumentNullException("dataCollectorVariableName");

			DataCollectorVariableName = dataCollectorVariableName;

			if (template != null)
				Data = template.Get();
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="InlineTpl"/> class.
		/// </summary>
		/// <param name="dataCollectorVariableName">Name of the data collector variable.</param>
		/// <param name="data">The data.</param>
		public InlineTpl(string dataCollectorVariableName, string data)
		{
			if (string.IsNullOrEmpty(dataCollectorVariableName))
				throw new ArgumentNullException("dataCollectorVariableName");

			DataCollectorVariableName = dataCollectorVariableName;
			Data = data;
		}

		/// <summary>
		/// Gets the name of the data collector variable.
		/// </summary>
		/// <value>
		/// The name of the data collector variable.
		/// </value>
		public string DataCollectorVariableName { get; private set; }

		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <value>
		/// The data.
		/// </value>
		public string Data { get; private set; }

		/// <summary>
		/// Processes this response
		/// </summary>
		public override ControllerResponseResult Process()
		{
			DataCollector.Add(DataCollectorVariableName, Data);

			return ControllerResponseResult.Default;
		}
	}
}