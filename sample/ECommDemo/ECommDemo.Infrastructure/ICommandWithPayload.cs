using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCqrs.Commanding;

namespace ECommDemo.Infrastructure
{
	interface ICommandWithPayload<T> : ICommand
	{
		/// <summary>
		/// Gets or sets the payload, something that contain the command data incapsulated in a class.
		/// </summary>
		/// <value>
		/// The payload.
		/// </value>
		T Payload { get; set; }
	}
}
