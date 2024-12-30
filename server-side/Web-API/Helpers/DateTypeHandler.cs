﻿using Dapper;
using System.Data;

namespace Web_API.Helpers
{
	public class DateTypeHandler
	{
		public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
		{
			public override void SetValue(IDbDataParameter parameter, DateOnly value)
			{
				parameter.Value = value.ToDateTime(TimeOnly.MinValue);
				parameter.DbType = DbType.Date;
			}

			public override DateOnly Parse(object value)
			{
				return DateOnly.FromDateTime((DateTime)value);
			}
		}
	}
}
