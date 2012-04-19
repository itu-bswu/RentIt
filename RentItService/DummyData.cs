/*///<author> Frederik Lysgaard </author>
namespace RentItService
{
    using RentItService.Entities;

    public class DummyData
    {
        public DummyData()
        {
            using (var db = new RentItContext())
            {
                db.Movies.Add(new Movie { Title = "Ocean's Eleven", Description = "Danny Ocean and his eleven accomplices plan to rob three Las Vegas casinos simultaneously.", Genre = "Crime/Thriller" });
                db.Movies.Add(new Movie { Title = "Ocean's Thirteen", Description = "Danny Ocean rounds up the boys for a third heist, after casino owner Willy Bank double-crosses one of the original eleven, Reuben Tishkoff.", Genre = "Crime/Thriller" });
                db.Movies.Add(new Movie { Title = "Batman Begins", Description = "Bruce Wayne loses his philanthropic parents to a senseless crime, and years later becomes the Batman to save the crime-ridden Gotham City on the verge of destruction by an ancient order.", Genre = "Action/Crime/Drama" });
                db.Movies.Add(new Movie { Title = "The Matrix", Description = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.", Genre = "Action/Adventure/Sci-Fi" });
                db.Movies.Add(new Movie { Title = "The Mask", Description = "Bank clerk Stanley Ipkiss is transformed into a manic super-hero when he wears a mysterious mask.", Genre = "Action/Comedy/Crime" });
                db.Movies.Add(new Movie { Title = "Amélie", Description = "Amelie, an innocent and naive girl in Paris, with her own sense of justice, decides to help those around her and along the way, discovers love.", Genre = "Comedy/Fantasy/Romance" });
                db.Movies.Add(new Movie { Title = "American Pie", Description = "Four teenage boys enter a pact to lose their virginity by prom night.", Genre = "Comedy/Romance" });
                db.Movies.Add(new Movie { Title = "Harold & Kumar Go to White Castle", Description = "An Asian-American office worker and his Indian-American stoner friend embark on a quest to satisfy their desire for White Castle burgers.", Genre = "Adventure/Comedy" });
                db.Movies.Add(new Movie { Title = "The Lord of the Rings: The Fellowship of the Ring", Description = "An innocent hobbit of The Shire journeys with eight companions to the fires of Mount Doom to destroy the One Ring and the dark lord Sauron forever.", Genre = "Action/Adventure/Fantasy" });
                db.Movies.Add(new Movie { Title = "The Lord of the Rings: The Two Towers", Description = "While Frodo and Sam edge closer to Mordor with the help of the shifty Gollum, the divided fellowship makes a stand against Sauron's new ally, Saruman, and his hordes of Isengard.", Genre = "Action/Adventure/Fantasy" });
                db.Movies.Add(new Movie { Title = "The Lord of the Rings: The Return of the King", Description = "Aragorn leads the World of Men against Sauron's army to draw the dark lord's gaze from Frodo and Sam who are on the doorstep of Mount Doom with the One Ring.", Genre = "Action/Adventure/Fantasy" });
                db.Movies.Add(new Movie { Title = "Die Hard", Description = "New York cop John McClane gives terrorists a dose of their own medicine as they hold hostages in an LA office building.", Genre = "Action/Thriller" });
                db.Movies.Add(new Movie { Title = "Die Hard 2", Description = "John McClane is forced to battle mercenaries who seize control of an airport's communications and threaten to cause plane crashes if their demands are not met.", Genre = "Action/Crime/Thriller" });
                db.Movies.Add(new Movie { Title = "Star Trek: The Wrath of Khan", Description = "Admiral Kirk's midlife crisis is interrupted by the return of an old enemy looking for revenge and a potentially destructive device.", Genre = "Action/Adventure/Sci-Fi" });
                db.Movies.Add(new Movie { Title = "Star Trek", Description = "A chronicle of the early days of James T. Kirk and his fellow USS Enterprise crew members.", Genre = "Action/Adventure/Sci-Fi" });
                db.Movies.Add(new Movie { Title = "How the Grinch Stole Christmas", Description = "Big budget remake of the classic cartoon about a creature intent on stealing Christmas.", Genre = "Comedy/Family/Fantasy" });

                db.Users.Add(
                    new User { Username = "Derp", Password = "1234", Email = "derp@derpson.com", FullName = "derp derpson" });
                db.Users.Add(
                    new User { Username = "Herp", Password = "4321", Email = "herp@herpson.com", FullName = "herp herpson" });
                db.Users.Add(
                    new User { Username = "Emil89", Password = "1111", Email = "emil@mail.com", FullName = "emil emilsen" });
            }
        }
    }
}*/