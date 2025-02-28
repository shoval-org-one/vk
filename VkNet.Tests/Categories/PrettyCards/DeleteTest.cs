﻿using FluentAssertions;
using NUnit.Framework;
using VkNet.Model.RequestParams;
using VkNet.Tests.Infrastructure;

namespace VkNet.Tests.Categories.PrettyCards
{
	[TestFixture]

	public class DeleteTest : CategoryBaseTest
	{
		protected override string Folder => "PrettyCards";

		[Test]
		public void Delete()
		{
			Url = "https://api.vk.com/method/prettyCards.delete";

			ReadCategoryJsonPath(nameof(Api.PrettyCards.Delete));

			var result = Api.PrettyCards.Delete(new PrettyCardsDeleteParams
			{
				OwnerId = -126102803,
				CardId = "1488"
			});

			result.Should().NotBeNull();
			Assert.AreEqual(result.CardId, "1488");
			Assert.AreEqual(result.OwnerId, -126102803);
		}
	}
}