using System;
using FluentAssertions;
using Moq.AutoMock;
using VkNet.Exception;
using VkNet.Infrastructure;
using Xunit;

namespace VkNet.Tests
{
	public class VkApiVersionManagerTests
	{
		private readonly AutoMocker _mocker = new AutoMocker();

		[Fact]
		public void VersionIsNotEmpty()
		{
			// arrange
			var service = _mocker.CreateInstance<VkApiVersionManager>();

			// act

			// assert

			service.Should().NotBeNull();
			service.Version.Should().NotBeNullOrWhiteSpace();
		}

		[Fact]
		public void VersionIsChanged()
		{
			// arrange
			var service = _mocker.CreateInstance<VkApiVersionManager>();

			// act

			// assert
			service.SetVersion(999, 0);
			service.Version.Should().Be("999.0");
		}

		[Fact]
		public void IsGreaterThanOrEqual_GreaterValue()
		{
			// arrange
			var service = _mocker.CreateInstance<VkApiVersionManager>();

			// act

			// assert
			service.SetVersion(5, 92);
			service.IsGreaterThanOrEqual(5, 93).Should().BeFalse();
		}

		[Fact]
		public void IsGreaterThanOrEqual_EqualValue()
		{
			// arrange
			var service = _mocker.CreateInstance<VkApiVersionManager>();

			// act

			// assert
			service.SetVersion(5, 92);
			service.IsGreaterThanOrEqual(5, 92).Should().BeTrue();
		}

		[Fact]
		public void IsGreaterThanOrEqual_MinorLessValue()
		{
			// arrange
			var service = _mocker.CreateInstance<VkApiVersionManager>();

			// act

			// assert
			service.SetVersion(5, 92);
			service.IsGreaterThanOrEqual(5, 91).Should().BeTrue();
		}

		[Fact]
		public void IsGreaterThanOrEqual_MajorLessValue()
		{
			// arrange
			var service = _mocker.CreateInstance<VkApiVersionManager>();

			// act

			// assert
			service.SetVersion(5, 92);
			service.IsGreaterThanOrEqual(4, 95).Should().BeTrue();
		}

		[Fact]
		public void IsLessThanOrEqual_GreaterValue()
		{
			// arrange
			var service = _mocker.CreateInstance<VkApiVersionManager>();

			// act

			// assert
			service.SetVersion(5, 92);
			service.IsLessThanOrEqual(5, 93).Should().BeTrue();
		}

		[Fact]
		public void IsLessThanOrEqual_EqualValue()
		{
			// arrange
			var service = _mocker.CreateInstance<VkApiVersionManager>();

			// act

			// assert
			service.SetVersion(5, 92);
			service.IsLessThanOrEqual(5, 92).Should().BeTrue();
		}

		[Fact]
		public void IsLessThanOrEqual_MinorLessValue()
		{
			// arrange
			var service = _mocker.CreateInstance<VkApiVersionManager>();

			// act

			// assert
			service.SetVersion(5, 92);
			service.IsLessThanOrEqual(5, 91).Should().BeFalse();
		}

		[Fact]
		public void IsLessThanOrEqual_MajorLessValue()
		{
			// arrange
			var service = _mocker.CreateInstance<VkApiVersionManager>();

			// act

			// assert
			service.SetVersion(5, 92);
			service.IsLessThanOrEqual(4, 95).Should().BeFalse();
		}

		[Fact]
		public void MinimalVersion_5_81_ShouldThrowException()
		{
			// Arrange
			var service = _mocker.CreateInstance<VkApiVersionManager>();

			// Act
			Action action = () => service.SetVersion(5, 50);

			// Assert
			action.Should()
				.Throw<VkApiException>()
				.WithMessage("С 14 октября 2020 года прекратится поддержка версий ниже 5.81.")
				.And.HelpLink.Should()
				.Be("https://vk.com/dev/constant_version_updates");
		}

		[Fact]
		public void MinimalMajorVersion_5_ShouldThrowException()
		{
			// Arrange
			var service = _mocker.CreateInstance<VkApiVersionManager>();

			// Act
			Action action = () => service.SetVersion(4, 50);

			// Assert
			action.Should()
				.Throw<VkApiException>()
				.WithMessage("С 27 мая 2019 года версии API ниже 5.0 больше не поддерживаются.")
				.And.HelpLink.Should()
				.Be("https://vk.com/dev/version_update_2.0");
		}
	}
}