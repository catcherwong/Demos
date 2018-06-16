namespace OptionsTest
{
    using Xunit;
    using NSubstitute;
    using Microsoft.Extensions.Options;
    
    public class MyClassTest
    {
        private readonly MyClass myClass;

        public MyClassTest()
        {
            var options = new MyOptions {  Name = "catcher"};

            var fake = Substitute.For<IOptions<MyOptions>>();

            fake.Value.Returns(options);

            myClass = new MyClass(fake);
        }

        [Fact]
        public void GreetTest()
        {
            var res = myClass.Greet();

            Assert.Equal("Hello,catcher", res);
        }
    }
}
