using System;
using System.Linq;
using System.Collections.Generic;
	
namespace WebApplication1.Models
{   
	public  class 客戶報表Repository : EFRepository<客戶報表>, I客戶報表Repository
	{

	}

	public  interface I客戶報表Repository : IRepository<客戶報表>
	{

	}
}