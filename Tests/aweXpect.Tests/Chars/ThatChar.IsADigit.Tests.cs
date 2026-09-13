namespace aweXpect.Tests;

public sealed partial class ThatChar
{
	public sealed class IsADigit
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData('0')]
			[InlineData('5')]
			[InlineData('9')]
			[InlineData('\u0660')]
			public async Task WhenSubjectIsADigit_ShouldSucceed(char subject)
			{
				async Task Act()
					=> await That(subject).IsADigit();

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData('A')]
			[InlineData('½')]
			[InlineData(' ')]
			public async Task WhenSubjectIsNoDigit_ShouldFail(char subject)
			{
				async Task Act()
					=> await That(subject).IsADigit();

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is a digit,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}

		public sealed class NegatedTests
		{
			[Theory]
			[InlineData('0')]
			[InlineData('5')]
			[InlineData('9')]
			[InlineData('\u0660')]
			public async Task WhenSubjectIsADigit_ShouldFail(char subject)
			{
				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.IsADigit());

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not a digit,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[InlineData('A')]
			[InlineData('½')]
			[InlineData(' ')]
			public async Task WhenSubjectIsNoDigit_ShouldSucceed(char subject)
			{
				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.IsADigit());

				await That(Act).DoesNotThrow();
			}
		}
	}
}
