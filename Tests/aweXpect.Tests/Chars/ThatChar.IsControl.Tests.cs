namespace aweXpect.Tests;

public sealed partial class ThatChar
{
	public sealed class IsControl
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData('\0')]
			[InlineData('\t')]
			[InlineData('\n')]
			[InlineData('\u007F')]
			public async Task WhenSubjectIsControl_ShouldSucceed(char subject)
			{
				async Task Act() => await That(subject).IsControl();
				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData('a')]
			[InlineData('1')]
			[InlineData(' ')]
			public async Task WhenSubjectIsNotControl_ShouldFail(char subject)
			{
				async Task Act() => await That(subject).IsControl();
				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is a control character,
					              but it was {Formatter.Format(subject)}
					              """);
			}
		}

		public sealed class NegatedTests
		{
			[Fact]
			public async Task WhenSubjectIsControl_ShouldFail()
			{
				char subject = '\t';
				async Task Act() => await That(subject).DoesNotComplyWith(it => it.IsControl());
				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              is not a control character,
					              but it was {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNotControl_ShouldSucceed()
			{
				async Task Act() => await That('a').DoesNotComplyWith(it => it.IsControl());
				await That(Act).DoesNotThrow();
			}
		}
	}
}
