using Nancy;
using Nancy.Conventions;
using Nancy.TinyIoc;

namespace MovieDemo
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            //container.Register<IMoviesRepository, MoviesRepository>();
            //container.Register<IMovieTypesRepository, MovieTypesRepository>();        
        }
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Scripts"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("Content"));
        }
    }
}