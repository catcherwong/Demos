namespace OptionsTest
{
    using Microsoft.Extensions.Options;
    
    public class MyClass
    {
        private readonly MyOptions _options;

        public MyClass(IOptions<MyOptions> optionsAcc)
        {
            this._options = optionsAcc.Value;
        }

        public string Greet()
        {
            return $"Hello,{_options.Name}";
        }
    }


    public class MyOptions
    {
        public string Name { get; set; }        
    }
}
