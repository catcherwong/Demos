using Xunit;

namespace Catcher.Lib.Test
{
    public class MethodTest
    {
        Method method = new Method();

        [Fact]
        public void add_two_integers_should_success()
        {
            int num1 = 3;
            int num2 = 5;

            int expected = 8;

            int actual = method.Add(num1,num2);

            Assert.Equal(expected,actual);
        }

    }
}
