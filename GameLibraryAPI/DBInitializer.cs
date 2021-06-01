using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibraryAPI
{
    public class DBInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Tag.Any())
            {
                // Add all the basic started tags, more tags can be added in the future :D
                AddTag(context, "Action");
                AddTag(context, "Adventure");
                AddTag(context, "Casual");
                AddTag(context, "Free to play");
                AddTag(context, "Strategy");
                AddTag(context, "SinglePlayer");
            }

            if (!context.Game.Any())
            {
                var newGameScores = new GameScores()
                {
                    GeneralScore = 9,
                    Fun = 7,
                    Replayability = 7,
                    Action = 8
                };
                context.GameScores.Add(newGameScores);
                context.SaveChanges();
                Addgame(context,"Warframe","Digital Extremes","Action,Free to play", newGameScores);
                Addgame(context, "Minecraft", "Notch", "Survival,Creative", newGameScores);
                Addgame(context, "Overwatch", "Blizzard", "FPS,Action", newGameScores);
                Addgame(context, "Warcraft", "Blizzard", "Fantasy,MMO,Free to play", newGameScores);
                Addgame(context, "Call of duty", "Unknown", "Action,FPS", newGameScores);


            }
        }
        public static void AddTag(LibraryContext context, string tagName_)
        {
            var newTag = new Tag()
            {
                TagName = tagName_
            };
            context.Tag.Add(newTag);
            context.SaveChanges();
        }

        public static void Addgame(LibraryContext context, string _title,string _dev, string _tags,GameScores _gamescore)
        {
            //Check if the game already exists
            IQueryable<Game> gamequery = context.Game;
            gamequery = gamequery.Where(d => d.Title == _title);
            if (!gamequery.Any())
            {
                //Find dev
                IQueryable<Developer> devsquery = context.Developers;
                devsquery = devsquery.Where(d => d.DevName == _dev);
                Developer linkedDev;
                if (!devsquery.Any())
                {
                    linkedDev = new Developer();
                    linkedDev.DevName = _dev;
                    context.Developers.Add(linkedDev);
                    context.SaveChanges();

                }
                else
                {
                    linkedDev = devsquery.First();
                }


                Game newgame = new Game()
                {
                    Title = _title,
                    GameScores = _gamescore,
                    Developer = linkedDev
                };

                context.Game.Add(newgame);
                context.SaveChanges();

                //Connect relevant tags
                string[] tagsArr = _tags.Split(",");
                foreach (string i in tagsArr)
                {
                    //Find tag
                    IQueryable<Tag> tagsquery = context.Tag;
                    tagsquery = tagsquery.Where(d => d.TagName == i);
                    Tag linkedTag;
                    if (!tagsquery.Any())
                    {
                        linkedTag = new Tag();
                        linkedTag.TagName = i;
                        context.Tag.Add(linkedTag);
                        context.SaveChanges();

                    }
                    else
                    {
                        linkedTag = tagsquery.First();
                    }

                    TagsLink newtagslink = new TagsLink();
                    newtagslink.Game = newgame;
                    newtagslink.Tag = linkedTag;

                    context.TagLink.Add(newtagslink);
                    context.SaveChanges();
                }
            }
            else
            {
            }


        }
    }
}
