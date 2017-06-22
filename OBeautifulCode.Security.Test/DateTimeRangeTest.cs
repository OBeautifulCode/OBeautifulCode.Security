// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeRangeTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Security.Test
{
    using System;
    using System.Threading;

    using FluentAssertions;

    using Xunit;

    public static class DateTimeRangeTest
    {
        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameter_startDateTimeInUtc_is_not_DateTimeKind_Utc()
        {
            // Arrange
            var startDateTime1 = new DateTime(1, DateTimeKind.Local);
            var startDateTime2 = new DateTime(1, DateTimeKind.Unspecified);

            // Act
            var ex1 = Record.Exception(() => new DateTimeRange(startDateTime1, DateTime.UtcNow));
            var ex2 = Record.Exception(() => new DateTimeRange(startDateTime2, DateTime.UtcNow));

            // Assert
            // ReSharper disable PossibleNullReferenceException
            ex1.Should().BeOfType<ArgumentException>();
            ex1.Message.Should().Contain("startDateTimeInUtc DateTimeKind is not Utc");

            ex2.Should().BeOfType<ArgumentException>();
            ex2.Message.Should().Contain("startDateTimeInUtc DateTimeKind is not Utc");
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentException___When_parameter_endDateTimeInUtc_is_not_DateTimeKind_Utc()
        {
            // Arrange
            var endDateTime1 = new DateTime(1, DateTimeKind.Local);
            var endDateTime2 = new DateTime(1, DateTimeKind.Unspecified);

            // Act
            var ex1 = Record.Exception(() => new DateTimeRange(DateTime.UtcNow, endDateTime1));
            var ex2 = Record.Exception(() => new DateTimeRange(DateTime.UtcNow, endDateTime2));

            // Assert
            // ReSharper disable PossibleNullReferenceException
            ex1.Should().BeOfType<ArgumentException>();
            ex1.Message.Should().Contain("endDateTimeInUtc DateTimeKind is not Utc");

            ex2.Should().BeOfType<ArgumentException>();
            ex2.Message.Should().Contain("endDateTimeInUtc DateTimeKind is not Utc");
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void Constructor___Should_throw_ArgumentOutOfRangeException___When_parameter_startDateTimeInUtc_is_greater_than_or_equal_to_endDateTimeInUtc()
        {
            // Arrange
            var startDateTimeInUtc1 = DateTime.UtcNow;
            var endDateTimeInUtc1 = startDateTimeInUtc1;

            var endDateTimeInUtc2 = DateTime.UtcNow;
            Thread.Sleep(100);
            var startDateTimeInUtc2 = DateTime.UtcNow;

            // Act
            var ex1 = Record.Exception(() => new DateTimeRange(startDateTimeInUtc1, endDateTimeInUtc1));
            var ex2 = Record.Exception(() => new DateTimeRange(startDateTimeInUtc2, endDateTimeInUtc2));

            // Assert
            // ReSharper disable PossibleNullReferenceException
            ex1.Should().BeOfType<ArgumentOutOfRangeException>();
            ex1.Message.Should().Contain("startDateTimeInUtc is >= endDateTimeInUtc");

            ex2.Should().BeOfType<ArgumentOutOfRangeException>();
            ex2.Message.Should().Contain("startDateTimeInUtc is >= endDateTimeInUtc");
            // ReSharper restore PossibleNullReferenceException
        }

        [Fact]
        public static void StartDateTimeInUtc___Should_return_same_startDateTimeInUtc_passed_to_constructor___When_getting()
        {
            // Arrange
            var expected = DateTime.UtcNow;
            Thread.Sleep(100);
            var endDateTimeInUtc = DateTime.UtcNow;
            var systemUnderTest = new DateTimeRange(expected, endDateTimeInUtc);

            // Act
            var actual = systemUnderTest.StartDateTimeInUtc;

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void EndDateTimeInUtc___Should_return_same_endDateTimeInUtc_passed_to_constructor___When_getting()
        {
            // Arrange
            var startDateTimeInUtc = DateTime.UtcNow;
            Thread.Sleep(100);
            var expected = DateTime.UtcNow;
            var systemUnderTest = new DateTimeRange(startDateTimeInUtc, expected);

            // Act
            var actual = systemUnderTest.EndDateTimeInUtc;

            // Assert
            actual.Should().Be(expected);
        }
    }
}
