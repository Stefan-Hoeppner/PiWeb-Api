﻿#region copyright

/* * * * * * * * * * * * * * * * * * * * * * * * * */
/* Carl Zeiss Industrielle Messtechnik GmbH        */
/* Softwaresystem PiWeb                            */
/* (c) Carl Zeiss 2016                             */
/* * * * * * * * * * * * * * * * * * * * * * * * * */

#endregion


namespace Zeiss.PiWeb.Api.Rest.HttpClient.RawData.Filter.Conditions
{
	#region usings

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using JetBrains.Annotations;
	using Zeiss.PiWeb.Api.Rest.Common.Data.FilterString.Tree;

	#endregion

	public class AndFilterCondition : FilterCondition
	{
		#region members

		readonly List<FilterCondition> _ChildConditions;

		#endregion

		#region constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AndFilterCondition"/> class.
		/// </summary>
		/// <exception cref="ArgumentNullException"><paramref name="childConditions"/> is <see langword="null" />.</exception>
		public AndFilterCondition( [NotNull] IEnumerable<FilterCondition> childConditions )
		{
			if( childConditions == null )
				throw new ArgumentNullException( nameof( childConditions ) );

			_ChildConditions = new List<FilterCondition>( childConditions );
		}

		#endregion

		#region methods

		/// <inheritdoc />
		public override IFilterTree BuildFilterTree()
		{
			var subTrees = _ChildConditions.Select( condition => condition.BuildFilterTree() );
			return FilterTree.MakeAnd( subTrees.ToArray() );
		}

		#endregion
	}
}