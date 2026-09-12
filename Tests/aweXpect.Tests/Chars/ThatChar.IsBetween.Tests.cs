namespace aweXpect.Tests;

public sealed partial class ThatChar
{
	public sealed class IsBetween
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData('a', 'a', 'z')]
			[InlineData('m', 'a', 'z')]
			[InlineData('z', 'a', 'z')]
			[InlineData('Ж', 'Ѐ', 'ӿ')]
			public async Task WhenSubjectIsInsideInclusiveRange_ShouldSucceed(
				char subject,
				char minimum,
				char maximum)
			{
				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData('`', 'a', 'z')]
			[InlineData('{', 'a', 'z')]
			public async Task WhenSubjectIsOutsideRange_ShouldFail(
				char subject,
				char minimum,
				char maximum)
			{
				async Task Act()
					=> await That(subject).IsBetween(minimum).And(maximum);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is between {Formatter.Format(minimum)} and {Formatter.Format(maximum)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}

	public sealed class IsNotBetween
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenSubjectIsOutsideRange_ShouldSucceed()
			{
				char subject = '{';

				async Task Act()
					=> await That(subject).IsNotBetween('a').And('z');

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsInsideRange_ShouldFail()
			{
				char subject = 'm';
				char minimum = 'a';
				char maximum = 'z';

				async Task Act()
					=> await That(subject).IsNotBetween(minimum).And(maximum);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not between {Formatter.Format(minimum)} and {Formatter.Format(maximum)},
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}
	}
}
