using Raven.Abstractions;
using Raven.Database.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using Raven.Database.Linq.PrivateExtensions;
using Lucene.Net.Documents;
using System.Globalization;
using System.Text.RegularExpressions;
using Raven.Database.Indexing;


public class Index_Auto_2fWebTexts_2fByLangAndView : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_2fWebTexts_2fByLangAndView()
	{
		this.ViewText = @"from doc in docs.WebTexts
select new { View = doc.View, Lang = doc.Lang }";
		this.ForEntityNames.Add("WebTexts");
		this.AddMapDefinition(docs => 
			from doc in docs
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "WebTexts", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				View = doc.View,
				Lang = doc.Lang,
				__document_id = doc.__document_id
			});
		this.AddField("View");
		this.AddField("Lang");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("View");
		this.AddQueryParameterForMap("Lang");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("View");
		this.AddQueryParameterForReduce("Lang");
		this.AddQueryParameterForReduce("__document_id");
	}
}
